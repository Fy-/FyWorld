/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using System;
using UnityEngine;
using Fy.Entity;
using Fy.Definitions;
using Fy.Helpers;
using Fy.Visuals;

namespace Fy.World {
	// A bucket (region in a grid)
	public class LayerGridBucket {
		/* Rect is rectangle of the physical space of our bucket */
		public RectI rect { get; protected set; }

		/* List of tilables for Layer T */
		public Tilable[] tilables { get; protected set; }

		/* Bucket ID */
		public int uid { get; protected set; }

		/* Dictionary of tilables in the grid by Type */
		public Dictionary<TilableType, HashSet<Tilable>> titlablesByType { get; protected set; }

		/* Dictionary of tilables matrices indexed by GraphicInstance.uid */
		public Dictionary<int, List<Matrix4x4>> tilablesMatrices { get; protected set; }
		public Dictionary<int, Matrix4x4[]> tilablesMatricesArr { get; protected set; }

		public Layer layer { get; protected set; }

		public bool rebuildMatrices;

		private BucketRenderer _staticRenderer;
		

		public LayerGridBucket(int uid, RectI rect, Layer layer, Type renderer) {
			this.uid = uid;
			this.rect = rect;
			this.layer = layer;
			this.tilables = new Tilable[this.rect.width * this.rect.height];
			this.titlablesByType = new Dictionary<TilableType, HashSet<Tilable>>();
			this.tilablesMatrices = new Dictionary<int, List<Matrix4x4>>();
			this.tilablesMatricesArr = new Dictionary<int, Matrix4x4[]>();
			if (renderer != null) {
				this._staticRenderer = (BucketRenderer)Activator.CreateInstance(renderer, this, this.layer);
			}
		}

		public bool IsVisible() {
			return (
				this.rect.min.x >= Loki.cameraController.viewRect.min.x - Settings.BUCKET_SIZE &&
				this.rect.max.x <= Loki.cameraController.viewRect.max.x + Settings.BUCKET_SIZE &&
				this.rect.min.y >= Loki.cameraController.viewRect.min.y - Settings.BUCKET_SIZE &&
				this.rect.max.y <= Loki.cameraController.viewRect.max.y + Settings.BUCKET_SIZE 
			);
		}

		public void DrawStatics() {
			this._staticRenderer.Draw();
		}

		public void CheckMatriceUpdates() {
			if (this.rebuildMatrices && this.IsVisible()) {
				this.UpdateMatrices();
				this.rebuildMatrices = false;
			}
		}

		public void DrawInstanced() {
			foreach (KeyValuePair<int, Matrix4x4[]> kv in this.tilablesMatricesArr) {
				Graphics.DrawMeshInstanced(
					GraphicInstance.instances[kv.Key].mesh,
					0,
					GraphicInstance.instances[kv.Key].material,
					kv.Value
				);
			}
		}

		public void BuildStaticMeshes() {
			this._staticRenderer.BuildMeshes();
		}

		public Vector2Int GetLocalPosition(Vector2Int globalPosition) {
			return new Vector2Int(globalPosition.x-this.rect.min.x, globalPosition.y-this.rect.min.y);
		}

		public Tilable GetTilableAt(Vector2Int position) {
			Vector2Int localPosition = this.GetLocalPosition(position);
			if (
				localPosition.x >= 0 && localPosition.y >= 0 && 
				localPosition.x < this.rect.width && localPosition.y < this.rect.height
			) {
				return this.tilables[localPosition.x + localPosition.y * this.rect.width];
			}

			return null;
		}

		public void UpdateMatrices() {
			this.tilablesMatrices = new Dictionary<int, List<Matrix4x4>>();
			foreach (Tilable tilable in this.tilables) {
				if (tilable != null && tilable.def.graphics.isInstanced) {
					this.AddMatrice(tilable.mainGraphic.uid, tilable.GetMatrice(tilable.mainGraphic.uid));
					if (tilable.addGraphics != null) {
						foreach (GraphicInstance graphicInstance in tilable.addGraphics.Values) {
							this.AddMatrice(graphicInstance.uid, tilable.GetMatrice(graphicInstance.uid));
						}
					}
				}
			}

			this.tilablesMatricesArr = new Dictionary<int, Matrix4x4[]>();
			foreach (KeyValuePair<int, List<Matrix4x4>> kv in this.tilablesMatrices) {
				this.tilablesMatricesArr.Add(kv.Key, kv.Value.ToArray());
			}
		}

		public void DelTilable(Tilable tilable) {
			Vector2Int localPosition = this.GetLocalPosition(tilable.position);
			this.tilables[localPosition.x + localPosition.y * this.rect.width] = null;

			if (tilable.def.type != TilableType.Undefined) {
				this.titlablesByType[tilable.def.type].Remove(tilable);
				if (this.titlablesByType[tilable.def.type].Count == 0) {
					this.titlablesByType.Remove(tilable.def.type);
				}
			}

			this.rebuildMatrices = true;
		}

		public void AddTilable(Tilable tilable) {
			// Maybe we set the local position for the tilable
			Vector2Int localPosition = this.GetLocalPosition(tilable.position);
			this.tilables[localPosition.x + localPosition.y * this.rect.width] = tilable;
			tilable.SetBucket(this);
			
			// Add to tilableByType dictionary
			if (tilable.def.type != TilableType.Undefined) {
				if (!this.titlablesByType.ContainsKey(tilable.def.type)) {
					this.titlablesByType.Add(tilable.def.type, new HashSet<Tilable>());
				}
				this.titlablesByType[tilable.def.type].Add(tilable);
			}

			// Add matrice to list if isInstanced.
			if (tilable.def.graphics.isInstanced) {
				this.AddMatrice(tilable.mainGraphic.uid, tilable.GetMatrice(tilable.mainGraphic.uid));
				if (tilable.addGraphics != null) {
					foreach (GraphicInstance graphicInstance in tilable.addGraphics.Values) {
						this.AddMatrice(graphicInstance.uid, tilable.GetMatrice(graphicInstance.uid));
					}
				}
			}

			this.rebuildMatrices = true;
		}


		public void AddMatrice(int graphicID, Matrix4x4 matrice) {
			if (!this.tilablesMatrices.ContainsKey(graphicID)) {
				this.tilablesMatrices.Add(graphicID, new List<Matrix4x4>());
			}
			this.tilablesMatrices[graphicID].Add(matrice);
		}
	}
}
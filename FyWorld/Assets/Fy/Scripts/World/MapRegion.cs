/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using UnityEngine;
using Fy.Helpers;
using Fy.Visuals;
using Fy.Definitions;
using Fy.Entity;

namespace Fy.World {
	// Represents a region on the map.
	public class MapRegion {
		/* Rectangle defining our region. */
		public RectI regionRect { get; protected set; }

		/* Reference to our map? */
		public Map map { get; protected set; }

		/* Identifier */
		public int id { get; protected set; }

		/* For some of our layers we need a region renderer */
		public Dictionary<Layer, RegionRenderer> renderers { get; protected set; }

		/* Matrices dictionary for each tilable in this region */
		private Dictionary<int, Matrix4x4[]> _matrices;
		private bool _needToRefreshMatrices = true;

		public MapRegion(int id, RectI rect, Map map) {
			this.regionRect = rect;
			this.id = id;
			this.map = map;
			this.renderers = new Dictionary<Layer, RegionRenderer>();
			this.AddRenderers();
		}

		/// Rebuild matrices
		public void BuildMatrices() {
			Dictionary<int, List<Matrix4x4>> tmpMatrices = new Dictionary<int, List<Matrix4x4>>();
			foreach (Vector2Int position in this.regionRect) {
				foreach (Tilable tilable in this.map[position].GetAllTilables()) {
					if (tilable.def.graphics.isInstanced) {
						if (!tmpMatrices.ContainsKey(tilable.mainGraphic.uid)) {
							tmpMatrices.Add(tilable.mainGraphic.uid, new List<Matrix4x4>());
						}
						tmpMatrices[tilable.mainGraphic.uid].Add(tilable.GetMatrice(tilable.mainGraphic.uid));
							
						if (tilable.addGraphics != null) {
							
							foreach (GraphicInstance graphicInstance in tilable.addGraphics.Values) {

								if (!tmpMatrices.ContainsKey(graphicInstance.uid)) {
									tmpMatrices.Add(graphicInstance.uid, new List<Matrix4x4>());
								}
								tmpMatrices[graphicInstance.uid].Add(tilable.GetMatrice(graphicInstance.uid));
							}
						}
					}
				}
			}
			this._matrices = new Dictionary<int, Matrix4x4[]>();
			foreach (int id in tmpMatrices.Keys) {
				this._matrices.Add(id, new Matrix4x4[tmpMatrices[id].Count]);
				tmpMatrices[id].CopyTo(this._matrices[id]);
			}
		}

		public bool IsVisible() {
			return (
				this.regionRect.min.x >= Loki.cameraController.viewRect.min.x - Map.REGION_SIZE &&
				this.regionRect.max.x <= Loki.cameraController.viewRect.max.x + Map.REGION_SIZE &&
				this.regionRect.min.y >= Loki.cameraController.viewRect.min.y - Map.REGION_SIZE &&
				this.regionRect.max.y <= Loki.cameraController.viewRect.max.y + Map.REGION_SIZE 
			);
		}

		/// Draw all the meshes from our renderers.
		public void Draw() {
			foreach (RegionRenderer renderer in this.renderers.Values) {
				renderer.Draw();
			}

			this.DrawTilables();
		}

		/// Draw Tilables
		public void DrawTilables() {
			if (this._needToRefreshMatrices) {
				this.BuildMatrices();
				this._needToRefreshMatrices = false;
			}

			foreach (KeyValuePair<int, Matrix4x4[]> kv in this._matrices) {
				Graphics.DrawMeshInstanced(
					MeshPool.GetPlaneMesh(GraphicInstance.instances[kv.Key].def.size), 
					0, 
					GraphicInstance.instances[kv.Key].material, 
					kv.Value 
				);
			}
		}

		/// Build all the meshes in all our renderers
		public void BuildMeshes() {
			foreach (RegionRenderer renderer in this.renderers.Values) {
				renderer.BuildMeshes();
			}
		}

		/// Add renderes for some layers.
		private void AddRenderers() {
			this.renderers.Add(Layer.Ground, new RegionGroundRenderer(this, Layer.Ground));
		}
	}
}
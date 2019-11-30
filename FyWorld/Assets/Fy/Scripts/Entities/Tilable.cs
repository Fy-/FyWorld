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
using Fy.Definitions;
using Fy.Visuals;
using Fy.World;
using Fy.Helpers;

namespace Fy.Entities {
	/// A thing in our game
	public abstract class Entity {
		/*  Position */
		public Vector2Int position { get; protected set; }
	}

	// A tilable is just an entity contain in a tile/layer.
	public class Tilable : Entity {
		/* Scale */
		public Vector3 scale = Vector3.one;

		/*  Definition */
		public TilableDef def { get; protected set; }

		/*  Graphic Instance */
		public GraphicInstance mainGraphic { get; set; }

		/* Additional graphics */
		public Dictionary<string, GraphicInstance> addGraphics { get; set; }

		/* Tilable tick counts */
		protected int ticks = 0;

		/* Matrix */
		private Dictionary<int, Matrix4x4> _matrices;

		/* Do we need to reset matrices */
		public bool resetMatrices = false;

		/* If this is True the tilable is not drawn */
		public bool hidden = false;

		/* Parent bucket */
		public LayerGridBucket bucket { get; protected set; }

		/// Register the bucket of the tilable
		public void SetBucket(LayerGridBucket bucket) {
			this.bucket = bucket;
		}

		// Current Order on the tilable
		public MenuOrderDef currentOrder { get; protected set; }
		public bool hasOrder { get { return !(this.currentOrder == null); } }

		/// Destroy this tilable
		public virtual void Destroy() {
			if (this.bucket != null) {
				this.bucket.DelTilable(this);
			}
		}

		/// Generic method to update graphics
		public virtual void UpdateGraphics() {}

		public virtual void AddOrder(MenuOrderDef def) {
			this.currentOrder = def;
			if (this.addGraphics == null) {
				this.addGraphics = new Dictionary<string, GraphicInstance>();
			}
			this.UpdateOrderGraphics();
		}

		public virtual void ClearOrder() {
			this.addGraphics.Remove(this.currentOrder.name);
			this.currentOrder = null;
		}

		public virtual void UpdateOrderGraphics() {
			if (!this.addGraphics.ContainsKey(this.currentOrder.name)) {
				this.addGraphics.Add(this.currentOrder.name, 
					GraphicInstance.GetNew(
						this.currentOrder.graphics,
						Color.white,
						Res.textures[this.currentOrder.graphics.textureName],
						42
					)
				);
			}
		}

		/// Get the matrice of our tilable
		public Matrix4x4 GetMatrice(int graphicUID) {
			if (this._matrices == null || this.resetMatrices) {
				this._matrices = new Dictionary<int, Matrix4x4>();
				this.resetMatrices = true;
			}
			if (!this._matrices.ContainsKey(graphicUID)) {
				Matrix4x4 mat = Matrix4x4.identity;
				mat.SetTRS(
					new Vector3(
						this.position.x
						-this.def.graphics.pivot.x*this.scale.x
						+(1f-this.scale.x)/2f
						,this.position.y
						-this.def.graphics.pivot.y*this.scale.y
						+(1f-this.scale.y)/2f
						,LayerUtils.Height(this.def.layer) + GraphicInstance.instances[graphicUID].priority
					), 
					Quaternion.identity, 
					this.scale
				);
				this._matrices.Add(graphicUID, mat);
			}
			return this._matrices[graphicUID];
		}
	}
}
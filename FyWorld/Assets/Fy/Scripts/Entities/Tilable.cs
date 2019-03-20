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

namespace Fy.Entity {
	// A tilable is just an entity contain in a tile/layer.
	public class Tilable {
		/*  Position */
		public Vector2Int position { get; protected set; }

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

		public bool hidden = false;

		public LayerGridBucket bucket { get; protected set; }


		public void SetBucket(LayerGridBucket bucket) {
			this.bucket = bucket;
		}

		public virtual void Destroy() {
			if (this.bucket != null) {
				this.bucket.DelTilable(this);
			}
		}

		public virtual void UpdateGraphics() {}

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
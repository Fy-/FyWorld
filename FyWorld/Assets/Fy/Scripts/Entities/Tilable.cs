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

namespace Fy.Entity {
	// A tilable is just an entity contain in a tile/layer.
	public class Tilable {
		/*  Position */
		public Vector2Int position { get; protected set; }

		/*  Definition */
		public TilableDef def { get; protected set; }

		/*  Graphic Instance */
		public GraphicInstance mainGraphic { get; protected set; }

		/* Additional graphics */
		public Dictionary<string, GraphicInstance> addGraphics { get; protected set; }

		/*  Matrix */
		private Dictionary<int, Matrix4x4> _matrices;

		/// Get the matrice of our tilable
		public Matrix4x4 GetMatrice(int graphicUID) {
			if (this._matrices == null) {
				this._matrices = new Dictionary<int, Matrix4x4>();
			}
			if (!this._matrices.ContainsKey(graphicUID)) {
				Matrix4x4 mat = Matrix4x4.identity;
				mat.SetTRS(
					new Vector3(
						this.position.x-this.def.graphics.pivot.x,
						this.position.y-this.def.graphics.pivot.y,
						LayerUtils.Height(this.def.layer) + GraphicInstance.instances[graphicUID].priority
					), 
					Quaternion.identity, 
					Vector3.one/2f
				);
				this._matrices.Add(graphicUID, mat);
			}
			return this._matrices[graphicUID];
		}
	}
}
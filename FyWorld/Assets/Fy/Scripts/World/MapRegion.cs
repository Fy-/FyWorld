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
			foreach (Vector2Int v  in this.regionRect) {
				foreach (Tilable t in this.map[v].GetAllTilables()) {
					if (t.def.graphics.isInstanced) {
						if (!tmpMatrices.ContainsKey(t.graphics.uid)) {
							tmpMatrices.Add(t.graphics.uid, new List<Matrix4x4>());
						}
						tmpMatrices[t.graphics.uid].Add(t.GetMatrice());
						// TODO: if we add childs to our tilable, don't forget them!
					}
				}
			}
			this._matrices = new Dictionary<int, Matrix4x4[]>();
			foreach (int id in tmpMatrices.Keys) {
				this._matrices.Add(id, new Matrix4x4[tmpMatrices[id].Count]);
				tmpMatrices[id].CopyTo(this._matrices[id]);
			}
		}

		/// Draw all the meshes from our renderers.
		public void Draw() {
			foreach (RegionRenderer renderer in this.renderers.Values) {
				renderer.Draw();
			}

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
			this.renderers.Add(Layer.Ground, new RegionRenderer(this, Layer.Ground));
		}
	}
}
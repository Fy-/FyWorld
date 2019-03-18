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
using Fy.World;
using Fy.Definitions;
using Fy.Entity;

namespace Fy.Visuals {

	// Renderer a region
	public class RegionRenderer {
		/* Region */
		public MapRegion region { get; protected set; }

		/* Layer */
		public Layer layer { get; protected set; }

		/* One mesh per GraphicInstance on this layer */
		public Dictionary<int, MeshData> meshes { get; protected set; }

		/* Do we need to redraw this region? */
		private bool _redraw = true;

		/* Region position */
		private Vector3 _position;

		public RegionRenderer(MapRegion region, Layer layer) {
			this.region = region;
			this.layer = layer;
			this.meshes = new Dictionary<int, MeshData>();
			this._position = new Vector3(0, 0, 0);
		}

		// Get the current mesh for the graphic instance (or create a new one)
		public MeshData GetMesh(int graphicInstance, bool useSize = true, MeshFlags flags = MeshFlags.Base) {
			if (this.meshes.ContainsKey(graphicInstance)) {
				return this.meshes[graphicInstance];
			}

			if (useSize)
				this.meshes.Add(graphicInstance, new MeshData(this.region.regionRect.area, flags));
			else
				this.meshes.Add(graphicInstance, new MeshData(flags));

			return this.meshes[graphicInstance];
		}

		// Draw the mesh on the screen
		public void Draw() {
			if (this._redraw) {
				// Clear the current meshes.
				this.BuildMeshes();
				this._redraw = false;
			}

			foreach (KeyValuePair<int, MeshData> kv in this.meshes) {
				Graphics.DrawMesh(
					kv.Value.mesh,
					this._position,
					Quaternion.identity,
					GraphicInstance.instances[kv.Key].material,
					0
				);
			}
		}

		/// Clear all our meshes
		public void ClearMeshes() {
			foreach (MeshData meshData in this.meshes.Values) {
				meshData.Clear();
			}
		}

		/// Build all meshes for this region
		public virtual void BuildMeshes() {
			foreach (Vector2Int v in this.region.regionRect) {
				Tilable tilable = this.region.map[v].GetTilable(this.layer);
				if (tilable != null) {
					MeshData currentMesh = this.GetMesh(tilable.mainGraphic.uid);
					int vIndex = currentMesh.vertices.Count;

					currentMesh.vertices.Add(new Vector3(v.x, v.y));
					currentMesh.vertices.Add(new Vector3(v.x, v.y+1));
					currentMesh.vertices.Add(new Vector3(v.x+1, v.y+1));
					currentMesh.vertices.Add(new Vector3(v.x+1, v.y));

					currentMesh.AddTriangle(vIndex, 0, 1, 2);
					currentMesh.AddTriangle(vIndex, 0, 2, 3);
				}
			}

			foreach (MeshData meshData in this.meshes.Values) {
				meshData.Build();
			}
		}
	}
}
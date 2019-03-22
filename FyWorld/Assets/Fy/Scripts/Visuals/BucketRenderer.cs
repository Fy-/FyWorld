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
	// Renderer a bucket (only not instanced graphics)
	public class BucketRenderer {
		/* Bucket */
		public LayerGridBucket bucket { get; protected set; }

		/* Layer */
		public Layer layer { get; protected set; }

		/* One mesh per GraphicInstance on this layer */
		public Dictionary<int, MeshData> meshes { get; protected set; }

		/* Do we need to redraw this region? */
		private bool _redraw = true;

		/* Region position */
		private Vector3 _position;

		public BucketRenderer(LayerGridBucket bucket, Layer layer) {
			this.bucket = bucket;
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
				this.meshes.Add(graphicInstance, new MeshData(this.bucket.rect.area, flags));
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
			foreach (Tilable tilable in this.bucket.tilables) {
				if (tilable != null && !tilable.hidden) {
					MeshData currentMesh = this.GetMesh(tilable.mainGraphic.uid);
					int vIndex = currentMesh.vertices.Count;

					currentMesh.vertices.Add(new Vector3(tilable.position.x, tilable.position.y));
					currentMesh.vertices.Add(new Vector3(tilable.position.x, tilable.position.y+1));
					currentMesh.vertices.Add(new Vector3(tilable.position.x+1, tilable.position.y+1));
					currentMesh.vertices.Add(new Vector3(tilable.position.x+1, tilable.position.y));

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
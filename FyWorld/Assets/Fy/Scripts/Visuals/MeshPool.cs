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

namespace Fy.Visuals {
	public static class MeshPool {
		/* Dictionary of planes where the identifier is representative of the size of the plane. */
		public static Dictionary<float, MeshData> planes = new Dictionary<float, MeshData>();
		public static Dictionary<int, MeshData> cornerPlanes = new Dictionary<int, MeshData>();
		public static Dictionary<float, MeshData> humanPlanes = new Dictionary<float, MeshData>();

		/// Get the mesh for corners for a connected tilable
		public static Mesh GetCornersPlane(bool[] corners) {
			int id = HashUtils.HashBoolArray(corners);
			if (MeshPool.cornerPlanes.ContainsKey(id))  {
				return MeshPool.cornerPlanes[id].mesh;
			}
			MeshPool.cornerPlanes.Add(id, MeshPool.GenCornersPlane(corners));
			return MeshPool.cornerPlanes[id].mesh;
		}

		/// Get a plane mesh of the size "size"
		public static Mesh GetPlaneMesh(Vector2 size) {
			float id = (size.x + size.y*666f);
			if (MeshPool.planes.ContainsKey(id)) {
				return MeshPool.planes[id].mesh;
			}
			MeshPool.planes.Add(id, MeshPool.GenPlaneMesh(size));
			return MeshPool.planes[id].mesh;
		}

		/// Get a plane mesh of the size "size"
		public static Mesh GetHumanPlaneMesh(Vector2 size, Direction direction) {
			float id = (size.x + size.y*666f)+(int)direction*333f;
			if (MeshPool.humanPlanes.ContainsKey(id)) {
				return MeshPool.humanPlanes[id].mesh;
			}
			MeshPool.humanPlanes.Add(id, MeshPool.GenHumanMesh(size, direction));
			return MeshPool.humanPlanes[id].mesh;
		}

		/// Generate the mesh for corners for a connected tilable
		public static MeshData GenCornersPlane(bool[] corners) {
			MeshData meshData = new MeshData(4, (MeshFlags.Base | MeshFlags.UV));
			for (int i = 0; i < 4; i++) {
				if (corners[i] == true) {
					Vector2 loc = Vector2.zero;
					float sx = .42f;
					float sy = .55f;

					if (i == 1) {
						sy = .42f;
						loc.y = (1-sy);
					} else if (i == 2) {
						sy = .42f;
						loc.x = (1-sx);
						loc.y = (1-sy);
					} else if (i == 3) {
						loc.x = (1-sx);
					}

					int vIndex = meshData.vertices.Count;
					meshData.vertices.Add(new Vector3(loc.x, loc.y));
					meshData.vertices.Add(new Vector3(loc.x, loc.y+sy));
					meshData.vertices.Add(new Vector3(loc.x+sx, loc.y+sy));
					meshData.vertices.Add(new Vector3(loc.x+sx, loc.y));
					meshData.UVs.Add(new Vector2(0f, 0f));
					meshData.UVs.Add(new Vector2(0f, 1f));
					meshData.UVs.Add(new Vector2(1f, 1f));
					meshData.UVs.Add(new Vector2(1f, 0f));
					meshData.AddTriangle(vIndex, 0, 1, 2);
					meshData.AddTriangle(vIndex, 0, 2, 3);
				}
			}
			meshData.Build();
			return meshData;
		}

		/// Generate a plane with uv corresponding to the direction of the character.
		public static MeshData GenHumanMesh(Vector2 size, Direction direction) {
			float uy = 1f/3f;

			MeshData meshData = new MeshData(1, (MeshFlags.Base | MeshFlags.UV));
			meshData.vertices.Add(new Vector3(0, 0));
			meshData.vertices.Add(new Vector3(0, size.y));
			meshData.vertices.Add(new Vector3(size.x, size.y));
			meshData.vertices.Add(new Vector3(size.x, 0));

			if (direction == Direction.N) {
				meshData.UVs.Add(new Vector2(0f, uy));
				meshData.UVs.Add(new Vector2(0f, uy*2f));
				meshData.UVs.Add(new Vector2(1f, uy*2f));
				meshData.UVs.Add(new Vector2(1f, uy));
			} else if (direction == Direction.S) {
				meshData.UVs.Add(new Vector2(0f, uy*2f));
				meshData.UVs.Add(new Vector2(0f, 1f));
				meshData.UVs.Add(new Vector2(1f, 1f));
				meshData.UVs.Add(new Vector2(1f, uy*2f));
			} else if (direction == Direction.E) {
				meshData.UVs.Add(new Vector2(0f, 0f));
				meshData.UVs.Add(new Vector2(0f, uy));
				meshData.UVs.Add(new Vector2(1f, uy));
				meshData.UVs.Add(new Vector2(1f, 0f));
			} else if (direction == Direction.W) {
				meshData.UVs.Add(new Vector2(1f, 0f));
				meshData.UVs.Add(new Vector2(1f, uy));
				meshData.UVs.Add(new Vector2(0f, uy));
				meshData.UVs.Add(new Vector2(0f, 0f));
			}

			meshData.AddTriangle(0, 0, 1, 2);
			meshData.AddTriangle(0, 0, 2, 3);
			meshData.Build();
			return meshData;
		}

		/// Generate a plane mesh of the size "size"
		public static MeshData GenPlaneMesh(Vector2 size) {
			MeshData meshData = new MeshData(1, (MeshFlags.Base | MeshFlags.UV));
			meshData.vertices.Add(new Vector3(0, 0));
			meshData.vertices.Add(new Vector3(0, size.y));
			meshData.vertices.Add(new Vector3(size.x, size.y));
			meshData.vertices.Add(new Vector3(size.x, 0));
			meshData.UVs.Add(new Vector2(0f, 0f));
			meshData.UVs.Add(new Vector2(0f, 1f));
			meshData.UVs.Add(new Vector2(1f, 1f));
			meshData.UVs.Add(new Vector2(1f, 0f));
			meshData.AddTriangle(0, 0, 1, 2);
			meshData.AddTriangle(0, 0, 2, 3);
			meshData.Build();
			return meshData;
		}
	}
}
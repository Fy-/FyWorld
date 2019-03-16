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

namespace Fy.Visuals {
	public static class MeshPool {
		/* Dictionary of planes where the identifier is representative of the size of the plane. */
		public static Dictionary<float, MeshData> planes = new Dictionary<float, MeshData>();

		/// Get a plane mesh of the size "size"
		public static Mesh GetPlaneMesh(Vector2 size) {
			float id = (size.x + size.y*666f);
			if (MeshPool.planes.ContainsKey(id)) {
				return MeshPool.planes[id].mesh;
			}
			MeshPool.planes.Add(id, MeshPool.GenPlaneMesh(size));
			return MeshPool.planes[id].mesh;
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
/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using Fy.World;
using Fy.Definitions;

namespace Fy.Helpers {
	public static class DebugRenderer {
		public static void DrawBuckets() {
			Gizmos.color = new Color(0, 0, 1, .5f);
			foreach (LayerGridBucket bucket in Loki.map.grids[Layer.Ground].buckets) {
				Gizmos.DrawCube(
					new Vector3(
						bucket.rect.max.x-(bucket.rect.width/2f),
						bucket.rect.max.y-(bucket.rect.height/2f)
					), 
					new Vector3(bucket.rect.width-.5f, bucket.rect.height-.5f, 1f)
				);
			}
		}

		public static void DrawTiles() {
			Gizmos.color = Color.white;
			foreach (Vector2Int v in Loki.cameraController.viewRect) {
				Gizmos.DrawWireCube(
					new Vector3(v.x+.5f, v.y+.5f), 
					Vector3.one
				);
			}
		}

		public static void DrawNoiseMap() {
			foreach (Vector2Int v in Loki.cameraController.viewRect) {
				try {
					float h = Loki.map.groundNoiseMap[v.x + v.y * Loki.map.size.x];
					Gizmos.color = new Color(h, 0, 0, .9f);
					Gizmos.DrawCube(
						new Vector3(v.x+.5f, v.y+.5f), 
						Vector3.one
					);
				} catch {}
			}
		}
	}
}
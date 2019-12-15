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
using Fy.Characters;
using Fy.World;
using Fy.Entities;

namespace Fy.Helpers {
	public static class DebugRenderer {
		public static void DrawCurrentPath(CharacterMovement movement) {
			if (movement.destination != movement.position) {
				Gizmos.color = Color.red;
				Vector2Int[] path = movement.path.ToArray();
				for (int i = 0; i < path.Length; i++) {
					if (i == 0) {
						Gizmos.DrawLine(
							new Vector3(movement.visualPosition.x + .5f, movement.visualPosition.y + .5f),
							new Vector3(path[i].x + .5f, path[i].y + .5f)
						);
					} else {
						Gizmos.DrawLine(
							new Vector3(path[i-1].x + .5f, path[i-1].y + .5f),
							new Vector3(path[i].x + .5f, path[i].y + .5f)
						);
					}
				}
			}
		}

		public static void DrawFertility() {
			Color color1 = new Color(1, 0, 0, .6f);
			Color color2 = new Color(0, 1, 0, .6f);
			foreach (Vector2Int v in Loki.cameraController.viewRect) {
				if (Loki.map[v] != null) {
					Gizmos.color = Color.Lerp(color1, color2, Loki.map[v].fertility);
					Gizmos.DrawCube(
						new Vector3(v.x+.5f, v.y+.5f), 
						Vector3.one
					);
				}
			}
		}

		public static void DrawAStar() {
			Color color1 = new Color(1, 0, 0, .4f);
			Color color2 = new Color(0, 1, 0, .4f);
			foreach (Vector2Int v in Loki.cameraController.viewRect) {
				if (Loki.map[v] != null) {
					Gizmos.color = Color.Lerp(color1, color2, Loki.map[v].pathCost);
					Gizmos.DrawCube(
						new Vector3(v.x+.5f, v.y+.5f), 
						Vector3.one
					);
				}
			}
		}

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

		public static void DrawReserved() {
			Gizmos.color = Color.red;
			foreach (Vector2Int v in Loki.cameraController.viewRect) {
				if (Loki.map[v] != null && Loki.map[v].reserved) {
					Gizmos.DrawWireCube(
						new Vector3(v.x+.5f, v.y+.5f), 
						Vector3.one
					);
				}
			}
		}
		public static void DrawRecipes() {
			foreach (Vector2Int v in Loki.cameraController.viewRect) {
				foreach (Recipe r in WorldUtils.recipes) {
					if (r.finished) {
						Gizmos.color = Color.green;
					} else {
						Gizmos.color = Color.blue;
					}
					Gizmos.DrawWireCube(
						new Vector3(r.position.x+.5f, r.position.y+.5f), 
						Vector3.one
					);
				}
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
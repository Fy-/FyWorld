/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Fy.Entities;
using Fy.Helpers;

namespace Fy.World {
	public struct BucketResult {
		public bool result;
		public LayerGridBucket bucket;
		public Tilable tilable;
	}

	public static class WorldUtils {

		public static Tilable ClosestTilableFromEnum (Vector2Int position, IEnumerable<Tilable> tilables) {
			Tilable result = null;
			float minDistance = float.MaxValue;
			foreach (Tilable tilable in tilables) {
				float currentMinDistance = Utils.Distance(position, tilable.position);
				if (currentMinDistance < minDistance) {
					minDistance = currentMinDistance;
					result = tilable;
				}
			}

			return result;
		}

		public static BucketResult HasVegetalNutrimentsInBucket(Vector2Int position) {
			foreach (LayerGrid grid in Loki.map.grids.Values) {
				LayerGridBucket bucket = grid.GetBucketAt(position);
				if (bucket != null && bucket.properties.vegetalNutriments > 0f) {
					Tilable rt = WorldUtils.ClosestTilableFromEnum(
						position, 
						bucket.tilables.Where(
							t => 
								t != null && 
								Loki.map[t.position].reserved == false 
								&& t.def.nutriments > 0
						)
					);

					if (rt != null) {
						return new BucketResult {
							result = true,
							bucket = bucket,
							tilable = rt
						};
					}
				}
			}

			return new BucketResult {
				result = false,
				bucket = null,
				tilable = null
			};
		}
	}
}
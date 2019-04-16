/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using Fy.Entities;

namespace Fy.World {
	public struct BucketResult {
		public bool result;
		public LayerGridBucket bucket;
		public Tilable tilable;
	}

	public static class WorldUtils {

		public static BucketResult HasVegetalNutrimentsInBucket(Vector2Int position) {
			foreach (LayerGrid grid in Loki.map.grids.Values) {
				LayerGridBucket bucket = grid.GetBucketAt(position);
				if (bucket != null && bucket.properties.vegetalNutriments > 0f) {
					Tilable rt = null;
					foreach (Tilable tilable in bucket.tilables) { // maybe us tilableType here but
						if (tilable != null && Loki.map[tilable.position].reserved == false && tilable.def.nutriments > 0f) {
							rt = tilable;
							break;
						}
					} 

					return new BucketResult {
						result = true,
						bucket = bucket,
						tilable = rt
					};
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
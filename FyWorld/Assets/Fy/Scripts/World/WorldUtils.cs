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
using Fy.Definitions;

namespace Fy.World {
	public struct BucketResult {
		public bool result;
		public LayerGridBucket bucket;
		public Tilable tilable;
	}

	public static class WorldUtils {
		public static List<Tilable> cutOrdered = new List<Tilable>();

		public static Tilable FieldNextToCut(Vector2Int playerPosition) {
			List<Tilable> toCut = new List<Tilable>();

			foreach (GrowArea area in GrowArea.areas) {
				foreach (Vector2Int position in area.positions) {
					Tilable tilable = Loki.map.grids[Layer.Plant].GetTilableAt(position);
					if (!Loki.map[position].reserved &&  tilable != null && tilable.def != area.plantDef) {
						toCut.Add(tilable);
					}
				}
			}
			return WorldUtils.ClosestTilableFromEnum(playerPosition, toCut);
		}

		public static Tilable NextToCut(Vector2Int playerPosition) {
			List<Tilable> toCut = new List<Tilable>(); 
			foreach (Tilable tilable in WorldUtils.cutOrdered) {
				if (!Loki.map[tilable.position].reserved) { // RETHINK THIS
					toCut.Add(tilable);
				}
			}
			return WorldUtils.ClosestTilableFromEnum(playerPosition, toCut);
		}

		public static Tilable FieldNextTileToDirt(Vector2Int playerPosition) {
			List<Tilable> toDirt = new List<Tilable>();
			foreach (GrowArea area in GrowArea.areas) {
				foreach (Vector2Int position in area.positions) {
					Field field = (Field)Loki.map.grids[Layer.Helpers].GetTilableAt(position);
					if (!Loki.map[position].reserved && field.dirt == false) {
						toDirt.Add((Tilable)field);
					}
				}
			}

			return WorldUtils.ClosestTilableFromEnum(playerPosition, toDirt);
		}

		public static Tilable FieldNextTileToSow(Vector2Int playerPosition) {
			List<Tilable> toSow = new List<Tilable>();
			foreach (GrowArea area in GrowArea.areas) {
				foreach (Vector2Int position in area.positions) {
					Tilable tilable = Loki.map.grids[Layer.Plant].GetTilableAt(position);
					Field field = (Field)Loki.map.grids[Layer.Helpers].GetTilableAt(position);
					if (!Loki.map[position].reserved && tilable == null && field.dirt == true) {
						toSow.Add((Tilable)field);
					}
				}
			}

			return WorldUtils.ClosestTilableFromEnum(playerPosition, toSow);
		}


		public static bool FieldHasWork() {
			if (
				WorldUtils.FieldHasPlantsToCut() ||
				WorldUtils.FieldHasDirtWork() ||
				WorldUtils.FieldHasPlantsToSow()
			) {
				return true;
			}

			return false;
		}

		public static bool FieldHasPlantsToCut() {
			foreach (GrowArea area in GrowArea.areas) {
				foreach (Vector2Int position in area.positions) {
					Tilable tilable = Loki.map.grids[Layer.Plant].GetTilableAt(position);
					if (!Loki.map[position].reserved && tilable != null && tilable.def != area.plantDef && tilable.def.cuttable) {
						return true;
					}
				}
			}
			return false;
		}

		public static bool HasPlantsToCut() {
			return WorldUtils.cutOrdered.Count > 0;
		}

		public static bool FieldHastPlantsToHarverst() {
			foreach (GrowArea area in GrowArea.areas) {
				foreach (Vector2Int position in area.positions) {
					Plant plant = (Plant)Loki.map.grids[Layer.Plant].GetTilableAt(position);
					if (!Loki.map[position].reserved & plant != null && plant.state == area.plantDef.plantDef.states) {
						return true;
					}
				}
			}
			return false;
		}

		public static bool FieldHasPlantsToSow() {
			foreach (GrowArea area in GrowArea.areas) {
				foreach (Vector2Int position in area.positions) {
					Tilable tilable = Loki.map.grids[Layer.Plant].GetTilableAt(position);
					Field field = (Field)Loki.map.grids[Layer.Helpers].GetTilableAt(position);
					if (!Loki.map[position].reserved && tilable == null && field.dirt == true) {
						return true;
					}
				}
			}
			return false;
		}

		public static bool FieldHasDirtWork() {
			foreach (GrowArea area in GrowArea.areas) {
				foreach (Vector2Int position in area.positions) {
					Field field = (Field)Loki.map.grids[Layer.Helpers].GetTilableAt(position);
					if (!Loki.map[position].reserved && field.dirt == false) {
						return true;
					}
				}
			}
			return false;
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

	}
}
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
using Fy.Definitions;
using Fy.Entity;
using Fy.Helpers;

namespace Fy.World {
	public class Map {
		/* Size of our map */
		public Vector2Int size { get; protected set; }

		/* Rect representing our map */
		public RectI mapRect;

		/* Ground noise map */
		public float[] groundNoiseMap { get; protected set; }

		public Dictionary<Layer, LayerGrid> grids;

		/// Let's create a world, that's not ostentatious at all.
		public Map(int width, int height) {
			this.size = new Vector2Int(width, height);
			this.mapRect = new RectI(new Vector2Int(0, 0), width, height);

			this.grids = new Dictionary<Layer, LayerGrid>();
			this.grids.Add(Layer.Ground, new GroundGrid(this.size));
			this.grids.Add(Layer.Plant, new TilableGrid(this.size));
			this.grids.Add(Layer.Mountain, new TilableGrid(this.size));
		}

		public float GetFertilityAt(Vector2Int position) {
			float fertility = 1f;
			foreach (Tilable tilable in this.GetAllTilablesAt(position)) {
				if (tilable.def.fertility == 0f) {
					return 0f;
				}
				fertility *= tilable.def.fertility;
			}
			return fertility;
		}

		public void BuildAllMeshes() {
			foreach (LayerGrid grid in this.grids.Values) {
				grid.BuildStaticMeshes();
			}
		}

		public void CheckAllMatrices() {
			foreach (LayerGrid grid in this.grids.Values) {
				grid.CheckMatriceUpdates();
			}
		}

		public void DrawTilables() {
			foreach (LayerGrid grid in this.grids.Values) {
				grid.DrawBuckets();
			}
		}

		public Tilable GetTilableAt(Vector2Int position, Layer layer) {
			return this.grids[layer].GetTilableAt(position);
		}

		public IEnumerable<Tilable> GetAllTilablesAt(Vector2Int position) {
			foreach (LayerGrid grid in this.grids.Values) {
				Tilable tilable = grid.GetTilableAt(position);
				if (tilable != null) { // Need to optimize this!
					yield return tilable;
				}
			}
		}

		/// Temporary method to add a Ground of definition "dirt" to all our tiles.
		public void TempMapGen() {
			this.groundNoiseMap = NoiseMap.GenerateNoiseMap(this.size, 11, NoiseMap.GroundWave(42));
			foreach (Vector2Int position in this.mapRect) {
				this.grids[Layer.Ground].AddTilable(
					new Ground(
						position,
						Ground.GroundByHeight(this.groundNoiseMap[position.x + position.y * this.size.x])
					)
				);

				if (this.grids[Layer.Ground].GetTilableAt(position).def.uid == "rocks") {
					this.grids[Layer.Mountain].AddTilable(
						new Mountain(position, Defs.mountains["mountain"])
					);
				}

				float _tileFertility = this.GetFertilityAt(position);
				if (_tileFertility > 0f) {
					foreach (TilableDef tilableDef in Defs.plants.Values) {
						if (
							_tileFertility >= tilableDef.plantDef.minFertility &&
							Random.value <= tilableDef.plantDef.probability
						) {
							this.grids[Layer.Plant].AddTilable(
								new Plant(position, tilableDef, true)
							);
							break;
						}
					}
				}
			}

			foreach (LayerGridBucket bucket in this.grids[Layer.Mountain].buckets) {
				bool changed = false;
				foreach (Tilable tilable in bucket.tilables) {
					if (tilable != null) {
						tilable.UpdateGraphics();
						changed = true;
					}
				}
				if (changed) {
					bucket.rebuildMatrices = true;
				}
			}
		}

		public override string ToString() {
			return "Map(size="+this.size+", area="+this.mapRect.area+")";
		}
	}
}
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
	// Our map
	public class Map {
		/* Rect representing our map */
		public RectI rect { get; protected set; }

		/* Size of our map */
		public Vector2Int size { get { return this.rect.size; } }

		/* Ground noise map */
		public float[] groundNoiseMap { get; protected set; }

		/* List of all the grids (one per layer + some utility grids) */
		public Dictionary<Layer, LayerGrid> grids;

		/// Let's create a world, that's not ostentatious at all.
		public Map(int width, int height) {
			this.rect = new RectI(new Vector2Int(0, 0), width, height);

			this.grids = new Dictionary<Layer, LayerGrid>();
			this.grids.Add(Layer.Ground, new GroundGrid(this.size));
			this.grids.Add(Layer.Plant, new TilableGrid(this.size));
			this.grids.Add(Layer.Mountain, new TilableGrid(this.size));
			this.grids.Add(Layer.Stackable, new TilableGrid(this.size));
		}

		public void UpdateVisibles() {
			int i = 0;
			foreach (LayerGridBucket bucket in this.grids[Layer.Ground].buckets){
				bool bucketVisible = bucket.CalcVisible();
				foreach (LayerGrid grid in this.grids.Values) {
					if (grid.layer != Layer.Ground) {
						grid.buckets[i].SetVisible(bucketVisible);
					}
				}
				i++;
			}
		}

		/// Get the fertility on a specific position. (Maybe we want a grid for this).
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

		/// Build all static meshes.
		public void BuildAllMeshes() {
			foreach (LayerGrid grid in this.grids.Values) {
				grid.BuildStaticMeshes();
			}
		}

		/// Update visible matrices for our tilables.
		public void CheckAllMatrices() {
			foreach (LayerGrid grid in this.grids.Values) {
				grid.CheckMatriceUpdates();
			}
		}

		/// Draw all tilables for visible buckets.
		public void DrawTilables() {
			foreach (LayerGrid grid in this.grids.Values) {
				grid.DrawBuckets();
			}
		}

		/// Spawn a tilable on the map
		public void Spawn(Vector2Int position, Tilable tilable, bool force=false) {
			if (force || tilable.def.layer == Layer.Undefined || this.GetTilableAt(position, tilable.def.layer) == null) {
				this.grids[tilable.def.layer].AddTilable(tilable);
			}
		}

		/// Get tilable on at position on a specific layer
		public Tilable GetTilableAt(Vector2Int position, Layer layer) {
			return this.grids[layer].GetTilableAt(position);
		}

		/// Get all tilable at a specific position.
		public IEnumerable<Tilable> GetAllTilablesAt(Vector2Int position) {
			foreach (LayerGrid grid in this.grids.Values) {
				Tilable tilable = grid.GetTilableAt(position);
				if (tilable != null) { // Need to optimize this!
					yield return tilable;
				}
			}
		}

		/// Temporary method @TODO: Clean this shit.
		public void TempMapGen() {
			this.groundNoiseMap = NoiseMap.GenerateNoiseMap(this.size, 11, NoiseMap.GroundWave(42));
			foreach (Vector2Int position in this.rect) {
				this.Spawn(
					position,
					new Ground(
						position,
						Ground.GroundByHeight(this.groundNoiseMap[position.x + position.y * this.size.x])
					)
				);

				if (this.grids[Layer.Ground].GetTilableAt(position).def.uid == "rocks") {
					this.Spawn(
						position,
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
							this.Spawn(
								position,
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

			foreach (Vector2Int position in new RectI(new Vector2Int(10, 10), 10, 10)) {
				this.Spawn(position, new Stackable(
					position,
					Defs.stackables["logs"],
					Random.Range(1, Defs.stackables["logs"].maxStack)
				));
			}
		}

		public override string ToString() {
			return "Map(size="+this.size+", area="+this.rect.area+")";
		}
	}
}
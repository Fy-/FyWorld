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

		public GroundGrid groundGrid;
		public PlantGrid plantGrid;

		/// Let's create a world, that's not ostentatious at all.
		public Map(int width, int height) {
			this.size = new Vector2Int(width, height);
			this.mapRect = new RectI(new Vector2Int(0, 0), width, height);

			this.groundGrid = new GroundGrid(this.size);
			this.plantGrid = new PlantGrid(this.size);
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
			this.groundGrid.BuildStaticMeshes();
			this.plantGrid.BuildStaticMeshes();
		}

		public IEnumerable<Tilable> GetAllTilablesAt(Vector2Int position) {
			
			Tilable tilable = this.groundGrid.GetTilableAt(position);
			if (tilable != null) {
				yield return tilable;
			}
			tilable = this.plantGrid.GetTilableAt(position);
			if (tilable != null) {
				yield return tilable;
			}
		}

		/// Temporary method to add a Ground of definition "dirt" to all our tiles.
		public void TempMapGen() {
			this.groundNoiseMap = NoiseMap.GenerateNoiseMap(this.size, 11, NoiseMap.GroundWave(42));
			foreach (Vector2Int position in this.mapRect) {
				this.groundGrid.AddTilable(
					new Ground(
						position,
						Ground.GroundByHeight(this.groundNoiseMap[position.x + position.y * this.size.x])
					)
				);
				float _tileFertility = this.GetFertilityAt(position);
				if (_tileFertility > 0f) {
					foreach (TilableDef tilableDef in Defs.plants.Values) {
						if (
							_tileFertility >= tilableDef.plantDef.minFertility &&
							Random.value <= tilableDef.plantDef.probability
						) {
							this.plantGrid.AddTilable(
								new Plant(position, tilableDef, true)
							);
							break;
						}
					}
				}
			}
		}

		public override string ToString() {
			return "Map(size="+this.size+")";
		}
	}
}
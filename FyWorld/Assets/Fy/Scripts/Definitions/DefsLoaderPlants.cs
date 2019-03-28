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

namespace Fy.Definitions {
	// A static class to load our definitions
	// Everything will be loaded from code or JSON files. 
	public static partial  class Defs {
		/// Add a plant definition to our plant dictionary
		public static void AddPlant(TilableDef def) {
			Defs.plants.Add(def.uid, def);
		}

		/// Load all plant definitions
		public static void LoadPlantsFromCode() {
			Defs.plants = new Dictionary<string, TilableDef>();

			Defs.AddPlant(new TilableDef{
				uid = "grass",
				layer = Layer.Plant,
				pathCost = .95f,
				blockPlant = true,
				blockStackable = true,
				type = TilableType.Grass,
				graphics = new GraphicDef{
					textureName = "grass"
				},
				plantDef = new PlantDef{
					probability = .5f,
					minFertility = .1f
				}
			});
			Defs.AddPlant(new TilableDef{
				uid = "tree",
				layer = Layer.Plant,
				type = TilableType.Tree,
				blockPath = true,
				blockStackable = true,
				blockPlant = true,
				graphics = new GraphicDef{
					textureName = "tree",
					size = new Vector2(2, 3f),
					pivot = new Vector2(.5f, 0)
				},
				plantDef = new PlantDef{
					probability = .1f,
					minFertility = .2f
				}
			});
		}
	}
}
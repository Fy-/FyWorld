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
		/// Add a plant definition to our building dictionary
		public static void AddBuilding(TilableDef def) {
			Defs.buildings.Add(def.uid, def);
		}

		/// Load all plant definitions
		public static void LoadBuildingsFromCode() {
			Defs.buildings = new Dictionary<string, TilableDef>();
			RecipeDef recipe = new RecipeDef();
			recipe.reqs.Add(
				Defs.stackables["logs"],
				2
			);

			Defs.AddBuilding(new TilableDef{
				uid = "wood_wall",
				layer = Layer.Building,
				type = TilableType.BuildingConnected,
				blockPath = true,
				blockStackable = true,
				blockPlant = true,
				graphics = new GraphicDef{
					textureName = "wall"
				},
				recipeDef = recipe
			});
		}
	}
}
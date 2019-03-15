/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;

namespace Fy.Definitions {
	// A static class to load our definitions
	// Everything will be loaded from code or JSON files. 
	public static class Defs {
		/// List of ground definitions
		public static Dictionary<string, TilableDef> grounds;

		/// Add a ground to our ground dictionary
		public static void AddGround(TilableDef def) {
			Defs.grounds.Add(def.uid, def);
		}

		/// Load all ground definitions
		public static void LoadGroundsFromCode() {
			Defs.grounds = new Dictionary<string, TilableDef>();

			/* Dirt */
			Defs.AddGround(
				new GroundDef{
					uid = "dirt",
					layer = Layer.Ground,
					graphics = new GraphicDef{
						textureName = "dirt",
						materialName = "grounds"
					}
				}
			);


			/* Water */
			Defs.AddGround(
				new GroundDef{
					uid = "water",
					layer = Layer.Ground,
					graphics = new GraphicDef{
						textureName = "water",
						materialName = "grounds"
					}
				}
			);
		}
	}
}
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
		/// Add a ground definition to our ground dictionary
		public static void AddGround(TilableDef def) {
			Defs.grounds.Add(def.uid, def);
		}

		/// Load all ground definitions
		public static void LoadGroundsFromCode() {
			Defs.grounds = new Dictionary<string, TilableDef>();
			Defs.groundsByHeight = new SortedDictionary<float, TilableDef>();

			/* Water */
			Defs.AddGround(
				new TilableDef{
					uid = "water",
					fertility = 0,
					layer = Layer.Ground,
					blockPath = true,
					graphics = new GraphicDef{
						textureName = "water",
						materialName = "grounds",
						isInstanced = false,
						drawPriority = 0,
					},
					groundDef = new GroundDef {
						maxHeight = .30f
					}
				}
			);

			/* Dirt */
			Defs.AddGround(
				new TilableDef{
					uid = "dirt",
					layer = Layer.Ground,
					fertility = 1f,
					graphics = new GraphicDef{
						textureName = "dirt",
						materialName = "grounds",
						isInstanced = false,
						drawPriority = 1
					},
					groundDef = new GroundDef {
						maxHeight = .5f
					}
				}
			);

			/* Rocks */
			Defs.AddGround(
				new TilableDef{
					uid = "rocks",
					layer = Layer.Ground,
					pathCost = 1.05f,
					graphics = new GraphicDef{
						textureName = "rocks",
						materialName = "grounds",
						isInstanced = false,
						drawPriority = 2
					},
					groundDef = new GroundDef {
						maxHeight = .75f
					}
				}
			);

			foreach (TilableDef tilableDef in Defs.grounds.Values) {
				Defs.groundsByHeight.Add(tilableDef.groundDef.maxHeight, tilableDef);
			}
		}
	}
}
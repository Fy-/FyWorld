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
		/// Add a mountain definition to our mountain directionnary
		public static void AddMountain(TilableDef def) {
			Defs.mountains.Add(def.uid, def);
		}
		/// Load all mountains definitions
		public static void LoadMountainsFromCode() {
			Defs.mountains = new Dictionary<string, TilableDef>();
			Defs.AddMountain(
				new TilableDef {
					uid = "mountain",
					blockPath = true,
					blockStackable = true,
					supportRoof = true,
					blockBuilding = true,
					blockPlant = true,
					layer = Layer.Mountain,
					graphics = new GraphicDef{
						textureName = "mountain",
						color = new Color(72/255f, 72/255f, 72/255f, 1f)
					}
				}
			);
		}
	}
}
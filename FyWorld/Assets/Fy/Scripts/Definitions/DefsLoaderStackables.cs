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
		public static void AddStackable(TilableDef def) {
			Defs.stackables.Add(def.uid, def);
		}

		/// Load all plant definitions
		public static void LoadStackablesFromCode() {
			Defs.stackables = new Dictionary<string, TilableDef>();

			Defs.AddStackable(new TilableDef{
				uid = "logs",
				layer = Layer.Stackable,
				blockStackable = true,
				graphics = new GraphicDef{
					textureName = "logs_stack",
					color = new Color(112/255f, 78/255f, 46/255f, 1f)
				},
				maxStack = 25
			});
		}
	}
}
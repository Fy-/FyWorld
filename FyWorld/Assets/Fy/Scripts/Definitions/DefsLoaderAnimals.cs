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
		public static void AddAnimal(AnimalDef def) {
			Defs.animals.Add(def.uid, def);
		}

		/// Load all plant definitions
		public static void LoadAnimalsFromCode() {
			Defs.animals = new Dictionary<string, AnimalDef>();

			Defs.AddAnimal(new AnimalDef{
				uid = "chicken",
				shortDesc = "A chicken (Gallus gallus domesticus) is a kind of domesticated bird. It is raised in many places for its meat and eggs.",
				graphics = new GraphicDef{
					textureName = "chicken_front"
				},
			});
		}
	}
}
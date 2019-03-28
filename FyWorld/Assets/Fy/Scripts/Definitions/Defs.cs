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
		/* List of ground definitions */
		public static Dictionary<string, TilableDef> grounds;
		/*  List of ground ordered by maxHeight */
		public static SortedDictionary<float, TilableDef> groundsByHeight;

		/* List of color palettes definitions */
		public static Dictionary<string, ColorPaletteDef> colorPallets;

		/* List of plant definitions */
		public static Dictionary<string, TilableDef> plants;

		/* List of mountains definitions */
		public static Dictionary<string, TilableDef> mountains;

		/* List of stackables defintions */
		public static Dictionary<string, TilableDef> stackables;

		/* List of animals definitions */
		public static Dictionary<string, AnimalDef> animals;

		/* List of tasks definitions */
		public static Dictionary<string, TaskDef> tasks;
	}
}
/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Fy.Entities;
using Fy.Helpers;
using Fy.Definitions;
using Fy.Characters.AI;


namespace Fy.World {
	public static partial class WorldUtils {
		public static Dictionary<TilableDef, List<Stackable>> stackables = new Dictionary<TilableDef, List<Stackable>>();
		public static Dictionary<TilableDef, int> stackablesCount = new Dictionary<TilableDef, int>();


		public static int StackableCount(TilableDef def) {
			if (WorldUtils.stackablesCount.ContainsKey(def)) {
				return WorldUtils.stackablesCount[def];
			}

			return 0;
		}

		public static void AddStackable(TilableDef def, Stackable stackable) {
			if (!WorldUtils.stackables.ContainsKey(def)) {
				WorldUtils.stackables.Add(def, new List<Stackable>());
				WorldUtils.stackablesCount.Add(def, 0);
			} 

			WorldUtils.stackables[def].Add(stackable);
			WorldUtils.stackablesCount[def] += stackable.inventory.count;
		}

		public static void ClearStackable(TilableDef def, Stackable stackable) {
			if (WorldUtils.stackables.ContainsKey(def)) {
				WorldUtils.stackables[def].Remove(stackable);
			}
		}

		public static void UpdateStackableCount(TilableDef def, int qty) {
			if (WorldUtils.stackablesCount.ContainsKey(def)) {
				WorldUtils.stackablesCount[def] += qty;
				if (WorldUtils.stackablesCount[def] < 0) {
					WorldUtils.stackablesCount[def] = 0;
				}
			}
		}
	}
}
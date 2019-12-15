/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using System;
using Fy.Definitions;
using Fy.World;
using Fy.Entities;

namespace Fy.Characters.AI {
	public class N_HaulRecipe : BrainNodeConditional {
		private class _HaulRecipe: BrainNode {
			public override Task GetTask() {
				TargetList targets = WorldUtils.RecipesToComplete(10, this.character);
				if (targets != null && targets.targets.Count != 0) {
					return new Task(
						Defs.tasks["haul_recipe"],
						targets
					);
				}
				return null;
			}
		}

		public N_HaulRecipe(Func<bool> condition) : base(condition) {
			this.subNodes.Add(new _HaulRecipe());
		}
	}
}
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

namespace Fy.Characters.AI {
	public class N_EatVegies : BrainNodeConditional {
		private class N_EatVegiesTaskData : BrainNode {
			public override Task GetTask() {
				BucketResult result = WorldUtils.HasVegetalNutrimentsInBucket(this.character.position);

				if (result.result) {
					return new Task(
						Defs.tasks["task_eat"],
						new TargetList(new Target(result.tilable))
					);
				}
				// We want to order the animal to change region, maybe go back to the spawning region.
				return null;
			}
		}

		public N_EatVegies(Func<bool> condition) : base(condition) {
			this.subNodes.Add(new N_EatVegiesTaskData());
		}
	}
}
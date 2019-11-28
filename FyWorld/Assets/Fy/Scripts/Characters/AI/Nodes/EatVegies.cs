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
	public class EatVegiesNode : BrainNodeConditional {
		private class EatVegiesNodeTaskData : BrainNode {
			public override TaskData GetTaskData() {
				BucketResult result = WorldUtils.HasVegetalNutrimentsInBucket(this.character.position);

				if (result.result) {
					return new TaskData(
						Defs.tasks["task_eat"],
						new TargetList(new Target(result.tilable)),
						this.character
					);
				}
				// We want to order the animal to change region, maybe go back to the spawning region.
				return null;
			}
		}

		public EatVegiesNode(Func<bool> condition) : base(condition) {
			this.subNodes.Add(new EatVegiesNodeTaskData());
		}
	}
}
/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System;
using Fy.Definitions;

namespace Fy.Characters.AI {
	public class N_Sleep : BrainNodeConditional {
		private class N_SleepTask : BrainNode {
			public override Task GetTask() {
				return new Task(
					Defs.tasks["task_sleep"],
					new TargetList(Target.GetRandomTargetInRange(this.character.position))
				);
			}
		}

		public N_Sleep(Func<bool> condition) : base(condition) {
			this.subNodes.Add(new N_SleepTask());
		}
	}
}
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
	public class SleepNode : BrainNodeConditional {
		private class SleepNodeTaskData : BrainNode {
			public override TaskData GetTaskData() {
				return new TaskData(
					Defs.tasks["task_sleep"],
					new TargetList(Target.GetRandomTargetInRange(this.character.position)),
					this.character
				);
			}
		}

		public SleepNode(Func<bool> condition) : base(condition) {
			this.subNodes.Add(new SleepNodeTaskData());
		}
	}
}
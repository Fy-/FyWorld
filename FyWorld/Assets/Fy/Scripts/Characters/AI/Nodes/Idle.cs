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
	public class IdleNodeTaskData : BrainNode {
		public override TaskData GetTaskData() {
			return new TaskData(
				Defs.tasks["task_idle"],
				new TargetList(Target.GetRandomTargetInRange(this.character.position)),
				this.character,
				UnityEngine.Random.Range(100, 250)
			);
		}
	}
}
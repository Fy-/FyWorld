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
	public class N_Idle : BrainNode {
		public override Task GetTask() {
			return new Task(
				Defs.tasks["task_idle"],
				new TargetList(Target.GetRandomTargetInRange(this.character.position)),
				UnityEngine.Random.Range(100, 250)
			);
		}
	}
}
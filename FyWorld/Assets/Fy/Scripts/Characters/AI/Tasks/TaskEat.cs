/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using Fy.Entities;

namespace Fy.Characters.AI {
	public class TaskEat : Task {
		public TaskEat(TaskData taskData, TaskRunner taskRunner) : base(taskData, taskRunner) {}

		public override bool Perform() {
			Tilable tilable = (Tilable)this.targets.current.entity;
			this.character.stats.vitals[Vitals.Hunger].currentValue += tilable.def.nutriments * 100f;
			tilable.Destroy();
			return true;
		}
	}
}
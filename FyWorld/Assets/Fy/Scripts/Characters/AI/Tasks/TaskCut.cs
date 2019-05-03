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
	public class TaskCut : Task {
		public TaskCut(TaskData taskData, TaskRunner taskRunner) : base(taskData, taskRunner) {}

		public override bool Perform() {
			Plant plant = (Plant)this.targets.current.entity;
			plant.Cut();
			return true;
		}
	}

	public class TaskDirt : Task {
		public TaskDirt(TaskData taskData, TaskRunner taskRunner) : base(taskData, taskRunner) {}

		public override bool Perform() {
			Field field = (Field)this.targets.current.entity;
			field.WorkDirt();
			return true;
		}
	}

	public class TaskSow : Task {
		public TaskSow(TaskData taskData, TaskRunner taskRunner) : base(taskData, taskRunner) {}

		public override bool Perform() {
			Field field = (Field)this.targets.current.entity;
			Loki.map.Spawn(this.targets.current.position, new Plant(
				field.position, 
				field.area.plantDef, 
				false)
			);
			return true;
		}
	}
}
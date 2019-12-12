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
	public class TaskEat : TaskClass {
		public TaskEat(BaseCharacter character, Task task) : base(character, task) {}

		public override bool Perform() {
			Tilable tilable = (Tilable)this.task.targets.current.tilable;
			this.character.stats.vitals[Vitals.Hunger].currentValue += tilable.def.nutriments * 100f;
			tilable.Destroy();
			return true;
		}
	}
}
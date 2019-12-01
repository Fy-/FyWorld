/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

namespace Fy.Characters.AI {
	public class TaskSleep : TaskClass {
		public TaskSleep(BaseCharacter character, Task task) : base(character, task) {}

		public override bool Perform() {
			if (this.character.stats.sleep == false) {
				this.character.stats.Sleep();
				return false;
			} 

			if (this.character.stats.vitals[Vitals.Energy].currentValue < this.character.stats.vitals[Vitals.Energy].value) {
				return false;
			}
			
			this.character.stats.WakeUp();
			return true;
		}
	}
}
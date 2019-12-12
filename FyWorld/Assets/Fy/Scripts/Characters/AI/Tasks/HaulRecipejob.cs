/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using Fy.Entities;
using Fy.Characters;
using Fy.Definitions;

namespace Fy.Characters.AI {
	public class HaulRecipeJob : JobClass {
		public HaulRecipeJob(BaseCharacter character, Task task) : base(character, task) {
			this.jobs = HaulRecipeJob.Haul(character, task);
			this.Next(false);
		}

		public static Queue<Job> Haul(BaseCharacter character, Task task) {
			Queue<Job> jobs = new Queue<Job>();
			Job let = new Job(
				delegate {
					return character.inventory.count > 0;
				}
			);

			let.OnEnd = delegate {
				Building building = (Building)Loki.map.GetTilableAt(task.targets.current.position, Layer.Building);
				Recipe recipe = building.recipe;

				if (recipe.needs[character.inventory.def].full == false) {
					character.inventory.TransfertTo(recipe.needs[character.inventory.def], recipe.needs[character.inventory.def].max);
				}
			};

			HaulResult res = HaulJob.Get(character, task, character.inventory.free);
			jobs.Enqueue(res.get);

			List<Target> targetList = task.targets.ToList();
			foreach (Target target in targetList) {
				if (target.tilable is Stackable) {
					res = HaulJob.Get(character, task, character.inventory.free);
					jobs.Enqueue(res.get);
				} else {
					jobs.Enqueue(let);
				}
			}

			return jobs;
		}
	}
}

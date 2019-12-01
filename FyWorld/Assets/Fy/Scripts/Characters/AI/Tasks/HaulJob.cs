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
using Fy.Definitions;

namespace Fy.Characters.AI {
	public struct HaulResult {
		public Job get;
		public int qty;

		public HaulResult(Job get, int qty = 0) {
			this.get = get;
			this.qty = qty;
		}
	}

	public class HaulJob : JobClass {
		public HaulJob(BaseCharacter character, Task task) : base(character, task) {
			this.jobs = HaulJob.Haul(character, task);
			this.Next(false);
		}

		public static HaulResult Get(BaseCharacter character, Task task, int qty =1) {
			Job get = new Job(
				delegate {
					// check inv?
					return false;
				}
			);
			get.OnEnd += delegate {
				Stackable stack = (Stackable)Loki.map.grids[Layer.Stackable].GetTilableAt(task.targets.current.position);
				if (stack == null || stack.inventory.count == 0) {
					task.state = TaskState.Failed;
					return;
				}

				stack.inventory.TransfertTo(character.inventory, qty);
			};


			Stackable _stack = (Stackable)Loki.map.grids[Layer.Stackable].GetTilableAt(task.targets.current.position);
			if (_stack != null) {
				return new HaulResult(get, _stack.inventory.count);
			} else {
				return new HaulResult(get, 0);
			}
		}

		public static Queue<Job> Haul(BaseCharacter character, Task task) {
			Queue<Job> jobs = new Queue<Job>();
			Job put = new Job(
				delegate {
					return character.inventory.count > 0;
				}
			);
			// To continue
			return jobs;
		}
	}
}
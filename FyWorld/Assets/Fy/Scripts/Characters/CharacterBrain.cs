/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using Fy.Characters.AI;

namespace Fy.Characters {
	// Base Btree for character actions
	public class CharacterBrain {
		/* Character */
		public BaseCharacter character { get; protected set; }

		/* Root node */
		public BrainNode brainNode { get; protected set; }

		/* Current Task */
		public Task currentTask;

		public CharacterBrain(BaseCharacter character, BrainNode brainNode) {
			this.character = character;
			this.brainNode = brainNode;
			this.brainNode.SetCharacter(character);
			this.currentTask = null;

/*			this.taskRunner.onEndTask = delegate {
				if (this.taskRunner.task.taskStatus == TaskStatus.Success) {
					///Debug.Log("Clearing task (success)");
					this.currentTaskData = null;
				} else if (this.taskRunner.task.taskStatus ==  TaskStatus.Failed) {
					//Debug.Log("Clearing task (failed)");
					this.currentTaskData = null;
				}
			};
			*/
		}

		/// Check if we have a current task (the data at least), if not get the next task in tree.
		public void Update() {
			if (this.currentTask == null) {
				this.GetNextTask();
			} else {
				if (this.currentTask.taskClass == null) {
					this.currentTask.GetClass(this.character);
				} {
					if (this.currentTask.state == TaskState.Success) {
						this.currentTask = null;
						// clear inv ?
					} else if (this.currentTask.state == TaskState.Failed) {
						// clear inv ?
						this.currentTask = null;
					} else {
						this.currentTask.taskClass.Tick();
					}
				}
			}
		} 

		/// Get the next task data in tree.
		public void GetNextTask() {
			Task nextTask = this.brainNode.GetTask();
			if (nextTask != null) {
				this.StartNextTask(nextTask);
			}
		}

		public void StartNextTask(Task nextTask) {
			this.currentTask = nextTask;
			// CallBacks for character end task ?
			/*
			if (this.character.onTaskEnd != null) {
				this.currentTask.endAction += this.character.onTaskEnd;
			}
			if (this.character.onTaskStart != null) {
				this.currentTask.startAction += this.character.onTaskStart;
			}*/
		}
	}
}
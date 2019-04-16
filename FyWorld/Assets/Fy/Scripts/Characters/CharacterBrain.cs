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
	public class CharacterBrain {
		public BaseCharacter character { get; protected set; }
		public BrainNode brainNode { get; protected set; }
		public TaskRunner taskRunner { get; protected set; }
		public TaskData currentTaskData { get; protected set; }

		public CharacterBrain(BaseCharacter character, BrainNode brainNode) {
			this.character = character;
			this.brainNode = brainNode;
			this.brainNode.SetCharacter(character);
			this.taskRunner = new TaskRunner();
			this.currentTaskData = null;

			this.taskRunner.onEndTask = delegate {
				if (this.taskRunner.task.taskStatus == TaskStatus.Success) {
					///Debug.Log("Clearing task (success)");
					this.currentTaskData = null;
				} else if (this.taskRunner.task.taskStatus ==  TaskStatus.Failed) {
					//Debug.Log("Clearing task (failed)");
					this.currentTaskData = null;
				}
			};
		}

		public void Update() {
			if (this.currentTaskData == null) {
				this.GetNextTaskData();
			} else {
				if (this.taskRunner.running == false) {
					//Debug.Log("Starting new task: "+this.currentTaskData.def.uid);
					this.taskRunner.StartTask(this.currentTaskData);
				} else {
					if (this.taskRunner.task.taskStatus == TaskStatus.Running) {
						this.taskRunner.task.Update();
					}
				}
			}
		} 

		public void GetNextTaskData() {
			TaskData nextTaskData = this.brainNode.GetTaskData();
			if (nextTaskData != null) {
				this.currentTaskData = nextTaskData;
			}
		}
	}
}
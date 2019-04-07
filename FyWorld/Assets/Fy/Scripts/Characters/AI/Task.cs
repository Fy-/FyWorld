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
using UnityEngine;

namespace Fy.Characters.AI {
	[System.Serializable]
	public enum TargetType {
		None,
		Tile,
		Adjacent,
	}

	[System.Serializable]
	public enum TaskStatus {
		Running,
		Success,
		Failed,
	}

	[System.Serializable]
	public enum TaskType {
		Idle,
		Sleep,
		Eat
	}

	[System.Serializable]
	public class TaskData {
		public TaskDef def;
		public TargetList targets;
		public BaseCharacter character;
		public int ticksToPerform;

		public TaskData(TaskDef def, TargetList targets, BaseCharacter character, int ticksToPerform = 0) {
			this.def = def;
			this.targets = targets;
			this.character = character;
			this.ticksToPerform = ticksToPerform;
		}
	}

	public class TaskRunner {
		public TaskDef def { get; protected set; }
		public Task task { get; protected set; }
		public bool running { get; protected set; }
		public Action onEndTask = null;

		public TaskRunner() {
			this.running = false;
		}

		public void StartTask(TaskData taskData) {
			this.def = taskData.def;
			if (this.def.taskType == TaskType.Sleep) {
				this.task = new TaskSleep(taskData, this);
			} else if (this.def.taskType == TaskType.Idle) {
				this.task = new TaskIdle(taskData, this);
			}
			this.running = true;
		}

		public void EndTask() {
			if (this.onEndTask != null) {
				this.onEndTask();
			}
			this.running = false;
			this.task = null;
		}
	}

	public abstract class Task {
		public BaseCharacter character { get; protected set; }
		public TaskRunner taskRunner { get; protected set; }
		public TaskStatus taskStatus { get; set; }
		public TargetList targets { get; protected set; }
		public TaskDef def { get { return taskRunner.def; } }

		private bool _inRange = false;
		private int _ticks = 0;
		private int _ticksToPerform = 0;

		public Task(TaskData taskData, TaskRunner taskRunner) {
			this.taskRunner = taskRunner;
			this.character = taskData.character;
			this.targets = taskData.targets;
			this._ticksToPerform = taskData.ticksToPerform;
			this.Start();
		}

		public virtual void Update() {
			if (this.taskStatus != TaskStatus.Running) {
				this.taskRunner.EndTask();
				return;
			}
			if (this.def.targetType == TargetType.None) {
				this.Run();
			} else {
				if (!this._inRange) {
					this.MoveInRange();
				} else {
					this.Run();
				}
			}
		}

		private void MoveInRange() {
			if (this.targets.current == null) {
				this.taskStatus = TaskStatus.Failed; // Specific failure condition?
				return;
			} else {
				if (
					this.character.position == this.targets.currentPosition
				) {
					this._inRange = true;
					return;
				}
				this.character.movement.Move(this);
			}
		}

		private void Run() {
			if (this.Perform()) {
				this.End();
			}
		}

		public virtual void End() {
			this.taskStatus = TaskStatus.Success;
			this.taskRunner.EndTask();
		}

		public virtual void Start() {
			this.taskStatus = TaskStatus.Running;
		}

		public virtual bool Perform() {
			if (this._ticksToPerform <= 0) {
				return true;
			} else {
				this._ticks++;
				if (this._ticks >= this._ticksToPerform) {
					return true;
				}
			}

			return false;
		}
	}
 }

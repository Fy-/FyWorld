/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
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

	public class TaskRunner {
		public TaskDef def { get; protected set; }
		public Task task { get; protected set; }
		public bool running { get; protected set; }
		
		public TaskRunner() {
			this.running = false;
		}

		public void StartTask(TaskDef def, BaseCharacter character, TargetList targets) {
			this.def = def;
			if (this.def.taskType == TaskType.Sleep) {
				this.task = new TaskSleep(character, targets, this);
			} else if (this.def.taskType == TaskType.Idle) {
				this.task = new TaskIdle(character, targets, this);
			}
			this.running = true;
		}

		public void EndTask() {
			this.running = false;
		}
	}

	public abstract class Task {
		public BaseCharacter character { get; protected set; }
		public TaskRunner taskRunner { get; protected set; }
		public TaskStatus taskStatus { get; set; }
		public TargetList targets { get; protected set; }
		public TaskDef def { get { return taskRunner.def; } }

		private bool _start = false;
		private bool _inRange = false;
		private int _ticks = 0;
		private int _ticksToPerform = 0;

		public Task(BaseCharacter character, TargetList targets, TaskRunner taskRunner) {
			this.taskRunner = taskRunner;
			this.character = character;
			this.targets = targets;
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
			if (!this._start) {
				this._start = true;
				this.Start();
			}
			if (this.Perform()) {
				this.End();
			}
		}

		public virtual void End() {
			this.taskStatus = TaskStatus.Success;
		}

		public virtual void Start() {
			this.taskStatus = TaskStatus.Running;
			this._ticksToPerform = this.def.ticksToPerform;
		}

		public virtual bool Perform() {
			if (this.def.ticksToPerform <= 0) {
				this.End();
				return true;
			} else {
				this._ticks++;
				if (this._ticks >= this._ticksToPerform) {
					this.End();
					return true;
				}
			}

			return false;
		}
	}

	public class TaskIdle : Task {
		public TaskIdle(BaseCharacter character, TargetList targets, TaskRunner taskRunner) : base(character, targets, taskRunner) {}
	}

	public class TaskSleep : Task {
		public TaskSleep(BaseCharacter character, TargetList targets, TaskRunner taskRunner) : base(character, targets, taskRunner) {}
	}
 }

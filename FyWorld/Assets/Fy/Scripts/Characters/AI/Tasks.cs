/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using Fy.Definitions;
using System;

namespace Fy.Characters.AI {
	public enum TaskState {
		Running,
		Success,
		Failed,
	}
	public enum TaskType {
		Idle,
		Sleep,
		Eat,
		Cut,
		Harvest,
		Sow, 
		Dirt
	}


	public abstract class TaskClass {
		public BaseCharacter character;

		// FX here ?
		public Task task;
		public int ticks = 0;
		public Action OnEnd = null;
		public Action OnStart = null;
		public Action OnTick = null;
		private bool _inRange =  false;

		public TaskClass(BaseCharacter character, Task task) {
			this.character = character;
			this.task = task;
			this.task.state = TaskState.Running;

			if (this.task.def.targetType == TargetType.Adjacent) {
				bool path = this.task.targets.current.GetClosestAdj(this.character.position);
				if (!path) {
					this.task.state = TaskState.Failed;
				}
			} else if (this.task.def.targetType == TargetType.None) {
				this._inRange = true;
			}
		}

		public virtual void Tick() {
			if (this.task.state != TaskState.Running) {
				return;
			}

			if (this._inRange) {
				if (this.ticks == 0 && this.OnStart != null) {
					this.OnStart();
				}
				if (this.Perform()) {
					this.End();
				}
			} else {
				if (this.task.def.targetType != TargetType.None) {
					this.MoveInRange();
				}
			}
		}

		public virtual void End() {
			this.task.state = TaskState.Success;
			this.task.targets.Free();
			if (this.OnEnd != null) {
				this.OnEnd();
			}
		}

		public virtual bool Perform() {
			this.ticks++;
			if (this.OnTick != null) {
				this.OnTick();
			}
			if (this.ticks >= this.task.ticksToPerform) {
				return true;
			}

			return false;
		}

		private void MoveInRange() {
			if (this.task.targets.current == null) {
				this.task.state = TaskState.Failed; // Specific failure condition?
				return;
			} else {
				if (
					this.character.position == this.task.targets.currentPosition
				) {
					this._inRange = true;
					return;
				}
				this.character.movement.Move(this.task);
			}
		}
	}

	public class Task {
		public TargetList targets;
		public TaskState state;
		public TaskDef def;
		public TaskClass taskClass;
		public int ticksToPerform = 0;

		public Task(TaskDef def) {
			this.def = def;
			this.ticksToPerform = def.ticksToPerform;
		}
		public Task(TaskDef def, TargetList targets) : this(def) {
			this.targets = targets;
			this.ticksToPerform = def.ticksToPerform;
		}
		public Task(TaskDef def, TargetList targets, int ticksToPerform) : this(def, targets) {
			this.ticksToPerform = ticksToPerform;
		}
		public Task(TaskDef def, int ticksToPerform) : this(def) {
			this.ticksToPerform = ticksToPerform;
		}

		public void GetClass(BaseCharacter character) {
			switch (this.def.taskType) {
				case TaskType.Cut:
					this.taskClass = new TaskCut(character, this);
					break;
				case TaskType.Dirt:
					this.taskClass = new TaskDirt(character, this);
					break;
				case TaskType.Sow:
					this.taskClass = new TaskSow(character, this);
					break;
				case TaskType.Sleep:
					this.taskClass = new TaskSleep(character, this);
					break;
				case TaskType.Idle:
					this.taskClass = new TaskIdle(character, this);
					break;
				default: 
					break;
			}
		}
	}

	public class Job {
		public JobClass taskClass;
		public bool interuptable = false;

		public Func<bool> preCondition;
		public Action OnEnd = null;
		public Action OnTick = null;
		public Action OnStart = null;

		public Job(Func<bool> preCondition) {
			this.preCondition = preCondition;
		}
		public Job() {
			this.preCondition = null;
		}
	}

	/*
		A JobClass is basicaly a set of dynamic tasks.
	*/
	public class JobClass : TaskClass {
		public Queue<Job> jobs = new Queue<Job>();
		public Job job { get; protected set; }

		public JobClass(BaseCharacter character, Task task) : base(character, task) {}

		public void Next(bool next = true) {
			if (this.jobs.Count == 0) {
				this.task.state = TaskState.Success;
				return;
			}
			this.job = this.jobs.Dequeue();
			if (next) {
				this.task.targets.Next();
			}

			if (this.job.preCondition == null || this.job.preCondition()) {
				this.ticks = 0;
				this.task.state = TaskState.Running;
				this.OnEnd += this.OnEnd;
				this.OnStart += this.OnStart;
				this.OnTick += this.OnTick;

				if (this.task.def.targetType == TargetType.Adjacent) {
					bool path = this.task.targets.current.GetClosestAdj(this.character.position);
					if (!path) {
						this.task.state = TaskState.Failed;
					}
				} 
			} else {
				if (this.job.interuptable) {
					this.task.state = TaskState.Failed;
				} else {
					if (this.jobs.Count == 0) {
						this.task.state = TaskState.Failed; // ?
					} else {
						this.Next(true);
					}
				}
			}
		}

		public override void End() {
			this.task.state = TaskState.Success;
			this.task.targets.Free();
		}

		public override bool Perform() {
			this.ticks++;
			if (this.OnTick != null) {
				this.OnTick();
			}
			if (this.ticks >= this.task.ticksToPerform) {
				if (this.jobs.Count == 0) {
					return true;
				} else {
					this.Next(true);
				}
			}
			return false;
		}
	}
}
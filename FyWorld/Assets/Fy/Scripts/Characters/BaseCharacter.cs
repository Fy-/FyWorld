/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using UnityEngine;
using Fy.Definitions;
using Fy.Visuals;
using Fy.Entities;
using Fy.Characters.AI;

namespace Fy.Characters { 
	public abstract class BaseCharacter : Entity {
		public BaseStats stats { get; protected set; }
		public LivingDef def { get; protected set; }
		public CharacterMovement movement { get; protected set; }
		public TaskRunner taskRunner { get; protected set; }
		public new Vector2Int position { get { return this.movement.position; } }
		public GraphicInstance graphics { get; protected set; }
		public string name { get; protected set; }

		private Mesh _mesh;

		public BaseCharacter(Vector2Int position, LivingDef def) {
			this.stats = new BaseStats();
			this.def = def;
			this.movement = new CharacterMovement(position, this);
			this.taskRunner = new TaskRunner();
			this.name = "Undefined "+Random.Range(1000,9999);

			if (this.def.graphics != null) {
				this.graphics = GraphicInstance.GetNew(this.def.graphics);
			}

			Loki.tick.toAdd.Enqueue(this.Update);
		}

		public virtual void Update() {
			///this.movement.Move(Target.GetRandomTargetInRange(this.position));
			if (this.taskRunner.running == false || this.taskRunner.task.taskStatus == TaskStatus.Failed || this.taskRunner.task.taskStatus == TaskStatus.Success) {
				this.taskRunner.StartTask(Defs.tasks["task_idle"], this, new TargetList(Target.GetRandomTargetInRange(this.position)));
			} else {
				this.taskRunner.task.Update();
			}
			this.stats.Update();
		}

		public virtual void UpdateDraw() {
			if (this.graphics == null) {
				return;
			}

			if (this._mesh == null) {
				this._mesh = MeshPool.GetPlaneMesh(this.def.graphics.size);
			}

			Graphics.DrawMesh(
				this._mesh,
				this.movement.visualPosition,
				Quaternion.identity,
				this.graphics.material,
				0
			);
		}
	}
}
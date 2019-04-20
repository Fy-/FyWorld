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
		public LivingDef def { get; protected set; }
		public CharacterStats stats { get; protected set; }
		public CharacterMovement movement { get; protected set; }
		public CharacterBrain brain { get; protected set; }
		public new Vector2Int position { get { return this.movement.position; } }
		public GraphicInstance graphics { get; set; }
		public string name { get; protected set; }

		private Mesh _mesh;

		public BaseCharacter(Vector2Int position, LivingDef def) {
			this.stats = new CharacterStats();
			this.def = def;
			this.movement = new CharacterMovement(position, this);
			this.brain = new CharacterBrain(this, this.GetBrainNode());
			this.name = this.SetName();

			if (this.def.graphics != null && this.def.graphics.textureName != string.Empty) { // @TODO: do this but better.
				this.graphics = GraphicInstance.GetNew(this.def.graphics);

			}

			Loki.tick.toAdd.Enqueue(this.Update);
		}

		public virtual string SetName() {
			return "Undefined "+Random.Range(1000,9999);
		}

		public abstract BrainNodePriority GetBrainNode();

		public virtual void Update() {
			this.brain.Update();
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
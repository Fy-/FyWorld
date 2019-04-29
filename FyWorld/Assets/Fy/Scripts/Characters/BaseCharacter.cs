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
	// Represent any living character in the game
	public abstract class BaseCharacter : Entity {
		/* Definition */
		public LivingDef def { get; protected set; }

		/* All statistics */
		public CharacterStats stats { get; protected set; }

		/* Movement */
		public CharacterMovement movement { get; protected set; }

		/* Action AI (BTree) */
		public CharacterBrain brain { get; protected set; }

		/* Shortcut/getter for the position (from this.movement) */
		public new Vector2Int position { get { return this.movement.position; } }

		/* Base graphic for a simple character, can be overwritten or not used at all */
		public GraphicInstance graphics { get; set; }

		/* Character name */
		public string name { get; protected set; }

		/* Base mesh for a simple character, , can be overwritten or not used at all */
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

		/// Set name (this should be overwritten by childrens)
		public virtual string SetName() {
			return "Undefined "+Random.Range(1000,9999);
		}

		/// Get the root BrainNode for the Action AI (this should be overwritten by childrens)
		public abstract BrainNodePriority GetBrainNode();

		/// Uddate stats, movement, AI
		public virtual void Update() {
			this.brain.Update();
			this.stats.Update();
		}

		/// Character default draw method
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
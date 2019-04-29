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
using Fy.Helpers;
using Fy.Definitions;
using Fy.Characters.AI;

namespace Fy.Characters {
	// Character movement
	public class CharacterMovement {
		/* Callback called when we change direction */
		public System.Action<Direction> onChangeDirection;

		/* Current tile */
		public Vector2Int position { get; protected set; }

		/* Direction we are looking at */
		public Direction lookingAt { get; protected set; }

		/* Final destination */
		public Vector2Int destination { get; protected set; }

		/* Current path queue, list of tile positions */
		public Queue<Vector2Int> path { get { return this._path; } }

		/* Character position on screen */
		public Vector3 visualPosition { 
			get {
				return new Vector3( 
					Mathf.Lerp(this.position.x, this._nextPosition.x, this._movementPercent),
					Mathf.Lerp(this.position.y, this._nextPosition.y, this._movementPercent),
					LayerUtils.Height(Layer.Count)
				);
			}
		}

		/* Movement percent between currentPosition and nextPosition */
		private float _movementPercent;

		/* Next tile */
		private Vector2Int _nextPosition;

		/* Do we have a destionation ? */
		private bool _hasDestination;

		/* Current path queue, list of tile positions */
		private Queue<Vector2Int> _path;

		/* Character speed. TODO: definine this using character.stats */
		private float _speed = .1f;

		/* Character */
		private BaseCharacter _character;

		public CharacterMovement(Vector2Int position, BaseCharacter character) {
			this.position = position;
			this._character = character;
			Loki.map[this.position].characters.Add(this._character);
			this.ResetMovement();
		}

		// Check if lookingAt is the same if not, call onChangeDirection.
		private void UpdateLookingAt(Vector2Int nextPos) {
			Direction original = this.lookingAt;
			Vector2Int t = nextPos - this.position;

			if (t.x > 0) {
				this.lookingAt = Direction.E;
			} else if (t.x < 0) {
				this.lookingAt = Direction.W;
			} else if (t.y > 0) {
				this.lookingAt = Direction.N;
			} else {
				this.lookingAt = Direction.S;
			}

			if (this.lookingAt != original && this.onChangeDirection != null) {
				this.onChangeDirection(this.lookingAt);
			}
		}

		/// Check if we have a path, if not try to get one. Then move towards the destination, tile by tile.
		public void Move(Task task) {
			if (this._hasDestination == false) {
				PathResult pathResult = AI.PathFinder.GetPath(this.position, task.targets.currentPosition);

				if (pathResult.success == false) {
					task.taskStatus = TaskStatus.Failed; // Maybe a special failed condition;
					this.ResetMovement();
					return;
				}
				
				this._hasDestination = true;
				this._path = new Queue<Vector2Int>(pathResult.path);
				this.destination = task.targets.currentPosition;
			}
			// Are we on our final destination 
			if (this.destination == this.position) {
				this.ResetMovement();
				return;
			}
			
			if (this.position == this._nextPosition) {
				this._nextPosition = this._path.Dequeue();
				this.UpdateLookingAt(this._nextPosition);
			}

			float distance = Utils.Distance(this.position, this._nextPosition);
			float distanceThisFrame = this._speed * Loki.map[this.position].pathCost; 
			this._movementPercent += distanceThisFrame / distance;

			if (this._movementPercent >= 1f) {
				Loki.map[this.position].characters.Remove(this._character);
				Loki.map[this._nextPosition].characters.Add(this._character);
				this.position = this._nextPosition;
				this._movementPercent = 0f;
			}
		}

		// Reset all data about the movement (used at the end of a path)
		private void ResetMovement() {
			this.destination = this.position;
			this._hasDestination = false;
			this._nextPosition = this.position;
			this._movementPercent = 0f;
			this._path = new Queue<Vector2Int>();
		}
	}
}
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
	public class CharacterMovement {
		public Vector2Int position { get; protected set; }
		public Direction lookingAt { get; protected set; }
		public Vector2Int destination { get; protected set; }
		public Queue<Vector2Int> path { get { return this._path; } }
		public Vector3 visualPosition { 
			get {
				return new Vector3( 
					Mathf.Lerp(this.position.x, this._nextPosition.x, this._movementPercent),
					Mathf.Lerp(this.position.y, this._nextPosition.y, this._movementPercent),
					LayerUtils.Height(Layer.Count)
				);
			}
		}

		private float _movementPercent;
		private Vector2Int _nextPosition;
		private bool _hasDestination;
		private Queue<Vector2Int> _path;
		private float _speed = .1f;
		private BaseCharacter _character;

		public CharacterMovement(Vector2Int position, BaseCharacter character) {
			this.position = position;
			this._character = character;
			Loki.map[this.position].characters.Add(this._character);
			this.ResetMovement();
		}

		private void UpdateLookingAt() {}

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
				this.UpdateLookingAt();
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



		private void ResetMovement() {
			this.destination = this.position;
			this._hasDestination = false;
			this._nextPosition = this.position;
			this._movementPercent = 0f;
			this._path = new Queue<Vector2Int>();
		}
	}
}
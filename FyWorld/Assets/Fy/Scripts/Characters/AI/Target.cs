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
using Fy.Entities;
using Fy.Helpers;
using Fy.World;

namespace Fy.Characters.AI {
	// List of targets in our Game
	public class TargetList {
		public Queue<Target> targets = new Queue<Target>();
		public Target current { get; protected set; }
		public bool setAdjs = false;

		public Vector2Int currentPosition { 
			get {
				if (this.current.closestAdj != new Vector2Int(-1,-1)) {
					return this.current.closestAdj;
				}
				return this.current.position;
			}
		}

		public TargetList(Entity entity) {
			this.Enqueue(entity);
			this.Next();
		}

		public TargetList(Target target) {
			this.Enqueue(target);
			this.Next();
		}

		public TargetList(Vector2Int position) {
			this.Enqueue(position);
			this.Next();
		}

		public TargetList(List<Entity> entities) {
			foreach (Entity entity in entities) {
				this.Enqueue(entity);
			}
			this.Next();
		}

		public void Enqueue(Target target) {
			Loki.map[target.position].reserved = true; // @Check adj
			this.targets.Enqueue(target);
		}

		public void Enqueue(Entity entity) {
			Loki.map[entity.position].reserved = true;
			this.targets.Enqueue(new Target(entity));
		}

		public void Enqueue(Vector2Int position) {
			Loki.map[position].reserved = true;
			this.targets.Enqueue(new Target(position));
		}

		public void Next() {
			if (this.current != null) {
				Loki.map[this.current.position].reserved = false;
			}
			this.current = this.targets.Dequeue();
			
		}
	}

	// A target in our game.
	public class Target {
		public Entity entity { get; protected set; }
		public Vector2Int position { get; protected set; }
		public Vector2Int closestAdj { get; protected set; }

		public Target(Entity entity) : this(entity.position) {
			this.entity = entity;
		}

		public Target (Vector2Int position) {
			this.position = position;
			this.closestAdj = new Vector2Int(-1, -1);
		}

		public bool GetClosestAdj(Vector2Int fromPosition) {
			float distance = float.MaxValue;
			TileProperty closestNeigbour = null; 

			for (int i = 0; i < 8; i++) {
				TileProperty tileProperty = Loki.map[this.position+DirectionUtils.neighbours[i]];
				if (tileProperty != null && !tileProperty.blockPath) {
					float d = Utils.Distance(fromPosition, this.position);
					if (d < distance) {
						distance = d;
						closestNeigbour = tileProperty;
					}
				}
			}

			if (closestNeigbour != null) {
				this.closestAdj = closestNeigbour.position;
				return true;
			}
			return false;
		}

		public static Target GetRandomTargetInRange(Vector2Int position, int range = 5) {
			Vector2Int targetPosition = new Vector2Int(
				Random.Range(position.x-range, position.x+range),
				Random.Range(position.y-range, position.y+range)
			);

			// Maybe do a max try and error if we reach this.
			while (Loki.map[targetPosition] == null || Loki.map[targetPosition].blockPath || Loki.map[targetPosition].reserved) {
				targetPosition = new Vector2Int(
					Random.Range(position.x-range, position.x+range),
					Random.Range(position.y-range, position.y+range)
				);
			}

			return new Target(targetPosition);
		}
	}
}
/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;
using Fy.Entities;
using Fy.Characters;

namespace Fy.World {
	public class TileProperty  {
		public Vector2Int position { get; protected set ; }
		public float pathCost { get; protected set ; }
		public float fertility { get; protected set ; }

		public bool blockPath { get; protected set ; }
		public bool blockPlant { get; protected set ; }
		public bool blockStackable { get; protected set ; }
		public bool blockBuilding { get; protected set ; }
		public bool supportRoof { get; protected set ; }
		public bool reserved { get; set; }

		public float gCost { get; set; }
		public float hCost { get; set; }
		public float fCost { get { return this.gCost + this.hCost; } }
		public TileProperty parent { get; set; }

		public List<BaseCharacter> characters { get; set; }

		public TileProperty(Vector2Int position) {
			this.position = position;
			this.characters = new List<BaseCharacter>();
			this.Reset();
		}

		public void Reset() {
			this.reserved = false;
			this.fertility = 1f;
			this.pathCost = 1f;
			this.blockPath = false;
			this.blockPlant = false;
			this.blockStackable = false;
			this.blockBuilding = false;
			this.supportRoof = false;
			this.gCost = 0;
			this.hCost = 0;
			this.parent = null;
		}

		public void Update() {
			this.Reset();

			foreach (Tilable tilable in Loki.map.GetAllTilablesAt(this.position)) {
				if (this.fertility != 0f) {
					this.fertility *= tilable.def.fertility;
				}
				if (!this.blockPath && this.pathCost != 0) {
					this.pathCost *= tilable.def.pathCost;
				}
				if (this.blockPath == false) {
					this.blockPath = tilable.def.blockPath;
				}
				if (this.blockStackable == false) {
					this.blockStackable = tilable.def.blockStackable;
				}
				if (this.blockPlant == false) {
					this.blockPlant = tilable.def.blockPlant;
				}
				if (this.blockBuilding == false) {
					this.blockBuilding = tilable.def.blockBuilding;
				}
				if (this.supportRoof == false) {
					this.supportRoof = tilable.def.supportRoof;
				}
			}


			if (this.blockPath) {
				this.pathCost = 0f;
			} else if (this.pathCost <= 0) {
				this.blockPath = true;
			}
		}
	}
}
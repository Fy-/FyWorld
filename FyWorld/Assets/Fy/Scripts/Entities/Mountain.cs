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
using Fy.Helpers;
using Fy.Visuals;

namespace Fy.Entity {
	public class ConnectedTilable {
		public bool[] connections { get; protected set; }
		public int connectionsInt { get; protected set; }
		public bool[] corners { get; protected set; }
		public bool allCorners { get; protected set; }
		public Tilable tilable { get; protected set; }

		public ConnectedTilable(Tilable tilable) {
			this.tilable = tilable;
			this.connections = new bool[8];
			this.connectionsInt = -1;
			this.allCorners = false;
			this.corners = new bool[4];
		}

		private void SetLinks() {
			// Iterate over each neighbour and check if the tilable is linked.
			for (int i = 0; i < 8; i++) {
				this.connections[i] = this.HasLink(this.tilable.position+DirectionUtils.neighbours[i]);
			}
		}

		private bool HasLink(Vector2Int position) {
			Tilable tilable = Loki.map.GetTilableAt(position, this.tilable.def.layer);
			if (tilable == null || this.tilable.def != tilable.def) {
				return false;
			}
			return true;
		}

		public void UpdateGraphics() {
			int connsInt = 0;
			this.SetLinks();
			int i = 0;
			foreach (int direction in DirectionUtils.cardinals) {
				if (this.connections[direction]) {
					connsInt += DirectionUtils.connections[i];
				}
				i++;
			}

			// Check if we need a roof or basicly checks corners here.
			i = 0;
			this.allCorners = true;
			bool hasCorner = false;
			foreach (int direction in DirectionUtils.corners) {
				if (
					(i != 0 || this.connections[direction] && (this.connections[(int)Direction.W] && this.connections[(int)Direction.S])) &&
					(i != 1 || this.connections[direction] && (this.connections[(int)Direction.W] && this.connections[(int)Direction.N])) &&
					(i != 2 || this.connections[direction] && (this.connections[(int)Direction.E] && this.connections[(int)Direction.N])) &&
					(i != 3 || this.connections[direction] && (this.connections[(int)Direction.E] && this.connections[(int)Direction.S]))
				) {
					this.corners[i]  = true;
					hasCorner = true;
				} else {
					this.corners[i] = false;
					this.allCorners = false;
				}
				
				i++;
			}

			if (connsInt != this.connectionsInt) {
				this.connectionsInt = connsInt;
				

				if (this.allCorners) {
					this.tilable.mainGraphic = GraphicInstance.GetNew(
						this.tilable.def.graphics,
						default(Color),
						Res.textures[this.tilable.def.graphics.textureName+"_cover"],
						1
					);
					
					Loki.map.GetTilableAt(this.tilable.position, Layer.Ground).hidden = true;
				} else {
					this.tilable.mainGraphic = GraphicInstance.GetNew(
						this.tilable.def.graphics,
						default(Color),
						Res.textures[this.tilable.def.graphics.textureName+"_"+this.connectionsInt.ToString()],
						1
					);

					if (hasCorner) {
						this.tilable.addGraphics.Add("cover",  
							GraphicInstance.GetNew(
								this.tilable.def.graphics,
								default(Color),
								Res.textures[this.tilable.def.graphics.textureName+"_cover"],
								2,
								MeshPool.GetCornersPlane(this.corners)
							)
						);
					}
					Loki.map.GetTilableAt(this.tilable.position, Layer.Ground).hidden = false;
				}
			}
		}
	}

	public class Mountain : Tilable {
		private ConnectedTilable _connectedUtility;

		public Mountain(Vector2Int position, TilableDef def) {
			this.position = position;
			this.def = def;
			this._connectedUtility = new ConnectedTilable(this);
			this.mainGraphic = GraphicInstance.GetNew(
				this.def.graphics,
				default(Color),
				Res.textures[this.def.graphics.textureName+"_0"],
				1
			);
			this.addGraphics = new Dictionary<string, GraphicInstance>();
		}

		public override void UpdateGraphics() {
			this._connectedUtility.UpdateGraphics();
		}
	}
}
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
using Fy.World;

namespace Fy.Entities {
	/// Mountain tilable	

	public class Recipe {
		public RecipeDef def;
		public Dictionary<TilableDef, int> current;
		public bool finished;

		public Recipe(RecipeDef def) {
			this.def = def;
			this.finished = false;
			this.current = new Dictionary<TilableDef, int>();
			foreach (KeyValuePair<TilableDef, int> kv in this.def.reqs) {
				this.current[kv.Key] = 0;
			}
			WorldUtils.recipes.Add(this);
		}

		public bool IsComplete() {
			if (!this.finished) {
				foreach (KeyValuePair<TilableDef, int> kv in this.def.reqs) {
					if (this.current[kv.Key] < this.def.reqs[kv.Key]) {
						return false;
					}
				}
				this.finished = true;
				WorldUtils.recipes.Remove(this);
				// Add work in world?
				return true;
			}
			return true;
		}
	}

	public class Building : Tilable {
		private ConnectedTilable _connectedUtility;
		protected Recipe recipe;
		public bool isBlueprint { get { return this.def.buildingDef.work == this.work; } }
		public int work;

		public Building(Vector2Int position, TilableDef def) {
			this.position = position;
			this.def = def;
			this.recipe = new Recipe(this.def.recipeDef);
			this.work = 0;

			if (this.def.type == TilableType.BuildingConnected) {

				this.mainGraphic = GraphicInstance.GetNew(
					this.def.graphics,
					new Color(112/255f, 78/255f, 46/255f, .5f), // update alpha when constructed
					Res.textures[this.def.graphics.textureName+"_0"],
					1
				);
				this._connectedUtility = new ConnectedTilable(this);
			}
			this.addGraphics = new Dictionary<string, GraphicInstance>();

			Tilable tilable = Loki.map.grids[Layer.Plant].GetTilableAt(this.position);
			if (tilable != null && tilable.def.cuttable) {
				tilable.AddOrder(Defs.orders["cut_plants"]);
			}
		}

		public override void UpdateGraphics() {
			if (this.def.type ==  TilableType.BuildingConnected) {
				this._connectedUtility.UpdateGraphics();
			}
		}
	}
}
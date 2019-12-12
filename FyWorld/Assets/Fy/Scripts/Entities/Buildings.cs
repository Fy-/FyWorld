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

	public class Recipe: Tilable {
		public new RecipeDef def;
		public Dictionary<TilableDef, Inventory> needs;
		public bool finished { get; protected set; }
		public Building building;

		public bool canBeComplete { 
			get {
				foreach (KeyValuePair<TilableDef, Inventory> kv in this.needs) {
					if (WorldUtils.StackableCount(kv.Key) < kv.Value.free) {
						return false;
					}
				}
				return true;
			}
		}

		public Recipe(RecipeDef def, Building building, Vector2Int position) {
			this.def = def;
			this.hidden = true;
			this.finished = false;
			this.building = building;
			this.position = position;
			this.needs = new Dictionary<TilableDef, Inventory>();
			foreach (KeyValuePair<TilableDef, int> kv in this.def.reqs) {
				this.needs.Add(kv.Key, new Inventory(
					kv.Key, kv.Value
				));
				this.needs[kv.Key].OnChangeCount = (qty) => {
					this.IsComplete();
				};
			}


			WorldUtils.recipes.Add(this);
		}

		public bool IsComplete() {
			if (!this.finished) {
				foreach (Inventory inv in this.needs.Values) {
					if (!inv.full) {
						return false;
					}
				}
				this.finished = true;
				Debug.Log("FINISHED");
				this.building.Construct();
				WorldUtils.recipes.Remove(this);
				return true;
			}
			return true;
		}
	}

	public class Building : Tilable {
		private ConnectedTilable _connectedUtility;
		public Recipe recipe { get; protected set; }
		public bool isBlueprint { get { return this.def.buildingDef.work == this.work; } }
		public int work;

		public Building(Vector2Int position, TilableDef def) {
			this.position = position;
			this.def = def;
			this.recipe = new Recipe(this.def.recipeDef, this, position);
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

		public void Construct() {
			this.mainGraphic = GraphicInstance.GetNew(
				this.def.graphics,
				new Color(112/255f, 78/255f, 46/255f, 1f), 
				this.mainGraphic.texture,
				1
			);
			this.UpdateGraphics();
			if (this.bucket != null) {
				this.bucket.rebuildMatrices = true;
			}
		}

		public override void UpdateGraphics() {
			if (this.def.type ==  TilableType.BuildingConnected) {
				this._connectedUtility.UpdateGraphics();
			}
		}
	}
}
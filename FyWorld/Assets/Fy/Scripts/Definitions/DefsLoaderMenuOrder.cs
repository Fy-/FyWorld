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

namespace Fy.Definitions {
	// A static class to load our definitions
	// Everything will be loaded from code or JSON files. 
	public static partial  class Defs {
		/// Add a plant definition to our plant dictionary
		public static void AddMenuOrder(MenuOrderDef def) {
			Defs.orders.Add(def.uid, def);
		}

		/// Load all plant definitions
		public static void LoadMenuOrdersFromCode() {
			Defs.orders = new Dictionary<string, MenuOrderDef>();

			Defs.AddMenuOrder(
				new MenuOrderDef {
					uid = "cut_wood",
					name = "Cut Wood",
					shortDesc = "Cut all trees in a designated area.",
					selector = SelectorType.AreaTile,
					layer = Layer.Orders,
					sprite = Res.sprites["order_to_cut"],
					actionArea = (RectI rect) => {
						foreach (Vector2Int position in rect) {
							Plant plant = (Plant)Loki.map.grids[Layer.Plant].GetTilableAt(position);
							if (plant != null && plant.def.cuttable && plant.def.type == TilableType.Tree) {
								//plant.OrderToCut();
								plant.AddOrder(Defs.orders["cut_wood"]);
							}
						}
					},
					graphics = new GraphicDef{
						textureName = "order_to_cut"
					},
				}
			);
			Defs.AddMenuOrder(
				new MenuOrderDef {
					uid = "cut_plants",
					name = "Cut Plants",
					layer = Layer.Orders,
					shortDesc = "Cut all plants in a designated area.",
					selector = SelectorType.AreaTile,
					sprite = Res.sprites["order_to_cut_plant"],
					actionArea = (RectI rect) => {
						foreach (Vector2Int position in rect) {
							Plant plant = (Plant)Loki.map.grids[Layer.Plant].GetTilableAt(position);
							if (plant != null && plant.def.cuttable) {
								plant.AddOrder(Defs.orders["cut_plants"]);
							}
						}
					},
					graphics = new GraphicDef{
						textureName = "order_to_cut_plant"
					},
				}
			);
			Defs.AddMenuOrder(
				new MenuOrderDef {
					uid = "harvest_plants",
					name = "Haverst Plants",
					layer = Layer.Orders,
					shortDesc = "Harvest all plants in a designated area.",
					selector = SelectorType.AreaTile,
					sprite = Res.sprites["order_harvest"],
					actionArea = (RectI rect) => {
						foreach (Vector2Int position in rect) {
							Plant plant = (Plant)Loki.map.grids[Layer.Plant].GetTilableAt(position);
							if (plant != null && plant.def.cuttable && plant.def.type == TilableType.Plant) {
								plant.AddOrder(Defs.orders["harvest_plants"]);
							}
						}
					},
					graphics = new GraphicDef{
						textureName = "order_harvest"
					},
				}
			);
		}
	}
}
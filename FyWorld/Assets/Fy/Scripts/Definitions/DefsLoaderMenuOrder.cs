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
					sprite = Res.sprites["order_to_cut"],
					action = (Vector2Int position) => {
						Plant plant = (Plant)Loki.map.grids[Layer.Plant].GetTilableAt(position);
						if (plant != null && plant.def.cuttable && plant.def.type == TilableType.Tree) {
							plant.OrderToCut();
						}
					}
				}
			);
			Defs.AddMenuOrder(
				new MenuOrderDef {
					uid = "cut_plants",
					name = "Cut Plants",
					shortDesc = "Cut all plants in a designated area.",
					selector = SelectorType.AreaTile,
					sprite = Res.sprites["order_to_cut_plant"],
					action = (Vector2Int position) => {
						Plant plant = (Plant)Loki.map.grids[Layer.Plant].GetTilableAt(position);
						if (plant != null && plant.def.cuttable) {
							plant.OrderToCut();
						}
					}
				}
			);
			Defs.AddMenuOrder(
				new MenuOrderDef {
					uid = "harvest_plants",
					name = "Haverst Plants",
					shortDesc = "Harvest all plants in a designated area.",
					selector = SelectorType.AreaTile,
					sprite = Res.sprites["order_harvest"],
					action = (Vector2Int position) => {
						Plant plant = (Plant)Loki.map.grids[Layer.Plant].GetTilableAt(position);
						if (plant != null && plant.def.cuttable && plant.def.type == TilableType.Plant) {
							plant.OrderToCut();
						}
					}
				}
			);
		}
	}
}
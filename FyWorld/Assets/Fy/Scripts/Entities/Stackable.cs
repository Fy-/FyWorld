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
	/// Stackable tilable	
	public class Stackable : Tilable {
		public InventoryTilable inventory { get; protected set; }

		public Stackable(Vector2Int position, TilableDef def, int count) {
			this.position = position;
			this.def = def;
			this.inventory = new InventoryTilable(this, count);
			this.position = position;
			this.def = def;
			this.mainGraphic = GraphicInstance.GetNew(this.def.graphics);

			Loki.stackableLabelController.AddLabel(this);
		}
	}
}
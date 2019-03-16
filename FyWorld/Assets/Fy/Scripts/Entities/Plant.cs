/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using Fy.Definitions;
using Fy.Visuals;

namespace Fy.Entity {
	// Plant
	public class Plant : Tilable {
		public Plant(Vector2Int position, TilableDef def) {
			this.position = position;
			this.def = def;
			this.graphics = GraphicInstance.GetNew(def.graphics, Color.green);
		}
	}
}
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
	// Ground
	public class Ground : Tilable {
		public Ground(Vector2Int position, TilableDef def) {
			this.position = position;
			this.def = def;
			this.graphics = GraphicInstance.GetNew(def.graphics);
		}
	}
}
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

namespace Fy.Entities {
	/// Ground Tilable
	public class Ground : Tilable {
		public Ground(Vector2Int position, TilableDef def) {
			this.position = position;
			this.def = def;
			this.mainGraphic = GraphicInstance.GetNew(def.graphics);
		}

		/// Get the correct ground definition by height.
		public static TilableDef GroundByHeight(float height) {
			foreach (TilableDef tilableDef in Defs.groundsByHeight.Values) {
				if (height <= tilableDef.groundDef.maxHeight) {
					return tilableDef;
				}
			}

			return Defs.grounds["water"];
		}
	}
}
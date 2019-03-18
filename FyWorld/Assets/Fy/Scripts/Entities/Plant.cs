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
using Fy.Visuals;
using Fy.Helpers;

namespace Fy.Entity {
	// Plant
	public class Plant : Tilable {
		private Color _leafColor;
		private Color _woodColor;

		public Plant(Vector2Int position, TilableDef def) {
			this.position = position;
			this.def = def;
			
			if (this.def.type == TilableType.Grass) {
				this._leafColor = Defs.colorPallets["cols_leafsGreen"].GetRandom();
				this.mainGraphic = GraphicInstance.GetNew(def.graphics, this._leafColor);
			} else if (this.def.type == TilableType.Tree) {
				this.addGraphics = new Dictionary<string, GraphicInstance>();

				this._leafColor = Defs.colorPallets["cols_leafsGreen"].GetRandom();
				this._woodColor = Defs.colorPallets["cols_wood"].colors[0];
				this.mainGraphic = GraphicInstance.GetNew(
					def.graphics, 
					this._woodColor, 
					Res.textures[this.def.graphics.textureName+"_base"],
					0
				);

				this.addGraphics.Add("leafs", 
					GraphicInstance.GetNew(
						def.graphics,
						this._leafColor,
						Res.textures[this.def.graphics.textureName+"_leafs"],
						1
					)
				);
			} else {
				this.mainGraphic = GraphicInstance.GetNew(def.graphics);
			}
		}
	}
}
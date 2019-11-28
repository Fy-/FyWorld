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

namespace Fy.Entities {
	/// Mountain tilable	
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
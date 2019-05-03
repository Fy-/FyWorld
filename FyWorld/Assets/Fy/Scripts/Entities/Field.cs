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
	public class Field : Tilable {
		public GrowArea area { get; protected set; }
		public bool dirt { get; protected set; }

		public Field(Vector2Int position, TilableDef def, GrowArea area) {
			this.addGraphics = new Dictionary<string, GraphicInstance>();
			this.area = area;
			this.dirt = false;
			this.position = position;
			this.def = def;
			this.mainGraphic = GraphicInstance.GetNew(
				this.def.graphics, 
				this.area.color, 
				Res.TextureUnicolor(this.area.color),
				1
			);
			this.addGraphics.Add("dirt", 
				GraphicInstance.GetNew(
					this.def.graphics,
					Color.white,
					Res.TextureUnicolor(Color.clear),
					2
				)
			);
		}

		public void WorkDirt() {
			this.dirt = true;
			this.addGraphics["dirt"] = GraphicInstance.GetNew(
				this.def.graphics,
				Color.white,
				Res.textures["dirt_ready"],
				2
			);
			if (this.bucket != null) {
				this.bucket.rebuildMatrices = true;
			}
		}
	}
}
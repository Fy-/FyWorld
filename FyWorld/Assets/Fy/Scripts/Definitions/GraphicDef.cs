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

namespace Fy.Definitions {
	/// Represent a grahpic in out game.
	[System.Serializable]
	public class GraphicDef : Def {
		/* Texture name in the Resources/Textures directory */
		public string textureName;

		/* Material name in the Resources/Materials directory */
		public string materialName = "tilables";

		/* Graphic size */
		public Vector2 size = Vector2.one;

		/* Graphic color */
		public Color color = Color.white;

		/* Is Instanced ? */
		public bool isInstanced = true;

		/* Draw Priority */
		public float drawPriority = 0f;

		/* Graphic pivot */
		public Vector2 pivot = Vector2.zero;
	}

}
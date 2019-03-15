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
	// A tilable is just an entity contain in a tile/layer.
	public class Tilable {
		// Position 
		public Vector2Int position { get; protected set; }

		// Definition
		public TilableDef def { get; protected set; }

		// Graphic Instance
		public GraphicInstance graphics { get; protected set; }
	}
}
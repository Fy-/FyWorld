/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using Fy.Visuals;
using Fy.Entities;
using Fy.Definitions;

namespace Fy.World {
	// Generic grid with no renderer for tilables.
	public  class TilableGrid : LayerGrid {
		public TilableGrid (Vector2Int size) : base(size, Layer.Plant) {
			this.renderer = null;
			this.GenerateBuckets();
		}
	}
}
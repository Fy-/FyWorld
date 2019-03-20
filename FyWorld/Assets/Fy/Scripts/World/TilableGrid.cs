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
using Fy.Entity;
using Fy.Definitions;

namespace Fy.World {
	public  class TilableGrid : LayerGrid {
		public TilableGrid (Vector2Int size) : base(size, Layer.Plant) {
			this.renderer = null;
			this.GenerateBuckets();
		}
	}
}
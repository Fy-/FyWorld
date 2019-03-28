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
	// Grid used for the ground with a special renderer (BucketGroundRenderer)
	public  class GroundGrid : LayerGrid {
		public GroundGrid (Vector2Int size) : base(size, Layer.Ground) {
			this.renderer = typeof(BucketGroundRenderer);
			this.GenerateBuckets();
		}
	}
}
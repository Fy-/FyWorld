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
	/// Definition for a tilable
	[System.Serializable]
	public class TilableDef : Def {
		/* Layer */
		public Layer layer;

		/* Tilable type */
		public TilableType type = TilableType.Undefined;

		/* Grahpic data (for example size, texture, shader/material) */
		public GraphicDef graphics;

		/* Ground def */
		public GroundDef groundDef;

		/* Plant def */
		public PlantDef plantDef;

		/* Fertility of our tilable */
		public float fertility = 0f;

		/* Food value */
		public float nutriments = 0f;

		/* pathCost of our tilable */
		public float pathCost = 1f;

		public bool blockPath = false;
		public bool blockPlant = false;
		public bool blockStackable = false;
		public bool blockBuilding = false;
		public bool supportRoof = false;

		/* Max stack count */
		public int maxStack = 0;
	}
}
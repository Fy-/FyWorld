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
	/// Definition for a plant
	[System.Serializable]
	public class PlantDef : Def {
		public float probability = 0f;
		public float minFertility = 0f;
		public int states = 5;
		public float lifetime = 2f; // Days
	}
}
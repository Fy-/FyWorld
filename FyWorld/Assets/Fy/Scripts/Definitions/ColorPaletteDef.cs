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
	// Definition for a color palette 
	[System.Serializable]
	public class ColorPaletteDef : Def {
		public List<Color> colors = new List<Color>(15);

		public Color GetRandom() {
			return this.colors[Random.Range(0, this.colors.Count)];
		}
	}
}
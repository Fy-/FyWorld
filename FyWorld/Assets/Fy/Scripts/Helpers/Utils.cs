/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;

namespace Fy.Helpers {
	public static class Utils {
		public static float Normalize(float min, float max, float value) {
			return (value - min) / (max - min);
		}

		public static float Distance(Vector2Int a, Vector2Int b) {
			if (
				Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) == 1
			) {
				return 1f;
			}

			if (
				Mathf.Abs(a.x - b.x) == 1 && 
				Mathf.Abs(a.y - b.y) == 1
			) {
				return 1.41121356237f;
			}

			return Mathf.Sqrt(
				Mathf.Pow((float)a.x-(float)b.x, 2) +
				Mathf.Pow((float)a.y-(float)b.y, 2)				
			);
		}
	}
}
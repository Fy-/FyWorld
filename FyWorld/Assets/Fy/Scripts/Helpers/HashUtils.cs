/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/

namespace Fy.Helpers {
	public static class HashUtils {
		public static int HashBoolArray(bool[] arr) {
			int hash = arr.Length;
			foreach (bool v in arr) {
				hash = unchecked(hash*314159 + ((!v) ? 1 : 2));
			}
			return hash;
		}
	}
}
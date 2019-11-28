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
	// List of directions
	public enum Direction {
		S, SW, W, NW, N, NE, E, SE
	}

	// Utils for directions (position, cardinals, corners).
	public static class DirectionUtils {
		public static Vector2Int[] neighbours = new Vector2Int[8];
		public static int[] cardinals = new int[4];
		public static int[] corners = new int[4];
		public static int[] connections = new int[4];

		/// Set all statics for directions
		public static void LoadStatics() {
			DirectionUtils.neighbours[0] = new Vector2Int(0,-1);
			DirectionUtils.neighbours[1] = new Vector2Int(-1, -1);
			DirectionUtils.neighbours[2] = new Vector2Int(-1, 0);
			DirectionUtils.neighbours[3] = new Vector2Int(-1, 1);
			DirectionUtils.neighbours[4] = new Vector2Int(0, 1);
			DirectionUtils.neighbours[5] = new Vector2Int(1, 1);
			DirectionUtils.neighbours[6] = new Vector2Int(1, 0);
			DirectionUtils.neighbours[7] = new Vector2Int(1, -1);

			DirectionUtils.cardinals[0] = (int)Direction.N;
			DirectionUtils.cardinals[1] = (int)Direction.W;
			DirectionUtils.cardinals[2] = (int)Direction.E;
			DirectionUtils.cardinals[3] = (int)Direction.S;

			DirectionUtils.connections[0] = 1;
			DirectionUtils.connections[1] = 2;
			DirectionUtils.connections[2] = 4;
			DirectionUtils.connections[3] = 8;

			DirectionUtils.corners[0] = (int)Direction.SW;
			DirectionUtils.corners[1] = (int)Direction.NW;
			DirectionUtils.corners[2] = (int)Direction.NE;
			DirectionUtils.corners[3] = (int)Direction.SE;
		}
	}
}
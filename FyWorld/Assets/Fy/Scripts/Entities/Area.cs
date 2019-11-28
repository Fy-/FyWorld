/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using System.Collections.Generic;
using Fy.Helpers;
using Fy.Definitions;

namespace Fy.Entities {
	public abstract class Area {
		public HashSet<Vector2Int> positions { get; protected set; }
		public Color color { get; protected set; }

		public Area() {
			this.positions = new HashSet<Vector2Int>();
		}

		protected virtual void AddTilable(Vector2Int position) {
			this.positions.Add(position);
		}
		protected virtual void DelTilable(Vector2Int position) {
			this.positions.Remove(position);
		}

		public void Add(Vector2Int position) {
			this.AddTilable(position);
		}

		public void Del(Vector2Int position) {
			this.DelTilable(position);
		}

		public void Add(RectI rect) {
			foreach (Vector2Int position in rect) {
				this.AddTilable(position);
			}
		}

		public void Del(RectI rect) {
			foreach (Vector2Int position in rect) {
				this.positions.Remove(position);
			}
		}
	}
}
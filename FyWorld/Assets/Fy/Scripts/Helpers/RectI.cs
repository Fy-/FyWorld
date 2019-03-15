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

namespace Fy.Helpers {
	// Represents a rectangle
	public struct RectI {
		/*      ___________max
		*      |           *
		*      |           |
		*      *___________|
		*    min
		*/
		public Vector2Int min;
		public Vector2Int max;
		public Vector2Int size { get { return new Vector2Int(this.width, this.height); } }
		public int width { get { return this.max.x - this.min.x; } }
		public int height { get { return  this.max.y - this.min.y; } }
		public int area { get { return  this.width * this.height; } }

		public RectI(Vector2Int min, Vector2Int max) {
			this.min = min;
			this.max = max;
		}

		public RectI(Vector2Int min, int width, int height) {
			this.min = min;
			this.max = new Vector2Int(this.min.x+width, this.min.y+height);
		}

		/// Enumerator: foreach (Vector2Int p in rect)
		public IEnumerator<Vector2Int> GetEnumerator() {
			for (int x = this.min.x; x < this.max.x; x++) {
				for (int y = this.min.y; y < this.max.y; y++) {
					yield return new Vector2Int(x, y);
				}
			}
		}

		/// Clip: clip this rectangle inside an other
		public void Clip(RectI other) {
			if (this.min.x < other.min.x) {
				this.min.x = other.min.x;
			}
			if (this.max.x > other.max.x) {
				this.max.x = other.max.x;
			}
			if (this.min.y < other.min.y) {
				this.min.y = other.min.y;
			}
			if (this.max.y > other.max.y) {
				this.max.y = other.max.y;
			}
		}

		/// Contains: is Vector2Int in our rectangle
		public bool Contains(Vector2Int v) {
			return (
				v.x >= this.min.x &&
				v.y >= this.min.y &&
				v.x < this.max.x &&
				v.y < this.max.y
			);
		}

		/// GetHashCode
		public override int GetHashCode() {
			return this.min.x + this.min.y * this.width + this.max.x * this.height + this.max.y;
		}

		/// ToString 
		public override string ToString() {
			return "RectI(min="+this.min+", max="+this.max+", size="+this.size+", area="+this.area+")";
		}
	}
}
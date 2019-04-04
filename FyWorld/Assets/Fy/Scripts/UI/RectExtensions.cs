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
using UnityEngine.UI;

namespace Fy.UI {
	public static class RectExtensions {
		public static Rect RoundToInt(this Rect self) {
			return new Rect(
				Mathf.RoundToInt(self.x),
				Mathf.RoundToInt(self.y),
				Mathf.RoundToInt(self.width),
				Mathf.RoundToInt(self.height)
			);
		}
		public static Rect AtZero(this Rect self) {
			return new Rect(
				0,
				0,
				Mathf.RoundToInt(self.width),
				Mathf.RoundToInt(self.height)
			);
		}
		
		public static Rect Padding(this Rect self, RectOffset padding) {
			return new Rect(
				self.x+padding.left,
				self.y+padding.top,
				self.width-padding.horizontal,
				self.height-padding.vertical
			);
		}
		public static Rect ReSize(this Rect self, float w, float h) {
			return new Rect(
				self.x,
				self.y,
				w,
				h
			);
		}

		public static Rect Height(this Rect self, float height) {
			return self.ReSize(self.width, height);
		}

		public static Rect Width(this Rect self, float width) {
			return self.ReSize(width, self.height);
		}

		public static Rect Expand(this Rect self, float d) {
			return new Rect(
				self.x - (d),
				self.y - (d),
				self.width + (d*2),
				self.height + (d*2)
			);
		}

		public static Rect Contract(this Rect self, float d) {
			return self.Expand(-d);
		}
		
		public static Rect FromTopRight(this Rect self, Rect parent, float right, float top) {
			return new Rect(
				parent.width - self.width - right,
				top,
				self.width,
				self.height
			);
		}

		public static Rect[] HorizontalGrid(this Rect self, float[] widths, float space = 0) {
			float x = self.x;
			List<Rect> result = new List<Rect>();
			result.Add(self);

			foreach (float width in widths) {
				result.Add(new Rect(x, self.y, width-space, self.height));
				x += width+space;
			}
			return result.ToArray();
		}

		public static Rect[] VerticalGrid(this Rect self, float[] heights, float space = 0) {
			float y = self.y;
			List<Rect> result = new List<Rect>();
			result.Add(self);
			foreach (float height in heights) {
				result.Add(new Rect(self.x, y, self.width, height-space));
				y += height+space;
			}
			result[0].Height(y);
			return result.ToArray();
		}
	}
}
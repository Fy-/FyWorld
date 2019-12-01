/*w
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
	public class WindowBuildMenu : Window {
		public WindowBuildMenu() : base() {
			this._hasTitle = true;
			this.title = "Build Menu";
			this.closeButton = false;
			this.draggable = false;
			this._centered = false;
			this.resizeable = false;
			this._show = true;
			this.tabs = new List<string>();
			this.padding = new RectOffset(0, 0, 0, 0);
			this.initialSize = new Vector2(Screen.width, 100f);
			this.rect = new Rect(0, Screen.height-100, Screen.width, 100);
			this.size = this.initialSize;
		}
		public override void Content() {
		
		}
	}
}
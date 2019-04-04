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
	public class WindowTest : Window {
		public WindowTest() : base() {

		}
		public override void Content() {
			GUILayout.BeginVertical();
			WindowComponents.Label("Hello <3");
			WindowComponents.Label("Hello <3");
			WindowComponents.Label("Hello <3");
			GUILayout.EndVertical();
		}
	}
}
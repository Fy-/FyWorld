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
using Fy.Characters;
using Fy.Helpers;

namespace Fy.Controllers {
	public class WindowController : MonoBehaviour {
		private void Start() {
			UI.WindowComponents.LoadComponents();
		}
		private void OnGUI() {
			foreach (UI.Window window in UI.Window.windows) {
				window.OnGUI();
			}
		}
	}
}
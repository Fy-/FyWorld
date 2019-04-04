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
using Fy.Characters;
using Fy.Definitions;
using Fy.Helpers;

namespace Fy.UI {
	public class WindowCharacter : Window {
		private BaseCharacter _character;

		public WindowCharacter(BaseCharacter character) : base() {
			this._character = character;
			this.SetTitle(this._character.name);
			this.AddTab("Character");
			this.AddTab("Skills");
			this.AddTab("Detailed stats");
		}

		public override void Content() {
			if (this.activeTab == 0) {
				Rect contentRect = this.rect;
				if (this._hasTitle) {
				 	contentRect = contentRect.FromTopRight(this.rect, 0, this.headerSize);
				}
				contentRect = contentRect.Padding(this.padding);

				float[] heights = new float[this._character.stats.vitals.Values.Count];
				int i;
				for (i = 0; i < heights.Length; i++) { heights[i] = 26; }
				
				Rect[] vGrid = contentRect.VerticalGrid(heights, 5);
				i = 1;
				foreach (Vital vital in this._character.stats.vitals.Values) {
					WindowComponents.FillableBarWithLabelValue(
						vGrid[i], 
						vital.name, 
						Utils.Normalize(0, vital.value, vital.currentValue), 
						Defs.namedColorPallets["cols_vitals"].colors[vital.name]
					);
					i++;
				}
			} else if (this.activeTab == 1) {
				
			}

			/*

			GUILayout.BeginVertical();
			WindowComponents.Label("Hello <3");
			WindowComponents.Label("Hello <3");
			WindowComponents.Label("Hello <3");
			GUILayout.EndVertical();*/
		}
	}
}
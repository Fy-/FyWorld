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
			//this.AddTab("Skills");
			this.AddTab("Infos");
			this.AddTab("Inventory");
		}

		public override void Content() {
			if (this.activeTab == 0) {
				this.vGrid.H2("Vitals");
				if (this._character.brain.currentTask != null) {
					this.vGrid.Span(this._character.brain.currentTask.def.uid);
				} else {
					this.vGrid.Span("Doing nothing...");
				}
				foreach (Vital vital in this._character.stats.vitals.Values) {
					WindowComponents.FillableBarWithLabelValue(
						this.vGrid.GetNewRect(20f),
						vital.name,
						vital,
						Defs.namedColorPallets["cols_vitals"].colors[vital.name]
					);
				}
				this.vGrid.H2("Stats");
				foreach (Stat stat in this._character.stats.stats.Values) {
					WindowComponents.SimpleStat(this.vGrid.GetNewRect(20f), stat.name, stat.value, stat.baseValue);
				}
			} else if (this.activeTab == 1) {
				this.vGrid.H2("Infos");
				this.vGrid.Paragraph(this._character.def.shortDesc);
				this.vGrid.H2("Detailled Stats");
				foreach (Stat attr in this._character.stats.attributes.Values) {
					WindowComponents.SimpleStat(this.vGrid.GetNewRect(20f), attr.name, attr.value, attr.baseValue);
				}
			} else if (this.activeTab == 2) {
				this.vGrid.H2("Inventory");
				if (this._character.inventory.def != null) {
					this.vGrid.Paragraph(this._character.inventory.def.name +  " : " + this._character.inventory.count + "/" + this._character.inventory.max);
				} else {
					this.vGrid.Paragraph("Empty");
				}
			}
		}
	}
}
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

namespace Fy.Characters {
	public class HumanStats : CharacterStats {
		public HumanStats() : base() {}
		
		protected override void LoadAttributes() {
			base.LoadAttributes();
			
			this.attributes[Attributes.InventorySize].AddModifier(new StatModifier(this.stats[Stats.Strength], 3f));

			foreach (Attribute att in this.attributes.Values) {
				att.Update();
			}
		}

		public override string ToString() {
			string str = "HumanStats(";
			foreach (Stat stat in this.stats.Values) {
				str += stat.name+":"+stat.value+",";
			}
			foreach (Vital stat in this.vitals.Values) {
				str += stat.name+":"+stat.value+",";
			}
			foreach (Attribute stat in this.attributes.Values) {
				str += stat.name+":"+stat.value+",";
			}
			return str+")";
		}
	}
}


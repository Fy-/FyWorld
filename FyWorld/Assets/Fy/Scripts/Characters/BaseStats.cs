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
	public class BaseStats {
		// stats[Stats.Strength].value;
		public Dictionary<Stats, Stat> stats;
		public Dictionary<Vitals, Vital> vitals;
		public Dictionary<Attributes, Attribute> attributes;

		public BaseStats() {
			this.stats = new Dictionary<Stats, Stat>();
			foreach (Stats stat in StatsUtils.stats) {
				this.stats.Add(stat, new Stat(stat.ToString()));
				this.stats[stat].baseValue = Random.Range(1, 5);
			}

			this.vitals = new Dictionary<Vitals, Vital>();
			foreach (Vitals vital in StatsUtils.vitals) {
				this.vitals.Add(vital, new Vital(vital.ToString()));
			}
			this.LoadVitals();

			this.attributes = new Dictionary<Attributes, Attribute>();
			foreach (Attributes att in StatsUtils.attributes) {
				this.attributes.Add(att, new Attribute(att.ToString()));
			}
			this.LoadAttributes();
		}
		
		protected virtual void LoadAttributes() {

			this.attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(this.stats[Stats.Strength], .5f));
			this.attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(this.stats[Stats.Endurance], .5f));

			this.attributes[Attributes.HealthRegen].AddModifier(new StatModifier(this.stats[Stats.Strength], .3f));
			this.attributes[Attributes.HealthRegen].AddModifier(new StatModifier(this.stats[Stats.Endurance], .3f));

			this.attributes[Attributes.EnergyRegen].AddModifier(new StatModifier(this.stats[Stats.Wisdom], .3f));
			this.attributes[Attributes.EnergyRegen].AddModifier(new StatModifier(this.stats[Stats.Endurance], .3f));

			foreach (Attribute att in this.attributes.Values) {
				att.Update();
			}
		}

		protected virtual void LoadVitals() {
			this.vitals[Vitals.Health].AddModifier(new StatModifier(this.stats[Stats.Endurance], 20f));
			this.vitals[Vitals.Energy].AddModifier(new StatModifier(this.stats[Stats.Agility], 2f));
			this.vitals[Vitals.Energy].AddModifier(new StatModifier(this.stats[Stats.Strength], 2f));
			this.vitals[Vitals.Energy].AddModifier(new StatModifier(this.stats[Stats.Wisdom], 2f));
			this.vitals[Vitals.Mana].AddModifier(new StatModifier(this.stats[Stats.Intellect], 10f));

			foreach (Vital vital in this.vitals.Values) {
				vital.Update();
				vital.Fill();
			}
		}

		public override string ToString() {
			string str = "BaseStats(";
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


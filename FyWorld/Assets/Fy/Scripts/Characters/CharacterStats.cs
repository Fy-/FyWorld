/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using Fy.Helpers;

namespace Fy.Characters {
	// Character stats
	public class CharacterStats {
		/* All base stats */
		public Dictionary<Stats, Stat> stats { get; protected set; }

		/* All vitals, (bars, hp, mp, etc...) */
		public Dictionary<Vitals, Vital> vitals { get; protected set; }

		/* Attributes (or implicit stats) */
		public Dictionary<Attributes, Attribute> attributes { get; protected set; }

		/* Are we sleeping */
		public bool sleep { get; protected set; }

		/* Callback when we wake up */
		public Action onWakeUp = null;

		/* Callback when we go to sleep */
		public Action onSleep = null;

		public CharacterStats() {
			this.sleep = false;
			this.stats = new Dictionary<Stats, Stat>();
			foreach (Stats stat in StatsUtils.stats) {
				this.stats.Add(stat, new Stat(stat.ToString()));
				this.stats[stat].baseValue = UnityEngine.Random.Range(5, 10);
			}

			this.vitals = new Dictionary<Vitals, Vital>();
			foreach (Vitals vital in StatsUtils.vitals) {
				if (vital == Vitals.Hunger || vital  == Vitals.Joy) {
					this.vitals.Add(vital, new Vital(vital.ToString(), 100));
				} else {
					this.vitals.Add(vital, new Vital(vital.ToString()));
				}
			}
			this.LoadVitals();

			this.attributes = new Dictionary<Attributes, Attribute>();
			foreach (Attributes att in StatsUtils.attributes) {
				this.attributes.Add(att, new Attribute(att.ToString()));
			}
			this.LoadAttributes();
		}
		
		/// Define all default attributes.
		protected virtual void LoadAttributes() {

			this.attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(this.stats[Stats.Strength], .3f));
			this.attributes[Attributes.WalkSpeed].AddModifier(new StatModifier(this.stats[Stats.Endurance], .2f));

			this.attributes[Attributes.HealthRegen].AddModifier(new StatModifier(this.stats[Stats.Strength], .1f));
			this.attributes[Attributes.HealthRegen].AddModifier(new StatModifier(this.stats[Stats.Endurance], .2f));

			this.attributes[Attributes.EnergyRegen].AddModifier(new StatModifier(this.stats[Stats.Endurance], .2f));

			this.attributes[Attributes.PhysicalArmour].AddModifier(new StatModifier(this.stats[Stats.Strength], .1f));
			this.attributes[Attributes.PhysicalArmour].AddModifier(new StatModifier(this.stats[Stats.Endurance], .3f));

			this.attributes[Attributes.PhysicalAttack].AddModifier(new StatModifier(this.stats[Stats.Strength], .3f));
			this.attributes[Attributes.PhysicalAttack].AddModifier(new StatModifier(this.stats[Stats.Agility], .2f));

			this.attributes[Attributes.ManaRegen].AddModifier(new StatModifier(this.stats[Stats.Intellect], .3f));
			this.attributes[Attributes.ManaRegen].AddModifier(new StatModifier(this.stats[Stats.Wisdom], .2f));

			this.attributes[Attributes.CriticalChance].AddModifier(new StatModifier(this.stats[Stats.Agility], .2f));
			this.attributes[Attributes.CriticalChance].AddModifier(new StatModifier(this.stats[Stats.Intellect], .2f));

			this.attributes[Attributes.Charisma].AddModifier(new StatModifier(this.stats[Stats.Wisdom], .2f));
			this.attributes[Attributes.Charisma].AddModifier(new StatModifier(this.stats[Stats.Strength], .2f));


			foreach (Attribute att in this.attributes.Values) {
				att.Update();
			}
		}

		// Define all default vitals.
		protected virtual void LoadVitals() {
			this.vitals[Vitals.Health].AddModifier(new StatModifier(this.stats[Stats.Endurance], 20f));
			this.vitals[Vitals.Energy].AddModifier(new StatModifier(this.stats[Stats.Agility], 5f));
			this.vitals[Vitals.Energy].AddModifier(new StatModifier(this.stats[Stats.Strength], 5f));
			this.vitals[Vitals.Energy].AddModifier(new StatModifier(this.stats[Stats.Wisdom], 5f));
			this.vitals[Vitals.Mana].AddModifier(new StatModifier(this.stats[Stats.Intellect], 10f));
			this.vitals[Vitals.Joy].baseValue = 100;

			foreach (Vital vital in this.vitals.Values) {
				vital.Update();
				vital.Fill();
			}
		}

		// Update stats
		public virtual void Update() {
			if (!this.sleep) {
				if (this.vitals[Vitals.Energy].currentValue > 0) {
					this.vitals[Vitals.Energy].currentValue -= .15f;
				}
			} else {
				if (this.vitals[Vitals.Energy].currentValue < this.vitals[Vitals.Energy].value) {
					this.vitals[Vitals.Energy].currentValue += this.attributes[Attributes.EnergyRegen].value;
				}
			}

			if (this.vitals[Vitals.Hunger].currentValue > 0) {
				this.vitals[Vitals.Hunger].currentValue -= .3f;
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

		/// Go to sleep
		public void Sleep() {
			this.sleep = true;
			if (this.onSleep != null) {
				this.onSleep();
			}
		}

		/// Wake up
		public void WakeUp() {
			this.sleep = false;
			if (this.onWakeUp != null) {
				this.onWakeUp();
			}
		}
	}
}


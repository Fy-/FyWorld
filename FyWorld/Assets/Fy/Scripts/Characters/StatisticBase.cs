/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;

namespace Fy.Characters {
	public class Stat {
		/* Stat name */
		public string name;

		/* Value of the stat */
		public int baseValue;

		/* Buff value */
		public int buffValue;

		/* Real value */
		public int value { get { return this.baseValue + this.buffValue; } }

		public Stat(string name) {
			this.name = name;
			this.baseValue = 0;
			this.buffValue = 0;
		}
	}

	// Modifier of an implicit statistic
	public class StatModifier {
		/* Stat */
		public Stat stat;

		/* Ratio */
		public float ratio;

		public StatModifier(Stat stat, float ratio) {
			this.stat = stat;
			this.ratio = ratio;
		}
	}

	// Implicit statistic (calculated in ratio to one or many implicit statistic)
	public class Attribute : Stat {
		/* List of modifiers for this stat */
		private List<StatModifier> _modifiers;


		public Attribute(string name) : base(name) {
			this._modifiers = new List<StatModifier>();
		}

		public void AddModifier(StatModifier modifier) {
			this._modifiers.Add(modifier);
		}

		public void Update() {
			this.baseValue = 0;
			if (this._modifiers.Count > 0) {
				foreach (StatModifier modifier in this._modifiers) {
					this.baseValue += (int)(modifier.stat.value * modifier.ratio);
				}
			}
		}
	}

	// Vital
	public class Vital : Attribute {
		private int _currentValue;

		public int currentValue {
			get {
				if (this._currentValue > this.value) {
					this._currentValue = value;
				}
				return this._currentValue;
			}
			set {
				this._currentValue = value;
			}
		}

		public Vital(string name) : base(name) {
			this._currentValue = 0;
		}

		public void Fill() {
			this._currentValue = this.value;
		}
	}
}
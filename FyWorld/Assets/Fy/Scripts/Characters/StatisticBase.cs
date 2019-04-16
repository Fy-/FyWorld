/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using Fy.Helpers;

namespace Fy.Characters {
	public class Stat {
		/* Stat name */
		public string name;

		/* Value of the stat */
		public float baseValue;

		/* Buff value */
		public float buffValue;

		/* Real value */
		public float value { get { return this.baseValue + this.buffValue; } }

		public Stat(string name) {
			this.name = name;
			this.baseValue = 0;
			this.buffValue = 0;
		}

		public Stat(string name, int max) {
			this.name = name;
			this.baseValue = max;
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

		public Attribute(string name, int max) : base(name, max) {
			this._modifiers = new List<StatModifier>();
		}

		public void AddModifier(StatModifier modifier) {
			this._modifiers.Add(modifier);
		}

		public void Update() {
			if (this.baseValue == 0) {
				if (this._modifiers.Count > 0) {
					foreach (StatModifier modifier in this._modifiers) {
						this.baseValue += (int)(modifier.stat.value * modifier.ratio);
					}
				}
			}
		}
	}

	// Vital
	public class Vital : Attribute {
		private float _currentValue;

		public float currentValue {
			get {
				if (this._currentValue > this.value) {
					this._currentValue = value;
				}
				if (this._currentValue < 0) {
					this._currentValue = 0;
				}
				return this._currentValue;
			}
			set {
				this._currentValue = value;
			}
		}

		public bool ValueInfToPercent(float v) {
			if (v >= Utils.Normalize(0, this.value, this.currentValue)) {
				return true;
			}
			return false;
		}

		public Vital(string name) : base(name) {
			this._currentValue = 0;
		}

		public Vital(string name, int max) : base(name, max) {
			this.baseValue = max;
			this._currentValue = max;
		}

		public void Fill() {
			this._currentValue = this.value;
		}
	}
}
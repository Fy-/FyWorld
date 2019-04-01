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
using Fy.Helpers;

namespace Fy.Controllers {
	public class CharacterDataStatsController : MonoBehaviour {
		private Stat _stat;
		private Text _label;
		private Text _value;
		private bool _loaded = false;

		public void LoadStat(Stat stat) {
			this._label = this.transform.GetChild(0).gameObject.GetComponent<Text>();
			this._value = this.transform.GetChild(1).gameObject.GetComponent<Text>();
			this._stat = stat;
			this._label.text = this._stat.name;
			this._loaded = true;
		}

		private void Update() {
			if (this._loaded) {
				this._value.text = this._stat.value.ToString();
			}
		}
	}
}
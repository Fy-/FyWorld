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
	public class CharacterDataVitalController : MonoBehaviour {
		private Color _color;
		private Vital _vital;
		private bool _loaded = false;
		private Text _label;
		private Image _fill;
		private Text _percent;

		public void LoadVital(Vital vital, Color color) {
			this._color = color;
			this._vital = vital;
			this._loaded = true;
			this._label = this.transform.GetChild(0).gameObject.GetComponent<Text>();
			this._percent = this.transform.GetChild(2).gameObject.GetComponent<Text>();
			this._fill = this.transform.GetChild(1).gameObject.GetComponentsInChildren<Image>()[1];
			this._fill.fillAmount = Utils.Normalize(0, this._vital.value, this._vital.currentValue);
			this._fill.color = this._color;
			this._label.text = this._vital.name;
			this._percent.text = Mathf.Round(Utils.Normalize(0, this._vital.value, this._vital.currentValue)*100)+"%";
		}

		private void Update() {
			if (this._loaded) {
				this._fill.fillAmount = Utils.Normalize(0, this._vital.value, this._vital.currentValue);
				this._percent.text = Mathf.Round(Utils.Normalize(0, this._vital.value, this._vital.currentValue)*100)+"%";
			}
		}
	}
}
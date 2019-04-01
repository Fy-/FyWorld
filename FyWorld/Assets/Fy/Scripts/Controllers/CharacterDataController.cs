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

namespace Fy.Controllers {
	public class CharacterDataController : MonoBehaviour {
		/*
		public GameObject vitalPrefab;
		public GameObject textPrefab;
		public GameObject statsPrefab;

		public Transform containerTransform;

		private Text _name;
		private GameObject _statsRight;
		private GameObject _statsLeft;

		private BaseCharacter _character;
		
		*/
		private BaseCharacter _character;
		private List<CharacterDataVitalController> _vitals;
		private List<CharacterDataStatsController> _stats;
		private List<CharacterDataStatsController> _attrs;
		private Text _name;
		private Transform _leftPanel;
		private Transform _centerPanel;
		private Transform _rightPanel;
		private Button _close;

		public void LoadCharacter(BaseCharacter character) {
			this._character = character;
			this._name = this.transform.GetChild(0).gameObject.GetComponent<Text>();
			this._leftPanel = this.transform.GetChild(1);
			this._centerPanel = this.transform.GetChild(2);
			this._rightPanel = this.transform.GetChild(3);
			this._close = this.transform.GetChild(4).gameObject.GetComponent<Button>();

			this._name.text = this._character.name;

			this._vitals = new List<CharacterDataVitalController>();
			foreach (Vital vital in this._character.stats.vitals.Values) {
				GameObject go = GameObject.Instantiate(Res.prefabs["CharacterVital"], this._leftPanel);
				CharacterDataVitalController vc =  go.AddComponent<CharacterDataVitalController>();
				vc.LoadVital(vital, Defs.namedColorPallets["cols_vitals"].colors[vital.name]);
				this._vitals.Add(vc);
			}

			this._stats = new List<CharacterDataStatsController>();
			foreach (Stat stat in this._character.stats.stats.Values) {
				GameObject go = GameObject.Instantiate(Res.prefabs["CharacterStats"], this._centerPanel);
				CharacterDataStatsController sc = go.AddComponent<CharacterDataStatsController>();
				sc.LoadStat(stat);
			}
			this._attrs = new List<CharacterDataStatsController>();
			foreach (Attribute attr in this._character.stats.attributes.Values) {
				if (attr.value == 0) {
					continue;
				}
				GameObject go = GameObject.Instantiate(Res.prefabs["CharacterStats"], this._rightPanel);
				CharacterDataStatsController sc = go.AddComponent<CharacterDataStatsController>();
				sc.LoadStat(attr);
			}
			
		}
	}
}
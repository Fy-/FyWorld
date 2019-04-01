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
using Fy.Characters;
using Fy.Helpers;

namespace Fy.Controllers {
	public class TileSelectorController : MonoBehaviour {
		private Dictionary<BaseCharacter, CharacterDataController> _characterPanels;
		private Vector2Int currentTilePosition;
		private int currentIndex = 0;

		private void Awake() {
			this._characterPanels = new Dictionary<BaseCharacter, CharacterDataController>();
		}

		private void Update() {
			if (Loki.manager.ready) {
				int i = 0;
				if (Input.GetMouseButton(0)) { /// Check if we're in building mode;
					foreach (BaseCharacter character in Loki.map[Loki.cameraController.tileMapMousePosition].characters) {
						if (i == this.currentIndex) {
							this.DisplayCharacterData(Loki.cameraController.tileMapMousePosition, character);
						}
						i++;
					}
				}
			}
		}

		private void DisplayCharacterData(Vector2Int position, BaseCharacter character) {
			if (this.currentTilePosition == position) {
				this.currentIndex++;
			} else {
				this.currentIndex = 0;
			}
			
			if (this._characterPanels.ContainsKey(character)) {
				Debug.Log("Just show it");
			} else {
				GameObject go = GameObject.Instantiate(Res.prefabs["CharacterPanel"], this.transform);
				CharacterDataController cdc = go.AddComponent<CharacterDataController>();
				cdc.LoadCharacter(character);
				this._characterPanels.Add(character, cdc);
			}
		}
	}
}
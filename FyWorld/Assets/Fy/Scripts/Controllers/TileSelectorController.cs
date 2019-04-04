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
using Fy.UI;

namespace Fy.Controllers {
	public class TileSelectorController : MonoBehaviour {
		private Vector2Int currentTilePosition;
		private int currentIndex = 0;
		private Dictionary<BaseCharacter, WindowCharacter> _characterWindows;
		
		private void Awake() {
			this._characterWindows = new Dictionary<BaseCharacter, WindowCharacter>();
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
			
			if (this._characterWindows.ContainsKey(character)) {
				Debug.Log("Just show it");
			} else {
				this._characterWindows.Add(character, new WindowCharacter(character));
			}
		}
	}
}
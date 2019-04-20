/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fy.Definitions;
using Fy.World;
using Fy.Entities;
using Fy.Helpers;
using Fy.Controllers;
using Fy.Characters;

namespace Fy {
	// Manage the game. (yep).
	public class GameManager : MonoBehaviour
	{
		/* Map */
		public CameraController cameraController;
		public StackableLabelController stackableLabelController;
		public Tick tick;
		public Map map;
		public bool DrawGizmosTiles = false;
		public bool DrawNoiseMap = false;
		public bool DrawBuckets = false;
		public bool DrawAStar = false;
		public bool DrawFertility = false;
		public bool DrawPaths = false;
		public bool ready { get { return this._ready; } }

		/* Are we ready ? */
		private bool _ready;

		/// Load defs
		void Awake() {
			this._ready = false;
			this.cameraController = this.GetComponent<CameraController>();
			this.stackableLabelController = this.GetComponentInChildren<StackableLabelController>();
			Loki.LoadStatics();
			Loki.NewGame(this);
		}

		/// Generating the map, spawning things.
		void Start() {
			this.tick = new Tick();
			this.map = new Map(275, 275);
			this.map.TempMapGen();
			this.map.BuildAllMeshes();
			Debug.Log(this.map);

			/// TEST STUFF
			for (int i = 0; i < 5; i++) {
				this.map.SpawnCharacter(new Animal(new Vector2Int(15,15), Defs.animals["chicken"]));
			}
			for (int i = 0; i < 5; i++) {
				this.map.SpawnCharacter(new Human(new Vector2Int(15,15), Defs.animals["human"]));
			}
//			Fy.Characters.AI.TargetList.GetRandomTargetInRange(new Vector2Int(10, 10));

			this.StartCoroutine(this.TickLoop());
			this._ready = true;
		}

		// Draw the regions
		void Update() {
			if (this._ready) {
				this.map.DrawTilables();
				this.map.DrawCharacters();
			}
		}

		void LateUpdate() {
			if (this._ready) {
				this.map.CheckAllMatrices();
			}
		}

		IEnumerator TickLoop() {
			for(;;) {
				yield return new WaitForSeconds(.01f/this.tick.speed);
				this.tick.DoTick();
			}
		}

		// WARNING WARNING : Clean this shit.
		void OnDrawGizmos() {
			if (this._ready && Settings.DEBUG) {
				if (this.DrawPaths) {
					foreach (BaseCharacter character in this.map.characters) {
						DebugRenderer.DrawCurrentPath(character.movement);
					}
				}
				if (this.DrawAStar) {
					DebugRenderer.DrawAStar();
				}
				if (this.DrawBuckets) {
					DebugRenderer.DrawBuckets();
				}
				if (this.DrawGizmosTiles) {
					DebugRenderer.DrawTiles();
				}
				if (this.DrawNoiseMap) {
					DebugRenderer.DrawNoiseMap();
				}
				if (this.DrawFertility) {
					DebugRenderer.DrawFertility();
				}
			}
		}
	}
}
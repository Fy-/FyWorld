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
using Fy.Entity;
using Fy.Helpers;
using Fy.Controllers;

namespace Fy {
	// Manage the game. (yep).
	public class GameManager : MonoBehaviour
	{
		/* Map */
		public CameraController cameraController;
		public Tick tick;
		public Map map;
		public bool DrawGizmosTiles = false;
		public bool DrawNoiseMap = false;
		public bool DrawPlants = false;
		public bool DrawBuckets = false;

		/* Are we ready ? */
		private bool _ready;

		/// Load defs
		void Awake() {
			this._ready = false;
			this.cameraController = this.GetComponent<CameraController>();
			Loki.LoadStatics();
			Loki.NewGame(this);
		}

		/// Generating the map, spawning things.
		void Start() {
			this.tick = new Tick();
			this.map = new Map(275, 275);
			this.map.TempMapGen();
			this.map.groundGrid.BuildStaticMeshes();
			//this.map.BuildAllRegionMeshes();
			Debug.Log(this.map);

			this.StartCoroutine(this.TickLoop());
			this._ready = true;
		}

		// Draw the regions
		void Update() {
			if (this._ready) {
			//	this.map.DrawRegions();
				this.map.groundGrid.DrawBuckets();
				this.map.plantGrid.DrawBuckets();
			}
		}

		IEnumerator TickLoop() {
			for(;;) {
				yield return new WaitForSeconds(.1f/this.tick.speed);
				this.tick.DoTick();
			}
		}

		/// Helpers (used for debug).
		void OnDrawGizmos() {
			if (this._ready) {
				if (this.DrawBuckets) {
					foreach (Vector2Int v in this.map.mapRect) {
						LayerGridBucket bucket = this.map.groundGrid.GetBucketAt(v);
						Gizmos.color = new Color(bucket.uid/(float)this.map.groundGrid.buckets.Length, 0, 0, .7f);
						Gizmos.DrawCube(
							new Vector3(v.x+.5f, v.y+.5f), 
							Vector3.one
						);
					}
				}
				if (this.DrawNoiseMap) {
					foreach (Vector2Int v in this.cameraController.viewRect) {
						float h = this.map.groundNoiseMap[v.x + v.y * this.map.size.x];
						Gizmos.color = new Color(h, h, h, .9f);
						Gizmos.DrawCube(
							new Vector3(v.x+.5f, v.y+.5f), 
							Vector3.one
						);
					}
				}

				if (this.DrawPlants) {
					/*
					foreach (Vector2Int v in this.cameraController.viewRect) {
						Tile tile = this.map[v];
						Gizmos.color = new Color(0, 1, 0, .4f);
						if (tile != null && tile.GetTilable(Layer.Tree) != null) {
							Gizmos.DrawCube(
								new Vector3(v.x+.5f, v.y+.5f), 
								Vector3.one
							);
						}
					}*/
				}

				if (this.DrawGizmosTiles) {
					foreach (Vector2Int v in this.cameraController.viewRect) {
						Gizmos.DrawWireCube(
							new Vector3(v.x+.5f, v.y+.5f), 
							Vector3.one
						);
					}
				}
			}
		}
	}
}
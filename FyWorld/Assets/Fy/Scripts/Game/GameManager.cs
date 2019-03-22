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
			this.map.BuildAllMeshes();
			Debug.Log(this.map);

			this.StartCoroutine(this.TickLoop());
			this._ready = true;
		}

		// Draw the regions
		void Update() {
			if (this._ready) {
				this.map.DrawTilables();
			}
		}

		void LateUpdate() {
			if (this._ready) {
				this.map.CheckAllMatrices();
			}
		}

		IEnumerator TickLoop() {
			for(;;) {
				yield return new WaitForSeconds(.1f/this.tick.speed);
				this.tick.DoTick();
			}
		}

		// WARNING WARNING : Clean this shit.
		void OnDrawGizmos() {
			if (this._ready) {
				/*

					foreach (MapRegion region in this.map.regions) {
						Gizmos.color = new Color(0, 0, 1, .5f);
						Gizmos.DrawCube(
							new Vector3(
								region.regionRect.max.x-(region.regionRect.width/2f),
								region.regionRect.max.y-(region.regionRect.height/2f)
							), 
							new Vector3(region.regionRect.width-.5f, region.regionRect.height-.5f, 1f)
						);
					}
				*/

				if (this.DrawBuckets) {
					Gizmos.color = new Color(0, 0, 1, .5f);
					foreach (LayerGridBucket bucket in this.map.grids[Layer.Ground].buckets) {
						Gizmos.DrawCube(
							new Vector3(
								bucket.rect.max.x-(bucket.rect.width/2f),
								bucket.rect.max.y-(bucket.rect.height/2f)
							), 
							new Vector3(bucket.rect.width-.5f, bucket.rect.height-.5f, 1f)
						);
					}
				}
				/*
				if (this.DrawNoiseMap) {
					foreach (Vector2Int v in this.cameraController.viewRect) {
						float h = this.map.groundNoiseMap[v.x + v.y * this.map.size.x];
						Gizmos.color = new Color(h, h, h, .9f);
						Gizmos.DrawCube(
							new Vector3(v.x+.5f, v.y+.5f), 
							Vector3.one
						);
					}
				}*/

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
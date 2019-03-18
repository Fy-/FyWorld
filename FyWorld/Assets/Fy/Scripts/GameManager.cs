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
		public Map map;
		public bool DrawGizmosTiles = false;
		public bool DrawGizmosRegions = false;
		public bool DrawNoiseMap = false;

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
			this.map = new Map(275, 275);
			this.map.TempMapGen();
			this.map.BuildAllRegionMeshes();
			Debug.Log(this.map);
			this._ready = true;
		}

		// Draw the regions
		void Update() {
			if (this._ready) {
				this.map.DrawRegions();
			}
		}

		/// Helpers (used for debug).
		void OnDrawGizmos() {
			if (this._ready) {
				if (this.DrawNoiseMap) {
					foreach (Vector2Int v in this.map.mapRect) {
						float h = this.map.groundNoiseMap[v.x + v.y * this.map.size.x];
						Gizmos.color = new Color(h, h, h, .9f);
						Gizmos.DrawCube(
							new Vector3(v.x+.5f, v.y+.5f), 
							Vector3.one
						);
					}
				}

				if (this.DrawGizmosTiles) {
					foreach (Tile t in this.map) {
						Ground g = (Ground)t.GetTilable(Layer.Ground);
						if (g != null) {
							Gizmos.DrawWireCube(
								new Vector3(g.position.x+.5f, g.position.y+.5f), 
								Vector3.one
							);
						}
					}
				}
				if (this.DrawGizmosRegions) {
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
				}
			}
		}
	}
}
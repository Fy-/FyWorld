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

namespace Fy {
	// Manage the game. (yep).
	public class GameManager : MonoBehaviour
	{
		/* Map */
		public Map map;
		public bool DrawGizmosTiles = false;
		public bool DrawGizmosRegions = false;

		/* Are we ready ? */
		private bool _ready;

		/// Load defs
		void Awake() {
			this._ready = false;
			Res.Load(); // Load all our resources;
			Defs.LoadGroundsFromCode(); // Loading our ground definitions;
			Defs.LoadPlantsFromCode();; // Loading our plants definitions;
		}

		/// Generating the map, spawning things.
		void Start() {
			this.map = new Map(100, 100);
			this.map.TempMapGen();
			Debug.Log(this.map);
			foreach (MapRegion region in this.map.regions) {
				region.BuildMeshes();

			}

			this._ready = true;
		}

		// Draw the regions
		void Update() {
			if (this._ready) {
				foreach (MapRegion region in this.map.regions) {
					region.Draw();
				}
			}
		}

		/// Helpers (used for debug).
		void OnDrawGizmos() {
			if (this._ready) {
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
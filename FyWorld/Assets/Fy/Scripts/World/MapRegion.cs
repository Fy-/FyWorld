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
using Fy.Helpers;
using Fy.Visuals;
using Fy.Definitions;

namespace Fy.World {
	// Represents a region on the map.
	public class MapRegion {
		/* Rectangle defining our region. */
		public RectI regionRect { get; protected set; }

		/* Reference to our map? */
		public Map map { get; protected set; }

		/* Identifier */
		public int id { get; protected set; }

		/* For some of our layers we need a region renderer */
		public Dictionary<Layer, RegionRenderer> renderers { get; protected set; }

		public MapRegion(int id, RectI rect, Map map) {
			this.regionRect = rect;
			this.id = id;
			this.map = map;
			this.renderers = new Dictionary<Layer, RegionRenderer>();
			this.AddRenderers();
		}

		// Draw all the meshes from our renderers.
		public void Draw() {
			foreach (RegionRenderer renderer in this.renderers.Values) {
				renderer.Draw();
			}
		}

		// Build all the meshes in all our renderers
		public void BuildMeshes() {
			foreach (RegionRenderer renderer in this.renderers.Values) {
				renderer.BuildMeshes();
			}
		}

		// Add renderes for some layers.
		private void AddRenderers() {
			this.renderers.Add(Layer.Ground, new RegionRenderer(this, Layer.Ground));
		}
	}
}
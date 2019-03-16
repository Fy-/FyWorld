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
using Fy.Entity;
using Fy.Definitions;

namespace Fy.World {
	// Tile
	public class Tile {
		/* Position in our map */
		public Vector2Int position { get; protected set; }

		/* Our map */
		public Map map { get; protected set; }

		/*
			(Need to change this, maybe)
			In a tile we have a container for each Layer in Layer.
			In each container, we have a Tilable object.
		*/
		private Dictionary<Layer, Tilable> _tilables;

		/// And a new tile is born.
		public Tile(Vector2Int position, Map map) {
			this.position = position;
			this.map = map;
			this._tilables = new Dictionary<Layer, Tilable>();
		}	

		/// All our tilables is this tile.
		public IEnumerable<Tilable> GetAllTilables() {
			foreach (Tilable tilable in this._tilables.Values) {
				yield return tilable;
			}
		}

		/// Add a tilable to a layer.
		public void AddTilable(Tilable tilable) {
			if (!this._tilables.ContainsKey(tilable.def.layer)) {
				this._tilables.Add(tilable.def.layer, tilable);
				return;
			}
			throw new System.Exception("[Tile.AddTilable] Trying to add a tilable on a taken position.");
		}

		/// Get a tilable on a layer.
		public Tilable GetTilable(Layer layer) {
			if (this._tilables.ContainsKey(layer)) {
				return this._tilables[layer];
			}
			return null;
		}

		/// Del a tilable from a layer.
	}
}
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
using Fy.Definitions;
using Fy.Entity;
using Fy.Helpers;

namespace Fy.World {
	public class Map {
		/* Temporary, we will make a settings class later */
		public const int REGION_SIZE = 25;

		/* Size of our map */
		public Vector2Int size { get; protected set; }

		/* Rect representing our map */
		public RectI mapRect;

		/* Public access to _regions */
		public MapRegion[] regions { get { return this._regions; } }

		/* All our tiles, this is just a 2D array flatten to 1D (https://bit.ly/2F0OiQa) */
		private Tile[] _tiles; 

		/* Regions in our map */
		private MapRegion[] _regions;

		/* Region for each position in our map */
		private Dictionary<int, int> _regionByPosition;

		/// Let's create a world, that's not ostentatious at all.
		public Map(int width, int height) {
			this.size = new Vector2Int(width, height);
			this._tiles = new Tile[width*height];
			this.mapRect = new RectI(new Vector2Int(0, 0), width, height);

			foreach (Vector2Int v in this.mapRect) {
				this._tiles[v.x + v.y * this.size.x] = new Tile(v, this);
			}

			this.SetRegions();
		}

		/// SetRegions: Define all regions in our map.
		public void SetRegions() {
			int _regionLength = (
				Mathf.CeilToInt(this.size.x/Map.REGION_SIZE) *
				Mathf.CeilToInt(this.size.y/Map.REGION_SIZE)
			);
			this._regions = new MapRegion[_regionLength];
			this._regionByPosition = new Dictionary<int, int>();

			int i = 0;
			for (int x = 0; x < this.size.x; x += Map.REGION_SIZE) {
				for (int y = 0; y < this.size.y; y += Map.REGION_SIZE) {
					RectI sectionRect = new RectI(
						new Vector2Int(x, y), Map.REGION_SIZE, Map.REGION_SIZE
					);
					sectionRect.Clip(this.mapRect);
					this._regions[i] = new MapRegion(i, sectionRect, this);
					
					foreach (Vector2Int v in sectionRect) {
						this._regionByPosition.Add(v.x + v.y * this.size.x, i);
					}
					i++;
				}
			}
		}

		/// Temporary method to add a Ground of definition "dirt" to all our tiles.
		public void TempMapGen() {
			foreach (Tile tile in this) {
				if (
					tile.position.x == 0 || tile.position.y == 0 || 
					tile.position.x == this.size.x-1 || tile.position.y == this.size.y-1
				) {
					tile.AddTilable(
						new Ground(tile.position, Defs.grounds["water"])
					);
				} else {
					tile.AddTilable(
						new Ground(tile.position, Defs.grounds["dirt"])
					);
				}
			}
		}

		/// Getter: map[x, y], get the tile at x,y
		public Tile this[int x, int y] {
			get {
				if (x >= 0 && y >= 0 && x < this.size.x && y < this.size.y) {
					return this._tiles[x + y * this.size.x];
				}
				return null;
			}
		}

		/// Getter: map[Vector2Int.zero], get the tile a v.
		public Tile this[Vector2Int v] {
			get {
				return this[v.x, v.y];
			}
		}

		/// Enumerator: foreach (Tile tile in map) {}
		public IEnumerator<Tile> GetEnumerator() {
			foreach (Vector2Int v in this.mapRect) {
				yield return this[v];
			}
		}

		/// Map string description
		public override string ToString() {
			return "Map(size="+this.size+")";
		}
	}
}
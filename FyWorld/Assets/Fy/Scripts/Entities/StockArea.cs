/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using System.Collections.Generic;
using Fy.Helpers;
using Fy.Definitions;

namespace Fy.Entities {
	public class StockArea : Area {
		public static List<StockArea> areas = new List<StockArea>();
		public TilableDef zoneConfig { get; protected set; }
		public HashSet<Stackable> stacks { get; protected set; }

		public StockArea(TilableDef zoneConfig) {
			this.stacks = new HashSet<Stackable>();
			this.zoneConfig = zoneConfig;
			this.color = new Color(1f, 0, 0, .4f);
			StockArea.areas.Add(this);
		}

		// First tile with space for item(def)*qty
		public Vector2Int FindFreeTile(TilableDef def, int qty) {
			return Vector2Int.zero;
		}

		// First empty tile in the area
		public Vector2Int FindFreeTile() {
			return Vector2Int.zero;
		}


		protected override void AddTilable(Vector2Int position) {
			// Add the field tialble, here we should check if we already have a field on this position.
/*			Field field = new Field(position, Defs.empty, this);
			Loki.map.Spawn(
				position,
				field
			);*/
			//this.fields.Add(field);
			Stackable stack = new Stackable(position, this);
			Loki.map.Spawn(position, stack);
			this.stacks.Add(stack);
			base.AddTilable(position);
			
		}

		protected override void DelTilable(Vector2Int position) {
			// Remove titlable,  here we should check if we already have a field on this position.
			base.DelTilable(position);
			// @TODO: Remove field from field hashset;
		}
	}
}
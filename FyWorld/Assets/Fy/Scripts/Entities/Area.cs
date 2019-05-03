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
	public abstract class Area {
		public HashSet<Vector2Int> positions { get; protected set; }
		public Color color { get; protected set; }

		public Area() {
			this.positions = new HashSet<Vector2Int>();
		}

		protected virtual void AddTilable(Vector2Int position) {
			this.positions.Add(position);
		}
		protected virtual void DelTilable(Vector2Int position) {
			this.positions.Remove(position);
		}

		public void Add(Vector2Int position) {
			this.AddTilable(position);
		}

		public void Del(Vector2Int position) {
			this.DelTilable(position);
		}

		public void Add(RectI rect) {
			foreach (Vector2Int position in rect) {
				this.AddTilable(position);
			}
		}

		public void Del(RectI rect) {
			foreach (Vector2Int position in rect) {
				this.positions.Remove(position);
			}
		}
	}

	public class GrowArea : Area {
		public static List<GrowArea> areas = new List<GrowArea>();
		public TilableDef plantDef { get; protected set; }
		//public HashSet<Field> fields { get; protected set; }

		public GrowArea(TilableDef plantDef) {
			//	this.fields = new HashsSet<Field>();
			this.plantDef = plantDef;
			this.color = new Color(0, 1f, 0, .2f);
			GrowArea.areas.Add(this);
		}

		protected override void AddTilable(Vector2Int position) {
			// Add the field tialble, here we should check if we already have a field on this position.
			Field field = new Field(position, Defs.empty, this);
			Loki.map.Spawn(
				position,
				field
			);
			//this.fields.Add(field);
			base.AddTilable(position);
			
		}

		protected override void DelTilable(Vector2Int position) {
			// Remove titlable,  here we should check if we already have a field on this position.
			base.DelTilable(position);
			// @TODO: Remove field from field hashset;
		}
	}
}
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
using UnityEngine.UI;
using Fy.Helpers;

namespace Fy.Definitions {
	public enum SelectorType {
		Tile,
		Area,
		AreaTile,
		Line, // Think about this
	}

	[System.Serializable]
	public class MenuOrderDef : Def {
		public string name;
		public string shortDesc;
		public TilableDef tilableDef;
		public Sprite sprite;
		public delegate void ActionDelegate(Vector2Int position);
		public delegate void ActionAreaDelegate(RectI position);
		public ActionDelegate action;
		public ActionAreaDelegate actionArea;
		public SelectorType selector;
		public GraphicDef graphics;
		public Layer layer;
		public KeyCode keyCode = KeyCode.Escape;
	}
}
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

namespace Fy.Definitions {
	/// Definition of all our entities.
	[System.Serializable]
	public class Def {
		/* Unique string identifier */
		public string uid { get; set; }

		public override int GetHashCode() {
			return this.uid.GetHashCode();
		}
	}

	/// Represent a grahpic in out game.
	[System.Serializable]
	public class GraphicDef : Def {
		/* Texture name in the Resources/Textures directory */
		public string textureName;

		/* Material name in the Resources/Materials directory */
		public string materialName = "tilables";

		/* Graphic size */
		public Vector2 size = Vector2.one;

		/* Graphic color */
		public Color color = Color.white;

		/* Is Instanced ? */
		public bool isInstanced = true;

		/* Draw Priority */
		public float drawPriority = 0f;

		/* Graphic pivot */
		public Vector2 pivot = Vector2.zero;
	}

	// Definition for a color palette 
	[System.Serializable]
	public class ColorPaletteDef : Def {
		public List<Color> colors = new List<Color>(15);

		public Color GetRandom() {
			return this.colors[Random.Range(0, this.colors.Count)];
		}
	}

	/// Definition for a tilable
	[System.Serializable]
	public class TilableDef : Def {
		// Layer
		public Layer layer;

		// Tilable type
		public TilableType type = TilableType.Undefined;

		// Grahpic data (for example size, texture, shader/material)
		public GraphicDef graphics;

		// Ground def
		public GroundDef groundDef;

		// Plant def
		public PlantDef plantDef;

		// Fertility of our tilable
		public float fertility = 0f;
	}

	/// Definition for a terrain/ground
	[System.Serializable]
	public class GroundDef : Def {
		public float maxHeight;
	}

	/// Definition for a plant
	[System.Serializable]
	public class PlantDef : TilableDef {
		public float probability = 0f;
		public float minFertility = 0f;
	}
}
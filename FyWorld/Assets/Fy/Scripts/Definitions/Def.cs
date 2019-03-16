/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
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
	}

	/// Definition for a tilable
	[System.Serializable]
	public abstract class TilableDef : Def {
		// Layer
		public Layer layer;

		// Grahpic data (for example size, texture, shader/material)
		public GraphicDef graphics;
	}

	/// Definition for a terrain/ground
	[System.Serializable]
	public class GroundDef : TilableDef {}

	/// Definition for a plant
	[System.Serializable]
	public class PlantDef : TilableDef {}
}
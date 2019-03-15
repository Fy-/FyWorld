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

namespace Fy.Helpers {
	// Resource manager
	public static class Res {
		public static Dictionary<string, Material> materials;
		public static Dictionary<string, Texture2D> textures;

		/// Load resources
		public static void Load() {
			/* Load all mats in Resources/Materials into our materials dictionary */
			Res.materials = new Dictionary<string, Material>();
			foreach (Material mat in Resources.LoadAll<Material>("Materials/")) {
				Res.materials.Add(mat.name, mat);
			}

			/* Load all textures in Resources/Textures into our textures dictionary */
			Res.textures = new Dictionary<string, Texture2D>();
			foreach (Texture2D text in Resources.LoadAll<Texture2D>("Textures/")) {
				Res.textures.Add(text.name, text);
			}
		}
	}
}
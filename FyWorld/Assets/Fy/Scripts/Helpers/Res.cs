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
		public static Dictionary<string, GameObject> prefabs;
		public static Dictionary<Color, Texture2D> unicolorTextures = new Dictionary<Color, Texture2D>();

		/// Load resources
		public static void Load() {
			/* Load all mats in Resources/Materials into our materials dictionary */
			Res.materials = new Dictionary<string, Material>();
			foreach (Material mat in Resources.LoadAll<Material>("Materials/")) {
				Res.materials.Add(mat.name, mat);
			}

			Res.prefabs = new Dictionary<string, GameObject>();
			foreach (GameObject prefab in Resources.LoadAll<GameObject>("Prefabs/")) {
				Res.prefabs.Add(prefab.name, prefab);
			}


			/* Load all textures in Resources/Textures into our textures dictionary */
			Res.textures = new Dictionary<string, Texture2D>();
			foreach (Texture2D text in Resources.LoadAll<Texture2D>("Textures/")) {
				Res.textures.Add(text.name, text);
			}
		}

		public static Texture2D TextureUnicolor(Color color) {
			if (Res.unicolorTextures.ContainsKey(color)) {
				return Res.unicolorTextures[color];
			}

			Texture2D text = new Texture2D(1,1);
			text.SetPixel(0,0,color);
			text.Apply();
			Res.unicolorTextures.Add(color, text);
			return text;	
		}
		
		public static Texture2D TextureResize(Texture2D source, int newWidth, int newHeight)
		{
		    source.filterMode = FilterMode.Trilinear;
		    RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight);
		    rt.filterMode = FilterMode.Trilinear;
		    RenderTexture.active = rt;
		    Graphics.Blit(source, rt);
		    Texture2D nTex = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);
		    nTex.ReadPixels(new Rect(0, 0, newWidth, newWidth), 0,0);
		    nTex.Apply();
		    RenderTexture.active = null;
		    return nTex;
		}
	}
}
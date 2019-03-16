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
using Fy.Helpers;

namespace Fy.Visuals {
	public class GraphicInstance {
		/* All graphics instances in our game */
		public static Dictionary<int, GraphicInstance> instances = new Dictionary<int, GraphicInstance>();

		/* Unique identifier */ 
		public int uid { get; protected set; }

		public Material material { get; protected set; }
		public Texture2D texture { get; protected set; }
		public Color color { get; protected set; }
		public GraphicDef def { get; protected set; }

		public GraphicInstance(int uid, GraphicDef def, Color color = default(Color), Texture2D texture = null) {
			this.def = def;
			this.uid = uid;
			this.material = new Material(Res.materials[def.materialName]);
			this.material.mainTexture = (texture == null) ? Res.textures[def.textureName] : texture;

			if (color != default(Color)) {
				this.SetColor(color);
			}
		}

		private void SetColor(Color color) {
			this.color = color;
			this.material.SetColor("_Color", this.color);
		}

		/// Get a new graphic instance (or an existing one)
		public static GraphicInstance GetNew(GraphicDef def, Color color = default(Color), Texture2D texture = null) {
			int id = GraphicInstance.GetUID(def, color, texture);
			if (GraphicInstance.instances.ContainsKey(id)) {
				return GraphicInstance.instances[id];
			}
			GraphicInstance.instances.Add(id, new GraphicInstance(id, def, color, texture));
			return GraphicInstance.instances[id];
		}

		public override int GetHashCode() {
			return this.uid;
		}

		// Unique id generator for GraphicInstance
		public static int GetUID(GraphicDef def, Color color, Texture2D texture) {
			int textureHash = (texture == null) ? def.textureName.GetHashCode() : texture.GetHashCode();
			int colorHash = (color == default(Color)) ? def.color.GetHashCode() : color.GetHashCode();

			return def.materialName.GetHashCode() + textureHash + colorHash;
		}
	}
}
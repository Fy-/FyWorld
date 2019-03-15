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

		public GraphicInstance(int uid, GraphicDef def) {
			this.def = def;
			this.uid = uid;
			this.material = new Material(Res.materials[def.materialName]);
			this.material.mainTexture = Res.textures[def.textureName];
		}

		/// Get a new graphic instance (or an existing one)
		public static GraphicInstance GetNew(GraphicDef def) {
			int id = GraphicInstance.GetUID(def);
			if (GraphicInstance.instances.ContainsKey(id)) {
				return GraphicInstance.instances[id];
			}
			GraphicInstance.instances.Add(id, new GraphicInstance(id, def));
			return GraphicInstance.instances[id];
		}

		public override int GetHashCode() {
			return this.uid;
		}

		// Unique id generator for GraphicInstance
		public static int GetUID(GraphicDef def) {
			return def.materialName.GetHashCode() + def.textureName.GetHashCode() + def.color.GetHashCode();
		}
	}
}
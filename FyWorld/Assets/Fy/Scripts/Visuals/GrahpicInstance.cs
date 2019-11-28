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
	// Represent graphics for a tilable in our game (or part of it)
	public class GraphicInstance {
		/* All graphics instances in our game */
		public static Dictionary<int, GraphicInstance> instances = new Dictionary<int, GraphicInstance>();

		/* Unique identifier */ 
		public int uid { get; protected set; }

		public Material material { get; protected set; }
		public Texture2D texture { get; protected set; }
		public Color color { get; protected set; }
		public GraphicDef def { get; protected set; }
		public float priority { get; protected set; }
		public Mesh mesh { get; protected set; }

		/// Create a new graphic instance
		public GraphicInstance(
			int uid, 
			GraphicDef def, 
			Mesh mesh,
			Color color = default(Color), 
			Texture2D texture = null,
			float drawPriority = -42f
		) {
			this.mesh = mesh;
			this.def = def;
			this.uid = uid;
			this.priority = drawPriority / -100f;
			this.material = new Material(Res.materials[def.materialName]);
			this.material.mainTexture = texture;
			this.texture = texture;

			if (color != default(Color)) {
				this.SetColor(color);
			}
		}

		/// Set the variable _Color of the material/shader
		private void SetColor(Color color) {
			this.color = color;
			this.material.SetColor("_Color", this.color);
		}

		/// Get a new graphic instance (or an existing one)
		public static GraphicInstance GetNew(
			GraphicDef def, 
			Color color = default(Color), 
			Texture2D texture = null,
			float drawPriority = -42f,
			Mesh mesh = null
		) {
			Mesh _mesh = (mesh == null) ? MeshPool.GetPlaneMesh(def.size) : mesh;
			Color _color = (color == default(Color)) ? def.color : color;
			Texture2D _texture = (texture == null) ? Res.textures[def.textureName] : texture;
			float _priority = (drawPriority == -42f) ? def.drawPriority : drawPriority;

			int id = GraphicInstance.GetUID(def, _color, _texture, _priority, _mesh);
			if (GraphicInstance.instances.ContainsKey(id)) {
				return GraphicInstance.instances[id];
			}
			GraphicInstance.instances.Add(id, new GraphicInstance(id, def, _mesh, _color, _texture, _priority));
			return GraphicInstance.instances[id];
		}

		public override int GetHashCode() {
			return this.uid;
		}

		/// Unique id generator for GraphicInstance
		public static int GetUID(
			GraphicDef def, 
			Color color, 
			Texture2D texture,
			float drawPriority,
			Mesh mesh
		) {
			return def.materialName.GetHashCode() + texture.GetHashCode() + color.GetHashCode() + drawPriority.GetHashCode() + mesh.GetHashCode();
		}

		public override string ToString() {
			return "GraphicInstance(gdef="+this.def.ToString()+", uid="+this.uid+", priority="+this.priority+", mat="+this.material.ToString()+", texture="+this.texture.ToString()+", mesh="+this.mesh.ToString()+")";
		}
	}
}
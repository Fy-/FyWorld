/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using Fy.Definitions;
using Fy.Characters.AI;
using Fy.Visuals;
using Fy.Helpers;

namespace Fy.Characters {
	// Handle the skin for humans
	[System.Serializable]
	public struct HumanSkinData {
		public int hairID;
		public int eyeID;
		public int bodyID;
		public int headID;
		public int hairColorID;
		public int bodyColorID;

		/// Create a human skin, can be randomized.
		public HumanSkinData(bool randomize = false) {
			if (!randomize) {
				this.eyeID = 0;
				this.bodyID = 0;
				this.hairColorID = 0;
				this.bodyColorID = 0;
				this.hairID = 0;
				this.headID = 0;
			} else {
				this.bodyID = 0;
				this.hairColorID = Defs.colorPallets["human_hair"].GetRandomID();
				this.bodyColorID = Defs.colorPallets["human_body"].GetRandomID();
				this.eyeID = Random.Range(0, Settings.EYE_COUNT);
				this.hairID = Random.Range(0, Settings.HAIR_COUNT);
				this.headID = 0;
			}
		}
	}



	public class HumanSkin  {
		/* Skin Data */
		public HumanSkinData skinData { get; protected set; }

		/* Hair graphics */
		public GraphicInstance hairGraphic { get; protected set; }

		/* Head graphics */
		public GraphicInstance headGraphic { get; protected set; }

		/* Eyes graphics */
		public GraphicInstance eyesGraphic { get; protected set; }
		
		/* Body graphics */
		public GraphicInstance bodyGraphic { get; protected set; }

		/* Human object (skin owner) */
		public Human human { get; protected set; }

		/* Skin size */
		public Vector2 size { get ; protected set; }

		public HumanSkin(Human human) {
			this.human = human;
			this.size = new Vector2(1f, 1.5f);
			this.Randomize();
			this.UpdateLookingAt(Direction.S);
		}

		// Update the meshes to correct UVs depending on the property lookingAt (direction from human.movement)
		public void UpdateLookingAt(Direction dir) {
			this.bodyGraphic = GraphicInstance.GetNew(
				this.human.def.graphics,
				Defs.colorPallets["human_body"].colors[this.skinData.bodyColorID],
				Res.textures["Body_"+(this.skinData.bodyID).ToString()],
				0,
				MeshPool.GetHumanPlaneMesh(this.size, dir)
			);
			this.headGraphic = GraphicInstance.GetNew(
				this.human.def.graphics,
				Defs.colorPallets["human_body"].colors[this.skinData.bodyColorID],
				Res.textures["Head_"+(this.skinData.headID).ToString()],
				1,
				MeshPool.GetHumanPlaneMesh(this.size, dir)
			);
			this.eyesGraphic = GraphicInstance.GetNew(
				this.human.def.graphics,
				Color.white,
				Res.textures["Eye_"+(this.skinData.eyeID).ToString()],
				2,
				MeshPool.GetHumanPlaneMesh(this.size, dir)
			);
			this.hairGraphic = GraphicInstance.GetNew(
				this.human.def.graphics,
				Defs.colorPallets["human_hair"].colors[this.skinData.hairColorID],
				Res.textures["Hair_"+(this.skinData.hairID).ToString()],
				3,
				MeshPool.GetHumanPlaneMesh(this.size, dir)
			);	
		}

		/// Get the correct visual position. @TODO: maybe use y?
		private Vector3 GetVisualPosition(float priority) {
			return this.human.movement.visualPosition + new Vector3(0, 0, priority);
		}

		/// Draw all the meshes for the skin.
		public void UpdateDraw() {
			Graphics.DrawMesh(
				this.bodyGraphic.mesh,
				this.GetVisualPosition(this.bodyGraphic.priority),
				Quaternion.identity,
				this.bodyGraphic.material,
				0
			);
			Graphics.DrawMesh(
				this.headGraphic.mesh,
				this.GetVisualPosition(this.headGraphic.priority),
				Quaternion.identity,
				this.headGraphic.material,
				0
			);
			Graphics.DrawMesh(
				this.eyesGraphic.mesh,
				this.GetVisualPosition(this.eyesGraphic.priority),
				Quaternion.identity,
				this.eyesGraphic.material,
				0
			);
			Graphics.DrawMesh(
				this.hairGraphic.mesh,
				this.GetVisualPosition(this.hairGraphic.priority),
				Quaternion.identity,
				this.hairGraphic.material,
				0
			);
		}

		/// Randomize the skin.
		public void Randomize() {
			this.skinData = new HumanSkinData(true);
		}
	}
}
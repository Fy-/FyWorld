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
using Fy.Definitions;
using Fy.Characters.AI;
using Fy.Visuals;
using Fy.World;
using Fy.Helpers;
using Fy.Entities;

namespace Fy.Characters {
	// Human representation in our game
	public class Human : BaseCharacter {
		/* Contain HumanSkinData, all data about the human skin */
		public HumanSkin humanSkin { get; protected set; }

		/// Instantiate an human object
		public Human(Vector2Int position, AnimalDef def) : base(position, def) {
			this.humanSkin = new HumanSkin(this);
			this.movement.onChangeDirection += this.humanSkin.UpdateLookingAt;
		}

		/// Get the root BrainNode for the human AI
		public override BrainNodePriority GetBrainNode() {
			BrainNodePriority brainNode = new BrainNodePriority();

			//brainNode.AddSubnode(new N_Sleep(() => (this.stats.vitals[Vitals.Energy].ValueInfToPercent(.15f))));
			brainNode.AddSubnode(new N_Cut(WorldUtils.HasPlantsToCut));
			//brainNode.AddSubnode(new N_Grow(WorldUtils.FieldHasWork));
			brainNode.AddSubnode(new N_HaulRecipe(WorldUtils.HaulRecipeNeeded));
			brainNode.AddSubnode(new N_Idle());

			//rainNode.AddSubnode(new EatVegiesNode(() => (this.stats.vitals[Vitals.Hunger].ValueInfToPercent(.25f))));

			return brainNode;
		}

		/// Draw the human skin with HumanSkinData 
		public override void UpdateDraw() {
			this.humanSkin.UpdateDraw();
		}


	}
}
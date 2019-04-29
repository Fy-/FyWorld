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

			brainNode.AddSubnode(new SleepNode(() => (this.stats.vitals[Vitals.Energy].ValueInfToPercent(.15f))));
			//brainNode.AddSubnode(new EatVegiesNode(() => (this.stats.vitals[Vitals.Hunger].ValueInfToPercent(.25f))));
			brainNode.AddSubnode(new IdleNodeTaskData());
			return brainNode;
		}

		/// Draw the human skin with HumanSkinData 
		public override void UpdateDraw() {
			this.humanSkin.UpdateDraw();
		}
	}
}
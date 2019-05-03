/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using System;
using Fy.Definitions;
using Fy.World;
using Fy.Entities;

namespace Fy.Characters.AI {
	public class GrowNode : BrainNodeConditional {
		private class CutPlantAtPosition: BrainNode {
			public override TaskData GetTaskData() {
				// Closest plant to cut to the character position
				Tilable tilable = WorldUtils.FieldNextPlantToCut(this.character.position);
				if (tilable != null) {
					return new TaskData(
						Defs.tasks["task_cut"],
						new TargetList(new Target(tilable)),
						this.character
					);
				}

				return null;
			}
		}

		private class HarvestPlantAtPosition : BrainNode {
			public override TaskData GetTaskData() {
				// Closest plant to harvest to the character position
				return null;
			}
		}

		private class SowPlantAtPosition : BrainNode {
			public override TaskData GetTaskData() {
				Tilable tilable = WorldUtils.FieldNextTileToSow(this.character.position);
				if (tilable != null) {
					return new TaskData(
						Defs.tasks["task_sow"],
						new TargetList(new Target(tilable)),
						this.character
					);
				}

				return null;			
			}
		}

		private class DirtAtPosition : BrainNode {
			// Closest plant to dirt to the character position
			public override TaskData GetTaskData() {
				Tilable tilable = WorldUtils.FieldNextTileToDirt(this.character.position);
				if (tilable != null) {
					return new TaskData(
						Defs.tasks["task_dirt"],
						new TargetList(new Target(tilable)),
						this.character
					);
				}

				return null;			
			}
		}

		public GrowNode(Func<bool> condition) : base(condition) {
			BrainNode cut = new BrainNodeConditional(WorldUtils.FieldHasPlantsToCut);
			BrainNode harvest = new BrainNodeConditional(WorldUtils.FieldHastPlantsToHarverst);
			BrainNode sow = new BrainNodeConditional(WorldUtils.FieldHasPlantsToSow);
			BrainNode dirt = new BrainNodeConditional(WorldUtils.FieldHasDirtWork);

			cut.AddSubnode(new CutPlantAtPosition());
			harvest.AddSubnode(new HarvestPlantAtPosition());
			sow.AddSubnode(new SowPlantAtPosition());
			dirt.AddSubnode(new DirtAtPosition());

			this.subNodes.Add(cut);
			this.subNodes.Add(harvest);
			this.subNodes.Add(sow);
			this.subNodes.Add(dirt);
		}
	}
}
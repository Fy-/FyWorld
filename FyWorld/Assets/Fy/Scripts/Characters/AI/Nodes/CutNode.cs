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
	public class CutNode : BrainNodeConditional {
		private class CutPlantAtPosition: BrainNode {
			public override TaskData GetTaskData() {
				// Closest plant to cut to the character position
				Tilable tilable = WorldUtils.NextToCut(this.character.position);
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


		public CutNode(Func<bool> condition) : base(condition) {
			this.subNodes.Add(new CutPlantAtPosition());
		}
	}
}
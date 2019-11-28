/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using System;

namespace Fy.Characters.AI {
	public abstract class BrainNode {
		public List<BrainNode> subNodes = new List<BrainNode>();
		public BaseCharacter character { get; protected set; }

		public void SetCharacter(BaseCharacter character) {
			this.character = character;
			foreach (BrainNode node in this.subNodes) {
				node.SetCharacter(character);
			}
		}

		public virtual TaskData GetTaskData() {
			return null;
		}

		public BrainNode AddSubnode(BrainNode node) {
			this.subNodes.Add(node);
			return this;
		}
	}

	public class BrainNodePriority : BrainNode {
		public override TaskData GetTaskData() {
			foreach (BrainNode node in this.subNodes) {
				TaskData taskData = node.GetTaskData();
				if (taskData != null) {
					return taskData;
				}
			}
			return null;
		}
	}

	public class BrainNodeConditional : BrainNodePriority {
		public Func<bool> condition { get; protected set; }

		public BrainNodeConditional(Func<bool> condition) {
			this.condition = condition;
		}

		public override TaskData GetTaskData() {
			if (this.condition()) {
				return base.GetTaskData();
			}

			return null;
		}
	}
}
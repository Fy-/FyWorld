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

		public virtual Task GetTask() {
			return null;
		}

		public BrainNode AddSubnode(BrainNode node) {
			this.subNodes.Add(node);
			return this;
		}
	}

	public class BrainNodePriority : BrainNode {
		public override Task GetTask() {
			foreach (BrainNode node in this.subNodes) {
				Task taskData = node.GetTask();
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

		public override Task GetTask() {
			if (this.condition()) {
				return base.GetTask();
			}

			return null;
		}
	}
}
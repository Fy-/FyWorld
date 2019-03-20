/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;

namespace Fy.Entity {
	public delegate void TickDelegate();

	public class Tick {
		public int tick = 0;
		public int speed = 1;

		public Queue<TickDelegate> toAdd = new Queue<TickDelegate>();
		public Queue<TickDelegate> toDel = new Queue<TickDelegate>();
		public List<TickDelegate> updates = new List<TickDelegate>();

		public void DoTick() {
			this.tick++;

			while (this.toDel.Count != 0) {
				this.updates.Remove(this.toDel.Dequeue());
			}
			while (this.toAdd.Count != 0) {
				this.updates.Add(this.toAdd.Dequeue());
			}

			for (int i = 0; i < this.updates.Count; i++) {
				this.updates[i].Invoke();
			}
		}
	}
}
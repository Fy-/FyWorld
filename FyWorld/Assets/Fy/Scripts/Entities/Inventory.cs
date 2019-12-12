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
using UnityEngine;
using Fy.Definitions;

namespace Fy.Entities {
	// Inventory of tilables
	public class Inventory {
		/* List of tilable or a queue */
		public Queue<Item> list = new Queue<Item>();

		/* Max (max tilables in this inventory) */
		public int max {
			get {
				if (this._max != -1) {
					return this._max;
				}
				if (this._parent == null || this._parent.def == null) {
					return 0;
				}
				return this._parent.def.maxStack;
			}
		}

		/* Count (number of tilables in this inventory) */
		public int count { get { return this.list.Count; } }

		/* Free (space remaining in this inventory) */
		public int free { get { return this.max - this.count; } }

		/* Full (Is this inventory full ?) */
		public bool full { get { return this.free <= 0; } }

		public int lastDiff { get; protected set; }

		/* Force max */
		private int _max = 0;

		/* Parent tilable */
		private Stackable _parent;

		private TilableDef _def;

		public Action OnClear;
		public Action OnAdd;
		public Action<int> OnChangeCount;

		/* Definition */
		public TilableDef def { 
			get { 
				if (this._parent != null) {
					return this._parent.def;
				}
				return this._def;
			}
		}

		public Inventory(TilableDef def = null, int max = -1) {
			this._max = max;
			this._def = def;
		}

		public Inventory(Stackable parent, int count = 1, int max = -1) {
			this._max = max;
			this._parent = parent;
			this.Create(count);
		}

		public Inventory(int max = -1) {
			this._max = max;
		}

		/// Create x items.
		private void Create(int count) {
			for (int i = 0; i < count; i++) {
				if (this.full) {
					break;
				}
				this.list.Enqueue(
					new Item(this._parent.def)
				);
			}
			if (this.OnAdd != null) {
				this.OnAdd();
			}
		}

		public void InitInventory(TilableDef def) {
			this._def = def;
			this.list = new Queue<Item>();
		}

		/// Transfert items from this to an other
		public void TransfertTo(Inventory to, int qty) {
			if ((to.def == null || to.def == this.def) && !to.full) {
				if (to.def == null) {
					to.InitInventory(this.def);
				}
				int added = 0;
				while (this.list.Count > 0 && added < qty && !to.full) {
					to.list.Enqueue(this.list.Dequeue());
					added++;
				}

				if (this.list.Count == 0) {
					if (this.OnClear != null) {
						this.OnClear();
					}

					if (this._parent == null) {
						this._def = null;
					} else {
						this._parent.Destroy();
					}
				}

				if (added != 0) {
					if (this.OnChangeCount != null) {
						this.OnChangeCount(added*-1);
					}
					if (to.OnChangeCount != null) {
						to.OnChangeCount(added);
					}
				}
			}
		}
	}	
}
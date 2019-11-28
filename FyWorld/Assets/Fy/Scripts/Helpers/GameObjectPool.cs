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

namespace Fy.Helpers {
	public class GameObjectPool {
		public Queue<GameObject> queue = new Queue<GameObject>();

		public GameObject GetFromPool() {
			GameObject go = this.queue.Dequeue();
			go.SetActive(true);
			return go;
		}

		public void AddFromClone(GameObject go, Transform parent, int qty) {
			for (int i = 0; i < qty; i++) {
				this.AddToPool(GameObject.Instantiate(go, parent));
			}
			Object.Destroy(go);
		}

		public void AddToPool(GameObject go) {
			go.SetActive(false);
			this.queue.Enqueue(go);
		}
	}
}
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
using TMPro;
using Fy.Helpers;
using Fy.World;
using Fy.Entity;
using Fy.Definitions;

namespace Fy.Controllers {
	public class StackableLabelController : MonoBehaviour {
		public static GameObjectPool goPool = new GameObjectPool();

		public List<GameObject> actives = new List<GameObject>();

		private void Awake() {
			if (StackableLabelController.goPool.queue.Count == 0) {
				GameObject go = new GameObject("Label GameObject");
				go.transform.SetParent(this.transform);
				go.AddComponent<LabelComponent>();
				StackableLabelController.goPool.AddFromClone(go, this.transform, 100);
			}
		}

		public void AddLabel(Stackable stackable) {
			GameObject go = StackableLabelController.goPool.GetFromPool();
			go.GetComponent<LabelComponent>().SetStackable(stackable);
		}
	}
}
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
using Fy.Entities;
using Fy.Definitions;

namespace Fy.Controllers {
	[RequireComponent(typeof(TextMeshPro))]
	public class LabelComponent : MonoBehaviour {
		private Stackable _stackable;
		private RectTransform _rt;
		private TextMeshPro _tm;

		private void Awake() {
			this.transform.position = new Vector3(0, 0, LayerUtils.Height(Layer.Stackable));
			this._tm = this.gameObject.GetComponent<TextMeshPro>();
			this._rt = (RectTransform)this.transform;
			this._rt.offsetMin = Vector2.zero;
			this._rt.offsetMax = Vector2.one;
			this._tm.alignment = TextAlignmentOptions.MidlineGeoAligned;
			this._tm.fontStyle = FontStyles.Bold;
			this._tm.fontSize = 4;
		}

		public void SetStackable(Stackable stackable) {
			this._stackable = stackable;
			this._rt.offsetMin = new Vector2(this._stackable.position.x, this._stackable.position.y);
			this._rt.offsetMax = new Vector2(this._stackable.position.x+1, this._stackable.position.y+1);
			this._tm.text = this._stackable.inventory.count.ToString();
		}

		private void Update() {
			if (this._stackable != null) {
				this._tm.text = this._stackable.inventory.count.ToString();
			}
		}
	}
}
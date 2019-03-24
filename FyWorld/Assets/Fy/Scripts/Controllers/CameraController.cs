/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;
using Fy.Helpers;

namespace Fy.Controllers {
	public class CameraController : MonoBehaviour {
		public const float PIXEL_PER_UNIT = 128;
		public float zoomDesired { get; protected set; }
		public float zoomMin { get; protected set; }
		public float zoomMax { get; protected set; }

		public float zoom { 
			get {
				return (
					(Screen.height / (this.zoomDesired * CameraController.PIXEL_PER_UNIT)*.5f)
				);
			}
		}
		
		public float sensitivity { get; protected set; }
		public Vector3 mousePosition { get; protected set; }

		public RectI viewRect;
		private Vector3 _lastMousePosition;
		private Camera _camera;

		/// This will update the camera position and camera zoom.
		private void UpdateCamera() {
			if (Input.GetMouseButton(2)) {
				Vector3 diff = this._lastMousePosition - this.mousePosition;

				if (diff != Vector3.zero) {
					this._camera.transform.Translate(diff);
					this.UpdateViewRect();
				}
			}

			this.zoomDesired += this.zoomDesired * Input.GetAxis("Mouse ScrollWheel") * this.sensitivity;
			this.zoomDesired = Mathf.Clamp(this.zoomDesired, this.zoomMin, this.zoomMax);

			if (this.zoom != this._camera.orthographicSize) {
				this._camera.orthographicSize = this.zoom;
				this.UpdateViewRect();
			}
		}

		private void UpdateViewRect() {
			this.viewRect = new RectI(
				new Vector2Int(
					Mathf.FloorToInt(this._camera.transform.position.x - this._camera.orthographicSize * this._camera.aspect ),
					Mathf.FloorToInt(this._camera.transform.position.y - this._camera.orthographicSize )
				),
				new Vector2Int(
					Mathf.FloorToInt(this._camera.transform.position.x + this._camera.orthographicSize * this._camera.aspect),
					Mathf.FloorToInt(this._camera.transform.position.y + this._camera.orthographicSize)
				)
			);
		}

		/// Set up the camera, max and min zoom and the initial desired zoom.!
		private void Start() {
			this._camera = Camera.main;
			this.zoomMin = .1f;
			this.zoomMax = .6f;
			this.sensitivity = 2f;
			this.zoomDesired = .3f;
		}

		/// Sets the mouse position and call UpdateCamera
		private void Update() {
			this.mousePosition = this._camera.ScreenToWorldPoint(Input.mousePosition);
			this.UpdateCamera();
			this._lastMousePosition =  this._camera.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}
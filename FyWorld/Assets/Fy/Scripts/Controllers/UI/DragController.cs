using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fy.Definitions;
using Fy.Helpers;

namespace Fy.UI {
	public class DragController : MonoBehaviour {
		public MenuController menuController;

		// Are we dragging ?
		public bool isDragging;

		// Origin of the drag
		public Vector2 origin;

		// Selection Rect
		public Rect currentSelection;
		public RectI currentSelectionOnMap;

		// Mouse cooordinates (in game)
		public Vector2 currentMousePosition;

		// Current order
		public MenuOrderDef currentOrder { get { return this.menuController.currentOrder; } }


		public void GetScreenRect(Vector2 origin, Vector2 mousePosition) {
			origin.y = Screen.height - origin.y;
			mousePosition.y = Screen.height - mousePosition.y;

			Vector2 topLeft = Vector2.Min(origin, mousePosition);
			Vector2 bottomRight = Vector2.Max(origin, mousePosition);
			this.currentSelection = Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
		}

		public void DrawScreenRect(Rect rect, Color color) {
			GUI.DrawTexture(rect, Res.TextureUnicolor(color));
		}

		public void DrawScreenRectBorder(Rect rect, Color color, float thickness) {
			this.DrawScreenRect(
				new Rect(
					rect.xMin, rect.yMin, rect.width, thickness
				),
				color
			);
			this.DrawScreenRect(
				new Rect(
					rect.xMin, rect.yMin, thickness, rect.height
				),
				color
			);
			this.DrawScreenRect(
				new Rect(
					rect.xMax - thickness, rect.yMin, thickness, rect.height
				),
				color
			);
			this.DrawScreenRect(
				new Rect(
					rect.xMin, rect.yMax - thickness, rect.width, thickness
				),
				color
			);
		}

		public void Start() {
			this.menuController = this.GetComponent<MenuController>();
			this.isDragging = false;

		}

		public void Reset() {
			this.origin = Vector2.zero;
			this.isDragging = false;
		}

		public void Update() {
			this.BeginSelection();
			this.UpdateSelection();
		}
		public void OnGUI() {
			this.UpdateDrawRect();
		}

		public void BeginSelection() {
			if (this.currentOrder == null) {
				return;
			}
			if (
				this.isDragging == false && 
				Input.GetMouseButtonDown(0) &&
				this.currentOrder.selector != SelectorType.Tile &&
				!EventSystem.current.IsPointerOverGameObject()
			) {
				this.origin = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				this.isDragging = true;
			}
		}

		public void UpdateSelection() {
			if (!this.isDragging) {
				return;
			}
			Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			this.GetScreenRect(this.origin, mousePosition);
			this.GetMapRect(this.origin, mousePosition);

			if (
				Input.GetMouseButtonUp(0) &&
				this.currentOrder.selector != SelectorType.Tile &&
				!EventSystem.current.IsPointerOverGameObject()
			) {
				
				// check orders and do stuff
				this.CheckOrders();
				
			}
		}

		public void UpdateDrawRect() {
			if (this.currentOrder == null || !this.isDragging) {
				return;
			}

			if (this.currentOrder.selector == SelectorType.AreaTile) {
				this.DrawScreenRectBorder(this.currentSelection, new Color(0, 1, 0, .5f), 3f);
			}
		}

		public void GetMapRect(Vector2 start, Vector2 end) {
			Vector2 startInGame = Camera.main.ScreenToWorldPoint(start);
			Vector2 endInGame = Camera.main.ScreenToWorldPoint(end);

			if (endInGame.x < startInGame.x) {
				float tmp = endInGame.x;
				endInGame.x = startInGame.x;
				startInGame.x = tmp;
			}
			if (endInGame.y < startInGame.y) {
				float tmp = endInGame.y;
				endInGame.y = startInGame.y;
				startInGame.y = tmp;
			}

			this.currentSelectionOnMap = new RectI(
				new Vector2Int(Mathf.FloorToInt(startInGame.x), Mathf.FloorToInt(startInGame.y)),
				new Vector2Int(Mathf.FloorToInt(endInGame.x), Mathf.FloorToInt(endInGame.y))
			);
		}


		public void CheckOrders() {
			if (this.currentOrder == null) {
				return;
			}
			if (this.currentOrder.selector == SelectorType.AreaTile) {
				this.currentOrder.actionArea(this.currentSelectionOnMap);
			}

			this.Reset();
		}
	}
}
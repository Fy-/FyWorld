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
using UnityEngine.UI;
using Fy.Helpers;

namespace Fy.UI {
	public abstract class Window {
		public static List<Window> windows = new List<Window>();

		/* Title */
		public string title { get; protected set; }

		public string titleTab { 
			get {
				if (!this._hasTabs) {
					return this.title;
				}
				return this.tabs[this.activeTab] + " : " + this.title;
			}
		}

		/* Draggable */
		public bool draggable { get; protected set; }

		/* Resizeable */
		public bool resizeable { get; protected set; }

		/* CloseButton */
		public bool closeButton { get; protected set; } // Maybe an enum with multiple close button types ("x", text button, no button, or other stuff?)

		/* InitialSize */
		public Vector2 initialSize { get; protected set; }

		/* InitialPosition */
		public Vector2 initialPosition { get; protected set; }

		/* size */
		public Vector2 size { get; protected set; }

		/* position */
		public Vector2 position { get; protected set; }

		/* Padding */
		public RectOffset padding { get; protected set; }

		/* Rect */
		public Rect rect { get; protected set; }

		/* Content Rect */
		public Rect contentRect { get; protected set; }

		/* Tabs */
		public List<string> tabs { get; protected set; }

		/* Active tab */
		public int activeTab { get; protected set; }

		/* Header Size */
		public float headerSize { get; protected set; }

	
		public WindowVerticalGrid vGrid { get; protected set; }
		/* False if the window has no title */
		protected bool _hasTitle;
		protected bool _hasTabs = false;
		protected bool _show = true;
	

		private int _id;

		/// Constructor
		public Window() {
			Window.windows.Add(this);
			this._id = Window.windows.Count;

			this._hasTitle = (this.title == "") ? false : true;
			this.closeButton = true;
			this.draggable = false;
			this.resizeable = false;
			this._show = true;
			this.tabs = new List<string>();
			this.padding = new RectOffset(20, 20, 10, 10);
			this.initialSize = new Vector2(400f, 400f);
			this.rect = this.GetRectAtCenter();
			this.size = this.initialSize;
		}

		public void Show() {
			this._show = true;
		}

		public void Hide() {
			this._show = false;
		}

		protected void AddTab(string name) {
			if (this._hasTabs == false) {
				this._hasTabs = true;
				this.activeTab = 0;
			}
			this.tabs.Add(name);
		}

		public void SetActiveTab(int id) {
			this.activeTab = id;
		}

		protected void SetTitle(string title) {
			this._hasTitle = true;
			this.title = title;
		}

		/// Get the position at the center of the screen
		public Rect GetRectAtCenter() {
			return new Rect(
				(Screen.width - this.initialSize.x) / 2f,
				(Screen.height - this.initialSize.y) / 2f,
				this.initialSize.x,
				this.initialSize.y
			);
		}

		public void UpdateHeight (float height) {
			this.initialSize = new Vector2(this.initialSize.x, height);
			this.rect = this.GetRectAtCenter();
		}

		public virtual void Header() {
			if (this._hasTabs) {
				this.vGrid.Tabs(this.tabs, this.activeTab);
			}
		}

		/// Content()
		public abstract void Content();

		/// OnGUI()
		public virtual void DoMyWindow(int windowID) {
			
			this.vGrid = new WindowVerticalGrid();
			this.vGrid.Begin(this.rect.size, this);
			this.Header();
			this.Content();
			this.vGrid.End();
		}

		public void OnGUI() {
			if (!this._show) {
				return;
			}
			GUI.skin = Res.defaultGUI;
			this.rect = GUI.Window(this._id, this.rect, this.DoMyWindow, this.titleTab);
		}
	}
}
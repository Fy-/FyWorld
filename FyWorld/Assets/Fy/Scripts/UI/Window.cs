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

		/* Tabs */
		public Dictionary<int, string> tabs { get; protected set; }

		/* Active tab */
		public int activeTab { get; protected set; }

		/* Header Size */
		public float headerSize { get; protected set; }

		/* False if the window has no title */
		protected bool _hasTitle;
		protected bool _hasTabs = false;

		private int _id;

		private GUIStyle _guiStyle;


		/* Think about the layer */

		/// Constructor
		public Window() {
			Window.windows.Add(this);
			this._id = Window.windows.Count;

			this._hasTitle = (this.title == "") ? false : true;
			this.closeButton = true;
			this.draggable = false;
			this.resizeable = false;
			this.tabs = new Dictionary<int, string>();
			this.padding = new RectOffset(20, 20, 10, 10);
			this.initialSize = new Vector2(400f, 400f);
			this.rect = this.GetRectAtCenter();
			this.size = this.initialSize;
			this._guiStyle = new GUIStyle();

			//this.position = this.initialPosition;

			// set the rect
			// set size and position
		}

		protected void AddTab(string name) {
			if (this._hasTabs == false) {
				this._hasTabs = true;
				this.activeTab = 0;
			}
			this.tabs.Add(this.tabs.Count+1, name);
		}

		public void SetActiveTab(int id) {
			this.activeTab = id;
		}

		protected void SetTitle(string title) {
			this._hasTitle = true;
			this.title = title;
		}

		/// Get the position at the center of the screen
		private Rect GetRectAtCenter() {
			return new Rect(
				(Screen.width - this.initialSize.x) / 2f,
				(Screen.height - this.initialSize.y) / 2f,
				this.initialSize.x,
				this.initialSize.y
			);
		}

		public virtual void Header() {
			this.headerSize = (this._hasTabs == true) ? 17 : 0;
			WindowComponents.WindowBackground(new Rect(0, this.headerSize, this.rect.width, this.rect.height-40));

			if (this._hasTabs) {
				WindowComponents.WindowTabs(
					new Rect(this.padding.left, 0, this.rect.width-this.padding.horizontal, 30),
					new List<string>(this.tabs.Values).ToArray(),
					this.activeTab,
					this
				);
			}
			if (this._hasTitle) { 
				
				WindowComponents.WindowTitle(
					new Rect(
						this.padding.left, 
						this.padding.top + this.headerSize, 
						this.rect.width-this.padding.horizontal,
						40), 
					this.title
				);
				/*if (WindowComponents.TextButton(Rect.zero.ReSize(20, 20).FromTopRight(this.rect, this.padding.right, this.padding.top), "x")) {
					Debug.Log("pressed");
				}*/
				this.headerSize += 40;
			}

			
		}

		/// Content()
		public abstract void Content();

		/// OnGUI()
		public virtual void DoMyWindow(int windowID) {
			
			
			this.Header();
			this.Content();
		}

		public void OnGUI() {
			this.rect = GUI.Window(this._id, this.rect, this.DoMyWindow, "", WindowComponents.emptyStyle);
		}

		
		/// Close()
		/// OnOpen
		/// OnClose
	}
}
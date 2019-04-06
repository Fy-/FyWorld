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
	public class WindowVerticalGrid {
		public float width { get; protected set; }
		public float spacing { get; protected set; }
		public int currentCount { get; protected set; }
		public float currentHeight { get; protected set; }
		private Window _window;

		public void Begin(Vector2 size, Window window) {
			this.currentHeight = Res.defaultGUI.window.padding.top;
			this.currentCount = 0;
			this.spacing = 6f;
			this._window = window;
			this.width = size.x;
			GUI.BeginGroup(new Rect(0, 0, size.x, size.y));
		}

		public void H1(string text) {
			this.H(text, WindowComponents.titleStyle);
		}

		public void H2(string text) {
			this.H(text, WindowComponents.subTitleStyle);
		}

		public void H(string text, GUIStyle style) {
			Rect rect = this.GetRect(style.CalcHeight(new GUIContent(text), this.width));
			GUI.Label(rect, text, style);
			GUI.DrawTexture(
				new Rect(rect.x, rect.y+rect.height, rect.width, 1),
				Res.TextureUnicolor(new Color(1,1,1,.5f))
			);
		}

		public void Tabs(List<string> list, int active = 0) {
			Rect rect = new Rect(8f, 0, this.width-8f, 30f);
			Rect closeRect = new Rect(rect.width-31f, 0, 31f, 30f-4f);
			this.currentHeight += this.spacing*2;

			float[] tabsWidths = new float[list.Count];
			for (int i = 0; i < list.Count; i++) {
				tabsWidths[i] = WindowComponents.buttonLabelStyle.CalcSize(new GUIContent(list[i])).x + 40;
			}
			Rect[]  horizontalGrid = rect.HorizontalGrid(tabsWidths, 5f);

			for (int i = 0; i < list.Count; i++) {
				float height = (active == i) ? horizontalGrid[i+1].height : horizontalGrid[i+1].height - 4;
				Rect tabRect = horizontalGrid[i+1].Height(height);
				string style = (active == i) ? "tab" : "tab_off";
			
				if (GUI.Button(tabRect, list[i], Res.defaultGUI.GetStyle(style))) {
					this._window.SetActiveTab(i);
				}
			}
			
			GUI.color = new Color(240/255f, 200/255f, 200/255, 1f);
			if (GUI.Button(closeRect, new GUIContent("x"), Res.defaultGUI.GetStyle("tab_close"))) {
				this._window.Hide();
			}
			GUI.color = Color.white;
		}

		public void Paragraph(string text) {
			Rect rect = this.GetRect(WindowComponents.blockTextStyle.CalcHeight(new GUIContent(text), this.width));
			GUI.Label(rect, new GUIContent(text), WindowComponents.blockTextStyle);
		}

		public void Span(string text) {
			Rect rect = this.GetRect(WindowComponents.labelStyle.CalcHeight(new GUIContent(text), this.width));
			GUI.Label(rect, text, WindowComponents.labelStyle);
		}

		public Rect GetNewRect(float height) {
			return this.GetRect(height);
		}

		private Rect GetRect(float height) {
			Rect r = new Rect(Res.defaultGUI.window.padding.left, this.currentHeight, this.width-Res.defaultGUI.window.padding.horizontal, height);
			this.currentHeight += height+this.spacing;
			this.currentCount ++;
			return r;
		}

		public void End() {
			this.currentHeight += Res.defaultGUI.window.padding.bottom;
			this._window.UpdateHeight(this.currentHeight);
			//WindowComponents.DrawWindowBackground(new Rect(0, 17f, this.width, this.currentHeight));
			GUI.EndGroup();
		}
	}
}
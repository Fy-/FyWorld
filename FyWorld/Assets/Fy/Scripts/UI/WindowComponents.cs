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
	public static class WindowComponents {
		public static GUIStyle emptyStyle;
		public static GUIStyle labelStyle;
		public static GUIStyle titleStyle;
		public static GUIStyle buttonLabelStyle;
		public static GUIStyle tabLabelStyle;

		public static Dictionary<int, Dictionary<Rect, Rect>> subRectCache;

		public static void LoadComponents() {
			WindowComponents.subRectCache = new Dictionary<int, Dictionary<Rect, Rect>>();
			WindowComponents.emptyStyle = new GUIStyle();

			WindowComponents.labelStyle = new GUIStyle("label");
			WindowComponents.labelStyle.alignment = TextAnchor.MiddleLeft;
			WindowComponents.labelStyle.fontSize = 13;

			WindowComponents.titleStyle = new GUIStyle("label");
			WindowComponents.titleStyle.fontSize = 16;
			WindowComponents.titleStyle.fontStyle = FontStyle.Bold;
			WindowComponents.titleStyle.alignment = TextAnchor.MiddleCenter;

			WindowComponents.buttonLabelStyle = new GUIStyle("label");
			WindowComponents.buttonLabelStyle.fontStyle = FontStyle.Bold;
			WindowComponents.buttonLabelStyle.fontSize = 14;
			WindowComponents.buttonLabelStyle.padding = new RectOffset(2,2,2,2);
			WindowComponents.buttonLabelStyle.alignment = TextAnchor.MiddleCenter;

			WindowComponents.tabLabelStyle = new GUIStyle("label");
			WindowComponents.tabLabelStyle.fontStyle = FontStyle.Bold;
			WindowComponents.tabLabelStyle.fontSize = 14;
			WindowComponents.tabLabelStyle.padding = new RectOffset(2, 2, 5, 2);
			WindowComponents.tabLabelStyle.alignment = TextAnchor.MiddleCenter;
		}

		public static void WindowTitle(Rect rect, string text) {
			GUI.Label(rect, text, WindowComponents.titleStyle);
		}
		public static void ButtonLabel(Rect rect, string text) {
			GUI.Label(rect, text, WindowComponents.buttonLabelStyle);
		}
		public static void TabLabel(Rect rect, string text) {
			GUI.Label(rect, text, WindowComponents.tabLabelStyle);
		}
		public static void Label(Rect rect, string text) {
			GUI.Label(rect, text, WindowComponents.labelStyle);
		}
		public static void Label(string text) {
			GUILayout.Label(text);
		}

		/// Window Background
		public static void WindowBackground(Rect rect) {
			WindowComponents.DrawTextureWithBorder(rect, Res.textures["box_default"]);
		}

		/// TextButton()
		public static bool TextButton(Rect rect, string text) {
			bool over = false;
			if (rect.Contains(Event.current.mousePosition)) {
				WindowComponents.DrawTextureWithBorder(rect, Res.textures["button_default_over"] );
				over = true;
			} else {
				WindowComponents.DrawTextureWithBorder(rect, Res.textures["button_default"]);
			}
			
			WindowComponents.ButtonLabel(rect, text);
			return over && Input.GetMouseButton(0);			
		}

		/// FillableBar()
		public static void FillableBar(Rect rect, float percent, Color fillColor) {
			WindowComponents.DrawTextureWithBorder(rect, Res.textures["button_default"]);
			rect = rect.Contract(2f);
			GUI.DrawTexture(rect.Width(rect.width*percent), Res.TextureUnicolor(fillColor));
		}

		/// FillableBarWithLabelValue
		public static void FillableBarWithLabelValue(Rect rect, string name, float percent, Color fillColor) {
			Rect[] hGrid = rect.HorizontalGrid(new float[] {70, rect.width-140, 70}, 5);
			WindowComponents.Label(hGrid[1], name);
			WindowComponents.FillableBar(hGrid[2], percent, fillColor);
			WindowComponents.Label(hGrid[3], Mathf.Round(percent*100).ToString()+"%");
		}

		/// WindowTabs 
		public static void WindowTabs(Rect rect, string[] tabsNames, int active, Window window) {
			float[] tabsWidths = new float[tabsNames.Length];
			for (int i = 0; i < tabsNames.Length; i++) {
				tabsWidths[i] = WindowComponents.buttonLabelStyle.CalcSize(new GUIContent(tabsNames[i])).x + WindowComponents.buttonLabelStyle.padding.horizontal + 40;
			}
			Rect[] hGrid = rect.HorizontalGrid(tabsWidths);
			for (int i = 0; i < tabsNames.Length; i++) {
				Texture2D texture = (active == i) ? Res.textures["tab_active"] : Res.textures["tab_default"];
				float height = (active == i) ? hGrid[i+1].height : hGrid[i+1].height - 4;
				Rect currentRect = hGrid[i+1].Height(height);

				if (active != i && currentRect.Contains(Event.current.mousePosition)) {
					WindowComponents.DrawTextureWithBorder(currentRect, Res.textures["tab_over"]);
					if (GUI.Button(currentRect, string.Empty, WindowComponents.emptyStyle)) {
						window.SetActiveTab(i);
					}
				} else {
					WindowComponents.DrawTextureWithBorder(currentRect, texture);
				}

				WindowComponents.TabLabel(currentRect, tabsNames[i]);
			}
		}

		/// ImageButton()
		/// CloseButton()
		/// VitalBar()
		
		/// EditableStat()
		/// Stat()

		/// DrawLine()
		/// DrawBox()
		/// DrawTexture()
		/// DrawFillableBar()
		/// DrawBackground()
		/// DrawShadow()

		/// DrawTexture() 
		public static Dictionary<Rect, Rect> SplitTexture(Rect r, float textureWidth) {
			int k = (int)(r.width+r.height*666)+(int)(r.x*777+r.y)+(int)(textureWidth*333);
			if (WindowComponents.subRectCache.ContainsKey(k)) {
				return WindowComponents.subRectCache[k];
			}
			Dictionary<Rect, Rect> result = new Dictionary<Rect, Rect>();
			float q = textureWidth/4f;
			result.Add(
				new Rect(0, 0, q, q),
				new Rect(0, 0, .25f, .25f)
			);
			result.Add(
				new Rect(r.width-q, 0, q, q),
				new Rect(.75f, 0f, .25f, .25f)
			);
			result.Add(
				new Rect(0, r.height - q, q, q),
				new Rect(0f, 0.75f, 0.25f, 0.25f)
			);
			result.Add(
				new Rect(r.width-q, r.height-q, q, q),
				new Rect(.75f, .75f, .25f, .25f)
			);
			result.Add(
				new Rect(q, q, r.width - q*2f, r.height - q*2f),
				new Rect(.25f, .25f, .5f, .5f)
			);
			result.Add(
				new Rect(q, 0f, r.width - q*2f, q),
				new Rect(.25f, 0f, .5f, .25f)
			);
			result.Add(
				new Rect(q, r.height-q, r.width - q*2f, q),
				new Rect(.25f, .75f, .5f, .25f)
			);
			result.Add(
				new Rect(0, q, q, r.height - q*2f),
				new Rect(0, .25f, .25f, .5f)
			);
			result.Add(
				new Rect(r.width -q, q, q, r.height - q*2f),
				new Rect(0.75f, .25f, .25f, .5f)
			);
			WindowComponents.subRectCache.Add(k, result);
			return WindowComponents.subRectCache[k];
		}

		public static void DrawTextureWithBorder(Rect rect, Texture2D texture) {
			Rect r = rect.RoundToInt();
			GUI.BeginGroup(r);

			foreach (KeyValuePair<Rect, Rect> kv in WindowComponents.SplitTexture(r, texture.width)) {
				WindowComponents.DrawTexture(kv.Key, kv.Value, texture);
			}

			GUI.EndGroup();
		}
		public static void DrawTexture(Rect dr, Rect tr, Texture2D texture)  {
			tr.y = 1f - tr.y - tr.height;
			GUI.DrawTextureWithTexCoords(dr, texture, tr);
		}


		/// VerticalGrid
		
	}
}
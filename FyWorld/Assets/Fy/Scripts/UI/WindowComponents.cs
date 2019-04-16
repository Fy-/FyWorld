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
using Fy.Characters;

namespace Fy.UI {
	public static class WindowComponents {
		public static GUIStyle emptyStyle;
		public static GUIStyle labelStyle;
		public static GUIStyle titleStyle;
		public static GUIStyle subTitleStyle;

		public static GUIStyle buttonLabelStyle;
		public static GUIStyle vitalLabelStyle;
		public static GUIStyle blockTextStyle;
		public static GUIStyle windowStyle;

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

			WindowComponents.subTitleStyle = new GUIStyle("label");
			WindowComponents.subTitleStyle.fontSize = 14;
			WindowComponents.subTitleStyle.fontStyle = FontStyle.Bold;
			WindowComponents.subTitleStyle.alignment = TextAnchor.MiddleLeft;

			WindowComponents.buttonLabelStyle = new GUIStyle("label");
			WindowComponents.buttonLabelStyle.fontStyle = FontStyle.Bold;
			WindowComponents.buttonLabelStyle.fontSize = 14;
			WindowComponents.buttonLabelStyle.padding = new RectOffset(2,2,2,2);
			WindowComponents.buttonLabelStyle.alignment = TextAnchor.MiddleCenter;

			WindowComponents.vitalLabelStyle = new GUIStyle("label");
			WindowComponents.vitalLabelStyle.fontStyle = FontStyle.Bold;
			WindowComponents.vitalLabelStyle.fontSize = 14;
			WindowComponents.vitalLabelStyle.padding = new RectOffset(2, 2, 2, 2);
			WindowComponents.vitalLabelStyle.alignment = TextAnchor.MiddleCenter;

			WindowComponents.blockTextStyle = new GUIStyle("label");
			WindowComponents.blockTextStyle.fontSize = 12;
			WindowComponents.blockTextStyle.padding = new RectOffset(2, 2, 2, 2);
			WindowComponents.blockTextStyle.alignment = TextAnchor.UpperLeft;
			WindowComponents.blockTextStyle.wordWrap = true;
		}

		public static void Label(Rect rect, string text) {
			GUI.Label(rect, text, WindowComponents.labelStyle);
		}

		/// FillableBar()
		public static void FillableBar(Rect rect, float percent, Vital vital, Color fillColor) {
			WindowComponents.DrawTextureWithBorder(rect, Res.textures["fillable_border"]);
			rect = rect.Contract(2f);
			GUI.DrawTexture(rect.Width(rect.width*percent), Res.TextureUnicolor(fillColor));
			GUI.Label(rect, Mathf.Round(vital.currentValue).ToString()+" / "+vital.value.ToString(), WindowComponents.vitalLabelStyle);
		}

		/// FillableBarWithLabelValue
		public static void FillableBarWithLabelValue(Rect rect, string name, Vital vital, Color fillColor) {
			float percent = Utils.Normalize(0, vital.value, vital.currentValue);
			Rect[] hGrid = rect.HorizontalGrid(new float[] {70, rect.width-140, 70}, 5);
			WindowComponents.Label(hGrid[1], name);
			WindowComponents.FillableBar(hGrid[2], percent, vital, fillColor);
			WindowComponents.Label(hGrid[3], Mathf.Round(percent*100).ToString()+"%");
		}

		/// SimpleStat
		public static void SimpleStat(Rect rect, string text, float value, float baseValue=-1f) {
			Rect[] hGrid = rect.HorizontalGrid(new float[]{rect.width-70, 70}, 5);

			WindowComponents.Label(hGrid[1], text);
			WindowComponents.Label(hGrid[2], "<b>"+value.ToString()+"</b>");
		}

		/// Split texture by corners/cards/filling 
		public static Dictionary<Rect, Rect> SplitTextureBorderUV(Rect r, float textureWidth) {
			int k = (int)((r.width+textureWidth)+r.height*666);
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

			foreach (KeyValuePair<Rect, Rect> kv in WindowComponents.SplitTextureBorderUV(r, texture.width)) {
				GUI.DrawTextureWithTexCoords(kv.Key, texture, kv.Value.InvertY());
			}

			GUI.EndGroup();
		}
	}
}
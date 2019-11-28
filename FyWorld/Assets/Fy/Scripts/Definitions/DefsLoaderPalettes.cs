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

namespace Fy.Definitions {
	// A static class to load our definitions
	// Everything will be loaded from code or JSON files. 
	public static partial  class Defs {
		/// Add a color palette definition to our dictionary
		public static void AddColorPalette(ColorPaletteDef def) {
			Defs.colorPallets.Add(def.uid, def);
		}
		public static void AddColorPalette(NamedColorPaletteDef def) {
			Defs.namedColorPallets.Add(def.uid, def);
		}

		/// Load all color definitions
		public static void LoadColorPalletsFromCode() {
			Defs.colorPallets = new Dictionary<string, ColorPaletteDef>();
			Defs.namedColorPallets = new Dictionary<string, NamedColorPaletteDef>();

			Defs.AddColorPalette(new ColorPaletteDef{
				uid = "human_hair",
				colors = new List<Color> {
					new Color(150/255f, 150/255f, 150/255f, 1),
					new Color(179/255f, 75/255f, 165/255f, 1),
					new Color(87/255f, 75/255f, 179/255f, 1),
					new Color(76/255f, 179/255f, 130/255f, 1),
					new Color(90/255f, 175/255f, 75/255f, 1),
					new Color(177/255f, 179/255f, 76/255f, 1),
					new Color(117/255f, 49/255f, 29/255f, 1),
					new Color(31/255f, 17/255f, 12/255f, 1),
				}
			});


			Defs.AddColorPalette(new ColorPaletteDef{
				uid = "human_body",
				colors = new List<Color> {
					new Color(190/255f, 160/255f, 135/255f, 1f),
					new Color(70/255f, 50/255f, 25/255f, 1f),
					new Color(39/255f, 29/255f, 8/255f, 1f),
					new Color(220/255f, 220/255f, 220/255f, 1f),
					new Color(161/255f, 128/255f, 68/255f, 1f),
					new Color(215/255f, 167/255f, 208/255f, 1f)
				}
			});


			Defs.AddColorPalette(new NamedColorPaletteDef{
				uid = "cols_vitals",
				colors = new Dictionary<string, Color>{
					{"Health", new Color(51/255f, 12/255f, 12/255f, 1)},
					{"Energy", new Color(20/255f, 38/255f, 12/255f, 1)},
					{"Mana", new Color(12/255f, 23/255f, 51/255f, 1)},
					{"Joy", new Color(51/255f, 12/255f, 50/255f, 1)},
					{"Hunger", new Color(51/255f, 12/255f, 50/255f, 1)},
				}
			});

			Defs.AddColorPalette(new ColorPaletteDef{
				uid = "cols_leafsGreen",
				colors = new List<Color>{
                    new Color(161/255f, 195/255f, 73/255f, 1f),
                    new Color(135/255f, 163/255f, 48/255f, 1f),
                    new Color(189/255f, 198/255f, 103/255f, 1f)
                }
			});
			Defs.AddColorPalette(new ColorPaletteDef{
				uid = "cols_leafsOrange",
				colors = new List<Color>{
                    new Color(187/255f, 102/255f, 0/255f, 1f),
                    new Color(170/255f, 80/255f, 66/48f, 1f),
                    new Color(186/255f, 90/255f, 49/255f, 1f)
                }
			});
			Defs.AddColorPalette(new ColorPaletteDef{
				uid = "cols_wood",
				colors = new List<Color>{
                    new Color(112/255f, 78/255f, 46/255f, 1f),
                    new Color(54/255f, 48/255f, 32/48f, 1f)
                }
			});
		}
	}
}
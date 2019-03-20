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
	public static class Defs {
		/* List of ground definitions */
		public static Dictionary<string, TilableDef> grounds;
		/*  List of ground ordered by maxHeight */
		public static SortedDictionary<float, TilableDef> groundsByHeight;

		/* List of color palettes definitions */
		public static Dictionary<string, ColorPaletteDef> colorPallets;

		/* List of plant definitions */
		public static Dictionary<string, TilableDef> plants;

		/* List of mountains definitions */
		public static Dictionary<string, TilableDef> mountains;


		/// Add a mountain definition to our mountain directionnary
		public static void AddMountain(TilableDef def) {
			Defs.mountains.Add(def.uid, def);
		}

		/// Add a ground definition to our ground dictionary
		public static void AddGround(TilableDef def) {
			Defs.grounds.Add(def.uid, def);
		}

		/// Add a plant definition to our plant dictionary
		public static void AddPlant(TilableDef def) {
			Defs.plants.Add(def.uid, def);
		}

		/// Add a color palette definition to our dictionary
		public static void AddColorPalette(ColorPaletteDef def) {
			Defs.colorPallets.Add(def.uid, def);
		}

		// Load all mountains definitions
		public static void LoadMountainsFromCode() {
			Defs.mountains = new Dictionary<string, TilableDef>();
			Defs.AddMountain(
				new TilableDef {
					uid = "mountain",
					layer = Layer.Mountain,
					graphics = new GraphicDef{
						textureName = "mountain",
						color = new Color(72/255f, 72/255f, 72/255f, 1f)
					}
				}
			);
		}

		/// Load all plant definitions
		public static void LoadPlantsFromCode() {
			Defs.plants = new Dictionary<string, TilableDef>();

			Defs.AddPlant(new TilableDef{
				uid = "grass",
				layer = Layer.Plant,
				type = TilableType.Grass,
				graphics = new GraphicDef{
					textureName = "grass"
				},
				plantDef = new PlantDef{
					probability = .5f,
					minFertility = .1f
				}
			});
			Defs.AddPlant(new TilableDef{
				uid = "tree",
				layer = Layer.Plant,
				type = TilableType.Tree,
				graphics = new GraphicDef{
					textureName = "tree",
					size = new Vector2(2, 3f),
					pivot = new Vector2(.5f, 0)
				},
				plantDef = new PlantDef{
					probability = .1f,
					minFertility = .2f
				}
			});
		}

		/// Load all ground definitions
		public static void LoadGroundsFromCode() {
			Defs.grounds = new Dictionary<string, TilableDef>();
			Defs.groundsByHeight = new SortedDictionary<float, TilableDef>();

			/* Water */
			Defs.AddGround(
				new TilableDef{
					uid = "water",
					layer = Layer.Ground,
					graphics = new GraphicDef{
						textureName = "water",
						materialName = "grounds",
						isInstanced = false,
						drawPriority = 0,
					},
					groundDef = new GroundDef {
						maxHeight = .30f
					}
				}
			);

			/* Dirt */
			Defs.AddGround(
				new TilableDef{
					uid = "dirt",
					layer = Layer.Ground,
					fertility = 1f,
					graphics = new GraphicDef{
						textureName = "dirt",
						materialName = "grounds",
						isInstanced = false,
						drawPriority = 1
					},
					groundDef = new GroundDef {
						maxHeight = .5f
					}
				}
			);

			/* Rocks */
			Defs.AddGround(
				new TilableDef{
					uid = "rocks",
					layer = Layer.Ground,
					graphics = new GraphicDef{
						textureName = "rocks",
						materialName = "grounds",
						isInstanced = false,
						drawPriority = 2
					},
					groundDef = new GroundDef {
						maxHeight = .75f
					}
				}
			);

			foreach (TilableDef tilableDef in Defs.grounds.Values) {
				Defs.groundsByHeight.Add(tilableDef.groundDef.maxHeight, tilableDef);
			}
		}

		/// Load all color definitions
		public static void LoadColorPalletsFromCode() {
			Defs.colorPallets = new Dictionary<string, ColorPaletteDef>();
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
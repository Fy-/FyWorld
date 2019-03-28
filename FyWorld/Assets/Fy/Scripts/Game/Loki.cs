/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using Fy.Controllers;
using Fy.World;
using Fy.Definitions;
using Fy.Helpers;
using Fy.Entities;

namespace Fy {
	// Static class use to find stuff in our game.
	public static class Loki {
		public static GameManager manager;
		public static CameraController cameraController { get { return Loki.manager.cameraController; } }
		public static StackableLabelController stackableLabelController { get { return Loki.manager.stackableLabelController; } }
		public static Map map { get { return Loki.manager.map; } }
		public static Tick tick { get { return Loki.manager.tick; } }

		/// Load all statics definitions
		public static void LoadStatics() {
			Res.Load(); // Load all our resources;
			DirectionUtils.LoadStatics(); // Set neighbours;
			Defs.LoadGroundsFromCode(); // Loading our ground definitions;
			Defs.LoadPlantsFromCode();; // Loading our plants definitions;
			Defs.LoadMountainsFromCode(); // Loading our mountains definitions;
			Defs.LoadStackablesFromCode(); // Loading our stackables definitions;
			Defs.LoadColorPalletsFromCode(); // Loading our pallets;
			Defs.LoadAnimalsFromCode(); // Loading animals;
			Defs.LoadTasksFromCode(); // Loading tasks;
		}

		/// Register the game manager when we start a game.
		public static void NewGame(GameManager manager) {
			Loki.manager = manager;
		}
	}
}
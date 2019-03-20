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
using Fy.Entity;

namespace Fy {
	public static class Loki {
		public static GameManager manager;
		public static CameraController cameraController { get { return Loki.manager.cameraController; } }
		public static Map map { get { return Loki.manager.map; } }
		public static Tick tick { get { return Loki.manager.tick; } }

		public static void LoadStatics() {
			Res.Load(); // Load all our resources;
			DirectionUtils.SetNeighbours(); // Set neighbours;
			Defs.LoadGroundsFromCode(); // Loading our ground definitions;
			Defs.LoadPlantsFromCode();; // Loading our plants definitions;
			Defs.LoadColorPalletsFromCode(); // Loading our pallets;
		}
		public static void NewGame(GameManager manager) {
			Loki.manager = manager;
		}
	}
}
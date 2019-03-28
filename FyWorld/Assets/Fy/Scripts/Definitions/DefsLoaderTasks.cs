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
using Fy.Characters.AI;

namespace Fy.Definitions {
	// A static class to load our definitions
	// Everything will be loaded from code or JSON files. 
	public static partial  class Defs {
		/// Add a plant definition to our plant dictionary
		public static void AddTask(TaskDef def) {
			Defs.tasks.Add(def.uid, def);
		}

		/// Load all tasks definitions
		public static void LoadTasksFromCode() {
			Defs.tasks = new Dictionary<string, TaskDef>();

			Defs.AddTask(new TaskDef{
				uid = "task_sleep",
				taskType = TaskType.Sleep
			});

			Defs.AddTask(new TaskDef{
				uid = "task_idle",
				taskType = TaskType.Idle
			});
		}
	}
}
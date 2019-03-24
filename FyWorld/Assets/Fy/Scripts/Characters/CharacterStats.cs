/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System;

namespace Fy.Characters {
	public static class StatsUtils {
		public static Stats[] stats = (Stats[])Enum.GetValues(typeof(Stats));
		public static Attributes[] attributes = (Attributes[])Enum.GetValues(typeof(Attributes));
		public static Vitals[] vitals = (Vitals[])Enum.GetValues(typeof(Vitals));
		public static Skills[] skills = (Skills[])Enum.GetValues(typeof(Skills));
	}

	[Serializable]
	public enum Stats {
		Strength,
		Agility,
		Endurance,
		Intellect,
		Wisdom
	}

	[Serializable]
	public enum Attributes {
		WalkSpeed, // [strength + endurance]
		InventorySize, // [strength]
		PhysicalArmour, // [strength + endurance]
		PhysicalAttack, // [strength + agility]
		MagicalAttack, // [intellect + wisdom]
		MagicalArmour, // [wisdom + endurance]
		HealthRegen, // [endurance + strength]
		EnergyRegen,  // [wisdom + endurance]
		ManaRegen, // [intellect + wisdom]
		CriticalChance, // [agility + intellect]
		Charisma, // [wisdom + strength] This will help us for positive and negative social interactions
	}

	[Serializable]
	public enum Vitals {
		Health, // [endurance] When this reach 0 we die.
		Energy, // [endurance] Every job or action consume energy, we need to sleep to refill this & eat
		Mana, // [intellect] We need this for magic stuff,
		Joy, // TBD
	}

	[Serializable]
	public enum Skills {
		Healing, // [wisdom + intellect]
		Building, // [agility + strength]
		Manufacturing, // [agility + intellect]
		Entertaining, // [charisma]
		Growing, // [agility + wisdom]
		Cutting, // [strength + agility]
		Mining, // [strength + agility]
		Cooking, // [agility + intellect]
	}
}
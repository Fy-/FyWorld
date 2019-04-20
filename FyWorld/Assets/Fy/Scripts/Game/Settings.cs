/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
namespace Fy {
	// All const settings
	public static class Settings {
		public const int BUCKET_SIZE = 32;
		public const int TICKS_PER_DAY = 30000;
		public const int DAYS_PER_SEASON = 14;
		public const int SEASONS_PER_YEAR = 4;
		public const int TICKS_PER_SEASON = Settings.TICKS_PER_DAY * Settings.DAYS_PER_SEASON;
		public const int TICKS_PER_YEAR = Settings.SEASONS_PER_YEAR * Settings.TICKS_PER_SEASON;
		public const bool DEBUG = true;
		public const int EYE_COUNT = 1;
		public const int HAIR_COUNT = 1;
		public const int BODY_COUNT = 0;
	}
}
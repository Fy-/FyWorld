/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
namespace Fy {
	public static class Settings {
		public const int REGION_SIZE = 25;
		public const int BUCKET_SIZE = 25;
		public const int TICKS_PER_DAY = 3000;
		public const int DAYS_PER_SEASON = 14;
		public const int SEASONS_PER_YEAR = 4;
		public const int TICKS_PER_SEASON = Settings.TICKS_PER_DAY * Settings.DAYS_PER_SEASON;
		public const int TICKS_PER_YEAR = Settings.SEASONS_PER_YEAR * Settings.TICKS_PER_SEASON;
	}
}
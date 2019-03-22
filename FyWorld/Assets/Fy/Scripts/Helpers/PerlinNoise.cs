/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using UnityEngine;

namespace Fy.Helpers {
	// Represent a wave of noise.
	public struct Wave {
		public float seed;
		public float frequency;
		public float amplitude;

		public Wave(float seed, float frequency, float amplitude) {
			this.seed = seed;
			this.frequency = frequency;
			this.amplitude = amplitude;
		}
	}

	// Utility to generate a noise map (used for our grounds).
	public static class NoiseMap {
		public static Wave[] GroundWave(float seed) {
			Wave[] waves = new Wave[3];
			waves[0] = new Wave(seed * 42, 1, 1);
			waves[1] = new Wave(seed * 666, .5f, 2f);
			waves[2] = new Wave(seed * 1337, .25f, 4f);

			return waves;
		}

		public static float[] GenerateNoiseMap(Vector2Int size, float scale, Wave[] waves) {
			float[] noisemap = new float[size.x*size.y];
			for (int x = 0; x < size.x; x++) {
				for (int y = 0; y < size.y; y++) {
					float sx = x / scale;
					float sy = y / scale;
					float noise = 0f;
					float norm = 0f;
					
					foreach (Wave wave in waves) {
						noise += wave.amplitude * Mathf.PerlinNoise(sx * wave.frequency + wave.seed, sy * wave.frequency + wave.seed);
						norm += wave.amplitude;
					}

					noise /= norm;
					noisemap[x + y * size.x] = noise;
				}
			}

			return noisemap;
		}
	}
}
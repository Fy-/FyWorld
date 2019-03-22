/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using System;
using UnityEngine;
using Fy.Entity;
using Fy.Definitions;
using Fy.Helpers;
using Fy.Visuals;

namespace Fy.World {
	// Grid
	public abstract class LayerGrid {
		/* Grid Size */
		public Vector2Int size { get { return this.rect.size; } }

		/* LayerGrid rect */
		public RectI rect { get; protected set; }

		/* Array of LayerGridBuckets */
		public LayerGridBucket[] buckets { get; protected set; }

		/* Type of our renderer */
		public Type renderer { get; protected set; }

		/* Layer */
		public Layer layer { get; protected set; }

		private int _bucketSizeX;
		private int _bucketSizeY;

		private int _bucketCount;

		public LayerGrid (Vector2Int size, Layer layer) {
			this.layer = layer;
			this.rect = new RectI(new Vector2Int(0, 0), size);
			this.renderer = typeof(BucketRenderer);
			this._bucketSizeX = Mathf.CeilToInt(this.size.x/(float)Settings.BUCKET_SIZE);
			this._bucketSizeY = Mathf.CeilToInt(this.size.y/(float)Settings.BUCKET_SIZE);
			this._bucketCount = this._bucketSizeY * this._bucketSizeX;
		}

		public void AddTilable(Tilable tilable) {
			this.GetBucketAt(tilable.position).AddTilable(tilable);
		}

		public LayerGridBucket GetBucketAt(Vector2Int position) {
			// maybe it's faster to check bounds of the position via our Rect ?

			int bucketID = (int)(position.x/Settings.BUCKET_SIZE) + (int)(position.y/Settings.BUCKET_SIZE) * this._bucketSizeX;
			if (bucketID >= 0 && bucketID < this.buckets.Length) {
				return this.buckets[bucketID];
			}
			return null;
		}

		public Tilable GetTilableAt(Vector2Int position) {
			LayerGridBucket bucket = this.GetBucketAt(position);

			if (bucket != null) {
				return this.GetBucketAt(position).GetTilableAt(position);
			}

			return null;
		}

		public void BuildStaticMeshes() {
			if (this.renderer == null) {
				return;
			}

			foreach (LayerGridBucket bucket in this.buckets) {
				bucket.BuildStaticMeshes();
			}
		}

		public void CheckMatriceUpdates() {
			foreach (LayerGridBucket bucket in this.buckets) {
				if (bucket.IsVisible()) {
					bucket.CheckMatriceUpdates();
				}
			}
		}

		public void DrawBuckets() {
			if (this.renderer == null) {
				foreach (LayerGridBucket bucket in this.buckets) {
					if (bucket.IsVisible()) {
						bucket.DrawInstanced();
					}
				}
				return;
			}

			foreach (LayerGridBucket bucket in this.buckets) {
				if (bucket.IsVisible()) {
					bucket.DrawStatics();
					bucket.DrawInstanced();
				}
			}
		}

		protected void GenerateBuckets() {
			this.buckets = new LayerGridBucket[this._bucketCount];
			for (int x = 0; x < this.size.x; x += Settings.BUCKET_SIZE) {
				for (int y = 0; y < this.size.y; y += Settings.BUCKET_SIZE) {
					RectI bucketRect = new RectI(new Vector2Int(x, y), Settings.BUCKET_SIZE, Settings.BUCKET_SIZE);
					bucketRect.Clip(this.rect);
					int bucketID = (int)(x/Settings.BUCKET_SIZE) + (int)(y/Settings.BUCKET_SIZE) * this._bucketSizeX;
					this.buckets[bucketID] = new LayerGridBucket(bucketID, bucketRect, this.layer, this.renderer);
				}
			}
		}
	}
}
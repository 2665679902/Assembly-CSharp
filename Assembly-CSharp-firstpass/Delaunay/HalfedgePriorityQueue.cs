using System;
using Delaunay.Utils;
using UnityEngine;

namespace Delaunay
{
	// Token: 0x02000149 RID: 329
	internal sealed class HalfedgePriorityQueue : Delaunay.Utils.IDisposable
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002AA61 File Offset: 0x00028C61
		public HalfedgePriorityQueue(float ymin, float deltay, int sqrt_nsites)
		{
			this._ymin = ymin;
			this._deltay = deltay;
			this._hashsize = 4 * sqrt_nsites;
			this.Initialize();
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002AA88 File Offset: 0x00028C88
		public void Dispose()
		{
			for (int i = 0; i < this._hashsize; i++)
			{
				this._hash[i].Dispose();
				this._hash[i] = null;
			}
			this._hash = null;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002AAC4 File Offset: 0x00028CC4
		private void Initialize()
		{
			this._count = 0;
			this._minBucket = 0;
			this._hash = new Halfedge[this._hashsize];
			for (int i = 0; i < this._hashsize; i++)
			{
				this._hash[i] = Halfedge.CreateDummy();
				this._hash[i].nextInPriorityQueue = null;
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002AB1C File Offset: 0x00028D1C
		public void Insert(Halfedge halfEdge)
		{
			int num = this.Bucket(halfEdge);
			if (num < this._minBucket)
			{
				this._minBucket = num;
			}
			Halfedge halfedge = this._hash[num];
			Halfedge nextInPriorityQueue;
			while ((nextInPriorityQueue = halfedge.nextInPriorityQueue) != null && (halfEdge.ystar > nextInPriorityQueue.ystar || (halfEdge.ystar == nextInPriorityQueue.ystar && halfEdge.vertex.x > nextInPriorityQueue.vertex.x)))
			{
				halfedge = nextInPriorityQueue;
			}
			halfEdge.nextInPriorityQueue = halfedge.nextInPriorityQueue;
			halfedge.nextInPriorityQueue = halfEdge;
			this._count++;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002ABB0 File Offset: 0x00028DB0
		public void Remove(Halfedge halfEdge)
		{
			int num = this.Bucket(halfEdge);
			if (halfEdge.vertex != null)
			{
				Halfedge halfedge = this._hash[num];
				while (halfedge.nextInPriorityQueue != halfEdge)
				{
					halfedge = halfedge.nextInPriorityQueue;
				}
				halfedge.nextInPriorityQueue = halfEdge.nextInPriorityQueue;
				this._count--;
				halfEdge.vertex = null;
				halfEdge.nextInPriorityQueue = null;
				halfEdge.Dispose();
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002AC18 File Offset: 0x00028E18
		private int Bucket(Halfedge halfEdge)
		{
			int num = (int)((halfEdge.ystar - this._ymin) / this._deltay * (float)this._hashsize);
			if (num < 0)
			{
				num = 0;
			}
			if (num >= this._hashsize)
			{
				num = this._hashsize - 1;
			}
			return num;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002AC5C File Offset: 0x00028E5C
		private bool IsEmpty(int bucket)
		{
			return this._hash[bucket].nextInPriorityQueue == null;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002AC6E File Offset: 0x00028E6E
		private void AdjustMinBucket()
		{
			while (this._minBucket < this._hashsize - 1 && this.IsEmpty(this._minBucket))
			{
				this._minBucket++;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002AC9E File Offset: 0x00028E9E
		public bool Empty()
		{
			return this._count == 0;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002ACAC File Offset: 0x00028EAC
		public Vector2 Min()
		{
			this.AdjustMinBucket();
			Halfedge nextInPriorityQueue = this._hash[this._minBucket].nextInPriorityQueue;
			return new Vector2(nextInPriorityQueue.vertex.x, nextInPriorityQueue.ystar);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002ACE8 File Offset: 0x00028EE8
		public Halfedge ExtractMin()
		{
			Halfedge nextInPriorityQueue = this._hash[this._minBucket].nextInPriorityQueue;
			this._hash[this._minBucket].nextInPriorityQueue = nextInPriorityQueue.nextInPriorityQueue;
			this._count--;
			nextInPriorityQueue.nextInPriorityQueue = null;
			return nextInPriorityQueue;
		}

		// Token: 0x04000719 RID: 1817
		private Halfedge[] _hash;

		// Token: 0x0400071A RID: 1818
		private int _count;

		// Token: 0x0400071B RID: 1819
		private int _minBucket;

		// Token: 0x0400071C RID: 1820
		private int _hashsize;

		// Token: 0x0400071D RID: 1821
		private float _ymin;

		// Token: 0x0400071E RID: 1822
		private float _deltay;
	}
}

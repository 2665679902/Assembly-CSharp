using System;
using Delaunay.Utils;
using UnityEngine;

namespace Delaunay
{
	// Token: 0x02000145 RID: 325
	internal sealed class EdgeList : Delaunay.Utils.IDisposable
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0002A1D0 File Offset: 0x000283D0
		public Halfedge leftEnd
		{
			get
			{
				return this._leftEnd;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0002A1D8 File Offset: 0x000283D8
		public Halfedge rightEnd
		{
			get
			{
				return this._rightEnd;
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002A1E0 File Offset: 0x000283E0
		public void Dispose()
		{
			Halfedge halfedge = this._leftEnd;
			while (halfedge != this._rightEnd)
			{
				Halfedge halfedge2 = halfedge;
				halfedge = halfedge.edgeListRightNeighbor;
				halfedge2.Dispose();
			}
			this._leftEnd = null;
			this._rightEnd.Dispose();
			this._rightEnd = null;
			for (int i = 0; i < this._hashsize; i++)
			{
				this._hash[i] = null;
			}
			this._hash = null;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002A248 File Offset: 0x00028448
		public EdgeList(float xmin, float deltax, int sqrt_nsites)
		{
			this._xmin = xmin;
			this._deltax = deltax;
			this._hashsize = 2 * sqrt_nsites;
			this._hash = new Halfedge[this._hashsize];
			this._leftEnd = Halfedge.CreateDummy();
			this._rightEnd = Halfedge.CreateDummy();
			this._leftEnd.edgeListLeftNeighbor = null;
			this._leftEnd.edgeListRightNeighbor = this._rightEnd;
			this._rightEnd.edgeListLeftNeighbor = this._leftEnd;
			this._rightEnd.edgeListRightNeighbor = null;
			this._hash[0] = this._leftEnd;
			this._hash[this._hashsize - 1] = this._rightEnd;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002A2F6 File Offset: 0x000284F6
		public void Insert(Halfedge lb, Halfedge newHalfedge)
		{
			newHalfedge.edgeListLeftNeighbor = lb;
			newHalfedge.edgeListRightNeighbor = lb.edgeListRightNeighbor;
			lb.edgeListRightNeighbor.edgeListLeftNeighbor = newHalfedge;
			lb.edgeListRightNeighbor = newHalfedge;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002A320 File Offset: 0x00028520
		public void Remove(Halfedge halfEdge)
		{
			halfEdge.edgeListLeftNeighbor.edgeListRightNeighbor = halfEdge.edgeListRightNeighbor;
			halfEdge.edgeListRightNeighbor.edgeListLeftNeighbor = halfEdge.edgeListLeftNeighbor;
			halfEdge.edge = Edge.DELETED;
			halfEdge.edgeListLeftNeighbor = (halfEdge.edgeListRightNeighbor = null);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002A36C File Offset: 0x0002856C
		public Halfedge EdgeListLeftNeighbor(Vector2 p)
		{
			int num = (int)((p.x - this._xmin) / this._deltax * (float)this._hashsize);
			if (num < 0)
			{
				num = 0;
			}
			if (num >= this._hashsize)
			{
				num = this._hashsize - 1;
			}
			Halfedge halfedge = this.GetHash(num);
			if (halfedge == null)
			{
				int num2 = 1;
				while ((halfedge = this.GetHash(num - num2)) == null && (halfedge = this.GetHash(num + num2)) == null)
				{
					num2++;
				}
			}
			if (halfedge == this.leftEnd || (halfedge != this.rightEnd && halfedge.IsLeftOf(p)))
			{
				do
				{
					halfedge = halfedge.edgeListRightNeighbor;
				}
				while (halfedge != this.rightEnd && halfedge.IsLeftOf(p));
				halfedge = halfedge.edgeListLeftNeighbor;
			}
			else
			{
				do
				{
					halfedge = halfedge.edgeListLeftNeighbor;
				}
				while (halfedge != this.leftEnd && !halfedge.IsLeftOf(p));
			}
			if (num > 0 && num < this._hashsize - 1)
			{
				this._hash[num] = halfedge;
			}
			return halfedge;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002A44C File Offset: 0x0002864C
		private Halfedge GetHash(int b)
		{
			if (b < 0 || b >= this._hashsize)
			{
				return null;
			}
			Halfedge halfedge = this._hash[b];
			if (halfedge != null && halfedge.edge == Edge.DELETED)
			{
				this._hash[b] = null;
				return null;
			}
			return halfedge;
		}

		// Token: 0x04000706 RID: 1798
		private float _deltax;

		// Token: 0x04000707 RID: 1799
		private float _xmin;

		// Token: 0x04000708 RID: 1800
		private int _hashsize;

		// Token: 0x04000709 RID: 1801
		private Halfedge[] _hash;

		// Token: 0x0400070A RID: 1802
		private Halfedge _leftEnd;

		// Token: 0x0400070B RID: 1803
		private Halfedge _rightEnd;
	}
}

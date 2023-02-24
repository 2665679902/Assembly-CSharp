using System;
using System.Collections.Generic;
using Delaunay.LR;
using Delaunay.Utils;
using UnityEngine;

namespace Delaunay
{
	// Token: 0x02000148 RID: 328
	public sealed class Halfedge : Delaunay.Utils.IDisposable
	{
		// Token: 0x06000AEF RID: 2799 RVA: 0x0002A6B0 File Offset: 0x000288B0
		public static Halfedge Create(Edge edge, Side? lr)
		{
			if (Halfedge._pool.Count > 0)
			{
				return Halfedge._pool.Pop().Init(edge, lr);
			}
			return new Halfedge(edge, lr);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002A6D8 File Offset: 0x000288D8
		public static Halfedge CreateDummy()
		{
			return Halfedge.Create(null, null);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002A6F4 File Offset: 0x000288F4
		public Halfedge(Edge edge = null, Side? lr = null)
		{
			this.Init(edge, lr);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002A705 File Offset: 0x00028905
		private Halfedge Init(Edge edge, Side? lr)
		{
			this.edge = edge;
			this.leftRight = lr;
			this.nextInPriorityQueue = null;
			this.vertex = null;
			return this;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002A724 File Offset: 0x00028924
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Halfedge (leftRight: ",
				this.leftRight.ToString(),
				"; vertex: ",
				this.vertex.ToString(),
				")"
			});
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002A778 File Offset: 0x00028978
		public void Dispose()
		{
			if (this.edgeListLeftNeighbor != null || this.edgeListRightNeighbor != null)
			{
				return;
			}
			if (this.nextInPriorityQueue != null)
			{
				return;
			}
			this.edge = null;
			this.leftRight = null;
			this.vertex = null;
			Halfedge._pool.Push(this);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002A7C4 File Offset: 0x000289C4
		public void ReallyDispose()
		{
			this.edgeListLeftNeighbor = null;
			this.edgeListRightNeighbor = null;
			this.nextInPriorityQueue = null;
			this.edge = null;
			this.leftRight = null;
			this.vertex = null;
			Halfedge._pool.Push(this);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002A800 File Offset: 0x00028A00
		internal bool IsLeftOf(Vector2 p)
		{
			Vector2 coord = this.edge.rightSite.Coord;
			bool flag = p.x > coord.x;
			Side? side;
			Side side2;
			if (flag)
			{
				side = this.leftRight;
				side2 = Side.LEFT;
				if ((side.GetValueOrDefault() == side2) & (side != null))
				{
					return true;
				}
			}
			if (!flag)
			{
				side = this.leftRight;
				side2 = Side.RIGHT;
				if ((side.GetValueOrDefault() == side2) & (side != null))
				{
					return false;
				}
			}
			bool flag3;
			if ((double)this.edge.a == 1.0)
			{
				float num = p.y - coord.y;
				float num2 = p.x - coord.x;
				bool flag2 = false;
				if ((!flag && (double)this.edge.b < 0.0) || (flag && (double)this.edge.b >= 0.0))
				{
					flag3 = num >= this.edge.b * num2;
					flag2 = flag3;
				}
				else
				{
					flag3 = p.x + p.y * this.edge.b > this.edge.c;
					if ((double)this.edge.b < 0.0)
					{
						flag3 = !flag3;
					}
					if (!flag3)
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					float num3 = coord.x - this.edge.leftSite.x;
					flag3 = (double)(this.edge.b * (num2 * num2 - num * num)) < (double)(num3 * num) * (1.0 + 2.0 * (double)num2 / (double)num3 + (double)(this.edge.b * this.edge.b));
					if ((double)this.edge.b < 0.0)
					{
						flag3 = !flag3;
					}
				}
			}
			else
			{
				float num4 = this.edge.c - this.edge.a * p.x;
				float num5 = p.y - num4;
				float num6 = p.x - coord.x;
				float num7 = num4 - coord.y;
				flag3 = num5 * num5 > num6 * num6 + num7 * num7;
			}
			side = this.leftRight;
			side2 = Side.LEFT;
			if (!((side.GetValueOrDefault() == side2) & (side != null)))
			{
				return !flag3;
			}
			return flag3;
		}

		// Token: 0x04000711 RID: 1809
		private static Stack<Halfedge> _pool = new Stack<Halfedge>();

		// Token: 0x04000712 RID: 1810
		public Halfedge edgeListLeftNeighbor;

		// Token: 0x04000713 RID: 1811
		public Halfedge edgeListRightNeighbor;

		// Token: 0x04000714 RID: 1812
		public Halfedge nextInPriorityQueue;

		// Token: 0x04000715 RID: 1813
		public Edge edge;

		// Token: 0x04000716 RID: 1814
		public Side? leftRight;

		// Token: 0x04000717 RID: 1815
		public Vertex vertex;

		// Token: 0x04000718 RID: 1816
		public float ystar;
	}
}

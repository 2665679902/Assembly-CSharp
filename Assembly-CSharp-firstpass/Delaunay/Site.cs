using System;
using System.Collections.Generic;
using Delaunay.Geo;
using Delaunay.LR;
using UnityEngine;

namespace Delaunay
{
	// Token: 0x0200014B RID: 331
	public sealed class Site : ICoord, IComparable
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0002AD36 File Offset: 0x00028F36
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x0002AD3E File Offset: 0x00028F3E
		public uint color { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0002AD47 File Offset: 0x00028F47
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x0002AD4F File Offset: 0x00028F4F
		public float weight { get; private set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0002AD58 File Offset: 0x00028F58
		internal List<Edge> edges
		{
			get
			{
				return this._edges;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002AD60 File Offset: 0x00028F60
		public float x
		{
			get
			{
				return this._coord.x;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002AD6D File Offset: 0x00028F6D
		internal float y
		{
			get
			{
				return this._coord.y;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002AD7A File Offset: 0x00028F7A
		public Vector2 Coord
		{
			get
			{
				return this._coord;
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002AD82 File Offset: 0x00028F82
		public float Dist(ICoord p)
		{
			return Vector2.Distance(p.Coord, this._coord);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002AD98 File Offset: 0x00028F98
		public override string ToString()
		{
			return "Site " + this._siteIndex.ToString() + ": " + this.Coord.ToString();
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002ADD3 File Offset: 0x00028FD3
		public static Site Create(Vector2 p, uint index, float weight, uint color)
		{
			if (Site._pool.Count > 0)
			{
				return Site._pool.Pop().Init(p, index, weight, color);
			}
			return new Site(p, index, weight, color);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002ADFF File Offset: 0x00028FFF
		internal static void SortSites(List<Site> sites)
		{
			sites.Sort();
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002AE08 File Offset: 0x00029008
		public int CompareTo(object obj)
		{
			Site site = (Site)obj;
			int num = Voronoi.CompareByYThenX(this, site);
			if (num == -1)
			{
				if (this._siteIndex > site._siteIndex)
				{
					uint num2 = this._siteIndex;
					this._siteIndex = site._siteIndex;
					site._siteIndex = num2;
				}
			}
			else if (num == 1 && site._siteIndex > this._siteIndex)
			{
				uint num2 = site._siteIndex;
				site._siteIndex = this._siteIndex;
				this._siteIndex = num2;
			}
			return num;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002AE7F File Offset: 0x0002907F
		private static bool CloseEnough(Vector2 p0, Vector2 p1)
		{
			return Vector2.Distance(p0, p1) < Site.EPSILON;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002AE8F File Offset: 0x0002908F
		private Site(Vector2 p, uint index, float weight, uint color)
		{
			this.Init(p, index, weight, color);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002AEA3 File Offset: 0x000290A3
		private Site Init(Vector2 p, uint index, float weight, uint color)
		{
			this.scaled_weight = -1f;
			this._coord = p;
			this._siteIndex = index;
			this.weight = weight;
			this.color = color;
			this._edges = new List<Edge>();
			this._region = null;
			return this;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002AEE0 File Offset: 0x000290E0
		private void Move(Vector2 p)
		{
			this.Clear();
			this._coord = p;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002AEEF File Offset: 0x000290EF
		public void Dispose()
		{
			this.Clear();
			Site._pool.Push(this);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002AF04 File Offset: 0x00029104
		private void Clear()
		{
			if (this._edges != null)
			{
				this._edges.Clear();
				this._edges = null;
			}
			if (this._edgeOrientations != null)
			{
				this._edgeOrientations.Clear();
				this._edgeOrientations = null;
			}
			if (this._region != null)
			{
				this._region.Clear();
				this._region = null;
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002AF5F File Offset: 0x0002915F
		public void AddEdge(Edge edge)
		{
			this._edges.Add(edge);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002AF70 File Offset: 0x00029170
		public Vector2 GetClosestPt(Vector2 p)
		{
			Vector2 normalized = (p - this._coord).normalized;
			return this._coord + normalized * this.weight;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002AFA9 File Offset: 0x000291A9
		public Edge NearestEdge()
		{
			this._edges.Sort((Edge a, Edge b) => Edge.CompareSitesDistances(a, b));
			return this._edges[0];
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002AFE4 File Offset: 0x000291E4
		public List<Site> NeighborSites()
		{
			if (this._edges == null || this._edges.Count == 0)
			{
				return new List<Site>();
			}
			if (this._edgeOrientations == null)
			{
				this.ReorderEdges();
			}
			List<Site> list = new List<Site>();
			for (int i = 0; i < this._edges.Count; i++)
			{
				Edge edge = this._edges[i];
				list.Add(this.NeighborSite(edge));
			}
			return list;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002B051 File Offset: 0x00029251
		private Site NeighborSite(Edge edge)
		{
			if (this == edge.leftSite)
			{
				return edge.rightSite;
			}
			if (this == edge.rightSite)
			{
				return edge.leftSite;
			}
			return null;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002B074 File Offset: 0x00029274
		internal List<Vector2> Region(Rect clippingBounds)
		{
			if (this._edges == null || this._edges.Count == 0)
			{
				return new List<Vector2>();
			}
			if (this._edgeOrientations == null)
			{
				this.ReorderEdges();
				this._region = this.ClipToBounds(clippingBounds);
				if (new Polygon(this._region).Winding() == Winding.CLOCKWISE)
				{
					this._region.Reverse();
				}
			}
			return this._region;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002B0DC File Offset: 0x000292DC
		internal List<Vector2> Region(Polygon clippingBounds)
		{
			if (this._edges == null || this._edges.Count == 0)
			{
				return new List<Vector2>();
			}
			if (this._edgeOrientations == null)
			{
				this.ReorderEdges();
				this._region = this.ClipToBounds(clippingBounds);
				if (new Polygon(this._region).Winding() == Winding.CLOCKWISE)
				{
					this._region.Reverse();
				}
			}
			return this._region;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002B144 File Offset: 0x00029344
		private void ReorderEdges()
		{
			EdgeReorderer edgeReorderer = new EdgeReorderer(this._edges, VertexOrSite.VERTEX);
			this._edges = edgeReorderer.edges;
			this._edgeOrientations = edgeReorderer.edgeOrientations;
			edgeReorderer.Dispose();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002B17C File Offset: 0x0002937C
		private List<Vector2> ClipToBounds(Rect bounds)
		{
			List<Vector2> list = new List<Vector2>();
			int count = this._edges.Count;
			int num = 0;
			while (num < count && !this._edges[num].visible)
			{
				num++;
			}
			if (num == count)
			{
				return new List<Vector2>();
			}
			Edge edge = this._edges[num];
			Side side = this._edgeOrientations[num];
			if (edge.clippedEnds[side] == null)
			{
				global::Debug.LogError("XXX: Null detected when there should be a Vector2!");
			}
			if (edge.clippedEnds[SideHelper.Other(side)] == null)
			{
				global::Debug.LogError("XXX: Null detected when there should be a Vector2!");
			}
			list.Add(edge.clippedEnds[side].Value);
			list.Add(edge.clippedEnds[SideHelper.Other(side)].Value);
			for (int i = num + 1; i < count; i++)
			{
				edge = this._edges[i];
				if (edge.visible)
				{
					this.Connect(list, i, bounds, false);
				}
			}
			this.Connect(list, num, bounds, true);
			return list;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002B2A3 File Offset: 0x000294A3
		private List<Vector2> ClipToBounds(Polygon bounds)
		{
			return this.ClipToBounds(bounds.bounds);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002B2B4 File Offset: 0x000294B4
		private void Connect(List<Vector2> points, int j, Rect bounds, bool closingUp = false)
		{
			Vector2 vector = points[points.Count - 1];
			Edge edge = this._edges[j];
			Side side = this._edgeOrientations[j];
			if (edge.clippedEnds[side] == null)
			{
				global::Debug.LogError("XXX: Null detected when there should be a Vector2!");
			}
			Vector2 value = edge.clippedEnds[side].Value;
			if (!Site.CloseEnough(vector, value))
			{
				if (vector.x != value.x && vector.y != value.y)
				{
					int num = BoundsCheck.Check(vector, bounds);
					int num2 = BoundsCheck.Check(value, bounds);
					if ((num & BoundsCheck.RIGHT) != 0)
					{
						float num3 = bounds.xMax;
						if ((num2 & BoundsCheck.BOTTOM) != 0)
						{
							float num4 = bounds.yMax;
							points.Add(new Vector2(num3, num4));
						}
						else if ((num2 & BoundsCheck.TOP) != 0)
						{
							float num4 = bounds.yMin;
							points.Add(new Vector2(num3, num4));
						}
						else if ((num2 & BoundsCheck.LEFT) != 0)
						{
							float num4;
							if (vector.y - bounds.y + value.y - bounds.y < bounds.height)
							{
								num4 = bounds.yMin;
							}
							else
							{
								num4 = bounds.yMax;
							}
							points.Add(new Vector2(num3, num4));
							points.Add(new Vector2(bounds.xMin, num4));
						}
					}
					else if ((num & BoundsCheck.LEFT) != 0)
					{
						float num3 = bounds.xMin;
						if ((num2 & BoundsCheck.BOTTOM) != 0)
						{
							float num4 = bounds.yMax;
							points.Add(new Vector2(num3, num4));
						}
						else if ((num2 & BoundsCheck.TOP) != 0)
						{
							float num4 = bounds.yMin;
							points.Add(new Vector2(num3, num4));
						}
						else if ((num2 & BoundsCheck.RIGHT) != 0)
						{
							float num4;
							if (vector.y - bounds.y + value.y - bounds.y < bounds.height)
							{
								num4 = bounds.yMin;
							}
							else
							{
								num4 = bounds.yMax;
							}
							points.Add(new Vector2(num3, num4));
							points.Add(new Vector2(bounds.xMax, num4));
						}
					}
					else if ((num & BoundsCheck.TOP) != 0)
					{
						float num4 = bounds.yMin;
						if ((num2 & BoundsCheck.RIGHT) != 0)
						{
							float num3 = bounds.xMax;
							points.Add(new Vector2(num3, num4));
						}
						else if ((num2 & BoundsCheck.LEFT) != 0)
						{
							float num3 = bounds.xMin;
							points.Add(new Vector2(num3, num4));
						}
						else if ((num2 & BoundsCheck.BOTTOM) != 0)
						{
							float num3;
							if (vector.x - bounds.x + value.x - bounds.x < bounds.width)
							{
								num3 = bounds.xMin;
							}
							else
							{
								num3 = bounds.xMax;
							}
							points.Add(new Vector2(num3, num4));
							points.Add(new Vector2(num3, bounds.yMax));
						}
					}
					else if ((num & BoundsCheck.BOTTOM) != 0)
					{
						float num4 = bounds.yMax;
						if ((num2 & BoundsCheck.RIGHT) != 0)
						{
							float num3 = bounds.xMax;
							points.Add(new Vector2(num3, num4));
						}
						else if ((num2 & BoundsCheck.LEFT) != 0)
						{
							float num3 = bounds.xMin;
							points.Add(new Vector2(num3, num4));
						}
						else if ((num2 & BoundsCheck.TOP) != 0)
						{
							float num3;
							if (vector.x - bounds.x + value.x - bounds.x < bounds.width)
							{
								num3 = bounds.xMin;
							}
							else
							{
								num3 = bounds.xMax;
							}
							points.Add(new Vector2(num3, num4));
							points.Add(new Vector2(num3, bounds.yMin));
						}
					}
				}
				if (closingUp)
				{
					return;
				}
				points.Add(value);
			}
			if (edge.clippedEnds[SideHelper.Other(side)] == null)
			{
				global::Debug.LogError("XXX: Null detected when there should be a Vector2!");
			}
			Vector2 value2 = edge.clippedEnds[SideHelper.Other(side)].Value;
			if (!Site.CloseEnough(points[0], value2))
			{
				points.Add(value2);
			}
		}

		// Token: 0x0400071F RID: 1823
		private static Stack<Site> _pool = new Stack<Site>();

		// Token: 0x04000720 RID: 1824
		private static readonly float EPSILON = 0.005f;

		// Token: 0x04000721 RID: 1825
		private Vector2 _coord;

		// Token: 0x04000724 RID: 1828
		public float scaled_weight;

		// Token: 0x04000725 RID: 1829
		private uint _siteIndex;

		// Token: 0x04000726 RID: 1830
		private List<Edge> _edges;

		// Token: 0x04000727 RID: 1831
		private List<Side> _edgeOrientations;

		// Token: 0x04000728 RID: 1832
		private List<Vector2> _region;
	}
}

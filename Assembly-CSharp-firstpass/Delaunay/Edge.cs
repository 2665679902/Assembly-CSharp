using System;
using System.Collections.Generic;
using Delaunay.Geo;
using Delaunay.LR;
using UnityEngine;

namespace Delaunay
{
	// Token: 0x02000144 RID: 324
	public sealed class Edge
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002996C File Offset: 0x00027B6C
		public static Edge CreateBisectingEdge(Site site0, Site site1)
		{
			Vector2 coord = site1.Coord;
			Vector2 coord2 = site0.Coord;
			float num = coord2.x - coord.x;
			float num2 = coord2.y - coord.y;
			float num3 = ((num > 0f) ? num : (-num));
			float num4 = ((num2 > 0f) ? num2 : (-num2));
			float num5 = coord.x * num + coord.y * num2 + (num * num + num2 * num2) * 0.5f;
			float num6;
			float num7;
			if (num3 > num4)
			{
				num6 = 1f;
				num7 = num2 / num;
				num5 /= num;
			}
			else
			{
				num7 = 1f;
				num6 = num / num2;
				num5 /= num2;
			}
			Edge edge = Edge.Create();
			edge.leftSite = site0;
			edge.rightSite = site1;
			site0.AddEdge(edge);
			site1.AddEdge(edge);
			edge._leftVertex = null;
			edge._rightVertex = null;
			edge.a = num6;
			edge.b = num7;
			edge.c = num5;
			return edge;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00029A5C File Offset: 0x00027C5C
		private static Edge Create()
		{
			Edge edge;
			if (Edge._pool.Count > 0)
			{
				edge = Edge._pool.Pop();
				edge.Init();
			}
			else
			{
				edge = new Edge();
			}
			return edge;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00029A90 File Offset: 0x00027C90
		public LineSegment DelaunayLine()
		{
			return new LineSegment(new Vector2?(this.leftSite.Coord), new Vector2?(this.rightSite.Coord));
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00029AB8 File Offset: 0x00027CB8
		public LineSegment VoronoiEdge()
		{
			if (!this.visible)
			{
				return new LineSegment(null, null);
			}
			return new LineSegment(this._clippedVertices[Side.LEFT], this._clippedVertices[Side.RIGHT]);
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00029B02 File Offset: 0x00027D02
		public Vertex leftVertex
		{
			get
			{
				return this._leftVertex;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00029B0A File Offset: 0x00027D0A
		public Vertex rightVertex
		{
			get
			{
				return this._rightVertex;
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00029B12 File Offset: 0x00027D12
		public Vertex Vertex(Side leftRight)
		{
			if (leftRight != Side.LEFT)
			{
				return this._rightVertex;
			}
			return this._leftVertex;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00029B24 File Offset: 0x00027D24
		public void SetVertex(Side leftRight, Vertex v)
		{
			if (leftRight == Side.LEFT)
			{
				this._leftVertex = v;
				return;
			}
			this._rightVertex = v;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00029B38 File Offset: 0x00027D38
		public bool IsPartOfConvexHull()
		{
			return this._leftVertex == null || this._rightVertex == null;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00029B50 File Offset: 0x00027D50
		public float SitesDistance()
		{
			return Vector2.Distance(this.leftSite.Coord, this.rightSite.Coord) + (this.leftSite.weight + this.rightSite.weight) * (this.leftSite.weight + this.rightSite.weight);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00029BA8 File Offset: 0x00027DA8
		public static int CompareSitesDistances_MAX(Edge edge0, Edge edge1)
		{
			float num = edge0.SitesDistance();
			float num2 = edge1.SitesDistance();
			if (num < num2)
			{
				return 1;
			}
			if (num > num2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00029BD0 File Offset: 0x00027DD0
		public static int CompareSitesDistances(Edge edge0, Edge edge1)
		{
			return -Edge.CompareSitesDistances_MAX(edge0, edge1);
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00029BDA File Offset: 0x00027DDA
		public Dictionary<Side, Vector2?> clippedEnds
		{
			get
			{
				return this._clippedVertices;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00029BE2 File Offset: 0x00027DE2
		public bool visible
		{
			get
			{
				return this._clippedVertices != null;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00029BED File Offset: 0x00027DED
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x00029BFB File Offset: 0x00027DFB
		public Site leftSite
		{
			get
			{
				return this._sites[Side.LEFT];
			}
			set
			{
				this._sites[Side.LEFT] = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00029C0A File Offset: 0x00027E0A
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x00029C18 File Offset: 0x00027E18
		public Site rightSite
		{
			get
			{
				return this._sites[Side.RIGHT];
			}
			set
			{
				this._sites[Side.RIGHT] = value;
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00029C27 File Offset: 0x00027E27
		public Site Site(Side leftRight)
		{
			return this._sites[leftRight];
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00029C38 File Offset: 0x00027E38
		public void Dispose()
		{
			this._leftVertex = null;
			this._rightVertex = null;
			if (this._clippedVertices != null)
			{
				this._clippedVertices[Side.LEFT] = null;
				this._clippedVertices[Side.RIGHT] = null;
				this._clippedVertices = null;
			}
			this._sites[Side.LEFT] = null;
			this._sites[Side.RIGHT] = null;
			this._sites = null;
			Edge._pool.Push(this);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00029CB8 File Offset: 0x00027EB8
		private Edge()
		{
			this._edgeIndex = Edge._nedges++;
			this.Init();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00029CD9 File Offset: 0x00027ED9
		private void Init()
		{
			this._sites = new Dictionary<Side, Site>();
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00029CE8 File Offset: 0x00027EE8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Edge ",
				this._edgeIndex.ToString(),
				"; sites ",
				this._sites[Side.LEFT].ToString(),
				", ",
				this._sites[Side.RIGHT].ToString(),
				"; endVertices ",
				(this._leftVertex != null) ? this._leftVertex.vertexIndex.ToString() : "null",
				", ",
				(this._rightVertex != null) ? this._rightVertex.vertexIndex.ToString() : "null",
				"::"
			});
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00029DB4 File Offset: 0x00027FB4
		public void ClipVertices(Rect bounds)
		{
			float xMin = bounds.xMin;
			float yMin = bounds.yMin;
			float xMax = bounds.xMax;
			float yMax = bounds.yMax;
			Vertex vertex;
			Vertex vertex2;
			if ((double)this.a == 1.0 && (double)this.b >= 0.0)
			{
				vertex = this._rightVertex;
				vertex2 = this._leftVertex;
			}
			else
			{
				vertex = this._leftVertex;
				vertex2 = this._rightVertex;
			}
			float num;
			float num2;
			float num3;
			float num4;
			if ((double)this.a == 1.0)
			{
				num = yMin;
				if (vertex != null && vertex.y > yMin)
				{
					num = vertex.y;
				}
				if (num > yMax)
				{
					return;
				}
				num2 = this.c - this.b * num;
				num3 = yMax;
				if (vertex2 != null && vertex2.y < yMax)
				{
					num3 = vertex2.y;
				}
				if (num3 < yMin)
				{
					return;
				}
				num4 = this.c - this.b * num3;
				if ((num2 > xMax && num4 > xMax) || (num2 < xMin && num4 < xMin))
				{
					return;
				}
				if (num2 > xMax)
				{
					num2 = xMax;
					num = (this.c - num2) / this.b;
				}
				else if (num2 < xMin)
				{
					num2 = xMin;
					num = (this.c - num2) / this.b;
				}
				if (num4 > xMax)
				{
					num4 = xMax;
					num3 = (this.c - num4) / this.b;
				}
				else if (num4 < xMin)
				{
					num4 = xMin;
					num3 = (this.c - num4) / this.b;
				}
			}
			else
			{
				num2 = xMin;
				if (vertex != null && vertex.x > xMin)
				{
					num2 = vertex.x;
				}
				if (num2 > xMax)
				{
					return;
				}
				num = this.c - this.a * num2;
				num4 = xMax;
				if (vertex2 != null && vertex2.x < xMax)
				{
					num4 = vertex2.x;
				}
				if (num4 < xMin)
				{
					return;
				}
				num3 = this.c - this.a * num4;
				if ((num > yMax && num3 > yMax) || (num < yMin && num3 < yMin))
				{
					return;
				}
				if (num > yMax)
				{
					num = yMax;
					num2 = (this.c - num) / this.a;
				}
				else if (num < yMin)
				{
					num = yMin;
					num2 = (this.c - num) / this.a;
				}
				if (num3 > yMax)
				{
					num3 = yMax;
					num4 = (this.c - num3) / this.a;
				}
				else if (num3 < yMin)
				{
					num3 = yMin;
					num4 = (this.c - num3) / this.a;
				}
			}
			this._clippedVertices = new Dictionary<Side, Vector2?>();
			if (vertex == this._leftVertex)
			{
				this._clippedVertices[Side.LEFT] = new Vector2?(new Vector2(num2, num));
				this._clippedVertices[Side.RIGHT] = new Vector2?(new Vector2(num4, num3));
				return;
			}
			this._clippedVertices[Side.RIGHT] = new Vector2?(new Vector2(num2, num));
			this._clippedVertices[Side.LEFT] = new Vector2?(new Vector2(num4, num3));
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002A090 File Offset: 0x00028290
		public void ClipVertices(Polygon bounds)
		{
			LineSegment lineSegment = new LineSegment(null, null);
			bool flag = (double)this.a == 1.0 && (double)this.b >= 0.0;
			if (flag)
			{
				lineSegment.p0 = new Vector2?(this._rightVertex.Coord);
				lineSegment.p1 = new Vector2?(this._leftVertex.Coord);
			}
			else
			{
				lineSegment.p0 = new Vector2?(this._leftVertex.Coord);
				lineSegment.p1 = new Vector2?(this._rightVertex.Coord);
			}
			LineSegment lineSegment2 = new LineSegment(null, null);
			bounds.ClipSegment(lineSegment, ref lineSegment2);
			this._clippedVertices = new Dictionary<Side, Vector2?>();
			if (!flag)
			{
				this._clippedVertices[Side.LEFT] = lineSegment2.p0;
				this._clippedVertices[Side.RIGHT] = lineSegment2.p1;
				return;
			}
			this._clippedVertices[Side.RIGHT] = lineSegment2.p0;
			this._clippedVertices[Side.LEFT] = lineSegment2.p1;
		}

		// Token: 0x040006FB RID: 1787
		private static Stack<Edge> _pool = new Stack<Edge>();

		// Token: 0x040006FC RID: 1788
		private static int _nedges = 0;

		// Token: 0x040006FD RID: 1789
		public static readonly Edge DELETED = new Edge();

		// Token: 0x040006FE RID: 1790
		public float a;

		// Token: 0x040006FF RID: 1791
		public float b;

		// Token: 0x04000700 RID: 1792
		public float c;

		// Token: 0x04000701 RID: 1793
		private Vertex _leftVertex;

		// Token: 0x04000702 RID: 1794
		private Vertex _rightVertex;

		// Token: 0x04000703 RID: 1795
		private Dictionary<Side, Vector2?> _clippedVertices;

		// Token: 0x04000704 RID: 1796
		private Dictionary<Side, Site> _sites;

		// Token: 0x04000705 RID: 1797
		private int _edgeIndex;
	}
}

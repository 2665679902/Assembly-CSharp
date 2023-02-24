using System;
using System.Collections.Generic;
using Delaunay.LR;
using UnityEngine;

namespace Delaunay
{
	// Token: 0x0200014E RID: 334
	public sealed class Vertex : ICoord
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x0002BB14 File Offset: 0x00029D14
		private static Vertex Create(float x, float y)
		{
			if (float.IsNaN(x) || float.IsNaN(y))
			{
				return Vertex.VERTEX_AT_INFINITY;
			}
			if (Vertex._pool.Count > 0)
			{
				return Vertex._pool.Pop().Init(x, y);
			}
			return new Vertex(x, y);
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0002BB52 File Offset: 0x00029D52
		public Vector2 Coord
		{
			get
			{
				return this._coord;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002BB5A File Offset: 0x00029D5A
		public int vertexIndex
		{
			get
			{
				return this._vertexIndex;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002BB62 File Offset: 0x00029D62
		public Vertex(float x, float y)
		{
			this.Init(x, y);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002BB73 File Offset: 0x00029D73
		private Vertex Init(float x, float y)
		{
			this._coord = new Vector2(x, y);
			return this;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002BB83 File Offset: 0x00029D83
		public void Dispose()
		{
			Vertex._pool.Push(this);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002BB90 File Offset: 0x00029D90
		public void SetIndex()
		{
			this._vertexIndex = Vertex._nvertices++;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002BBA5 File Offset: 0x00029DA5
		public override string ToString()
		{
			return "Vertex (" + this._vertexIndex.ToString() + ")";
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002BBC4 File Offset: 0x00029DC4
		public static Vertex Intersect(Halfedge halfedge0, Halfedge halfedge1)
		{
			Edge edge = halfedge0.edge;
			Edge edge2 = halfedge1.edge;
			if (edge == null || edge2 == null)
			{
				return null;
			}
			if (edge.rightSite == edge2.rightSite)
			{
				return null;
			}
			float num = edge.a * edge2.b - edge.b * edge2.a;
			if (-1E-10 < (double)num && (double)num < 1E-10)
			{
				return null;
			}
			float num2 = (edge.c * edge2.b - edge2.c * edge.b) / num;
			float num3 = (edge2.c * edge.a - edge.c * edge2.a) / num;
			Halfedge halfedge2;
			Edge edge3;
			if (Voronoi.CompareByYThenX(edge.rightSite, edge2.rightSite) < 0)
			{
				halfedge2 = halfedge0;
				edge3 = edge;
			}
			else
			{
				halfedge2 = halfedge1;
				edge3 = edge2;
			}
			bool flag = num2 >= edge3.rightSite.x;
			if (flag)
			{
				Side? side = halfedge2.leftRight;
				Side side2 = Side.LEFT;
				if ((side.GetValueOrDefault() == side2) & (side != null))
				{
					goto IL_11B;
				}
			}
			if (!flag)
			{
				Side? side = halfedge2.leftRight;
				Side side2 = Side.RIGHT;
				if ((side.GetValueOrDefault() == side2) & (side != null))
				{
					goto IL_11B;
				}
			}
			return Vertex.Create(num2, num3);
			IL_11B:
			return null;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0002BCF7 File Offset: 0x00029EF7
		public float x
		{
			get
			{
				return this._coord.x;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0002BD04 File Offset: 0x00029F04
		public float y
		{
			get
			{
				return this._coord.y;
			}
		}

		// Token: 0x0400072D RID: 1837
		public static readonly Vertex VERTEX_AT_INFINITY = new Vertex(float.NaN, float.NaN);

		// Token: 0x0400072E RID: 1838
		private static Stack<Vertex> _pool = new Stack<Vertex>();

		// Token: 0x0400072F RID: 1839
		private static int _nvertices = 0;

		// Token: 0x04000730 RID: 1840
		private Vector2 _coord;

		// Token: 0x04000731 RID: 1841
		private int _vertexIndex;
	}
}

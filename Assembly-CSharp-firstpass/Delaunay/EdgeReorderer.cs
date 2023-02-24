using System;
using System.Collections.Generic;
using Delaunay.LR;
using Delaunay.Utils;

namespace Delaunay
{
	// Token: 0x02000147 RID: 327
	internal sealed class EdgeReorderer : Delaunay.Utils.IDisposable
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0002A48D File Offset: 0x0002868D
		public List<Edge> edges
		{
			get
			{
				return this._edges;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002A495 File Offset: 0x00028695
		public List<Side> edgeOrientations
		{
			get
			{
				return this._edgeOrientations;
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002A49D File Offset: 0x0002869D
		public EdgeReorderer(List<Edge> origEdges, VertexOrSite criterion)
		{
			this._edges = new List<Edge>();
			this._edgeOrientations = new List<Side>();
			if (origEdges.Count > 0)
			{
				this._edges = this.ReorderEdges(origEdges, criterion);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002A4D2 File Offset: 0x000286D2
		public void Dispose()
		{
			this._edges = null;
			this._edgeOrientations = null;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002A4E4 File Offset: 0x000286E4
		private List<Edge> ReorderEdges(List<Edge> origEdges, VertexOrSite criterion)
		{
			int count = origEdges.Count;
			bool[] array = new bool[count];
			int i = 0;
			for (int j = 0; j < count; j++)
			{
				array[j] = false;
			}
			List<Edge> list = new List<Edge>();
			int k = 0;
			Edge edge = origEdges[k];
			list.Add(edge);
			this._edgeOrientations.Add(Side.LEFT);
			ICoord coord2;
			if (criterion != VertexOrSite.VERTEX)
			{
				ICoord coord = edge.leftSite;
				coord2 = coord;
			}
			else
			{
				ICoord coord = edge.leftVertex;
				coord2 = coord;
			}
			ICoord coord3 = coord2;
			ICoord coord4;
			if (criterion != VertexOrSite.VERTEX)
			{
				ICoord coord = edge.rightSite;
				coord4 = coord;
			}
			else
			{
				ICoord coord = edge.rightVertex;
				coord4 = coord;
			}
			ICoord coord5 = coord4;
			if (coord3 == Vertex.VERTEX_AT_INFINITY || coord5 == Vertex.VERTEX_AT_INFINITY)
			{
				return new List<Edge>();
			}
			array[k] = true;
			i++;
			while (i < count)
			{
				for (k = 1; k < count; k++)
				{
					if (!array[k])
					{
						edge = origEdges[k];
						ICoord coord6;
						if (criterion != VertexOrSite.VERTEX)
						{
							ICoord coord = edge.leftSite;
							coord6 = coord;
						}
						else
						{
							ICoord coord = edge.leftVertex;
							coord6 = coord;
						}
						ICoord coord7 = coord6;
						ICoord coord8;
						if (criterion != VertexOrSite.VERTEX)
						{
							ICoord coord = edge.rightSite;
							coord8 = coord;
						}
						else
						{
							ICoord coord = edge.rightVertex;
							coord8 = coord;
						}
						ICoord coord9 = coord8;
						if (coord7 == Vertex.VERTEX_AT_INFINITY || coord9 == Vertex.VERTEX_AT_INFINITY)
						{
							return new List<Edge>();
						}
						if (coord7 == coord5)
						{
							coord5 = coord9;
							this._edgeOrientations.Add(Side.LEFT);
							list.Add(edge);
							array[k] = true;
						}
						else if (coord9 == coord3)
						{
							coord3 = coord7;
							this._edgeOrientations.Insert(0, Side.LEFT);
							list.Insert(0, edge);
							array[k] = true;
						}
						else if (coord7 == coord3)
						{
							coord3 = coord9;
							this._edgeOrientations.Insert(0, Side.RIGHT);
							list.Insert(0, edge);
							array[k] = true;
						}
						else if (coord9 == coord5)
						{
							coord5 = coord7;
							this._edgeOrientations.Add(Side.RIGHT);
							list.Add(edge);
							array[k] = true;
						}
						if (array[k])
						{
							i++;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0400070F RID: 1807
		private List<Edge> _edges;

		// Token: 0x04000710 RID: 1808
		private List<Side> _edgeOrientations;
	}
}

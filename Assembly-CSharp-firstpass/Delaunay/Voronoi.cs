using System;
using System.Collections.Generic;
using Delaunay.Geo;
using Delaunay.LR;
using Delaunay.Utils;
using UnityEngine;

namespace Delaunay
{
	// Token: 0x0200014F RID: 335
	public sealed class Voronoi : Delaunay.Utils.IDisposable
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0002BD37 File Offset: 0x00029F37
		public Rect plotBounds
		{
			get
			{
				return this._plotBounds;
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002BD40 File Offset: 0x00029F40
		public void Dispose()
		{
			if (this._sites != null)
			{
				this._sites.Dispose();
				this._sites = null;
			}
			if (this._triangles != null)
			{
				int num = this._triangles.Count;
				for (int i = 0; i < num; i++)
				{
					this._triangles[i].Dispose();
				}
				this._triangles.Clear();
				this._triangles = null;
			}
			if (this._edges != null)
			{
				int num = this._edges.Count;
				for (int i = 0; i < num; i++)
				{
					this._edges[i].Dispose();
				}
				this._edges.Clear();
				this._edges = null;
			}
			this._sitesIndexedByLocation = null;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0002BDF4 File Offset: 0x00029FF4
		public Voronoi(List<Vector2> points, List<uint> colors, List<float> weights, Rect plotBounds)
		{
			this._plotBounds = plotBounds;
			this.min_weight = float.MaxValue;
			this.max_weight = float.MinValue;
			this._sites = new SiteList();
			this._sitesIndexedByLocation = new Dictionary<Vector2, Site>();
			this._triangles = new List<Triangle>();
			this._edges = new List<Edge>();
			this.AddSites(points, colors, weights);
			float num = this.max_weight - this.min_weight;
			if (num > 0f)
			{
				this._sites.ScaleWeight(1f + num);
			}
			this.FortunesAlgorithm();
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002BE88 File Offset: 0x0002A088
		private void AddSites(List<Vector2> points, List<uint> colors, List<float> weights)
		{
			this.weightSum = 0f;
			for (int i = 0; i < points.Count; i++)
			{
				this.AddSite(points[i], (colors != null) ? colors[i] : 0U, i, (weights == null) ? 1f : weights[i]);
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002BEE0 File Offset: 0x0002A0E0
		private void AddSite(Vector2 p, uint color, int index, float weight = 1f)
		{
			if (this._sitesIndexedByLocation.ContainsKey(p))
			{
				return;
			}
			Site site = Site.Create(p, (uint)index, weight, color);
			this.min_weight = Mathf.Min(this.min_weight, weight);
			this.max_weight = Mathf.Max(this.max_weight, weight);
			this._sitesIndexedByLocation[p] = site;
			this._sites.Add(site);
			this.weightSum += site.weight;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002BF5A File Offset: 0x0002A15A
		public Site GetSiteByLocation(Vector2 p)
		{
			return this._sitesIndexedByLocation[p];
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002BF68 File Offset: 0x0002A168
		public List<Edge> Edges()
		{
			return this._edges;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002BF70 File Offset: 0x0002A170
		public List<Vector2> Region(Vector2 p)
		{
			Site site = this._sitesIndexedByLocation[p];
			if (site == null)
			{
				return new List<Vector2>();
			}
			return site.Region(this._plotBounds);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		public List<Vector2> NeighborSitesForSite(Vector2 coord)
		{
			List<Vector2> list = new List<Vector2>();
			Site site = this._sitesIndexedByLocation[coord];
			if (site == null)
			{
				return list;
			}
			List<Site> list2 = site.NeighborSites();
			for (int i = 0; i < list2.Count; i++)
			{
				Site site2 = list2[i];
				list.Add(site2.Coord);
			}
			return list;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
		public HashSet<uint> NeighborSitesIDsForSite(Vector2 coord)
		{
			HashSet<uint> hashSet = new HashSet<uint>();
			Site site = this._sitesIndexedByLocation[coord];
			if (site == null)
			{
				return hashSet;
			}
			List<Site> list = site.NeighborSites();
			for (int i = 0; i < list.Count; i++)
			{
				hashSet.Add(list[i].color);
			}
			return hashSet;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002C04C File Offset: 0x0002A24C
		public List<uint> ListNeighborSitesIDsForSite(Vector2 coord)
		{
			List<uint> list = new List<uint>();
			Site site = this._sitesIndexedByLocation[coord];
			if (site == null)
			{
				return list;
			}
			List<Site> list2 = site.NeighborSites();
			for (int i = 0; i < list2.Count; i++)
			{
				list.Add(list2[i].color);
			}
			return list;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002C09C File Offset: 0x0002A29C
		public List<Circle> Circles()
		{
			return this._sites.Circles();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002C0A9 File Offset: 0x0002A2A9
		public List<LineSegment> VoronoiBoundaryForSite(Vector2 coord)
		{
			return DelaunayHelpers.VisibleLineSegments(DelaunayHelpers.SelectEdgesForSitePoint(coord, this._edges));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002C0BC File Offset: 0x0002A2BC
		public List<LineSegment> DelaunayLinesForSite(Vector2 coord)
		{
			return DelaunayHelpers.DelaunayLinesForEdges(DelaunayHelpers.SelectEdgesForSitePoint(coord, this._edges));
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002C0CF File Offset: 0x0002A2CF
		public List<LineSegment> VoronoiDiagram()
		{
			return DelaunayHelpers.VisibleLineSegments(this._edges);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002C0DC File Offset: 0x0002A2DC
		public List<LineSegment> DelaunayTriangulation()
		{
			return DelaunayHelpers.DelaunayLinesForEdges(DelaunayHelpers.SelectNonIntersectingEdges(this._edges));
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002C0EE File Offset: 0x0002A2EE
		public List<LineSegment> Hull()
		{
			return DelaunayHelpers.DelaunayLinesForEdges(this.HullEdges());
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002C0FB File Offset: 0x0002A2FB
		private List<Edge> HullEdges()
		{
			return this._edges.FindAll((Edge edge) => edge.IsPartOfConvexHull());
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002C128 File Offset: 0x0002A328
		public List<Vector2> HullPointsInOrder()
		{
			List<Edge> list = this.HullEdges();
			List<Vector2> list2 = new List<Vector2>();
			if (list.Count == 0)
			{
				return list2;
			}
			EdgeReorderer edgeReorderer = new EdgeReorderer(list, VertexOrSite.SITE);
			list = edgeReorderer.edges;
			List<Side> edgeOrientations = edgeReorderer.edgeOrientations;
			edgeReorderer.Dispose();
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				Edge edge = list[i];
				Side side = edgeOrientations[i];
				list2.Add(edge.Site(side).Coord);
			}
			return list2;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002C1A6 File Offset: 0x0002A3A6
		public List<LineSegment> SpanningTree(KruskalType type = KruskalType.MINIMUM)
		{
			return DelaunayHelpers.Kruskal(DelaunayHelpers.DelaunayLinesForEdges(DelaunayHelpers.SelectNonIntersectingEdges(this._edges)), type);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002C1BE File Offset: 0x0002A3BE
		public List<List<Vector2>> Regions()
		{
			return this._sites.Regions(this._plotBounds);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002C1D1 File Offset: 0x0002A3D1
		public List<uint> SiteColors()
		{
			return this._sites.SiteColors();
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002C1DE File Offset: 0x0002A3DE
		public List<Vector2> SiteCoords()
		{
			return this._sites.SiteCoords();
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002C1EC File Offset: 0x0002A3EC
		private void FortunesAlgorithm()
		{
			Vector2 vector = Vector2.zero;
			Rect sitesBounds = this._sites.GetSitesBounds();
			int num = (int)Mathf.Sqrt((float)(this._sites.Count + 4));
			HalfedgePriorityQueue halfedgePriorityQueue = new HalfedgePriorityQueue(sitesBounds.y, sitesBounds.height, num);
			EdgeList edgeList = new EdgeList(sitesBounds.x, sitesBounds.width, num);
			List<Halfedge> list = new List<Halfedge>();
			List<Vertex> list2 = new List<Vertex>();
			this.fortunesAlgorithm_bottomMostSite = this._sites.Next();
			Site site = this._sites.Next();
			for (;;)
			{
				if (!halfedgePriorityQueue.Empty())
				{
					vector = halfedgePriorityQueue.Min();
				}
				if (site != null && (halfedgePriorityQueue.Empty() || Voronoi.CompareByYThenX(site, vector) < 0))
				{
					Halfedge halfedge = edgeList.EdgeListLeftNeighbor(site.Coord);
					Halfedge halfedge2 = halfedge.edgeListRightNeighbor;
					Site site2 = this.FortunesAlgorithm_rightRegion(halfedge);
					Edge edge = Edge.CreateBisectingEdge(site2, site);
					this._edges.Add(edge);
					Halfedge halfedge3 = Halfedge.Create(edge, new Side?(Side.LEFT));
					list.Add(halfedge3);
					edgeList.Insert(halfedge, halfedge3);
					Vertex vertex;
					if ((vertex = Vertex.Intersect(halfedge, halfedge3)) != null)
					{
						list2.Add(vertex);
						halfedgePriorityQueue.Remove(halfedge);
						halfedge.vertex = vertex;
						halfedge.ystar = vertex.y + site.Dist(vertex);
						halfedgePriorityQueue.Insert(halfedge);
					}
					halfedge = halfedge3;
					halfedge3 = Halfedge.Create(edge, new Side?(Side.RIGHT));
					list.Add(halfedge3);
					edgeList.Insert(halfedge, halfedge3);
					if ((vertex = Vertex.Intersect(halfedge3, halfedge2)) != null)
					{
						list2.Add(vertex);
						halfedge3.vertex = vertex;
						halfedge3.ystar = vertex.y + site.Dist(vertex);
						halfedgePriorityQueue.Insert(halfedge3);
					}
					site = this._sites.Next();
				}
				else
				{
					if (halfedgePriorityQueue.Empty())
					{
						break;
					}
					Halfedge halfedge = halfedgePriorityQueue.ExtractMin();
					Halfedge edgeListLeftNeighbor = halfedge.edgeListLeftNeighbor;
					Halfedge halfedge2 = halfedge.edgeListRightNeighbor;
					Halfedge edgeListRightNeighbor = halfedge2.edgeListRightNeighbor;
					Site site2 = this.FortunesAlgorithm_leftRegion(halfedge);
					Site site3 = this.FortunesAlgorithm_rightRegion(halfedge2);
					Vertex vertex2 = halfedge.vertex;
					vertex2.SetIndex();
					halfedge.edge.SetVertex(halfedge.leftRight.Value, vertex2);
					halfedge2.edge.SetVertex(halfedge2.leftRight.Value, vertex2);
					edgeList.Remove(halfedge);
					halfedgePriorityQueue.Remove(halfedge2);
					edgeList.Remove(halfedge2);
					Side side = Side.LEFT;
					if (site2.y > site3.y)
					{
						Site site4 = site2;
						site2 = site3;
						site3 = site4;
						side = Side.RIGHT;
					}
					Edge edge = Edge.CreateBisectingEdge(site2, site3);
					this._edges.Add(edge);
					Halfedge halfedge3 = Halfedge.Create(edge, new Side?(side));
					list.Add(halfedge3);
					edgeList.Insert(edgeListLeftNeighbor, halfedge3);
					edge.SetVertex(SideHelper.Other(side), vertex2);
					Vertex vertex;
					if ((vertex = Vertex.Intersect(edgeListLeftNeighbor, halfedge3)) != null)
					{
						list2.Add(vertex);
						halfedgePriorityQueue.Remove(edgeListLeftNeighbor);
						edgeListLeftNeighbor.vertex = vertex;
						edgeListLeftNeighbor.ystar = vertex.y + site2.Dist(vertex);
						halfedgePriorityQueue.Insert(edgeListLeftNeighbor);
					}
					if ((vertex = Vertex.Intersect(halfedge3, edgeListRightNeighbor)) != null)
					{
						list2.Add(vertex);
						halfedge3.vertex = vertex;
						halfedge3.ystar = vertex.y + site2.Dist(vertex);
						halfedgePriorityQueue.Insert(halfedge3);
					}
				}
			}
			halfedgePriorityQueue.Dispose();
			edgeList.Dispose();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].ReallyDispose();
			}
			list.Clear();
			for (int j = 0; j < this._edges.Count; j++)
			{
				Edge edge = this._edges[j];
				edge.ClipVertices(this._plotBounds);
			}
			for (int k = 0; k < list2.Count; k++)
			{
				Vertex vertex = list2[k];
				vertex.Dispose();
			}
			list2.Clear();
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002C5F4 File Offset: 0x0002A7F4
		private Site FortunesAlgorithm_leftRegion(Halfedge he)
		{
			Edge edge = he.edge;
			if (edge == null)
			{
				return this.fortunesAlgorithm_bottomMostSite;
			}
			return edge.Site(he.leftRight.Value);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002C624 File Offset: 0x0002A824
		private Site FortunesAlgorithm_rightRegion(Halfedge he)
		{
			Edge edge = he.edge;
			if (edge == null)
			{
				return this.fortunesAlgorithm_bottomMostSite;
			}
			return edge.Site(SideHelper.Other(he.leftRight.Value));
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002C658 File Offset: 0x0002A858
		public static int CompareByYThenX(Site s1, Site s2)
		{
			if (s1.y < s2.y)
			{
				return -1;
			}
			if (s1.y > s2.y)
			{
				return 1;
			}
			if (s1.x < s2.x)
			{
				return -1;
			}
			if (s1.x > s2.x)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002C6A8 File Offset: 0x0002A8A8
		public static int CompareByYThenX(Site s1, Vector2 s2)
		{
			if (s1.y < s2.y)
			{
				return -1;
			}
			if (s1.y > s2.y)
			{
				return 1;
			}
			if (s1.x < s2.x)
			{
				return -1;
			}
			if (s1.x > s2.x)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04000732 RID: 1842
		private SiteList _sites;

		// Token: 0x04000733 RID: 1843
		private Dictionary<Vector2, Site> _sitesIndexedByLocation;

		// Token: 0x04000734 RID: 1844
		private List<Triangle> _triangles;

		// Token: 0x04000735 RID: 1845
		private List<Edge> _edges;

		// Token: 0x04000736 RID: 1846
		private float min_weight;

		// Token: 0x04000737 RID: 1847
		private float max_weight;

		// Token: 0x04000738 RID: 1848
		private Rect _plotBounds;

		// Token: 0x04000739 RID: 1849
		private float weightSum;

		// Token: 0x0400073A RID: 1850
		private Site fortunesAlgorithm_bottomMostSite;
	}
}

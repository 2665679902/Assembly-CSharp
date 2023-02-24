using System;
using System.Collections.Generic;
using Delaunay.Geo;
using Delaunay.LR;
using UnityEngine;

namespace Delaunay
{
	// Token: 0x02000143 RID: 323
	public static class DelaunayHelpers
	{
		// Token: 0x06000AC1 RID: 2753 RVA: 0x000295F4 File Offset: 0x000277F4
		public static List<LineSegment> VisibleLineSegments(List<Edge> edges)
		{
			List<LineSegment> list = new List<LineSegment>();
			for (int i = 0; i < edges.Count; i++)
			{
				Edge edge = edges[i];
				if (edge.visible)
				{
					Vector2? vector = edge.clippedEnds[Side.LEFT];
					Vector2? vector2 = edge.clippedEnds[Side.RIGHT];
					list.Add(new LineSegment(vector, vector2));
				}
			}
			return list;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00029654 File Offset: 0x00027854
		public static List<DelaunayHelpers.LineSegmentWithSites> VisibleLineSegmentsWithSite(List<Edge> edges)
		{
			List<DelaunayHelpers.LineSegmentWithSites> list = new List<DelaunayHelpers.LineSegmentWithSites>();
			for (int i = 0; i < edges.Count; i++)
			{
				Edge edge = edges[i];
				if (edge.visible)
				{
					Vector2? vector = edge.clippedEnds[Side.LEFT];
					Vector2? vector2 = edge.clippedEnds[Side.RIGHT];
					list.Add(new DelaunayHelpers.LineSegmentWithSites(vector, vector2, edge.leftSite.color, edge.rightSite.color));
				}
			}
			return list;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000296C8 File Offset: 0x000278C8
		public static List<Edge> SelectEdgesForSitePoint(Vector2 coord, List<Edge> edgesToTest)
		{
			return edgesToTest.FindAll((Edge edge) => (edge.leftSite != null && edge.leftSite.Coord == coord) || (edge.rightSite != null && edge.rightSite.Coord == coord));
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000296F4 File Offset: 0x000278F4
		public static List<Edge> SelectNonIntersectingEdges(List<Edge> edgesToTest)
		{
			return edgesToTest;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000296F8 File Offset: 0x000278F8
		public static List<LineSegment> DelaunayLinesForEdges(List<Edge> edges)
		{
			List<LineSegment> list = new List<LineSegment>();
			for (int i = 0; i < edges.Count; i++)
			{
				Edge edge = edges[i];
				list.Add(edge.DelaunayLine());
			}
			return list;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00029734 File Offset: 0x00027934
		public static List<LineSegment> Kruskal(List<LineSegment> lineSegments, KruskalType type = KruskalType.MINIMUM)
		{
			Dictionary<Vector2?, Node> dictionary = new Dictionary<Vector2?, Node>();
			List<LineSegment> list = new List<LineSegment>();
			Stack<Node> pool = Node.pool;
			if (type == KruskalType.MAXIMUM)
			{
				lineSegments.Sort((LineSegment l1, LineSegment l2) => LineSegment.CompareLengths(l1, l2));
			}
			else
			{
				lineSegments.Sort((LineSegment l1, LineSegment l2) => LineSegment.CompareLengths_MAX(l1, l2));
			}
			int num = lineSegments.Count;
			while (--num > -1)
			{
				LineSegment lineSegment = lineSegments[num];
				Node node2;
				if (!dictionary.ContainsKey(lineSegment.p0))
				{
					Node node = ((pool.Count > 0) ? pool.Pop() : new Node());
					node2 = (node.parent = node);
					node.treeSize = 1;
					dictionary[lineSegment.p0] = node;
				}
				else
				{
					Node node = dictionary[lineSegment.p0];
					node2 = DelaunayHelpers.Find(node);
				}
				Node node4;
				if (!dictionary.ContainsKey(lineSegment.p1))
				{
					Node node3 = ((pool.Count > 0) ? pool.Pop() : new Node());
					node4 = (node3.parent = node3);
					node3.treeSize = 1;
					dictionary[lineSegment.p1] = node3;
				}
				else
				{
					Node node3 = dictionary[lineSegment.p1];
					node4 = DelaunayHelpers.Find(node3);
				}
				if (node2 != node4)
				{
					list.Add(lineSegment);
					int treeSize = node2.treeSize;
					int treeSize2 = node4.treeSize;
					if (treeSize >= treeSize2)
					{
						node4.parent = node2;
						node2.treeSize += treeSize2;
					}
					else
					{
						node2.parent = node4;
						node4.treeSize += treeSize;
					}
				}
			}
			foreach (Node node5 in dictionary.Values)
			{
				pool.Push(node5);
			}
			return list;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00029940 File Offset: 0x00027B40
		private static Node Find(Node node)
		{
			if (node.parent == node)
			{
				return node;
			}
			Node node2 = DelaunayHelpers.Find(node.parent);
			node.parent = node2;
			return node2;
		}

		// Token: 0x02000A11 RID: 2577
		public class LineSegmentWithSites : LineSegment
		{
			// Token: 0x17000E4F RID: 3663
			// (get) Token: 0x06005445 RID: 21573 RVA: 0x0009CFEA File Offset: 0x0009B1EA
			// (set) Token: 0x06005446 RID: 21574 RVA: 0x0009CFF2 File Offset: 0x0009B1F2
			public uint id0 { get; private set; }

			// Token: 0x17000E50 RID: 3664
			// (get) Token: 0x06005447 RID: 21575 RVA: 0x0009CFFB File Offset: 0x0009B1FB
			// (set) Token: 0x06005448 RID: 21576 RVA: 0x0009D003 File Offset: 0x0009B203
			public uint id1 { get; private set; }

			// Token: 0x06005449 RID: 21577 RVA: 0x0009D00C File Offset: 0x0009B20C
			public LineSegmentWithSites(Vector2? p0, Vector2? p1, uint id0, uint id1)
				: base(p0, p1)
			{
				this.id0 = id0;
				this.id1 = id1;
			}
		}
	}
}

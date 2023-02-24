using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000276 RID: 630
	public sealed class RedirectedGraph : IGraph, IArcLookup
	{
		// Token: 0x06001344 RID: 4932 RVA: 0x0004AACB File Offset: 0x00048CCB
		public RedirectedGraph(IGraph graph, Func<Arc, RedirectedGraph.Direction> getDirection)
		{
			this.graph = graph;
			this.getDirection = getDirection;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0004AAE1 File Offset: 0x00048CE1
		public Node U(Arc arc)
		{
			if (this.getDirection(arc) != RedirectedGraph.Direction.Backward)
			{
				return this.graph.U(arc);
			}
			return this.graph.V(arc);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0004AB0B File Offset: 0x00048D0B
		public Node V(Arc arc)
		{
			if (this.getDirection(arc) != RedirectedGraph.Direction.Backward)
			{
				return this.graph.V(arc);
			}
			return this.graph.U(arc);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0004AB35 File Offset: 0x00048D35
		public bool IsEdge(Arc arc)
		{
			return this.getDirection(arc) == RedirectedGraph.Direction.Edge;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0004AB46 File Offset: 0x00048D46
		public IEnumerable<Node> Nodes()
		{
			return this.graph.Nodes();
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0004AB53 File Offset: 0x00048D53
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			if (filter != ArcFilter.All)
			{
				return from x in this.graph.Arcs(ArcFilter.All)
					where this.getDirection(x) == RedirectedGraph.Direction.Edge
					select x;
			}
			return this.graph.Arcs(ArcFilter.All);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0004AB84 File Offset: 0x00048D84
		private IEnumerable<Arc> FilterArcs(Node u, IEnumerable<Arc> arcs, ArcFilter filter)
		{
			switch (filter)
			{
			case ArcFilter.All:
				return arcs;
			case ArcFilter.Edge:
				return arcs.Where((Arc x) => this.getDirection(x) == RedirectedGraph.Direction.Edge);
			case ArcFilter.Forward:
				return arcs.Where(delegate(Arc x)
				{
					RedirectedGraph.Direction direction = this.getDirection(x);
					if (direction != RedirectedGraph.Direction.Forward)
					{
						return direction != RedirectedGraph.Direction.Backward || this.V(x) == u;
					}
					return this.U(x) == u;
				});
			default:
				return arcs.Where(delegate(Arc x)
				{
					RedirectedGraph.Direction direction2 = this.getDirection(x);
					if (direction2 != RedirectedGraph.Direction.Forward)
					{
						return direction2 != RedirectedGraph.Direction.Backward || this.U(x) == u;
					}
					return this.V(x) == u;
				});
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0004ABF3 File Offset: 0x00048DF3
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.FilterArcs(u, this.graph.Arcs(u, ArcFilter.All), filter);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0004AC0A File Offset: 0x00048E0A
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.FilterArcs(u, this.graph.Arcs(u, v, ArcFilter.All), filter);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0004AC22 File Offset: 0x00048E22
		public int NodeCount()
		{
			return this.graph.NodeCount();
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0004AC2F File Offset: 0x00048E2F
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			if (filter != ArcFilter.All)
			{
				return this.Arcs(filter).Count<Arc>();
			}
			return this.graph.ArcCount(ArcFilter.All);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0004AC4D File Offset: 0x00048E4D
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, filter).Count<Arc>();
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0004AC5C File Offset: 0x00048E5C
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, v, filter).Count<Arc>();
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0004AC6C File Offset: 0x00048E6C
		public bool HasNode(Node node)
		{
			return this.graph.HasNode(node);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0004AC7A File Offset: 0x00048E7A
		public bool HasArc(Arc arc)
		{
			return this.graph.HasArc(arc);
		}

		// Token: 0x040009F2 RID: 2546
		private IGraph graph;

		// Token: 0x040009F3 RID: 2547
		private Func<Arc, RedirectedGraph.Direction> getDirection;

		// Token: 0x02000A9E RID: 2718
		public enum Direction
		{
			// Token: 0x0400246D RID: 9325
			Forward,
			// Token: 0x0400246E RID: 9326
			Backward,
			// Token: 0x0400246F RID: 9327
			Edge
		}
	}
}

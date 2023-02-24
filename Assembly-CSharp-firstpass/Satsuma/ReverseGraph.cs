using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000277 RID: 631
	public sealed class ReverseGraph : IGraph, IArcLookup
	{
		// Token: 0x06001354 RID: 4948 RVA: 0x0004AC99 File Offset: 0x00048E99
		public static ArcFilter Reverse(ArcFilter filter)
		{
			if (filter == ArcFilter.Forward)
			{
				return ArcFilter.Backward;
			}
			if (filter == ArcFilter.Backward)
			{
				return ArcFilter.Forward;
			}
			return filter;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0004ACA8 File Offset: 0x00048EA8
		public ReverseGraph(IGraph graph)
		{
			this.graph = graph;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0004ACB7 File Offset: 0x00048EB7
		public Node U(Arc arc)
		{
			return this.graph.V(arc);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0004ACC5 File Offset: 0x00048EC5
		public Node V(Arc arc)
		{
			return this.graph.U(arc);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004ACD3 File Offset: 0x00048ED3
		public bool IsEdge(Arc arc)
		{
			return this.graph.IsEdge(arc);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004ACE1 File Offset: 0x00048EE1
		public IEnumerable<Node> Nodes()
		{
			return this.graph.Nodes();
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004ACEE File Offset: 0x00048EEE
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			return this.graph.Arcs(filter);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004ACFC File Offset: 0x00048EFC
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.graph.Arcs(u, ReverseGraph.Reverse(filter));
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004AD10 File Offset: 0x00048F10
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.graph.Arcs(u, v, ReverseGraph.Reverse(filter));
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0004AD25 File Offset: 0x00048F25
		public int NodeCount()
		{
			return this.graph.NodeCount();
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0004AD32 File Offset: 0x00048F32
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			return this.graph.ArcCount(filter);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0004AD40 File Offset: 0x00048F40
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.graph.ArcCount(u, ReverseGraph.Reverse(filter));
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0004AD54 File Offset: 0x00048F54
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.graph.ArcCount(u, v, ReverseGraph.Reverse(filter));
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0004AD69 File Offset: 0x00048F69
		public bool HasNode(Node node)
		{
			return this.graph.HasNode(node);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0004AD77 File Offset: 0x00048F77
		public bool HasArc(Arc arc)
		{
			return this.graph.HasArc(arc);
		}

		// Token: 0x040009F4 RID: 2548
		private IGraph graph;
	}
}

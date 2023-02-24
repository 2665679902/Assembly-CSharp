using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000283 RID: 643
	public sealed class UndirectedGraph : IGraph, IArcLookup
	{
		// Token: 0x060013CF RID: 5071 RVA: 0x0004C5FD File Offset: 0x0004A7FD
		public UndirectedGraph(IGraph graph)
		{
			this.graph = graph;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0004C60C File Offset: 0x0004A80C
		public Node U(Arc arc)
		{
			return this.graph.U(arc);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0004C61A File Offset: 0x0004A81A
		public Node V(Arc arc)
		{
			return this.graph.V(arc);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0004C628 File Offset: 0x0004A828
		public bool IsEdge(Arc arc)
		{
			return true;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0004C62B File Offset: 0x0004A82B
		public IEnumerable<Node> Nodes()
		{
			return this.graph.Nodes();
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0004C638 File Offset: 0x0004A838
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			return this.graph.Arcs(ArcFilter.All);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004C646 File Offset: 0x0004A846
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.graph.Arcs(u, ArcFilter.All);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0004C655 File Offset: 0x0004A855
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.graph.Arcs(u, v, ArcFilter.All);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004C665 File Offset: 0x0004A865
		public int NodeCount()
		{
			return this.graph.NodeCount();
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004C672 File Offset: 0x0004A872
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			return this.graph.ArcCount(ArcFilter.All);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004C680 File Offset: 0x0004A880
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.graph.ArcCount(u, ArcFilter.All);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0004C68F File Offset: 0x0004A88F
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.graph.ArcCount(u, v, ArcFilter.All);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0004C69F File Offset: 0x0004A89F
		public bool HasNode(Node node)
		{
			return this.graph.HasNode(node);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0004C6AD File Offset: 0x0004A8AD
		public bool HasArc(Arc arc)
		{
			return this.graph.HasArc(arc);
		}

		// Token: 0x04000A25 RID: 2597
		private IGraph graph;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000267 RID: 615
	public sealed class Matching : IMatching, IGraph, IArcLookup, IClearable
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00047D57 File Offset: 0x00045F57
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x00047D5F File Offset: 0x00045F5F
		public IGraph Graph { get; private set; }

		// Token: 0x06001288 RID: 4744 RVA: 0x00047D68 File Offset: 0x00045F68
		public Matching(IGraph graph)
		{
			this.Graph = graph;
			this.matchedArc = new Dictionary<Node, Arc>();
			this.arcs = new HashSet<Arc>();
			this.Clear();
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00047D93 File Offset: 0x00045F93
		public void Clear()
		{
			this.matchedArc.Clear();
			this.arcs.Clear();
			this.edgeCount = 0;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00047DB4 File Offset: 0x00045FB4
		public void Enable(Arc arc, bool enabled)
		{
			if (enabled == this.arcs.Contains(arc))
			{
				return;
			}
			Node node = this.Graph.U(arc);
			Node node2 = this.Graph.V(arc);
			if (enabled)
			{
				if (node == node2)
				{
					throw new ArgumentException("Matchings cannot have loop arcs.");
				}
				if (this.matchedArc.ContainsKey(node))
				{
					string text = "Node is already matched: ";
					Node node3 = node;
					throw new ArgumentException(text + node3.ToString());
				}
				if (this.matchedArc.ContainsKey(node2))
				{
					string text2 = "Node is already matched: ";
					Node node3 = node2;
					throw new ArgumentException(text2 + node3.ToString());
				}
				this.matchedArc[node] = arc;
				this.matchedArc[node2] = arc;
				this.arcs.Add(arc);
				if (this.Graph.IsEdge(arc))
				{
					this.edgeCount++;
					return;
				}
			}
			else
			{
				this.matchedArc.Remove(node);
				this.matchedArc.Remove(node2);
				this.arcs.Remove(arc);
				if (this.Graph.IsEdge(arc))
				{
					this.edgeCount--;
				}
			}
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00047EE8 File Offset: 0x000460E8
		public Arc MatchedArc(Node node)
		{
			Arc arc;
			if (!this.matchedArc.TryGetValue(node, out arc))
			{
				return Arc.Invalid;
			}
			return arc;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00047F0C File Offset: 0x0004610C
		public Node U(Arc arc)
		{
			return this.Graph.U(arc);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00047F1A File Offset: 0x0004611A
		public Node V(Arc arc)
		{
			return this.Graph.V(arc);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00047F28 File Offset: 0x00046128
		public bool IsEdge(Arc arc)
		{
			return this.Graph.IsEdge(arc);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00047F36 File Offset: 0x00046136
		public IEnumerable<Node> Nodes()
		{
			return this.matchedArc.Keys;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00047F43 File Offset: 0x00046143
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			if (filter == ArcFilter.All)
			{
				return this.arcs;
			}
			if (this.edgeCount == 0)
			{
				return Enumerable.Empty<Arc>();
			}
			return this.arcs.Where((Arc arc) => this.IsEdge(arc));
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00047F74 File Offset: 0x00046174
		private bool YieldArc(Node u, ArcFilter filter, Arc arc)
		{
			return filter == ArcFilter.All || this.IsEdge(arc) || (filter == ArcFilter.Forward && this.U(arc) == u) || (filter == ArcFilter.Backward && this.V(arc) == u);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00047FAA File Offset: 0x000461AA
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			Arc arc = this.MatchedArc(u);
			if (arc != Arc.Invalid && this.YieldArc(u, filter, arc))
			{
				yield return arc;
			}
			yield break;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00047FC8 File Offset: 0x000461C8
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			if (u != v)
			{
				Arc arc = this.MatchedArc(u);
				if (arc != Arc.Invalid && arc == this.MatchedArc(v) && this.YieldArc(u, filter, arc))
				{
					yield return arc;
				}
			}
			yield break;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00047FED File Offset: 0x000461ED
		public int NodeCount()
		{
			return this.matchedArc.Count;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00047FFA File Offset: 0x000461FA
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			if (filter != ArcFilter.All)
			{
				return this.edgeCount;
			}
			return this.arcs.Count;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00048014 File Offset: 0x00046214
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			Arc arc = this.MatchedArc(u);
			if (!(arc != Arc.Invalid) || !this.YieldArc(u, filter, arc))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00048044 File Offset: 0x00046244
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			if (!(u != v))
			{
				return 0;
			}
			Arc arc = this.MatchedArc(u);
			if (!(arc != Arc.Invalid) || !(arc == this.MatchedArc(v)) || !this.YieldArc(u, filter, arc))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0004808E File Offset: 0x0004628E
		public bool HasNode(Node node)
		{
			return this.Graph.HasNode(node) && this.matchedArc.ContainsKey(node);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000480AC File Offset: 0x000462AC
		public bool HasArc(Arc arc)
		{
			return this.Graph.HasArc(arc) && this.arcs.Contains(arc);
		}

		// Token: 0x040009AE RID: 2478
		private readonly Dictionary<Node, Arc> matchedArc;

		// Token: 0x040009AF RID: 2479
		private readonly HashSet<Arc> arcs;

		// Token: 0x040009B0 RID: 2480
		private int edgeCount;
	}
}

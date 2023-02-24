using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000254 RID: 596
	public sealed class ContractedGraph : IGraph, IArcLookup
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x00047143 File Offset: 0x00045343
		public ContractedGraph(IGraph graph)
		{
			this.graph = graph;
			this.nodeGroups = new DisjointSet<Node>();
			this.Reset();
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00047163 File Offset: 0x00045363
		public void Reset()
		{
			this.nodeGroups.Clear();
			this.unionCount = 0;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00047178 File Offset: 0x00045378
		public Node Merge(Node u, Node v)
		{
			DisjointSetSet<Node> disjointSetSet = this.nodeGroups.WhereIs(u);
			DisjointSetSet<Node> disjointSetSet2 = this.nodeGroups.WhereIs(v);
			if (disjointSetSet.Equals(disjointSetSet2))
			{
				return disjointSetSet.Representative;
			}
			this.unionCount++;
			return this.nodeGroups.Union(disjointSetSet, disjointSetSet2).Representative;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x000471D4 File Offset: 0x000453D4
		public Node Contract(Arc arc)
		{
			return this.Merge(this.graph.U(arc), this.graph.V(arc));
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000471F4 File Offset: 0x000453F4
		public Node U(Arc arc)
		{
			return this.nodeGroups.WhereIs(this.graph.U(arc)).Representative;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00047220 File Offset: 0x00045420
		public Node V(Arc arc)
		{
			return this.nodeGroups.WhereIs(this.graph.V(arc)).Representative;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0004724C File Offset: 0x0004544C
		public bool IsEdge(Arc arc)
		{
			return this.graph.IsEdge(arc);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0004725A File Offset: 0x0004545A
		public IEnumerable<Node> Nodes()
		{
			foreach (Node node in this.graph.Nodes())
			{
				if (this.nodeGroups.WhereIs(node).Representative == node)
				{
					yield return node;
				}
			}
			IEnumerator<Node> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0004726A File Offset: 0x0004546A
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			return this.graph.Arcs(filter);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00047278 File Offset: 0x00045478
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			DisjointSetSet<Node> disjointSetSet = this.nodeGroups.WhereIs(u);
			foreach (Node node in this.nodeGroups.Elements(disjointSetSet))
			{
				foreach (Arc arc in this.graph.Arcs(node, filter))
				{
					if (!(this.U(arc) == this.V(arc)) || (filter != ArcFilter.All && !this.IsEdge(arc)) || this.graph.U(arc) == node)
					{
						yield return arc;
					}
				}
				IEnumerator<Arc> enumerator2 = null;
			}
			IEnumerator<Node> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00047296 File Offset: 0x00045496
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			foreach (Arc arc in this.Arcs(u, filter))
			{
				if (this.Other(arc, u) == v)
				{
					yield return arc;
				}
			}
			IEnumerator<Arc> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000472BB File Offset: 0x000454BB
		public int NodeCount()
		{
			return this.graph.NodeCount() - this.unionCount;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000472CF File Offset: 0x000454CF
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			return this.graph.ArcCount(filter);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000472DD File Offset: 0x000454DD
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, filter).Count<Arc>();
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000472EC File Offset: 0x000454EC
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, v, filter).Count<Arc>();
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000472FC File Offset: 0x000454FC
		public bool HasNode(Node node)
		{
			return node == this.nodeGroups.WhereIs(node).Representative;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00047323 File Offset: 0x00045523
		public bool HasArc(Arc arc)
		{
			return this.graph.HasArc(arc);
		}

		// Token: 0x0400098D RID: 2445
		private IGraph graph;

		// Token: 0x0400098E RID: 2446
		private DisjointSet<Node> nodeGroups;

		// Token: 0x0400098F RID: 2447
		private int unionCount;
	}
}

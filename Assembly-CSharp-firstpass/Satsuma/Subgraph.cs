using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x0200027A RID: 634
	public sealed class Subgraph : IGraph, IArcLookup
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x0004B267 File Offset: 0x00049467
		public Subgraph(IGraph graph)
		{
			this.graph = graph;
			this.EnableAllNodes(true);
			this.EnableAllArcs(true);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0004B29A File Offset: 0x0004949A
		public void EnableAllNodes(bool enabled)
		{
			this.defaultNodeEnabled = enabled;
			this.nodeExceptions.Clear();
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0004B2AE File Offset: 0x000494AE
		public void EnableAllArcs(bool enabled)
		{
			this.defaultArcEnabled = enabled;
			this.arcExceptions.Clear();
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004B2C2 File Offset: 0x000494C2
		public void Enable(Node node, bool enabled)
		{
			if (this.defaultNodeEnabled != enabled)
			{
				this.nodeExceptions.Add(node);
				return;
			}
			this.nodeExceptions.Remove(node);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0004B2ED File Offset: 0x000494ED
		public void Enable(Arc arc, bool enabled)
		{
			if (this.defaultArcEnabled != enabled)
			{
				this.arcExceptions.Add(arc);
				return;
			}
			this.arcExceptions.Remove(arc);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0004B318 File Offset: 0x00049518
		public bool IsEnabled(Node node)
		{
			return this.defaultNodeEnabled ^ this.nodeExceptions.Contains(node);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004B32D File Offset: 0x0004952D
		public bool IsEnabled(Arc arc)
		{
			return this.defaultArcEnabled ^ this.arcExceptions.Contains(arc);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0004B342 File Offset: 0x00049542
		public Node U(Arc arc)
		{
			return this.graph.U(arc);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0004B350 File Offset: 0x00049550
		public Node V(Arc arc)
		{
			return this.graph.V(arc);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0004B35E File Offset: 0x0004955E
		public bool IsEdge(Arc arc)
		{
			return this.graph.IsEdge(arc);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004B36C File Offset: 0x0004956C
		private IEnumerable<Node> NodesInternal()
		{
			foreach (Node node in this.graph.Nodes())
			{
				if (this.IsEnabled(node))
				{
					yield return node;
				}
			}
			IEnumerator<Node> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0004B37C File Offset: 0x0004957C
		public IEnumerable<Node> Nodes()
		{
			if (this.nodeExceptions.Count != 0)
			{
				return this.NodesInternal();
			}
			if (this.defaultNodeEnabled)
			{
				return this.graph.Nodes();
			}
			return Enumerable.Empty<Node>();
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0004B3AB File Offset: 0x000495AB
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			foreach (Arc arc in this.graph.Arcs(filter))
			{
				if (this.IsEnabled(arc) && this.IsEnabled(this.graph.U(arc)) && this.IsEnabled(this.graph.V(arc)))
				{
					yield return arc;
				}
			}
			IEnumerator<Arc> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0004B3C2 File Offset: 0x000495C2
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			if (!this.IsEnabled(u))
			{
				yield break;
			}
			foreach (Arc arc in this.graph.Arcs(u, filter))
			{
				if (this.IsEnabled(arc) && this.IsEnabled(this.graph.Other(arc, u)))
				{
					yield return arc;
				}
			}
			IEnumerator<Arc> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0004B3E0 File Offset: 0x000495E0
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			if (!this.IsEnabled(u) || !this.IsEnabled(v))
			{
				yield break;
			}
			foreach (Arc arc in this.graph.Arcs(u, v, filter))
			{
				if (this.IsEnabled(arc))
				{
					yield return arc;
				}
			}
			IEnumerator<Arc> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004B405 File Offset: 0x00049605
		public int NodeCount()
		{
			if (!this.defaultNodeEnabled)
			{
				return this.nodeExceptions.Count;
			}
			return this.graph.NodeCount() - this.nodeExceptions.Count;
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004B434 File Offset: 0x00049634
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			if (this.nodeExceptions.Count != 0 || filter != ArcFilter.All)
			{
				return this.Arcs(filter).Count<Arc>();
			}
			if (!this.defaultNodeEnabled)
			{
				return 0;
			}
			if (!this.defaultArcEnabled)
			{
				return this.arcExceptions.Count;
			}
			return this.graph.ArcCount(ArcFilter.All) - this.arcExceptions.Count;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004B494 File Offset: 0x00049694
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, filter).Count<Arc>();
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004B4A3 File Offset: 0x000496A3
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, v, filter).Count<Arc>();
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0004B4B3 File Offset: 0x000496B3
		public bool HasNode(Node node)
		{
			return this.graph.HasNode(node) && this.IsEnabled(node);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0004B4CC File Offset: 0x000496CC
		public bool HasArc(Arc arc)
		{
			return this.graph.HasArc(arc) && this.IsEnabled(arc);
		}

		// Token: 0x04000A00 RID: 2560
		private IGraph graph;

		// Token: 0x04000A01 RID: 2561
		private bool defaultNodeEnabled;

		// Token: 0x04000A02 RID: 2562
		private HashSet<Node> nodeExceptions = new HashSet<Node>();

		// Token: 0x04000A03 RID: 2563
		private bool defaultArcEnabled;

		// Token: 0x04000A04 RID: 2564
		private HashSet<Arc> arcExceptions = new HashSet<Arc>();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x0200026E RID: 622
	public sealed class Path : IPath, IGraph, IArcLookup, IClearable
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00049476 File Offset: 0x00047676
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x0004947E File Offset: 0x0004767E
		public IGraph Graph { get; private set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00049487 File Offset: 0x00047687
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x0004948F File Offset: 0x0004768F
		public Node FirstNode { get; private set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00049498 File Offset: 0x00047698
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x000494A0 File Offset: 0x000476A0
		public Node LastNode { get; private set; }

		// Token: 0x060012DA RID: 4826 RVA: 0x000494A9 File Offset: 0x000476A9
		public Path(IGraph graph)
		{
			this.Graph = graph;
			this.nextArc = new Dictionary<Node, Arc>();
			this.prevArc = new Dictionary<Node, Arc>();
			this.arcs = new HashSet<Arc>();
			this.Clear();
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000494E0 File Offset: 0x000476E0
		public void Clear()
		{
			this.FirstNode = Node.Invalid;
			this.LastNode = Node.Invalid;
			this.nodeCount = 0;
			this.nextArc.Clear();
			this.prevArc.Clear();
			this.arcs.Clear();
			this.edgeCount = 0;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00049534 File Offset: 0x00047734
		public void Begin(Node node)
		{
			if (this.nodeCount > 0)
			{
				throw new InvalidOperationException("Path not empty.");
			}
			this.nodeCount = 1;
			this.LastNode = node;
			this.FirstNode = node;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0004956C File Offset: 0x0004776C
		public void AddFirst(Arc arc)
		{
			Node node = this.U(arc);
			Node node2 = this.V(arc);
			Node node3 = ((node == this.FirstNode) ? node2 : node);
			if ((node != this.FirstNode && node2 != this.FirstNode) || this.nextArc.ContainsKey(node3) || this.prevArc.ContainsKey(this.FirstNode))
			{
				throw new ArgumentException("Arc not valid or path is a cycle.");
			}
			if (node3 != this.LastNode)
			{
				this.nodeCount++;
			}
			this.nextArc[node3] = arc;
			this.prevArc[this.FirstNode] = arc;
			if (!this.arcs.Contains(arc))
			{
				this.arcs.Add(arc);
				if (this.IsEdge(arc))
				{
					this.edgeCount++;
				}
			}
			this.FirstNode = node3;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00049658 File Offset: 0x00047858
		public void AddLast(Arc arc)
		{
			Node node = this.U(arc);
			Node node2 = this.V(arc);
			Node node3 = ((node == this.LastNode) ? node2 : node);
			if ((node != this.LastNode && node2 != this.LastNode) || this.nextArc.ContainsKey(this.LastNode) || this.prevArc.ContainsKey(node3))
			{
				throw new ArgumentException("Arc not valid or path is a cycle.");
			}
			if (node3 != this.FirstNode)
			{
				this.nodeCount++;
			}
			this.nextArc[this.LastNode] = arc;
			this.prevArc[node3] = arc;
			if (!this.arcs.Contains(arc))
			{
				this.arcs.Add(arc);
				if (this.IsEdge(arc))
				{
					this.edgeCount++;
				}
			}
			this.LastNode = node3;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00049744 File Offset: 0x00047944
		public void Reverse()
		{
			Node firstNode = this.FirstNode;
			this.FirstNode = this.LastNode;
			this.LastNode = firstNode;
			Dictionary<Node, Arc> dictionary = this.nextArc;
			this.nextArc = this.prevArc;
			this.prevArc = dictionary;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00049788 File Offset: 0x00047988
		public Arc NextArc(Node node)
		{
			Arc arc;
			if (!this.nextArc.TryGetValue(node, out arc))
			{
				return Arc.Invalid;
			}
			return arc;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000497AC File Offset: 0x000479AC
		public Arc PrevArc(Node node)
		{
			Arc arc;
			if (!this.prevArc.TryGetValue(node, out arc))
			{
				return Arc.Invalid;
			}
			return arc;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000497D0 File Offset: 0x000479D0
		public Node U(Arc arc)
		{
			return this.Graph.U(arc);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000497DE File Offset: 0x000479DE
		public Node V(Arc arc)
		{
			return this.Graph.V(arc);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000497EC File Offset: 0x000479EC
		public bool IsEdge(Arc arc)
		{
			return this.Graph.IsEdge(arc);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000497FA File Offset: 0x000479FA
		public IEnumerable<Node> Nodes()
		{
			Node i = this.FirstNode;
			if (i == Node.Invalid)
			{
				yield break;
			}
			for (;;)
			{
				yield return i;
				Arc arc = this.NextArc(i);
				if (arc == Arc.Invalid)
				{
					break;
				}
				i = this.Graph.Other(arc, i);
				if (i == this.FirstNode)
				{
					goto Block_3;
				}
			}
			yield break;
			Block_3:
			yield break;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0004980A File Offset: 0x00047A0A
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

		// Token: 0x060012E7 RID: 4839 RVA: 0x0004983B File Offset: 0x00047A3B
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.ArcsHelper(u, filter);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00049848 File Offset: 0x00047A48
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return from arc in this.Arcs(u, filter)
				where this.Other(arc, u) == v
				select arc;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0004988E File Offset: 0x00047A8E
		public int NodeCount()
		{
			return this.nodeCount;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00049896 File Offset: 0x00047A96
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			if (filter != ArcFilter.All)
			{
				return this.edgeCount;
			}
			return this.arcs.Count;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000498AD File Offset: 0x00047AAD
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, filter).Count<Arc>();
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x000498BC File Offset: 0x00047ABC
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, v, filter).Count<Arc>();
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x000498CC File Offset: 0x00047ACC
		public bool HasNode(Node node)
		{
			return this.prevArc.ContainsKey(node) || (node != Node.Invalid && node == this.FirstNode);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000498F9 File Offset: 0x00047AF9
		public bool HasArc(Arc arc)
		{
			return this.arcs.Contains(arc);
		}

		// Token: 0x040009D4 RID: 2516
		private int nodeCount;

		// Token: 0x040009D5 RID: 2517
		private Dictionary<Node, Arc> nextArc;

		// Token: 0x040009D6 RID: 2518
		private Dictionary<Node, Arc> prevArc;

		// Token: 0x040009D7 RID: 2519
		private HashSet<Arc> arcs;

		// Token: 0x040009D8 RID: 2520
		private int edgeCount;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x0200026F RID: 623
	public sealed class PathGraph : IPath, IGraph, IArcLookup
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00049910 File Offset: 0x00047B10
		public Node FirstNode
		{
			get
			{
				if (this.nodeCount <= 0)
				{
					return Node.Invalid;
				}
				return new Node(1L);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00049928 File Offset: 0x00047B28
		public Node LastNode
		{
			get
			{
				if (this.nodeCount <= 0)
				{
					return Node.Invalid;
				}
				return new Node((long)(this.isCycle ? 1 : this.nodeCount));
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00049950 File Offset: 0x00047B50
		public PathGraph(int nodeCount, PathGraph.Topology topology, Directedness directedness)
		{
			this.nodeCount = nodeCount;
			this.isCycle = topology == PathGraph.Topology.Cycle;
			this.directed = directedness == Directedness.Directed;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00049973 File Offset: 0x00047B73
		public Node GetNode(int index)
		{
			return new Node(1L + (long)index);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0004997F File Offset: 0x00047B7F
		public int GetNodeIndex(Node node)
		{
			return (int)(node.Id - 1L);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0004998C File Offset: 0x00047B8C
		public Arc NextArc(Node node)
		{
			if (!this.isCycle && node.Id == (long)this.nodeCount)
			{
				return Arc.Invalid;
			}
			return new Arc(node.Id);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000499B8 File Offset: 0x00047BB8
		public Arc PrevArc(Node node)
		{
			if (node.Id != 1L)
			{
				return new Arc(node.Id - 1L);
			}
			if (!this.isCycle)
			{
				return Arc.Invalid;
			}
			return new Arc((long)this.nodeCount);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000499EF File Offset: 0x00047BEF
		public Node U(Arc arc)
		{
			return new Node(arc.Id);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000499FD File Offset: 0x00047BFD
		public Node V(Arc arc)
		{
			return new Node((arc.Id == (long)this.nodeCount) ? 1L : (arc.Id + 1L));
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00049A22 File Offset: 0x00047C22
		public bool IsEdge(Arc arc)
		{
			return !this.directed;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00049A2D File Offset: 0x00047C2D
		public IEnumerable<Node> Nodes()
		{
			int num;
			for (int i = 1; i <= this.nodeCount; i = num + 1)
			{
				yield return new Node((long)i);
				num = i;
			}
			yield break;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00049A3D File Offset: 0x00047C3D
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			if (this.directed && filter == ArcFilter.Edge)
			{
				yield break;
			}
			int i = 1;
			int j = this.ArcCountInternal();
			while (i <= j)
			{
				yield return new Arc((long)i);
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00049A54 File Offset: 0x00047C54
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.ArcsHelper(u, filter);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00049A60 File Offset: 0x00047C60
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return from arc in this.Arcs(u, filter)
				where this.Other(arc, u) == v
				select arc;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00049AA6 File Offset: 0x00047CA6
		public int NodeCount()
		{
			return this.nodeCount;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00049AAE File Offset: 0x00047CAE
		private int ArcCountInternal()
		{
			if (this.nodeCount == 0)
			{
				return 0;
			}
			if (!this.isCycle)
			{
				return this.nodeCount - 1;
			}
			return this.nodeCount;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00049AD1 File Offset: 0x00047CD1
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			if (!this.directed || filter != ArcFilter.Edge)
			{
				return this.ArcCountInternal();
			}
			return 0;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00049AE7 File Offset: 0x00047CE7
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, filter).Count<Arc>();
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00049AF6 File Offset: 0x00047CF6
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			return this.Arcs(u, v, filter).Count<Arc>();
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00049B06 File Offset: 0x00047D06
		public bool HasNode(Node node)
		{
			return node.Id >= 1L && node.Id <= (long)this.nodeCount;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00049B28 File Offset: 0x00047D28
		public bool HasArc(Arc arc)
		{
			return arc.Id >= 1L && arc.Id <= (long)this.ArcCountInternal();
		}

		// Token: 0x040009D9 RID: 2521
		private readonly int nodeCount;

		// Token: 0x040009DA RID: 2522
		private readonly bool isCycle;

		// Token: 0x040009DB RID: 2523
		private readonly bool directed;

		// Token: 0x02000A97 RID: 2711
		public enum Topology
		{
			// Token: 0x04002450 RID: 9296
			Path,
			// Token: 0x04002451 RID: 9297
			Cycle
		}
	}
}

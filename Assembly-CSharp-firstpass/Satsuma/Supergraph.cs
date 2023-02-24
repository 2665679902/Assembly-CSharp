using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x0200027B RID: 635
	public class Supergraph : IBuildableGraph, IClearable, IDestroyableGraph, IGraph, IArcLookup
	{
		// Token: 0x0600138F RID: 5007 RVA: 0x0004B4E8 File Offset: 0x000496E8
		public Supergraph(IGraph graph)
		{
			this.graph = graph;
			this.nodeAllocator = new Supergraph.NodeAllocator
			{
				Parent = this
			};
			this.arcAllocator = new Supergraph.ArcAllocator
			{
				Parent = this
			};
			this.nodes = new HashSet<Node>();
			this.arcs = new HashSet<Arc>();
			this.arcProperties = new Dictionary<Arc, Supergraph.ArcProperties>();
			this.edges = new HashSet<Arc>();
			this.nodeArcs_All = new Dictionary<Node, List<Arc>>();
			this.nodeArcs_Edge = new Dictionary<Node, List<Arc>>();
			this.nodeArcs_Forward = new Dictionary<Node, List<Arc>>();
			this.nodeArcs_Backward = new Dictionary<Node, List<Arc>>();
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0004B580 File Offset: 0x00049780
		public void Clear()
		{
			this.nodeAllocator.Rewind();
			this.arcAllocator.Rewind();
			this.nodes.Clear();
			this.arcs.Clear();
			this.arcProperties.Clear();
			this.edges.Clear();
			this.nodeArcs_All.Clear();
			this.nodeArcs_Edge.Clear();
			this.nodeArcs_Forward.Clear();
			this.nodeArcs_Backward.Clear();
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0004B5FC File Offset: 0x000497FC
		public Node AddNode()
		{
			if (this.NodeCount() == 2147483647)
			{
				throw new InvalidOperationException("Error: too many nodes!");
			}
			Node node = new Node(this.nodeAllocator.Allocate());
			this.nodes.Add(node);
			return node;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0004B644 File Offset: 0x00049844
		public Arc AddArc(Node u, Node v, Directedness directedness)
		{
			if (this.ArcCount(ArcFilter.All) == 2147483647)
			{
				throw new InvalidOperationException("Error: too many arcs!");
			}
			Arc arc = new Arc(this.arcAllocator.Allocate());
			this.arcs.Add(arc);
			bool flag = directedness == Directedness.Undirected;
			this.arcProperties[arc] = new Supergraph.ArcProperties(u, v, flag);
			Utils.MakeEntry<Node, List<Arc>>(this.nodeArcs_All, u).Add(arc);
			Utils.MakeEntry<Node, List<Arc>>(this.nodeArcs_Forward, u).Add(arc);
			Utils.MakeEntry<Node, List<Arc>>(this.nodeArcs_Backward, v).Add(arc);
			if (flag)
			{
				this.edges.Add(arc);
				Utils.MakeEntry<Node, List<Arc>>(this.nodeArcs_Edge, u).Add(arc);
			}
			if (v != u)
			{
				Utils.MakeEntry<Node, List<Arc>>(this.nodeArcs_All, v).Add(arc);
				if (flag)
				{
					Utils.MakeEntry<Node, List<Arc>>(this.nodeArcs_Edge, v).Add(arc);
					Utils.MakeEntry<Node, List<Arc>>(this.nodeArcs_Forward, v).Add(arc);
					Utils.MakeEntry<Node, List<Arc>>(this.nodeArcs_Backward, u).Add(arc);
				}
			}
			return arc;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0004B750 File Offset: 0x00049950
		public bool DeleteNode(Node node)
		{
			if (!this.nodes.Remove(node))
			{
				return false;
			}
			Func<Arc, bool> func = (Arc a) => this.U(a) == node || this.V(a) == node;
			Utils.RemoveAll<Arc>(this.arcs, func);
			Utils.RemoveAll<Arc>(this.edges, func);
			Utils.RemoveAll<Arc, Supergraph.ArcProperties>(this.arcProperties, func);
			this.nodeArcs_All.Remove(node);
			this.nodeArcs_Edge.Remove(node);
			this.nodeArcs_Forward.Remove(node);
			this.nodeArcs_Backward.Remove(node);
			return true;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004B800 File Offset: 0x00049A00
		public bool DeleteArc(Arc arc)
		{
			if (!this.arcs.Remove(arc))
			{
				return false;
			}
			Supergraph.ArcProperties arcProperties = this.arcProperties[arc];
			this.arcProperties.Remove(arc);
			Utils.RemoveLast<Arc>(this.nodeArcs_All[arcProperties.U], arc);
			Utils.RemoveLast<Arc>(this.nodeArcs_Forward[arcProperties.U], arc);
			Utils.RemoveLast<Arc>(this.nodeArcs_Backward[arcProperties.V], arc);
			if (arcProperties.IsEdge)
			{
				this.edges.Remove(arc);
				Utils.RemoveLast<Arc>(this.nodeArcs_Edge[arcProperties.U], arc);
			}
			if (arcProperties.V != arcProperties.U)
			{
				Utils.RemoveLast<Arc>(this.nodeArcs_All[arcProperties.V], arc);
				if (arcProperties.IsEdge)
				{
					Utils.RemoveLast<Arc>(this.nodeArcs_Edge[arcProperties.V], arc);
					Utils.RemoveLast<Arc>(this.nodeArcs_Forward[arcProperties.V], arc);
					Utils.RemoveLast<Arc>(this.nodeArcs_Backward[arcProperties.U], arc);
				}
			}
			return true;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0004B920 File Offset: 0x00049B20
		public Node U(Arc arc)
		{
			Supergraph.ArcProperties arcProperties;
			if (this.arcProperties.TryGetValue(arc, out arcProperties))
			{
				return arcProperties.U;
			}
			return this.graph.U(arc);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0004B950 File Offset: 0x00049B50
		public Node V(Arc arc)
		{
			Supergraph.ArcProperties arcProperties;
			if (this.arcProperties.TryGetValue(arc, out arcProperties))
			{
				return arcProperties.V;
			}
			return this.graph.V(arc);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0004B980 File Offset: 0x00049B80
		public bool IsEdge(Arc arc)
		{
			Supergraph.ArcProperties arcProperties;
			if (this.arcProperties.TryGetValue(arc, out arcProperties))
			{
				return arcProperties.IsEdge;
			}
			return this.graph.IsEdge(arc);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004B9B0 File Offset: 0x00049BB0
		private HashSet<Arc> ArcsInternal(ArcFilter filter)
		{
			if (filter != ArcFilter.All)
			{
				return this.edges;
			}
			return this.arcs;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004B9C4 File Offset: 0x00049BC4
		private List<Arc> ArcsInternal(Node v, ArcFilter filter)
		{
			List<Arc> list;
			switch (filter)
			{
			case ArcFilter.All:
				this.nodeArcs_All.TryGetValue(v, out list);
				break;
			case ArcFilter.Edge:
				this.nodeArcs_Edge.TryGetValue(v, out list);
				break;
			case ArcFilter.Forward:
				this.nodeArcs_Forward.TryGetValue(v, out list);
				break;
			default:
				this.nodeArcs_Backward.TryGetValue(v, out list);
				break;
			}
			return list ?? Supergraph.EmptyArcList;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004BA34 File Offset: 0x00049C34
		public IEnumerable<Node> Nodes()
		{
			if (this.graph != null)
			{
				return this.nodes.Concat(this.graph.Nodes());
			}
			return this.nodes;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004BA68 File Offset: 0x00049C68
		public IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All)
		{
			if (this.graph != null)
			{
				return this.ArcsInternal(filter).Concat(this.graph.Arcs(filter));
			}
			return this.ArcsInternal(filter);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004BA9F File Offset: 0x00049C9F
		public IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All)
		{
			if (this.graph == null || this.nodes.Contains(u))
			{
				return this.ArcsInternal(u, filter);
			}
			return this.ArcsInternal(u, filter).Concat(this.graph.Arcs(u, filter));
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004BADA File Offset: 0x00049CDA
		public IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			foreach (Arc arc in this.ArcsInternal(u, filter))
			{
				if (this.Other(arc, u) == v)
				{
					yield return arc;
				}
			}
			List<Arc>.Enumerator enumerator = default(List<Arc>.Enumerator);
			if (this.graph != null && !this.nodes.Contains(u) && !this.nodes.Contains(v))
			{
				foreach (Arc arc2 in this.graph.Arcs(u, v, filter))
				{
					yield return arc2;
				}
				IEnumerator<Arc> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004BAFF File Offset: 0x00049CFF
		public int NodeCount()
		{
			return this.nodes.Count + ((this.graph == null) ? 0 : this.graph.NodeCount());
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004BB23 File Offset: 0x00049D23
		public int ArcCount(ArcFilter filter = ArcFilter.All)
		{
			return this.ArcsInternal(filter).Count + ((this.graph == null) ? 0 : this.graph.ArcCount(filter));
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004BB49 File Offset: 0x00049D49
		public int ArcCount(Node u, ArcFilter filter = ArcFilter.All)
		{
			return this.ArcsInternal(u, filter).Count + ((this.graph == null || this.nodes.Contains(u)) ? 0 : this.graph.ArcCount(u, filter));
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004BB80 File Offset: 0x00049D80
		public int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All)
		{
			int num = 0;
			foreach (Arc arc in this.ArcsInternal(u, filter))
			{
				if (this.Other(arc, u) == v)
				{
					num++;
				}
			}
			return num + ((this.graph == null || this.nodes.Contains(u) || this.nodes.Contains(v)) ? 0 : this.graph.ArcCount(u, v, filter));
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004BC1C File Offset: 0x00049E1C
		public bool HasNode(Node node)
		{
			return this.nodes.Contains(node) || (this.graph != null && this.graph.HasNode(node));
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004BC44 File Offset: 0x00049E44
		public bool HasArc(Arc arc)
		{
			return this.arcs.Contains(arc) || (this.graph != null && this.graph.HasArc(arc));
		}

		// Token: 0x04000A05 RID: 2565
		private IGraph graph;

		// Token: 0x04000A06 RID: 2566
		private Supergraph.NodeAllocator nodeAllocator;

		// Token: 0x04000A07 RID: 2567
		private Supergraph.ArcAllocator arcAllocator;

		// Token: 0x04000A08 RID: 2568
		private HashSet<Node> nodes;

		// Token: 0x04000A09 RID: 2569
		private HashSet<Arc> arcs;

		// Token: 0x04000A0A RID: 2570
		private Dictionary<Arc, Supergraph.ArcProperties> arcProperties;

		// Token: 0x04000A0B RID: 2571
		private HashSet<Arc> edges;

		// Token: 0x04000A0C RID: 2572
		private Dictionary<Node, List<Arc>> nodeArcs_All;

		// Token: 0x04000A0D RID: 2573
		private Dictionary<Node, List<Arc>> nodeArcs_Edge;

		// Token: 0x04000A0E RID: 2574
		private Dictionary<Node, List<Arc>> nodeArcs_Forward;

		// Token: 0x04000A0F RID: 2575
		private Dictionary<Node, List<Arc>> nodeArcs_Backward;

		// Token: 0x04000A10 RID: 2576
		private static readonly List<Arc> EmptyArcList = new List<Arc>();

		// Token: 0x02000AA4 RID: 2724
		private class NodeAllocator : IdAllocator
		{
			// Token: 0x060056D8 RID: 22232 RVA: 0x000A24FB File Offset: 0x000A06FB
			protected override bool IsAllocated(long id)
			{
				return this.Parent.HasNode(new Node(id));
			}

			// Token: 0x04002492 RID: 9362
			public Supergraph Parent;
		}

		// Token: 0x02000AA5 RID: 2725
		private class ArcAllocator : IdAllocator
		{
			// Token: 0x060056DA RID: 22234 RVA: 0x000A2516 File Offset: 0x000A0716
			protected override bool IsAllocated(long id)
			{
				return this.Parent.HasArc(new Arc(id));
			}

			// Token: 0x04002493 RID: 9363
			public Supergraph Parent;
		}

		// Token: 0x02000AA6 RID: 2726
		private class ArcProperties
		{
			// Token: 0x17000EB9 RID: 3769
			// (get) Token: 0x060056DB RID: 22235 RVA: 0x000A2529 File Offset: 0x000A0729
			// (set) Token: 0x060056DC RID: 22236 RVA: 0x000A2531 File Offset: 0x000A0731
			public Node U { get; private set; }

			// Token: 0x17000EBA RID: 3770
			// (get) Token: 0x060056DD RID: 22237 RVA: 0x000A253A File Offset: 0x000A073A
			// (set) Token: 0x060056DE RID: 22238 RVA: 0x000A2542 File Offset: 0x000A0742
			public Node V { get; private set; }

			// Token: 0x17000EBB RID: 3771
			// (get) Token: 0x060056DF RID: 22239 RVA: 0x000A254B File Offset: 0x000A074B
			// (set) Token: 0x060056E0 RID: 22240 RVA: 0x000A2553 File Offset: 0x000A0753
			public bool IsEdge { get; private set; }

			// Token: 0x060056E1 RID: 22241 RVA: 0x000A255C File Offset: 0x000A075C
			public ArcProperties(Node u, Node v, bool isEdge)
			{
				this.U = u;
				this.V = v;
				this.IsEdge = isEdge;
			}
		}
	}
}

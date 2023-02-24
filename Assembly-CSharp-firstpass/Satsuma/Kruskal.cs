using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000279 RID: 633
	public sealed class Kruskal<TCost> where TCost : IComparable<TCost>
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0004AFAC File Offset: 0x000491AC
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x0004AFB4 File Offset: 0x000491B4
		public IGraph Graph { get; private set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0004AFBD File Offset: 0x000491BD
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x0004AFC5 File Offset: 0x000491C5
		public Func<Arc, TCost> Cost { get; private set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0004AFCE File Offset: 0x000491CE
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x0004AFD6 File Offset: 0x000491D6
		public Func<Node, int> MaxDegree { get; private set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0004AFDF File Offset: 0x000491DF
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x0004AFE7 File Offset: 0x000491E7
		public HashSet<Arc> Forest { get; private set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0004AFF0 File Offset: 0x000491F0
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x0004AFF8 File Offset: 0x000491F8
		public Dictionary<Node, int> Degree { get; private set; }

		// Token: 0x06001375 RID: 4981 RVA: 0x0004B004 File Offset: 0x00049204
		public Kruskal(IGraph graph, Func<Arc, TCost> cost, Func<Node, int> maxDegree = null)
		{
			this.Graph = graph;
			this.Cost = cost;
			this.MaxDegree = maxDegree;
			this.Forest = new HashSet<Arc>();
			this.Degree = new Dictionary<Node, int>();
			foreach (Node node in this.Graph.Nodes())
			{
				this.Degree[node] = 0;
			}
			List<Arc> list = this.Graph.Arcs(ArcFilter.All).ToList<Arc>();
			list.Sort(delegate(Arc a, Arc b)
			{
				TCost tcost = this.Cost(a);
				return tcost.CompareTo(this.Cost(b));
			});
			this.arcEnumerator = list.GetEnumerator();
			this.arcsToGo = this.Graph.NodeCount() - new ConnectedComponents(this.Graph, ConnectedComponents.Flags.None).Count;
			this.components = new DisjointSet<Node>();
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0004B0F0 File Offset: 0x000492F0
		public bool Step()
		{
			if (this.arcsToGo <= 0 || this.arcEnumerator == null || !this.arcEnumerator.MoveNext())
			{
				this.arcEnumerator = null;
				return false;
			}
			this.AddArc(this.arcEnumerator.Current);
			return true;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0004B12C File Offset: 0x0004932C
		public void Run()
		{
			while (this.Step())
			{
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0004B138 File Offset: 0x00049338
		public bool AddArc(Arc arc)
		{
			Node node = this.Graph.U(arc);
			if (this.MaxDegree != null && this.Degree[node] >= this.MaxDegree(node))
			{
				return false;
			}
			DisjointSetSet<Node> disjointSetSet = this.components.WhereIs(node);
			Node node2 = this.Graph.V(arc);
			if (this.MaxDegree != null && this.Degree[node2] >= this.MaxDegree(node2))
			{
				return false;
			}
			DisjointSetSet<Node> disjointSetSet2 = this.components.WhereIs(node2);
			if (disjointSetSet == disjointSetSet2)
			{
				return false;
			}
			this.Forest.Add(arc);
			this.components.Union(disjointSetSet, disjointSetSet2);
			Dictionary<Node, int> degree = this.Degree;
			Node node3 = node;
			int num = degree[node3];
			degree[node3] = num + 1;
			Dictionary<Node, int> degree2 = this.Degree;
			node3 = node2;
			num = degree2[node3];
			degree2[node3] = num + 1;
			this.arcsToGo--;
			return true;
		}

		// Token: 0x040009FD RID: 2557
		private IEnumerator<Arc> arcEnumerator;

		// Token: 0x040009FE RID: 2558
		private int arcsToGo;

		// Token: 0x040009FF RID: 2559
		private DisjointSet<Node> components;
	}
}

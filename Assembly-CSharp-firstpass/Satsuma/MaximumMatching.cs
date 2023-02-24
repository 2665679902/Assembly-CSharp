using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000268 RID: 616
	public sealed class MaximumMatching : IClearable
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x000480D3 File Offset: 0x000462D3
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x000480DB File Offset: 0x000462DB
		public IGraph Graph { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x000480E4 File Offset: 0x000462E4
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x000480EC File Offset: 0x000462EC
		public Func<Node, bool> IsRed { get; private set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x000480F5 File Offset: 0x000462F5
		public IMatching Matching
		{
			get
			{
				return this.matching;
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000480FD File Offset: 0x000462FD
		public MaximumMatching(IGraph graph, Func<Node, bool> isRed)
		{
			this.Graph = graph;
			this.IsRed = isRed;
			this.matching = new Matching(this.Graph);
			this.unmatchedRedNodes = new HashSet<Node>();
			this.Clear();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00048138 File Offset: 0x00046338
		public void Clear()
		{
			this.matching.Clear();
			this.unmatchedRedNodes.Clear();
			foreach (Node node in this.Graph.Nodes())
			{
				if (this.IsRed(node))
				{
					this.unmatchedRedNodes.Add(node);
				}
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000481B4 File Offset: 0x000463B4
		public int GreedyGrow(int maxImprovements = 2147483647)
		{
			int num = 0;
			List<Node> list = new List<Node>();
			foreach (Node node in this.unmatchedRedNodes)
			{
				foreach (Arc arc in this.Graph.Arcs(node, ArcFilter.All))
				{
					Node node2 = this.Graph.Other(arc, node);
					if (!this.matching.HasNode(node2))
					{
						this.matching.Enable(arc, true);
						list.Add(node);
						num++;
						if (num >= maxImprovements)
						{
							goto IL_AE;
						}
						break;
					}
				}
			}
			IL_AE:
			foreach (Node node3 in list)
			{
				this.unmatchedRedNodes.Remove(node3);
			}
			return num;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x000482D4 File Offset: 0x000464D4
		public void Add(Arc arc)
		{
			if (this.matching.HasArc(arc))
			{
				return;
			}
			this.matching.Enable(arc, true);
			Node node = this.Graph.U(arc);
			this.unmatchedRedNodes.Remove(this.IsRed(node) ? node : this.Graph.V(arc));
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00048334 File Offset: 0x00046534
		private Node Traverse(Node node)
		{
			Arc arc = this.matching.MatchedArc(node);
			if (this.IsRed(node))
			{
				using (IEnumerator<Arc> enumerator = this.Graph.Arcs(node, ArcFilter.All).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Arc arc2 = enumerator.Current;
						if (arc2 != arc)
						{
							Node node2 = this.Graph.Other(arc2, node);
							if (!this.parentArc.ContainsKey(node2))
							{
								this.parentArc[node2] = arc2;
								if (!this.matching.HasNode(node2))
								{
									return node2;
								}
								Node node3 = this.Traverse(node2);
								if (node3 != Node.Invalid)
								{
									return node3;
								}
							}
						}
					}
					goto IL_F7;
				}
			}
			Node node4 = this.Graph.Other(arc, node);
			if (!this.parentArc.ContainsKey(node4))
			{
				this.parentArc[node4] = arc;
				Node node5 = this.Traverse(node4);
				if (node5 != Node.Invalid)
				{
					return node5;
				}
			}
			IL_F7:
			return Node.Invalid;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00048450 File Offset: 0x00046650
		public void Run()
		{
			List<Node> list = new List<Node>();
			this.parentArc = new Dictionary<Node, Arc>();
			foreach (Node node in this.unmatchedRedNodes)
			{
				this.parentArc.Clear();
				this.parentArc[node] = Arc.Invalid;
				Node node2 = this.Traverse(node);
				if (!(node2 == Node.Invalid))
				{
					for (;;)
					{
						Arc arc = this.parentArc[node2];
						Node node3 = this.Graph.Other(arc, node2);
						Arc arc2 = ((node3 == node) ? Arc.Invalid : this.parentArc[node3]);
						if (arc2 != Arc.Invalid)
						{
							this.matching.Enable(arc2, false);
						}
						this.matching.Enable(arc, true);
						if (arc2 == Arc.Invalid)
						{
							break;
						}
						node2 = this.Graph.Other(arc2, node3);
					}
					list.Add(node);
				}
			}
			this.parentArc = null;
			foreach (Node node4 in list)
			{
				this.unmatchedRedNodes.Remove(node4);
			}
		}

		// Token: 0x040009B3 RID: 2483
		private readonly Matching matching;

		// Token: 0x040009B4 RID: 2484
		private readonly HashSet<Node> unmatchedRedNodes;

		// Token: 0x040009B5 RID: 2485
		private Dictionary<Node, Arc> parentArc;
	}
}

using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200027E RID: 638
	public sealed class CheapestLinkTsp<TNode> : ITsp<TNode>
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0004BCE8 File Offset: 0x00049EE8
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x0004BCF0 File Offset: 0x00049EF0
		public IList<TNode> Nodes { get; private set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0004BCF9 File Offset: 0x00049EF9
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x0004BD01 File Offset: 0x00049F01
		public Func<TNode, TNode, double> Cost { get; private set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0004BD0A File Offset: 0x00049F0A
		public IEnumerable<TNode> Tour
		{
			get
			{
				return this.tour;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0004BD12 File Offset: 0x00049F12
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x0004BD1A File Offset: 0x00049F1A
		public double TourCost { get; private set; }

		// Token: 0x060013AF RID: 5039 RVA: 0x0004BD23 File Offset: 0x00049F23
		public CheapestLinkTsp(IList<TNode> nodes, Func<TNode, TNode, double> cost)
		{
			this.Nodes = nodes;
			this.Cost = cost;
			this.tour = new List<TNode>();
			this.Run();
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0004BD4C File Offset: 0x00049F4C
		private void Run()
		{
			CompleteGraph graph = new CompleteGraph(this.Nodes.Count, Directedness.Undirected);
			Func<Arc, double> func = (Arc arc) => this.Cost(this.Nodes[graph.GetNodeIndex(graph.U(arc))], this.Nodes[graph.GetNodeIndex(graph.V(arc))]);
			Kruskal<double> kruskal = new Kruskal<double>(graph, func, (Node _) => 2);
			kruskal.Run();
			Dictionary<Node, Arc> dictionary = new Dictionary<Node, Arc>();
			Dictionary<Node, Arc> dictionary2 = new Dictionary<Node, Arc>();
			foreach (Arc arc4 in kruskal.Forest)
			{
				Node node = graph.U(arc4);
				(dictionary.ContainsKey(node) ? dictionary2 : dictionary)[node] = arc4;
				Node node2 = graph.V(arc4);
				(dictionary.ContainsKey(node2) ? dictionary2 : dictionary)[node2] = arc4;
			}
			foreach (Node node3 in graph.Nodes())
			{
				if (kruskal.Degree[node3] == 1)
				{
					Arc arc2 = Arc.Invalid;
					Node node4 = node3;
					for (;;)
					{
						this.tour.Add(this.Nodes[graph.GetNodeIndex(node4)]);
						if (arc2 != Arc.Invalid && kruskal.Degree[node4] == 1)
						{
							break;
						}
						Arc arc3 = dictionary[node4];
						arc2 = ((arc3 != arc2) ? arc3 : dictionary2[node4]);
						node4 = graph.Other(arc2, node4);
					}
					this.tour.Add(this.Nodes[graph.GetNodeIndex(node3)]);
					break;
				}
			}
			this.TourCost = TspUtils.GetTourCost<TNode>(this.tour, this.Cost);
		}

		// Token: 0x04000A13 RID: 2579
		private List<TNode> tour;
	}
}

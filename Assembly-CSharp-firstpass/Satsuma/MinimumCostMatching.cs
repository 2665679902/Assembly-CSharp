using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000269 RID: 617
	public sealed class MinimumCostMatching
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x000485C8 File Offset: 0x000467C8
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x000485D0 File Offset: 0x000467D0
		public IGraph Graph { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x000485D9 File Offset: 0x000467D9
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x000485E1 File Offset: 0x000467E1
		public Func<Node, bool> IsRed { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x000485EA File Offset: 0x000467EA
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x000485F2 File Offset: 0x000467F2
		public Func<Arc, double> Cost { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x000485FB File Offset: 0x000467FB
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x00048603 File Offset: 0x00046803
		public int MinimumMatchingSize { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0004860C File Offset: 0x0004680C
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x00048614 File Offset: 0x00046814
		public int MaximumMatchingSize { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0004861D File Offset: 0x0004681D
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x00048625 File Offset: 0x00046825
		public IMatching Matching { get; private set; }

		// Token: 0x060012B2 RID: 4786 RVA: 0x0004862E File Offset: 0x0004682E
		public MinimumCostMatching(IGraph graph, Func<Node, bool> isRed, Func<Arc, double> cost, int minimumMatchingSize = 0, int maximumMatchingSize = 2147483647)
		{
			this.Graph = graph;
			this.IsRed = isRed;
			this.Cost = cost;
			this.MinimumMatchingSize = minimumMatchingSize;
			this.MaximumMatchingSize = maximumMatchingSize;
			this.Run();
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00048664 File Offset: 0x00046864
		private void Run()
		{
			Supergraph supergraph = new Supergraph(new RedirectedGraph(this.Graph, delegate(Arc x)
			{
				if (!this.IsRed(this.Graph.U(x)))
				{
					return RedirectedGraph.Direction.Backward;
				}
				return RedirectedGraph.Direction.Forward;
			}));
			Node node = supergraph.AddNode();
			Node node2 = supergraph.AddNode();
			foreach (Node node3 in this.Graph.Nodes())
			{
				if (this.IsRed(node3))
				{
					supergraph.AddArc(node, node3, Directedness.Directed);
				}
				else
				{
					supergraph.AddArc(node3, node2, Directedness.Directed);
				}
			}
			Arc reflow = supergraph.AddArc(node2, node, Directedness.Directed);
			NetworkSimplex networkSimplex = new NetworkSimplex(supergraph, (Arc x) => (long)((x == reflow) ? this.MinimumMatchingSize : 0), (Arc x) => (long)((x == reflow) ? this.MaximumMatchingSize : 1), null, delegate(Arc x)
			{
				if (!this.Graph.HasArc(x))
				{
					return 0.0;
				}
				return this.Cost(x);
			});
			networkSimplex.Run();
			if (networkSimplex.State == SimplexState.Optimal)
			{
				Matching matching = new Matching(this.Graph);
				foreach (Arc arc in networkSimplex.UpperBoundArcs.Concat(from kv in networkSimplex.Forest
					where kv.Value == 1L
					select kv.Key))
				{
					if (this.Graph.HasArc(arc))
					{
						matching.Enable(arc, true);
					}
				}
				this.Matching = matching;
			}
		}
	}
}

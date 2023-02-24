using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000282 RID: 642
	public sealed class HamiltonianCycle
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0004C454 File Offset: 0x0004A654
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x0004C45C File Offset: 0x0004A65C
		public IGraph Graph { get; private set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x0004C465 File Offset: 0x0004A665
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x0004C46D File Offset: 0x0004A66D
		public IPath Cycle { get; private set; }

		// Token: 0x060013CC RID: 5068 RVA: 0x0004C476 File Offset: 0x0004A676
		public HamiltonianCycle(IGraph graph)
		{
			this.Graph = graph;
			this.Cycle = null;
			this.Run();
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0004C494 File Offset: 0x0004A694
		private void Run()
		{
			Func<Node, Node, double> func = (Node u, Node v) => (double)(this.Graph.Arcs(u, v, ArcFilter.Forward).Any<Arc>() ? 1 : 10);
			IEnumerable<Node> enumerable = null;
			double num = (double)this.Graph.NodeCount();
			InsertionTsp<Node> insertionTsp = new InsertionTsp<Node>(this.Graph.Nodes(), func, TspSelectionRule.Farthest);
			insertionTsp.Run();
			if (insertionTsp.TourCost == num)
			{
				enumerable = insertionTsp.Tour;
			}
			else
			{
				Opt2Tsp<Node> opt2Tsp = new Opt2Tsp<Node>(func, insertionTsp.Tour, new double?(insertionTsp.TourCost));
				opt2Tsp.Run();
				if (opt2Tsp.TourCost == num)
				{
					enumerable = opt2Tsp.Tour;
				}
			}
			if (enumerable == null)
			{
				this.Cycle = null;
				return;
			}
			Path path = new Path(this.Graph);
			if (enumerable.Any<Node>())
			{
				Node node = Node.Invalid;
				foreach (Node node2 in enumerable)
				{
					if (node == Node.Invalid)
					{
						path.Begin(node2);
					}
					else
					{
						path.AddLast(this.Graph.Arcs(node, node2, ArcFilter.Forward).First<Arc>());
					}
					node = node2;
				}
				path.AddLast(this.Graph.Arcs(node, enumerable.First<Node>(), ArcFilter.Forward).First<Arc>());
			}
			this.Cycle = path;
		}
	}
}

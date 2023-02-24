using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000271 RID: 625
	public sealed class Preflow : IFlow<double>
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00049B4A File Offset: 0x00047D4A
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x00049B52 File Offset: 0x00047D52
		public IGraph Graph { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00049B5B File Offset: 0x00047D5B
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x00049B63 File Offset: 0x00047D63
		public Func<Arc, double> Capacity { get; private set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00049B6C File Offset: 0x00047D6C
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x00049B74 File Offset: 0x00047D74
		public Node Source { get; private set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00049B7D File Offset: 0x00047D7D
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00049B85 File Offset: 0x00047D85
		public Node Target { get; private set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00049B8E File Offset: 0x00047D8E
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x00049B96 File Offset: 0x00047D96
		public double FlowSize { get; private set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00049B9F File Offset: 0x00047D9F
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x00049BA7 File Offset: 0x00047DA7
		public double Error { get; private set; }

		// Token: 0x06001318 RID: 4888 RVA: 0x00049BB0 File Offset: 0x00047DB0
		public Preflow(IGraph graph, Func<Arc, double> capacity, Node source, Node target)
		{
			this.Graph = graph;
			this.Capacity = capacity;
			this.Source = source;
			this.Target = target;
			this.flow = new Dictionary<Arc, double>();
			Dijkstra dijkstra = new Dijkstra(this.Graph, (Arc a) => -this.Capacity(a), DijkstraMode.Maximum);
			dijkstra.AddSource(this.Source);
			dijkstra.RunUntilFixed(this.Target);
			double num = -dijkstra.GetDistance(this.Target);
			if (double.IsPositiveInfinity(num))
			{
				this.FlowSize = double.PositiveInfinity;
				this.Error = 0.0;
				Node node = this.Target;
				Node node2 = Node.Invalid;
				while (node != this.Source)
				{
					Arc parentArc = dijkstra.GetParentArc(node);
					this.flow[parentArc] = double.PositiveInfinity;
					node2 = this.Graph.Other(parentArc, node);
					node = node2;
				}
				return;
			}
			if (double.IsNegativeInfinity(num))
			{
				num = 0.0;
			}
			this.U = (double)this.Graph.ArcCount(ArcFilter.All) * num;
			double num2 = 0.0;
			foreach (Arc arc in this.Graph.Arcs(this.Source, ArcFilter.Forward))
			{
				if (this.Graph.Other(arc, this.Source) != this.Source)
				{
					num2 += this.Capacity(arc);
					if (num2 > this.U)
					{
						break;
					}
				}
			}
			this.U = Math.Min(this.U, num2);
			double num3 = 0.0;
			foreach (Arc arc2 in this.Graph.Arcs(this.Target, ArcFilter.Backward))
			{
				if (this.Graph.Other(arc2, this.Target) != this.Target)
				{
					num3 += this.Capacity(arc2);
					if (num3 > this.U)
					{
						break;
					}
				}
			}
			this.U = Math.Min(this.U, num3);
			Supergraph supergraph = new Supergraph(this.Graph);
			Node node3 = supergraph.AddNode();
			this.artificialArc = supergraph.AddArc(node3, this.Source, Directedness.Directed);
			this.CapacityMultiplier = Utils.LargestPowerOfTwo(9.223372036854776E+18 / this.U);
			if (this.CapacityMultiplier == 0.0)
			{
				this.CapacityMultiplier = 1.0;
			}
			IntegerPreflow integerPreflow = new IntegerPreflow(supergraph, new Func<Arc, long>(this.IntegralCapacity), node3, this.Target);
			this.FlowSize = (double)integerPreflow.FlowSize / this.CapacityMultiplier;
			this.Error = (double)this.Graph.ArcCount(ArcFilter.All) / this.CapacityMultiplier;
			foreach (KeyValuePair<Arc, long> keyValuePair in integerPreflow.NonzeroArcs)
			{
				this.flow[keyValuePair.Key] = (double)keyValuePair.Value / this.CapacityMultiplier;
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00049F20 File Offset: 0x00048120
		private long IntegralCapacity(Arc arc)
		{
			return (long)(this.CapacityMultiplier * ((arc == this.artificialArc) ? this.U : Math.Min(this.U, this.Capacity(arc))));
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00049F57 File Offset: 0x00048157
		public IEnumerable<KeyValuePair<Arc, double>> NonzeroArcs
		{
			get
			{
				return this.flow.Where((KeyValuePair<Arc, double> kv) => kv.Value != 0.0);
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00049F84 File Offset: 0x00048184
		public double Flow(Arc arc)
		{
			double num;
			this.flow.TryGetValue(arc, out num);
			return num;
		}

		// Token: 0x040009E1 RID: 2529
		private Dictionary<Arc, double> flow;

		// Token: 0x040009E3 RID: 2531
		private Arc artificialArc;

		// Token: 0x040009E4 RID: 2532
		private double U;

		// Token: 0x040009E5 RID: 2533
		private double CapacityMultiplier;
	}
}

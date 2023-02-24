using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000247 RID: 583
	public sealed class BellmanFord
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00045FB2 File Offset: 0x000441B2
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00045FBA File Offset: 0x000441BA
		public IGraph Graph { get; private set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00045FC3 File Offset: 0x000441C3
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x00045FCB File Offset: 0x000441CB
		public Func<Arc, double> Cost { get; private set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00045FD4 File Offset: 0x000441D4
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x00045FDC File Offset: 0x000441DC
		public IPath NegativeCycle { get; private set; }

		// Token: 0x06001198 RID: 4504 RVA: 0x00045FE8 File Offset: 0x000441E8
		public BellmanFord(IGraph graph, Func<Arc, double> cost, IEnumerable<Node> sources)
		{
			this.Graph = graph;
			this.Cost = cost;
			this.distance = new Dictionary<Node, double>();
			this.parentArc = new Dictionary<Node, Arc>();
			foreach (Node node in sources)
			{
				this.distance[node] = 0.0;
				this.parentArc[node] = Arc.Invalid;
			}
			this.Run();
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00046080 File Offset: 0x00044280
		private void Run()
		{
			for (int i = this.Graph.NodeCount(); i > 0; i--)
			{
				foreach (Arc arc in this.Graph.Arcs(ArcFilter.All))
				{
					Node node = this.Graph.U(arc);
					Node node2 = this.Graph.V(arc);
					double num = this.GetDistance(node);
					double num2 = this.GetDistance(node2);
					double num3 = this.Cost(arc);
					if (this.Graph.IsEdge(arc))
					{
						if (num > num2)
						{
							Node node3 = node;
							node = node2;
							node2 = node3;
							double num4 = num;
							num = num2;
							num2 = num4;
						}
						if (!double.IsPositiveInfinity(num) && num3 < 0.0)
						{
							Path path = new Path(this.Graph);
							path.Begin(node);
							path.AddLast(arc);
							path.AddLast(arc);
							this.NegativeCycle = path;
							return;
						}
					}
					if (num + num3 < num2)
					{
						this.distance[node2] = num + num3;
						this.parentArc[node2] = arc;
						if (i == 0)
						{
							Node node4 = node;
							for (int j = this.Graph.NodeCount() - 1; j > 0; j--)
							{
								node4 = this.Graph.Other(this.parentArc[node4], node4);
							}
							Path path2 = new Path(this.Graph);
							path2.Begin(node4);
							Node node5 = node4;
							do
							{
								Arc arc2 = this.parentArc[node5];
								path2.AddFirst(arc2);
								node5 = this.Graph.Other(arc2, node5);
							}
							while (!(node5 == node4));
							this.NegativeCycle = path2;
							return;
						}
					}
				}
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00046268 File Offset: 0x00044468
		public bool Reached(Node node)
		{
			return this.parentArc.ContainsKey(node);
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00046276 File Offset: 0x00044476
		public IEnumerable<Node> ReachedNodes
		{
			get
			{
				return this.parentArc.Keys;
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00046284 File Offset: 0x00044484
		public double GetDistance(Node node)
		{
			if (this.NegativeCycle != null)
			{
				throw new InvalidOperationException("A negative cycle was found.");
			}
			double num;
			if (!this.distance.TryGetValue(node, out num))
			{
				return double.PositiveInfinity;
			}
			return num;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000462C0 File Offset: 0x000444C0
		public Arc GetParentArc(Node node)
		{
			if (this.NegativeCycle != null)
			{
				throw new InvalidOperationException("A negative cycle was found.");
			}
			Arc arc;
			if (!this.parentArc.TryGetValue(node, out arc))
			{
				return Arc.Invalid;
			}
			return arc;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000462F8 File Offset: 0x000444F8
		public IPath GetPath(Node node)
		{
			if (this.NegativeCycle != null)
			{
				throw new InvalidOperationException("A negative cycle was found.");
			}
			if (!this.Reached(node))
			{
				return null;
			}
			Path path = new Path(this.Graph);
			path.Begin(node);
			for (;;)
			{
				Arc arc = this.GetParentArc(node);
				if (arc == Arc.Invalid)
				{
					break;
				}
				path.AddFirst(arc);
				node = this.Graph.Other(arc, node);
			}
			return path;
		}

		// Token: 0x04000968 RID: 2408
		private const string NegativeCycleMessage = "A negative cycle was found.";

		// Token: 0x04000969 RID: 2409
		private readonly Dictionary<Node, double> distance;

		// Token: 0x0400096A RID: 2410
		private readonly Dictionary<Node, Arc> parentArc;
	}
}

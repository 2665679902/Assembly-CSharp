using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000257 RID: 599
	public sealed class Dijkstra
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00047503 File Offset: 0x00045703
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x0004750B File Offset: 0x0004570B
		public IGraph Graph { get; private set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00047514 File Offset: 0x00045714
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x0004751C File Offset: 0x0004571C
		public Func<Arc, double> Cost { get; private set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x00047525 File Offset: 0x00045725
		// (set) Token: 0x06001237 RID: 4663 RVA: 0x0004752D File Offset: 0x0004572D
		public DijkstraMode Mode { get; private set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00047536 File Offset: 0x00045736
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x0004753E File Offset: 0x0004573E
		public double NullCost { get; private set; }

		// Token: 0x0600123A RID: 4666 RVA: 0x00047548 File Offset: 0x00045748
		public Dijkstra(IGraph graph, Func<Arc, double> cost, DijkstraMode mode)
		{
			this.Graph = graph;
			this.Cost = cost;
			this.Mode = mode;
			this.NullCost = ((mode == DijkstraMode.Sum) ? 0.0 : double.NegativeInfinity);
			this.distance = new Dictionary<Node, double>();
			this.parentArc = new Dictionary<Node, Arc>();
			this.priorityQueue = new PriorityQueue<Node, double>();
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000475AE File Offset: 0x000457AE
		private void ValidateCost(double c)
		{
			if (this.Mode == DijkstraMode.Sum && c < 0.0)
			{
				throw new InvalidOperationException("Invalid cost: " + c.ToString());
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000475DB File Offset: 0x000457DB
		public void AddSource(Node node)
		{
			this.AddSource(node, this.NullCost);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000475EA File Offset: 0x000457EA
		public void AddSource(Node node, double nodeCost)
		{
			if (this.Reached(node))
			{
				throw new InvalidOperationException("Cannot add a reached node as a source.");
			}
			this.ValidateCost(nodeCost);
			this.parentArc[node] = Arc.Invalid;
			this.priorityQueue[node] = nodeCost;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00047628 File Offset: 0x00045828
		public Node Step()
		{
			if (this.priorityQueue.Count == 0)
			{
				return Node.Invalid;
			}
			double num;
			Node node = this.priorityQueue.Peek(out num);
			this.priorityQueue.Pop();
			if (double.IsPositiveInfinity(num))
			{
				return Node.Invalid;
			}
			this.distance[node] = num;
			foreach (Arc arc in this.Graph.Arcs(node, ArcFilter.Forward))
			{
				Node node2 = this.Graph.Other(arc, node);
				if (!this.Fixed(node2))
				{
					double num2 = this.Cost(arc);
					this.ValidateCost(num2);
					double num3 = ((this.Mode == DijkstraMode.Sum) ? (num + num2) : Math.Max(num, num2));
					double positiveInfinity;
					if (!this.priorityQueue.TryGetPriority(node2, out positiveInfinity))
					{
						positiveInfinity = double.PositiveInfinity;
					}
					if (num3 < positiveInfinity)
					{
						this.priorityQueue[node2] = num3;
						this.parentArc[node2] = arc;
					}
				}
			}
			return node;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004774C File Offset: 0x0004594C
		public void Run()
		{
			while (this.Step() != Node.Invalid)
			{
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00047760 File Offset: 0x00045960
		public Node RunUntilFixed(Node target)
		{
			if (this.Fixed(target))
			{
				return target;
			}
			Node node;
			do
			{
				node = this.Step();
			}
			while (!(node == Node.Invalid) && !(node == target));
			return node;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00047798 File Offset: 0x00045998
		public Node RunUntilFixed(Func<Node, bool> isTarget)
		{
			Node node = this.FixedNodes.FirstOrDefault(isTarget);
			if (node != Node.Invalid)
			{
				return node;
			}
			do
			{
				node = this.Step();
			}
			while (!(node == Node.Invalid) && !isTarget(node));
			return node;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000477DF File Offset: 0x000459DF
		public bool Reached(Node node)
		{
			return this.parentArc.ContainsKey(node);
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000477ED File Offset: 0x000459ED
		public IEnumerable<Node> ReachedNodes
		{
			get
			{
				return this.parentArc.Keys;
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000477FA File Offset: 0x000459FA
		public bool Fixed(Node node)
		{
			return this.distance.ContainsKey(node);
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x00047808 File Offset: 0x00045A08
		public IEnumerable<Node> FixedNodes
		{
			get
			{
				return this.distance.Keys;
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00047818 File Offset: 0x00045A18
		public double GetDistance(Node node)
		{
			double num;
			if (!this.distance.TryGetValue(node, out num))
			{
				return double.PositiveInfinity;
			}
			return num;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00047840 File Offset: 0x00045A40
		public Arc GetParentArc(Node node)
		{
			Arc arc;
			if (!this.parentArc.TryGetValue(node, out arc))
			{
				return Arc.Invalid;
			}
			return arc;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00047864 File Offset: 0x00045A64
		public IPath GetPath(Node node)
		{
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

		// Token: 0x0400099B RID: 2459
		private readonly Dictionary<Node, double> distance;

		// Token: 0x0400099C RID: 2460
		private readonly Dictionary<Node, Arc> parentArc;

		// Token: 0x0400099D RID: 2461
		private readonly PriorityQueue<Node, double> priorityQueue;
	}
}

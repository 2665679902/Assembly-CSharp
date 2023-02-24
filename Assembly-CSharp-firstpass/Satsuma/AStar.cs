using System;

namespace Satsuma
{
	// Token: 0x02000246 RID: 582
	public sealed class AStar
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00045E3E File Offset: 0x0004403E
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x00045E46 File Offset: 0x00044046
		public IGraph Graph { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00045E4F File Offset: 0x0004404F
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x00045E57 File Offset: 0x00044057
		public Func<Arc, double> Cost { get; private set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00045E60 File Offset: 0x00044060
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x00045E68 File Offset: 0x00044068
		public Func<Node, double> Heuristic { get; private set; }

		// Token: 0x0600118A RID: 4490 RVA: 0x00045E71 File Offset: 0x00044071
		public AStar(IGraph graph, Func<Arc, double> cost, Func<Node, double> heuristic)
		{
			this.Graph = graph;
			this.Cost = cost;
			this.Heuristic = heuristic;
			this.dijkstra = new Dijkstra(this.Graph, (Arc arc) => this.Cost(arc) - this.Heuristic(this.Graph.U(arc)) + this.Heuristic(this.Graph.V(arc)), DijkstraMode.Sum);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00045EAC File Offset: 0x000440AC
		private Node CheckTarget(Node node)
		{
			if (node != Node.Invalid && this.Heuristic(node) != 0.0)
			{
				throw new ArgumentException("Heuristic is nonzero for a target");
			}
			return node;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00045EDE File Offset: 0x000440DE
		public void AddSource(Node node)
		{
			this.dijkstra.AddSource(node, this.Heuristic(node));
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00045EF8 File Offset: 0x000440F8
		public Node RunUntilReached(Node target)
		{
			return this.CheckTarget(this.dijkstra.RunUntilFixed(target));
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00045F0C File Offset: 0x0004410C
		public Node RunUntilReached(Func<Node, bool> isTarget)
		{
			return this.CheckTarget(this.dijkstra.RunUntilFixed(isTarget));
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00045F20 File Offset: 0x00044120
		public double GetDistance(Node node)
		{
			this.CheckTarget(node);
			if (!this.dijkstra.Fixed(node))
			{
				return double.PositiveInfinity;
			}
			return this.dijkstra.GetDistance(node);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00045F4E File Offset: 0x0004414E
		public IPath GetPath(Node node)
		{
			this.CheckTarget(node);
			if (!this.dijkstra.Fixed(node))
			{
				return null;
			}
			return this.dijkstra.GetPath(node);
		}

		// Token: 0x04000964 RID: 2404
		private Dijkstra dijkstra;
	}
}

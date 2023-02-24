using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x02000248 RID: 584
	public sealed class Bfs
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00046364 File Offset: 0x00044564
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x0004636C File Offset: 0x0004456C
		public IGraph Graph { get; private set; }

		// Token: 0x060011A1 RID: 4513 RVA: 0x00046375 File Offset: 0x00044575
		public Bfs(IGraph graph)
		{
			this.Graph = graph;
			this.parentArc = new Dictionary<Node, Arc>();
			this.level = new Dictionary<Node, int>();
			this.queue = new Queue<Node>();
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000463A5 File Offset: 0x000445A5
		public void AddSource(Node node)
		{
			if (this.Reached(node))
			{
				return;
			}
			this.parentArc[node] = Arc.Invalid;
			this.level[node] = 0;
			this.queue.Enqueue(node);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000463DC File Offset: 0x000445DC
		public bool Step(Func<Node, bool> isTarget, out Node reachedTargetNode)
		{
			reachedTargetNode = Node.Invalid;
			if (this.queue.Count == 0)
			{
				return false;
			}
			Node node = this.queue.Dequeue();
			int num = this.level[node] + 1;
			foreach (Arc arc in this.Graph.Arcs(node, ArcFilter.Forward))
			{
				Node node2 = this.Graph.Other(arc, node);
				if (!this.parentArc.ContainsKey(node2))
				{
					this.queue.Enqueue(node2);
					this.level[node2] = num;
					this.parentArc[node2] = arc;
					if (isTarget != null && isTarget(node2))
					{
						reachedTargetNode = node2;
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000464C4 File Offset: 0x000446C4
		public void Run()
		{
			Node node;
			while (this.Step(null, out node))
			{
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000464DC File Offset: 0x000446DC
		public Node RunUntilReached(Node target)
		{
			if (this.Reached(target))
			{
				return target;
			}
			Node node2;
			while (this.Step((Node node) => node == target, out node2))
			{
			}
			return node2;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00046524 File Offset: 0x00044724
		public Node RunUntilReached(Func<Node, bool> isTarget)
		{
			Node node = this.ReachedNodes.FirstOrDefault(isTarget);
			if (node != Node.Invalid)
			{
				return node;
			}
			while (this.Step(isTarget, out node))
			{
			}
			return node;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00046559 File Offset: 0x00044759
		public bool Reached(Node x)
		{
			return this.parentArc.ContainsKey(x);
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x00046567 File Offset: 0x00044767
		public IEnumerable<Node> ReachedNodes
		{
			get
			{
				return this.parentArc.Keys;
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00046574 File Offset: 0x00044774
		public int GetLevel(Node node)
		{
			int num;
			if (!this.level.TryGetValue(node, out num))
			{
				return -1;
			}
			return num;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00046594 File Offset: 0x00044794
		public Arc GetParentArc(Node node)
		{
			Arc arc;
			if (!this.parentArc.TryGetValue(node, out arc))
			{
				return Arc.Invalid;
			}
			return arc;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000465B8 File Offset: 0x000447B8
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

		// Token: 0x0400096C RID: 2412
		private readonly Dictionary<Node, Arc> parentArc;

		// Token: 0x0400096D RID: 2413
		private readonly Dictionary<Node, int> level;

		// Token: 0x0400096E RID: 2414
		private readonly Queue<Node> queue;
	}
}

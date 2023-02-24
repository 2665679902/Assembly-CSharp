using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000255 RID: 597
	public abstract class Dfs
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00047331 File Offset: 0x00045531
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x00047339 File Offset: 0x00045539
		private protected IGraph Graph { protected get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00047342 File Offset: 0x00045542
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x0004734A File Offset: 0x0004554A
		private protected int Level { protected get; private set; }

		// Token: 0x0600122A RID: 4650 RVA: 0x00047354 File Offset: 0x00045554
		public void Run(IGraph graph, IEnumerable<Node> roots = null)
		{
			this.Graph = graph;
			Dfs.Direction direction;
			this.Start(out direction);
			if (direction != Dfs.Direction.Undirected)
			{
				if (direction != Dfs.Direction.Forward)
				{
					this.arcFilter = ArcFilter.Backward;
				}
				else
				{
					this.arcFilter = ArcFilter.Forward;
				}
			}
			else
			{
				this.arcFilter = ArcFilter.All;
			}
			this.traversed = new HashSet<Node>();
			foreach (Node node in (roots ?? this.Graph.Nodes()))
			{
				if (!this.traversed.Contains(node))
				{
					this.Level = 0;
					if (!this.Traverse(node, Arc.Invalid))
					{
						break;
					}
				}
			}
			this.traversed = null;
			this.StopSearch();
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00047414 File Offset: 0x00045614
		private bool Traverse(Node node, Arc arc)
		{
			this.traversed.Add(node);
			if (!this.NodeEnter(node, arc))
			{
				return false;
			}
			foreach (Arc arc2 in this.Graph.Arcs(node, this.arcFilter))
			{
				if (!(arc2 == arc))
				{
					Node node2 = this.Graph.Other(arc2, node);
					if (this.traversed.Contains(node2))
					{
						if (!this.BackArc(node, arc2))
						{
							return false;
						}
					}
					else
					{
						int num = this.Level;
						this.Level = num + 1;
						if (!this.Traverse(node2, arc2))
						{
							return false;
						}
						num = this.Level;
						this.Level = num - 1;
					}
				}
			}
			return this.NodeExit(node, arc);
		}

		// Token: 0x0600122C RID: 4652
		protected abstract void Start(out Dfs.Direction direction);

		// Token: 0x0600122D RID: 4653 RVA: 0x000474F0 File Offset: 0x000456F0
		protected virtual bool NodeEnter(Node node, Arc arc)
		{
			return true;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000474F3 File Offset: 0x000456F3
		protected virtual bool NodeExit(Node node, Arc arc)
		{
			return true;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000474F6 File Offset: 0x000456F6
		protected virtual bool BackArc(Node node, Arc arc)
		{
			return true;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000474F9 File Offset: 0x000456F9
		protected virtual void StopSearch()
		{
		}

		// Token: 0x04000991 RID: 2449
		private HashSet<Node> traversed;

		// Token: 0x04000992 RID: 2450
		private ArcFilter arcFilter;

		// Token: 0x02000A8B RID: 2699
		public enum Direction
		{
			// Token: 0x04002412 RID: 9234
			Undirected,
			// Token: 0x04002413 RID: 9235
			Forward,
			// Token: 0x04002414 RID: 9236
			Backward
		}
	}
}

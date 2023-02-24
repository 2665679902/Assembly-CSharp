using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200024D RID: 589
	public sealed class TopologicalOrder
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00046C95 File Offset: 0x00044E95
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x00046C9D File Offset: 0x00044E9D
		public IGraph Graph { get; private set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00046CA6 File Offset: 0x00044EA6
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x00046CAE File Offset: 0x00044EAE
		public bool Acyclic { get; private set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00046CB7 File Offset: 0x00044EB7
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x00046CBF File Offset: 0x00044EBF
		public List<Node> Order { get; private set; }

		// Token: 0x060011EF RID: 4591 RVA: 0x00046CC8 File Offset: 0x00044EC8
		public TopologicalOrder(IGraph graph, TopologicalOrder.Flags flags = TopologicalOrder.Flags.None)
		{
			this.Graph = graph;
			if ((flags & TopologicalOrder.Flags.CreateOrder) != TopologicalOrder.Flags.None)
			{
				this.Order = new List<Node>();
			}
			new TopologicalOrder.MyDfs
			{
				Parent = this
			}.Run(graph, null);
		}

		// Token: 0x02000A7E RID: 2686
		[Flags]
		public enum Flags
		{
			// Token: 0x040023DC RID: 9180
			None = 0,
			// Token: 0x040023DD RID: 9181
			CreateOrder = 1
		}

		// Token: 0x02000A7F RID: 2687
		private class MyDfs : Dfs
		{
			// Token: 0x0600561D RID: 22045 RVA: 0x000A05A2 File Offset: 0x0009E7A2
			protected override void Start(out Dfs.Direction direction)
			{
				direction = Dfs.Direction.Forward;
				this.Parent.Acyclic = true;
				this.exited = new HashSet<Node>();
			}

			// Token: 0x0600561E RID: 22046 RVA: 0x000A05BE File Offset: 0x0009E7BE
			protected override bool NodeEnter(Node node, Arc arc)
			{
				if (arc != Arc.Invalid && base.Graph.IsEdge(arc))
				{
					this.Parent.Acyclic = false;
					return false;
				}
				return true;
			}

			// Token: 0x0600561F RID: 22047 RVA: 0x000A05EA File Offset: 0x0009E7EA
			protected override bool NodeExit(Node node, Arc arc)
			{
				if (this.Parent.Order != null)
				{
					this.Parent.Order.Add(node);
				}
				this.exited.Add(node);
				return true;
			}

			// Token: 0x06005620 RID: 22048 RVA: 0x000A0618 File Offset: 0x0009E818
			protected override bool BackArc(Node node, Arc arc)
			{
				Node node2 = base.Graph.Other(arc, node);
				if (!this.exited.Contains(node2))
				{
					this.Parent.Acyclic = false;
					return false;
				}
				return true;
			}

			// Token: 0x06005621 RID: 22049 RVA: 0x000A0650 File Offset: 0x0009E850
			protected override void StopSearch()
			{
				if (this.Parent.Order != null)
				{
					if (this.Parent.Acyclic)
					{
						this.Parent.Order.Reverse();
						return;
					}
					this.Parent.Order.Clear();
				}
			}

			// Token: 0x040023DE RID: 9182
			public TopologicalOrder Parent;

			// Token: 0x040023DF RID: 9183
			private HashSet<Node> exited;
		}
	}
}

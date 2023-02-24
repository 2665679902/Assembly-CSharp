using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200024B RID: 587
	public sealed class ConnectedComponents
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00046B9E File Offset: 0x00044D9E
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00046BA6 File Offset: 0x00044DA6
		public IGraph Graph { get; private set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00046BAF File Offset: 0x00044DAF
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x00046BB7 File Offset: 0x00044DB7
		public int Count { get; private set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00046BC0 File Offset: 0x00044DC0
		// (set) Token: 0x060011DE RID: 4574 RVA: 0x00046BC8 File Offset: 0x00044DC8
		public List<HashSet<Node>> Components { get; private set; }

		// Token: 0x060011DF RID: 4575 RVA: 0x00046BD1 File Offset: 0x00044DD1
		public ConnectedComponents(IGraph graph, ConnectedComponents.Flags flags = ConnectedComponents.Flags.None)
		{
			this.Graph = graph;
			if ((flags & ConnectedComponents.Flags.CreateComponents) != ConnectedComponents.Flags.None)
			{
				this.Components = new List<HashSet<Node>>();
			}
			new ConnectedComponents.MyDfs
			{
				Parent = this
			}.Run(graph, null);
		}

		// Token: 0x02000A7A RID: 2682
		[Flags]
		public enum Flags
		{
			// Token: 0x040023D2 RID: 9170
			None = 0,
			// Token: 0x040023D3 RID: 9171
			CreateComponents = 1
		}

		// Token: 0x02000A7B RID: 2683
		private class MyDfs : Dfs
		{
			// Token: 0x06005616 RID: 22038 RVA: 0x000A041B File Offset: 0x0009E61B
			protected override void Start(out Dfs.Direction direction)
			{
				direction = Dfs.Direction.Undirected;
			}

			// Token: 0x06005617 RID: 22039 RVA: 0x000A0420 File Offset: 0x0009E620
			protected override bool NodeEnter(Node node, Arc arc)
			{
				if (arc == Arc.Invalid)
				{
					ConnectedComponents parent = this.Parent;
					int count = parent.Count;
					parent.Count = count + 1;
					if (this.Parent.Components != null)
					{
						this.Parent.Components.Add(new HashSet<Node> { node });
					}
				}
				else if (this.Parent.Components != null)
				{
					this.Parent.Components[this.Parent.Count - 1].Add(node);
				}
				return true;
			}

			// Token: 0x040023D4 RID: 9172
			public ConnectedComponents Parent;
		}
	}
}

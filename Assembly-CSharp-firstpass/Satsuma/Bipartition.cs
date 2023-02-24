using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200024C RID: 588
	public sealed class Bipartition
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00046C03 File Offset: 0x00044E03
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x00046C0B File Offset: 0x00044E0B
		public IGraph Graph { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00046C14 File Offset: 0x00044E14
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x00046C1C File Offset: 0x00044E1C
		public bool Bipartite { get; private set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00046C25 File Offset: 0x00044E25
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x00046C2D File Offset: 0x00044E2D
		public HashSet<Node> RedNodes { get; private set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00046C36 File Offset: 0x00044E36
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x00046C3E File Offset: 0x00044E3E
		public HashSet<Node> BlueNodes { get; private set; }

		// Token: 0x060011E8 RID: 4584 RVA: 0x00046C48 File Offset: 0x00044E48
		public Bipartition(IGraph graph, Bipartition.Flags flags = Bipartition.Flags.None)
		{
			this.Graph = graph;
			if ((flags & Bipartition.Flags.CreateRedNodes) != Bipartition.Flags.None)
			{
				this.RedNodes = new HashSet<Node>();
			}
			if ((flags & Bipartition.Flags.CreateBlueNodes) != Bipartition.Flags.None)
			{
				this.BlueNodes = new HashSet<Node>();
			}
			new Bipartition.MyDfs
			{
				Parent = this
			}.Run(graph, null);
		}

		// Token: 0x02000A7C RID: 2684
		[Flags]
		public enum Flags
		{
			// Token: 0x040023D6 RID: 9174
			None = 0,
			// Token: 0x040023D7 RID: 9175
			CreateRedNodes = 1,
			// Token: 0x040023D8 RID: 9176
			CreateBlueNodes = 2
		}

		// Token: 0x02000A7D RID: 2685
		private class MyDfs : Dfs
		{
			// Token: 0x06005619 RID: 22041 RVA: 0x000A04B5 File Offset: 0x0009E6B5
			protected override void Start(out Dfs.Direction direction)
			{
				direction = Dfs.Direction.Undirected;
				this.Parent.Bipartite = true;
				this.redNodes = this.Parent.RedNodes ?? new HashSet<Node>();
			}

			// Token: 0x0600561A RID: 22042 RVA: 0x000A04E0 File Offset: 0x0009E6E0
			protected override bool NodeEnter(Node node, Arc arc)
			{
				if ((base.Level & 1) == 0)
				{
					this.redNodes.Add(node);
				}
				else if (this.Parent.BlueNodes != null)
				{
					this.Parent.BlueNodes.Add(node);
				}
				return true;
			}

			// Token: 0x0600561B RID: 22043 RVA: 0x000A051C File Offset: 0x0009E71C
			protected override bool BackArc(Node node, Arc arc)
			{
				Node node2 = base.Graph.Other(arc, node);
				if (this.redNodes.Contains(node) == this.redNodes.Contains(node2))
				{
					this.Parent.Bipartite = false;
					if (this.Parent.RedNodes != null)
					{
						this.Parent.RedNodes.Clear();
					}
					if (this.Parent.BlueNodes != null)
					{
						this.Parent.BlueNodes.Clear();
					}
					return false;
				}
				return true;
			}

			// Token: 0x040023D9 RID: 9177
			public Bipartition Parent;

			// Token: 0x040023DA RID: 9178
			private HashSet<Node> redNodes;
		}
	}
}

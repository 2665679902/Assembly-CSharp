using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000252 RID: 594
	public class BiNodeConnectedComponents
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00046FDC File Offset: 0x000451DC
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x00046FE4 File Offset: 0x000451E4
		public IGraph Graph { get; private set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00046FED File Offset: 0x000451ED
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x00046FF5 File Offset: 0x000451F5
		public int Count { get; private set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00046FFE File Offset: 0x000451FE
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x00047006 File Offset: 0x00045206
		public List<HashSet<Node>> Components { get; private set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0004700F File Offset: 0x0004520F
		// (set) Token: 0x06001211 RID: 4625 RVA: 0x00047017 File Offset: 0x00045217
		public Dictionary<Node, int> Cutvertices { get; private set; }

		// Token: 0x06001212 RID: 4626 RVA: 0x00047020 File Offset: 0x00045220
		public BiNodeConnectedComponents(IGraph graph, BiNodeConnectedComponents.Flags flags = BiNodeConnectedComponents.Flags.None)
		{
			this.Graph = graph;
			if ((flags & BiNodeConnectedComponents.Flags.CreateComponents) != BiNodeConnectedComponents.Flags.None)
			{
				this.Components = new List<HashSet<Node>>();
			}
			if ((flags & BiNodeConnectedComponents.Flags.CreateCutvertices) != BiNodeConnectedComponents.Flags.None)
			{
				this.Cutvertices = new Dictionary<Node, int>();
			}
			new BiNodeConnectedComponents.BlockDfs
			{
				Parent = this
			}.Run(graph, null);
		}

		// Token: 0x02000A84 RID: 2692
		[Flags]
		public enum Flags
		{
			// Token: 0x040023EA RID: 9194
			None = 0,
			// Token: 0x040023EB RID: 9195
			CreateComponents = 1,
			// Token: 0x040023EC RID: 9196
			CreateCutvertices = 2
		}

		// Token: 0x02000A85 RID: 2693
		private class BlockDfs : LowpointDfs
		{
			// Token: 0x0600562A RID: 22058 RVA: 0x000A076A File Offset: 0x0009E96A
			protected override void Start(out Dfs.Direction direction)
			{
				base.Start(out direction);
				if (this.Parent.Components != null)
				{
					this.blockStack = new Stack<Node>();
				}
			}

			// Token: 0x0600562B RID: 22059 RVA: 0x000A078C File Offset: 0x0009E98C
			protected override bool NodeEnter(Node node, Arc arc)
			{
				if (!base.NodeEnter(node, arc))
				{
					return false;
				}
				if (this.Parent.Cutvertices != null && arc == Arc.Invalid)
				{
					this.Parent.Cutvertices[node] = -1;
				}
				if (this.Parent.Components != null)
				{
					this.blockStack.Push(node);
				}
				this.oneNodeComponent = arc == Arc.Invalid;
				return true;
			}

			// Token: 0x0600562C RID: 22060 RVA: 0x000A07FC File Offset: 0x0009E9FC
			protected override bool NodeExit(Node node, Arc arc)
			{
				if (arc == Arc.Invalid)
				{
					if (this.oneNodeComponent)
					{
						BiNodeConnectedComponents parent = this.Parent;
						int num = parent.Count;
						parent.Count = num + 1;
						if (this.Parent.Components != null)
						{
							this.Parent.Components.Add(new HashSet<Node> { node });
						}
					}
					if (this.Parent.Cutvertices != null && this.Parent.Cutvertices[node] == 0)
					{
						this.Parent.Cutvertices.Remove(node);
					}
					if (this.Parent.Components != null)
					{
						this.blockStack.Clear();
					}
				}
				else
				{
					Node node2 = base.Graph.Other(arc, node);
					if (this.lowpoint[node] >= base.Level - 1)
					{
						if (this.Parent.Cutvertices != null)
						{
							int num2;
							this.Parent.Cutvertices[node2] = (this.Parent.Cutvertices.TryGetValue(node2, out num2) ? num2 : 0) + 1;
						}
						BiNodeConnectedComponents parent2 = this.Parent;
						int num = parent2.Count;
						parent2.Count = num + 1;
						if (this.Parent.Components != null)
						{
							HashSet<Node> hashSet = new HashSet<Node>();
							Node node3;
							do
							{
								node3 = this.blockStack.Pop();
								hashSet.Add(node3);
							}
							while (!(node3 == node));
							hashSet.Add(node2);
							this.Parent.Components.Add(hashSet);
						}
					}
				}
				return base.NodeExit(node, arc);
			}

			// Token: 0x040023ED RID: 9197
			public BiNodeConnectedComponents Parent;

			// Token: 0x040023EE RID: 9198
			private Stack<Node> blockStack;

			// Token: 0x040023EF RID: 9199
			private bool oneNodeComponent;
		}
	}
}

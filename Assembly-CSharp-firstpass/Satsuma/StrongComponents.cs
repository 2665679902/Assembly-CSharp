using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200024E RID: 590
	public sealed class StrongComponents
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00046CFA File Offset: 0x00044EFA
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x00046D02 File Offset: 0x00044F02
		public IGraph Graph { get; private set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00046D0B File Offset: 0x00044F0B
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x00046D13 File Offset: 0x00044F13
		public int Count { get; private set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00046D1C File Offset: 0x00044F1C
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x00046D24 File Offset: 0x00044F24
		public List<HashSet<Node>> Components { get; private set; }

		// Token: 0x060011F6 RID: 4598 RVA: 0x00046D30 File Offset: 0x00044F30
		public StrongComponents(IGraph graph, StrongComponents.Flags flags = StrongComponents.Flags.None)
		{
			this.Graph = graph;
			if ((flags & StrongComponents.Flags.CreateComponents) != StrongComponents.Flags.None)
			{
				this.Components = new List<HashSet<Node>>();
			}
			StrongComponents.ForwardDfs forwardDfs = new StrongComponents.ForwardDfs();
			forwardDfs.Run(graph, null);
			new StrongComponents.BackwardDfs
			{
				Parent = this
			}.Run(graph, forwardDfs.ReverseExitOrder);
		}

		// Token: 0x02000A80 RID: 2688
		[Flags]
		public enum Flags
		{
			// Token: 0x040023E1 RID: 9185
			None = 0,
			// Token: 0x040023E2 RID: 9186
			CreateComponents = 1
		}

		// Token: 0x02000A81 RID: 2689
		private class ForwardDfs : Dfs
		{
			// Token: 0x06005623 RID: 22051 RVA: 0x000A0695 File Offset: 0x0009E895
			protected override void Start(out Dfs.Direction direction)
			{
				direction = Dfs.Direction.Forward;
				this.ReverseExitOrder = new List<Node>();
			}

			// Token: 0x06005624 RID: 22052 RVA: 0x000A06A5 File Offset: 0x0009E8A5
			protected override bool NodeExit(Node node, Arc arc)
			{
				this.ReverseExitOrder.Add(node);
				return true;
			}

			// Token: 0x06005625 RID: 22053 RVA: 0x000A06B4 File Offset: 0x0009E8B4
			protected override void StopSearch()
			{
				this.ReverseExitOrder.Reverse();
			}

			// Token: 0x040023E3 RID: 9187
			public List<Node> ReverseExitOrder;
		}

		// Token: 0x02000A82 RID: 2690
		private class BackwardDfs : Dfs
		{
			// Token: 0x06005627 RID: 22055 RVA: 0x000A06C9 File Offset: 0x0009E8C9
			protected override void Start(out Dfs.Direction direction)
			{
				direction = Dfs.Direction.Backward;
			}

			// Token: 0x06005628 RID: 22056 RVA: 0x000A06D0 File Offset: 0x0009E8D0
			protected override bool NodeEnter(Node node, Arc arc)
			{
				if (arc == Arc.Invalid)
				{
					StrongComponents parent = this.Parent;
					int count = parent.Count;
					parent.Count = count + 1;
					if (this.Parent.Components != null)
					{
						this.Parent.Components.Add(new HashSet<Node> { node });
					}
				}
				else if (this.Parent.Components != null)
				{
					this.Parent.Components[this.Parent.Components.Count - 1].Add(node);
				}
				return true;
			}

			// Token: 0x040023E4 RID: 9188
			public StrongComponents Parent;
		}
	}
}

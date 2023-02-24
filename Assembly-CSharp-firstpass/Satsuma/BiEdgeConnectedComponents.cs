using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000251 RID: 593
	public sealed class BiEdgeConnectedComponents
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x00046EE9 File Offset: 0x000450E9
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x00046EF1 File Offset: 0x000450F1
		public IGraph Graph { get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00046EFA File Offset: 0x000450FA
		// (set) Token: 0x06001204 RID: 4612 RVA: 0x00046F02 File Offset: 0x00045102
		public int Count { get; private set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00046F0B File Offset: 0x0004510B
		// (set) Token: 0x06001206 RID: 4614 RVA: 0x00046F13 File Offset: 0x00045113
		public List<HashSet<Node>> Components { get; private set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x00046F1C File Offset: 0x0004511C
		// (set) Token: 0x06001208 RID: 4616 RVA: 0x00046F24 File Offset: 0x00045124
		public HashSet<Arc> Bridges { get; private set; }

		// Token: 0x06001209 RID: 4617 RVA: 0x00046F30 File Offset: 0x00045130
		public BiEdgeConnectedComponents(IGraph graph, BiEdgeConnectedComponents.Flags flags = BiEdgeConnectedComponents.Flags.None)
		{
			this.Graph = graph;
			BridgeDfs bridgeDfs = new BridgeDfs();
			bridgeDfs.Run(graph, null);
			this.Count = bridgeDfs.ComponentCount;
			if ((flags & BiEdgeConnectedComponents.Flags.CreateBridges) != BiEdgeConnectedComponents.Flags.None)
			{
				this.Bridges = bridgeDfs.Bridges;
			}
			if ((flags & BiEdgeConnectedComponents.Flags.CreateComponents) != BiEdgeConnectedComponents.Flags.None)
			{
				Subgraph subgraph = new Subgraph(graph);
				foreach (Arc arc in bridgeDfs.Bridges)
				{
					subgraph.Enable(arc, false);
				}
				this.Components = new ConnectedComponents(subgraph, ConnectedComponents.Flags.CreateComponents).Components;
			}
		}

		// Token: 0x02000A83 RID: 2691
		[Flags]
		public enum Flags
		{
			// Token: 0x040023E6 RID: 9190
			None = 0,
			// Token: 0x040023E7 RID: 9191
			CreateComponents = 1,
			// Token: 0x040023E8 RID: 9192
			CreateBridges = 2
		}
	}
}

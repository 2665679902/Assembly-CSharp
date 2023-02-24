using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000250 RID: 592
	internal class BridgeDfs : LowpointDfs
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x00046E63 File Offset: 0x00045063
		protected override void Start(out Dfs.Direction direction)
		{
			base.Start(out direction);
			this.ComponentCount = 0;
			this.Bridges = new HashSet<Arc>();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00046E80 File Offset: 0x00045080
		protected override bool NodeExit(Node node, Arc arc)
		{
			if (arc == Arc.Invalid)
			{
				this.ComponentCount++;
			}
			else if (this.lowpoint[node] == base.Level)
			{
				this.Bridges.Add(arc);
				this.ComponentCount++;
			}
			return base.NodeExit(node, arc);
		}

		// Token: 0x04000983 RID: 2435
		public int ComponentCount;

		// Token: 0x04000984 RID: 2436
		public HashSet<Arc> Bridges;
	}
}

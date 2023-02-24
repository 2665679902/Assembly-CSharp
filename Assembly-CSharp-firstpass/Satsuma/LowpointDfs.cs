using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200024F RID: 591
	internal class LowpointDfs : Dfs
	{
		// Token: 0x060011F7 RID: 4599 RVA: 0x00046D80 File Offset: 0x00044F80
		private void UpdateLowpoint(Node node, int newLowpoint)
		{
			if (this.lowpoint[node] > newLowpoint)
			{
				this.lowpoint[node] = newLowpoint;
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00046D9E File Offset: 0x00044F9E
		protected override void Start(out Dfs.Direction direction)
		{
			direction = Dfs.Direction.Undirected;
			this.level = new Dictionary<Node, int>();
			this.lowpoint = new Dictionary<Node, int>();
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00046DB9 File Offset: 0x00044FB9
		protected override bool NodeEnter(Node node, Arc arc)
		{
			this.level[node] = base.Level;
			this.lowpoint[node] = base.Level;
			return true;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00046DE0 File Offset: 0x00044FE0
		protected override bool NodeExit(Node node, Arc arc)
		{
			if (arc != Arc.Invalid)
			{
				Node node2 = base.Graph.Other(arc, node);
				this.UpdateLowpoint(node2, this.lowpoint[node]);
			}
			return true;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00046E1C File Offset: 0x0004501C
		protected override bool BackArc(Node node, Arc arc)
		{
			Node node2 = base.Graph.Other(arc, node);
			this.UpdateLowpoint(node, this.level[node2]);
			return true;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00046E4B File Offset: 0x0004504B
		protected override void StopSearch()
		{
			this.level = null;
			this.lowpoint = null;
		}

		// Token: 0x04000981 RID: 2433
		protected Dictionary<Node, int> level;

		// Token: 0x04000982 RID: 2434
		protected Dictionary<Node, int> lowpoint;
	}
}

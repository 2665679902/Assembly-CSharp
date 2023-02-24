using System;
using System.Collections.Generic;

namespace Delaunay
{
	// Token: 0x02000141 RID: 321
	public class Node
	{
		// Token: 0x040006F5 RID: 1781
		public static Stack<Node> pool = new Stack<Node>();

		// Token: 0x040006F6 RID: 1782
		public Node parent;

		// Token: 0x040006F7 RID: 1783
		public int treeSize;
	}
}

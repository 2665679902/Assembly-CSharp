using System;
using System.Collections.Generic;
using System.Linq;

namespace Satsuma
{
	// Token: 0x0200027C RID: 636
	public static class TspUtils
	{
		// Token: 0x060013A5 RID: 5029 RVA: 0x0004BC78 File Offset: 0x00049E78
		public static double GetTourCost<TNode>(IEnumerable<TNode> tour, Func<TNode, TNode, double> cost)
		{
			double num = 0.0;
			if (tour.Any<TNode>())
			{
				TNode tnode = tour.First<TNode>();
				foreach (TNode tnode2 in tour.Skip(1))
				{
					num += cost(tnode, tnode2);
					tnode = tnode2;
				}
			}
			return num;
		}
	}
}

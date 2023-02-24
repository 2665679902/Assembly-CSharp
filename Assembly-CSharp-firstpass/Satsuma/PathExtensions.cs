using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200026D RID: 621
	public static class PathExtensions
	{
		// Token: 0x060012D0 RID: 4816 RVA: 0x000493D7 File Offset: 0x000475D7
		public static bool IsCycle(this IPath path)
		{
			return path.FirstNode == path.LastNode && path.ArcCount(ArcFilter.All) > 0;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000493F8 File Offset: 0x000475F8
		public static Node NextNode(this IPath path, Node node)
		{
			Arc arc = path.NextArc(node);
			if (arc == Arc.Invalid)
			{
				return Node.Invalid;
			}
			return path.Other(arc, node);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00049428 File Offset: 0x00047628
		public static Node PrevNode(this IPath path, Node node)
		{
			Arc arc = path.PrevArc(node);
			if (arc == Arc.Invalid)
			{
				return Node.Invalid;
			}
			return path.Other(arc, node);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00049458 File Offset: 0x00047658
		internal static IEnumerable<Arc> ArcsHelper(this IPath path, Node u, ArcFilter filter)
		{
			Arc arc = path.PrevArc(u);
			Arc arc2 = path.NextArc(u);
			if (arc == arc2)
			{
				arc2 = Arc.Invalid;
			}
			int num;
			for (int i = 0; i < 2; i = num + 1)
			{
				Arc arc3 = ((i == 0) ? arc : arc2);
				if (!(arc3 == Arc.Invalid))
				{
					switch (filter)
					{
					case ArcFilter.All:
						yield return arc3;
						break;
					case ArcFilter.Edge:
						if (path.IsEdge(arc3))
						{
							yield return arc3;
						}
						break;
					case ArcFilter.Forward:
						if (path.IsEdge(arc3) || path.U(arc3) == u)
						{
							yield return arc3;
						}
						break;
					case ArcFilter.Backward:
						if (path.IsEdge(arc3) || path.V(arc3) == u)
						{
							yield return arc3;
						}
						break;
					}
				}
				num = i;
			}
			yield break;
		}
	}
}

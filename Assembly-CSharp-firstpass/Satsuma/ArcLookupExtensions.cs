using System;

namespace Satsuma
{
	// Token: 0x02000262 RID: 610
	public static class ArcLookupExtensions
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x00047C74 File Offset: 0x00045E74
		public static string ArcToString(this IArcLookup graph, Arc arc)
		{
			if (arc == Arc.Invalid)
			{
				return "Arc.Invalid";
			}
			return graph.U(arc).ToString() + (graph.IsEdge(arc) ? "<-->" : "--->") + graph.V(arc).ToString();
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00047CD8 File Offset: 0x00045ED8
		public static Node Other(this IArcLookup graph, Arc arc, Node node)
		{
			Node node2 = graph.U(arc);
			if (node2 != node)
			{
				return node2;
			}
			return graph.V(arc);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00047D00 File Offset: 0x00045F00
		public static Node[] Nodes(this IArcLookup graph, Arc arc, bool allowDuplicates = true)
		{
			Node node = graph.U(arc);
			Node node2 = graph.V(arc);
			if (!allowDuplicates && node == node2)
			{
				return new Node[] { node };
			}
			return new Node[] { node, node2 };
		}
	}
}

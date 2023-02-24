using System;
using System.Collections.Generic;
using Delaunay.Geo;

namespace VoronoiTree
{
	// Token: 0x020004B4 RID: 1204
	public class Leaf : Node
	{
		// Token: 0x06003374 RID: 13172 RVA: 0x0006E8DD File Offset: 0x0006CADD
		public Leaf()
			: base(Node.NodeType.Leaf)
		{
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x0006E8E6 File Offset: 0x0006CAE6
		public Leaf(Diagram.Site site, Tree parent)
			: base(site, Node.NodeType.Leaf, parent)
		{
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
		public void GetIntersectingSites(LineSegment edge, List<Diagram.Site> intersectingSites)
		{
			if (this.site == null)
			{
				return;
			}
			if (this.site.poly == null)
			{
				return;
			}
			LineSegment lineSegment = new LineSegment(null, null);
			if (!this.site.poly.ClipSegment(edge, ref lineSegment))
			{
				return;
			}
			intersectingSites.Add(this.site);
		}
	}
}

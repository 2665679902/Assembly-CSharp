using System;
using System.Collections.Generic;
using System.Linq;

namespace MIConvexHull
{
	// Token: 0x020004AF RID: 1199
	public static class VoronoiMesh
	{
		// Token: 0x06003337 RID: 13111 RVA: 0x0006CA2B File Offset: 0x0006AC2B
		public static VoronoiMesh<TVertex, TCell, TEdge> Create<TVertex, TCell, TEdge>(IList<TVertex> data) where TVertex : IVertex where TCell : TriangulationCell<TVertex, TCell>, new() where TEdge : VoronoiEdge<TVertex, TCell>, new()
		{
			return VoronoiMesh<TVertex, TCell, TEdge>.Create(data);
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x0006CA33 File Offset: 0x0006AC33
		public static VoronoiMesh<TVertex, DefaultTriangulationCell<TVertex>, VoronoiEdge<TVertex, DefaultTriangulationCell<TVertex>>> Create<TVertex>(IList<TVertex> data) where TVertex : IVertex
		{
			return VoronoiMesh<TVertex, DefaultTriangulationCell<TVertex>, VoronoiEdge<TVertex, DefaultTriangulationCell<TVertex>>>.Create(data);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x0006CA3B File Offset: 0x0006AC3B
		public static VoronoiMesh<DefaultVertex, DefaultTriangulationCell<DefaultVertex>, VoronoiEdge<DefaultVertex, DefaultTriangulationCell<DefaultVertex>>> Create(IList<double[]> data)
		{
			return VoronoiMesh<DefaultVertex, DefaultTriangulationCell<DefaultVertex>, VoronoiEdge<DefaultVertex, DefaultTriangulationCell<DefaultVertex>>>.Create(data.Select((double[] p) => new DefaultVertex
			{
				Position = p.ToArray<double>()
			}).ToList<DefaultVertex>());
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x0006CA6C File Offset: 0x0006AC6C
		public static VoronoiMesh<TVertex, TCell, VoronoiEdge<TVertex, TCell>> Create<TVertex, TCell>(IList<TVertex> data) where TVertex : IVertex where TCell : TriangulationCell<TVertex, TCell>, new()
		{
			return VoronoiMesh<TVertex, TCell, VoronoiEdge<TVertex, TCell>>.Create(data);
		}
	}
}

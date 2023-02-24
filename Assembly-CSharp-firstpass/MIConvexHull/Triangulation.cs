using System;
using System.Collections.Generic;
using System.Linq;

namespace MIConvexHull
{
	// Token: 0x020004AA RID: 1194
	public static class Triangulation
	{
		// Token: 0x06003322 RID: 13090 RVA: 0x0006C840 File Offset: 0x0006AA40
		public static ITriangulation<TVertex, DefaultTriangulationCell<TVertex>> CreateDelaunay<TVertex>(IList<TVertex> data) where TVertex : IVertex
		{
			return DelaunayTriangulation<TVertex, DefaultTriangulationCell<TVertex>>.Create(data);
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x0006C848 File Offset: 0x0006AA48
		public static ITriangulation<DefaultVertex, DefaultTriangulationCell<DefaultVertex>> CreateDelaunay(IList<double[]> data)
		{
			return DelaunayTriangulation<DefaultVertex, DefaultTriangulationCell<DefaultVertex>>.Create(data.Select((double[] p) => new DefaultVertex
			{
				Position = p
			}).ToList<DefaultVertex>());
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x0006C879 File Offset: 0x0006AA79
		public static ITriangulation<TVertex, TFace> CreateDelaunay<TVertex, TFace>(IList<TVertex> data) where TVertex : IVertex where TFace : TriangulationCell<TVertex, TFace>, new()
		{
			return DelaunayTriangulation<TVertex, TFace>.Create(data);
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x0006C881 File Offset: 0x0006AA81
		public static VoronoiMesh<TVertex, TCell, TEdge> CreateVoronoi<TVertex, TCell, TEdge>(IList<TVertex> data) where TVertex : IVertex where TCell : TriangulationCell<TVertex, TCell>, new() where TEdge : VoronoiEdge<TVertex, TCell>, new()
		{
			return VoronoiMesh<TVertex, TCell, TEdge>.Create(data);
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x0006C889 File Offset: 0x0006AA89
		public static VoronoiMesh<TVertex, DefaultTriangulationCell<TVertex>, VoronoiEdge<TVertex, DefaultTriangulationCell<TVertex>>> CreateVoronoi<TVertex>(IList<TVertex> data) where TVertex : IVertex
		{
			return VoronoiMesh<TVertex, DefaultTriangulationCell<TVertex>, VoronoiEdge<TVertex, DefaultTriangulationCell<TVertex>>>.Create(data);
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x0006C891 File Offset: 0x0006AA91
		public static VoronoiMesh<DefaultVertex, DefaultTriangulationCell<DefaultVertex>, VoronoiEdge<DefaultVertex, DefaultTriangulationCell<DefaultVertex>>> CreateVoronoi(IList<double[]> data)
		{
			return VoronoiMesh<DefaultVertex, DefaultTriangulationCell<DefaultVertex>, VoronoiEdge<DefaultVertex, DefaultTriangulationCell<DefaultVertex>>>.Create(data.Select((double[] p) => new DefaultVertex
			{
				Position = p.ToArray<double>()
			}).ToList<DefaultVertex>());
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x0006C8C2 File Offset: 0x0006AAC2
		public static VoronoiMesh<TVertex, TCell, VoronoiEdge<TVertex, TCell>> CreateVoronoi<TVertex, TCell>(IList<TVertex> data) where TVertex : IVertex where TCell : TriangulationCell<TVertex, TCell>, new()
		{
			return VoronoiMesh<TVertex, TCell, VoronoiEdge<TVertex, TCell>>.Create(data);
		}
	}
}

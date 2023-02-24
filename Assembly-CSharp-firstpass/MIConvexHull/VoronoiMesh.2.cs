using System;
using System.Collections.Generic;
using System.Linq;

namespace MIConvexHull
{
	// Token: 0x020004B0 RID: 1200
	public class VoronoiMesh<TVertex, TCell, TEdge> where TVertex : IVertex where TCell : TriangulationCell<TVertex, TCell>, new() where TEdge : VoronoiEdge<TVertex, TCell>, new()
	{
		// Token: 0x0600333B RID: 13115 RVA: 0x0006CA74 File Offset: 0x0006AC74
		private VoronoiMesh()
		{
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x0006CA7C File Offset: 0x0006AC7C
		// (set) Token: 0x0600333D RID: 13117 RVA: 0x0006CA84 File Offset: 0x0006AC84
		public IEnumerable<TCell> Vertices { get; private set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x0006CA8D File Offset: 0x0006AC8D
		// (set) Token: 0x0600333F RID: 13119 RVA: 0x0006CA95 File Offset: 0x0006AC95
		public IEnumerable<TEdge> Edges { get; private set; }

		// Token: 0x06003340 RID: 13120 RVA: 0x0006CAA0 File Offset: 0x0006ACA0
		public static VoronoiMesh<TVertex, TCell, TEdge> Create(IList<TVertex> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			List<TCell> list = DelaunayTriangulation<TVertex, TCell>.Create(data).Cells.ToList<TCell>();
			HashSet<TEdge> hashSet = new HashSet<TEdge>(new VoronoiMesh<TVertex, TCell, TEdge>.EdgeComparer());
			foreach (TCell tcell in list)
			{
				for (int i = 0; i < tcell.Adjacency.Length; i++)
				{
					TCell tcell2 = tcell.Adjacency[i];
					if (tcell2 != null)
					{
						HashSet<TEdge> hashSet2 = hashSet;
						TEdge tedge = new TEdge();
						tedge.Source = tcell;
						tedge.Target = tcell2;
						hashSet2.Add(tedge);
					}
				}
			}
			return new VoronoiMesh<TVertex, TCell, TEdge>
			{
				Vertices = list,
				Edges = hashSet.ToList<TEdge>()
			};
		}

		// Token: 0x02000AD6 RID: 2774
		private class EdgeComparer : IEqualityComparer<TEdge>
		{
			// Token: 0x0600578A RID: 22410 RVA: 0x000A3560 File Offset: 0x000A1760
			public bool Equals(TEdge x, TEdge y)
			{
				return (x.Source == y.Source && x.Target == y.Target) || (x.Source == y.Target && x.Target == y.Source);
			}

			// Token: 0x0600578B RID: 22411 RVA: 0x000A35F9 File Offset: 0x000A17F9
			public int GetHashCode(TEdge obj)
			{
				return obj.Source.GetHashCode() ^ obj.Target.GetHashCode();
			}
		}
	}
}

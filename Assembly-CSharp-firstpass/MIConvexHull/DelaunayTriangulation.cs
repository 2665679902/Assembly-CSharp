using System;
using System.Collections.Generic;

namespace MIConvexHull
{
	// Token: 0x020004AB RID: 1195
	public class DelaunayTriangulation<TVertex, TCell> : ITriangulation<TVertex, TCell> where TVertex : IVertex where TCell : TriangulationCell<TVertex, TCell>, new()
	{
		// Token: 0x06003329 RID: 13097 RVA: 0x0006C8CA File Offset: 0x0006AACA
		private DelaunayTriangulation()
		{
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x0006C8D2 File Offset: 0x0006AAD2
		// (set) Token: 0x0600332B RID: 13099 RVA: 0x0006C8DA File Offset: 0x0006AADA
		public IEnumerable<TCell> Cells { get; private set; }

		// Token: 0x0600332C RID: 13100 RVA: 0x0006C8E4 File Offset: 0x0006AAE4
		public static DelaunayTriangulation<TVertex, TCell> Create(IList<TVertex> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Count == 0)
			{
				return new DelaunayTriangulation<TVertex, TCell>
				{
					Cells = new TCell[0]
				};
			}
			TCell[] delaunayTriangulation = ConvexHullAlgorithm.GetDelaunayTriangulation<TVertex, TCell>(data);
			return new DelaunayTriangulation<TVertex, TCell>
			{
				Cells = delaunayTriangulation
			};
		}
	}
}

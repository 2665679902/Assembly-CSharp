using System;
using System.Collections.Generic;

namespace MIConvexHull
{
	// Token: 0x020004A9 RID: 1193
	public interface ITriangulation<TVertex, TCell> where TVertex : IVertex where TCell : TriangulationCell<TVertex, TCell>, new()
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06003321 RID: 13089
		IEnumerable<TCell> Cells { get; }
	}
}

using System;

namespace MIConvexHull
{
	// Token: 0x020004AC RID: 1196
	public abstract class TriangulationCell<TVertex, TCell> : ConvexFace<TVertex, TCell> where TVertex : IVertex where TCell : ConvexFace<TVertex, TCell>
	{
	}
}

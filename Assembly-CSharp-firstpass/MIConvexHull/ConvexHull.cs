using System;
using System.Collections.Generic;
using System.Linq;

namespace MIConvexHull
{
	// Token: 0x02000499 RID: 1177
	public static class ConvexHull
	{
		// Token: 0x060032BA RID: 12986 RVA: 0x0006979E File Offset: 0x0006799E
		public static ConvexHull<TVertex, TFace> Create<TVertex, TFace>(IList<TVertex> data, double PlaneDistanceTolerance = 1E-10) where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>, new()
		{
			return ConvexHull<TVertex, TFace>.Create(data, PlaneDistanceTolerance);
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000697A7 File Offset: 0x000679A7
		public static ConvexHull<TVertex, DefaultConvexFace<TVertex>> Create<TVertex>(IList<TVertex> data, double PlaneDistanceTolerance = 1E-10) where TVertex : IVertex
		{
			return ConvexHull<TVertex, DefaultConvexFace<TVertex>>.Create(data, PlaneDistanceTolerance);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000697B0 File Offset: 0x000679B0
		public static ConvexHull<DefaultVertex, DefaultConvexFace<DefaultVertex>> Create(IList<double[]> data, double PlaneDistanceTolerance = 1E-10)
		{
			return ConvexHull<DefaultVertex, DefaultConvexFace<DefaultVertex>>.Create(data.Select((double[] p) => new DefaultVertex
			{
				Position = p
			}).ToList<DefaultVertex>(), PlaneDistanceTolerance);
		}
	}
}

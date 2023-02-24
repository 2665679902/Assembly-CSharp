using System;
using System.Collections.Generic;

namespace MIConvexHull
{
	// Token: 0x0200049A RID: 1178
	public class ConvexHull<TVertex, TFace> where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>, new()
	{
		// Token: 0x060032BD RID: 12989 RVA: 0x000697E2 File Offset: 0x000679E2
		internal ConvexHull()
		{
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000697EA File Offset: 0x000679EA
		// (set) Token: 0x060032BF RID: 12991 RVA: 0x000697F2 File Offset: 0x000679F2
		public IEnumerable<TVertex> Points { get; internal set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000697FB File Offset: 0x000679FB
		// (set) Token: 0x060032C1 RID: 12993 RVA: 0x00069803 File Offset: 0x00067A03
		public IEnumerable<TFace> Faces { get; internal set; }

		// Token: 0x060032C2 RID: 12994 RVA: 0x0006980C File Offset: 0x00067A0C
		public static ConvexHull<TVertex, TFace> Create(IList<TVertex> data, double PlaneDistanceTolerance)
		{
			if (data == null)
			{
				throw new ArgumentNullException("The supplied data is null.");
			}
			return ConvexHullAlgorithm.GetConvexHull<TVertex, TFace>(data, PlaneDistanceTolerance);
		}
	}
}

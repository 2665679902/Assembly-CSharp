using System;

namespace MIConvexHull
{
	// Token: 0x0200049F RID: 1183
	public abstract class ConvexFace<TVertex, TFace> where TVertex : IVertex where TFace : ConvexFace<TVertex, TFace>
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x00069BDD File Offset: 0x00067DDD
		// (set) Token: 0x060032D9 RID: 13017 RVA: 0x00069BE5 File Offset: 0x00067DE5
		public TFace[] Adjacency { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x00069BEE File Offset: 0x00067DEE
		// (set) Token: 0x060032DB RID: 13019 RVA: 0x00069BF6 File Offset: 0x00067DF6
		public TVertex[] Vertices { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x00069BFF File Offset: 0x00067DFF
		// (set) Token: 0x060032DD RID: 13021 RVA: 0x00069C07 File Offset: 0x00067E07
		public double[] Normal { get; set; }
	}
}

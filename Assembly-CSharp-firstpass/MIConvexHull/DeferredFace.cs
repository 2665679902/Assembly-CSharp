using System;

namespace MIConvexHull
{
	// Token: 0x020004A2 RID: 1186
	internal sealed class DeferredFace
	{
		// Token: 0x040011C7 RID: 4551
		public ConvexFaceInternal Face;

		// Token: 0x040011C8 RID: 4552
		public ConvexFaceInternal Pivot;

		// Token: 0x040011C9 RID: 4553
		public ConvexFaceInternal OldFace;

		// Token: 0x040011CA RID: 4554
		public int FaceIndex;

		// Token: 0x040011CB RID: 4555
		public int PivotIndex;
	}
}

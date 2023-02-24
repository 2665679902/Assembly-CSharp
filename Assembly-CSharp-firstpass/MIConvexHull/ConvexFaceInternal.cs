using System;

namespace MIConvexHull
{
	// Token: 0x020004A4 RID: 1188
	internal sealed class ConvexFaceInternal
	{
		// Token: 0x06003303 RID: 13059 RVA: 0x0006BC10 File Offset: 0x00069E10
		public ConvexFaceInternal(int dimension, int index, IndexBuffer beyondList)
		{
			this.Index = index;
			this.AdjacentFaces = new int[dimension];
			this.VerticesBeyond = beyondList;
			this.Normal = new double[dimension];
			this.Vertices = new int[dimension];
		}

		// Token: 0x040011D2 RID: 4562
		public int[] AdjacentFaces;

		// Token: 0x040011D3 RID: 4563
		public int FurthestVertex;

		// Token: 0x040011D4 RID: 4564
		public int Index;

		// Token: 0x040011D5 RID: 4565
		public bool InList;

		// Token: 0x040011D6 RID: 4566
		public bool IsNormalFlipped;

		// Token: 0x040011D7 RID: 4567
		public ConvexFaceInternal Next;

		// Token: 0x040011D8 RID: 4568
		public double[] Normal;

		// Token: 0x040011D9 RID: 4569
		public double Offset;

		// Token: 0x040011DA RID: 4570
		public ConvexFaceInternal Previous;

		// Token: 0x040011DB RID: 4571
		public int Tag;

		// Token: 0x040011DC RID: 4572
		public int[] Vertices;

		// Token: 0x040011DD RID: 4573
		public IndexBuffer VerticesBeyond;
	}
}

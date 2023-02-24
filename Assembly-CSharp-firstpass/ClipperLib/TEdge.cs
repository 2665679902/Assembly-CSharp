using System;

namespace ClipperLib
{
	// Token: 0x02000164 RID: 356
	internal class TEdge
	{
		// Token: 0x0400077B RID: 1915
		internal IntPoint Bot;

		// Token: 0x0400077C RID: 1916
		internal IntPoint Curr;

		// Token: 0x0400077D RID: 1917
		internal IntPoint Top;

		// Token: 0x0400077E RID: 1918
		internal IntPoint Delta;

		// Token: 0x0400077F RID: 1919
		internal double Dx;

		// Token: 0x04000780 RID: 1920
		internal PolyType PolyTyp;

		// Token: 0x04000781 RID: 1921
		internal EdgeSide Side;

		// Token: 0x04000782 RID: 1922
		internal int WindDelta;

		// Token: 0x04000783 RID: 1923
		internal int WindCnt;

		// Token: 0x04000784 RID: 1924
		internal int WindCnt2;

		// Token: 0x04000785 RID: 1925
		internal int OutIdx;

		// Token: 0x04000786 RID: 1926
		internal TEdge Next;

		// Token: 0x04000787 RID: 1927
		internal TEdge Prev;

		// Token: 0x04000788 RID: 1928
		internal TEdge NextInLML;

		// Token: 0x04000789 RID: 1929
		internal TEdge NextInAEL;

		// Token: 0x0400078A RID: 1930
		internal TEdge PrevInAEL;

		// Token: 0x0400078B RID: 1931
		internal TEdge NextInSEL;

		// Token: 0x0400078C RID: 1932
		internal TEdge PrevInSEL;
	}
}

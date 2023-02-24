using System;

namespace ClipperLib
{
	// Token: 0x02000169 RID: 361
	internal class OutRec
	{
		// Token: 0x04000796 RID: 1942
		internal int Idx;

		// Token: 0x04000797 RID: 1943
		internal bool IsHole;

		// Token: 0x04000798 RID: 1944
		internal bool IsOpen;

		// Token: 0x04000799 RID: 1945
		internal OutRec FirstLeft;

		// Token: 0x0400079A RID: 1946
		internal OutPt Pts;

		// Token: 0x0400079B RID: 1947
		internal OutPt BottomPt;

		// Token: 0x0400079C RID: 1948
		internal PolyNode PolyNode;
	}
}

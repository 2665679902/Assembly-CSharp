using System;

namespace ClipperLib
{
	// Token: 0x0200015C RID: 348
	public struct IntRect
	{
		// Token: 0x06000BBE RID: 3006 RVA: 0x0002E8A7 File Offset: 0x0002CAA7
		public IntRect(long l, long t, long r, long b)
		{
			this.left = l;
			this.top = t;
			this.right = r;
			this.bottom = b;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002E8C6 File Offset: 0x0002CAC6
		public IntRect(IntRect ir)
		{
			this.left = ir.left;
			this.top = ir.top;
			this.right = ir.right;
			this.bottom = ir.bottom;
		}

		// Token: 0x0400075A RID: 1882
		public long left;

		// Token: 0x0400075B RID: 1883
		public long top;

		// Token: 0x0400075C RID: 1884
		public long right;

		// Token: 0x0400075D RID: 1885
		public long bottom;
	}
}

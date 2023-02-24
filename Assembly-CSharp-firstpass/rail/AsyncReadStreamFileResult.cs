using System;

namespace rail
{
	// Token: 0x020003F8 RID: 1016
	public class AsyncReadStreamFileResult : EventBase
	{
		// Token: 0x04000F91 RID: 3985
		public uint try_read_length;

		// Token: 0x04000F92 RID: 3986
		public int offset;

		// Token: 0x04000F93 RID: 3987
		public string data;

		// Token: 0x04000F94 RID: 3988
		public string filename;
	}
}

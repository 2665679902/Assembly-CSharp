using System;

namespace rail
{
	// Token: 0x020003F7 RID: 1015
	public class AsyncReadFileResult : EventBase
	{
		// Token: 0x04000F8D RID: 3981
		public uint try_read_length;

		// Token: 0x04000F8E RID: 3982
		public int offset;

		// Token: 0x04000F8F RID: 3983
		public string data;

		// Token: 0x04000F90 RID: 3984
		public string filename;
	}
}

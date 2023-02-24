using System;

namespace rail
{
	// Token: 0x020003FB RID: 1019
	public class AsyncWriteStreamFileResult : EventBase
	{
		// Token: 0x04000F9B RID: 3995
		public uint written_length;

		// Token: 0x04000F9C RID: 3996
		public int offset;

		// Token: 0x04000F9D RID: 3997
		public uint try_write_length;

		// Token: 0x04000F9E RID: 3998
		public string filename;
	}
}

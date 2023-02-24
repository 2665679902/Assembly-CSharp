using System;

namespace rail
{
	// Token: 0x020003FA RID: 1018
	public class AsyncWriteFileResult : EventBase
	{
		// Token: 0x04000F97 RID: 3991
		public uint written_length;

		// Token: 0x04000F98 RID: 3992
		public int offset;

		// Token: 0x04000F99 RID: 3993
		public uint try_write_length;

		// Token: 0x04000F9A RID: 3994
		public string filename;
	}
}

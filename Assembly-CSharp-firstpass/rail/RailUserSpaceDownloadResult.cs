using System;

namespace rail
{
	// Token: 0x02000435 RID: 1077
	public class RailUserSpaceDownloadResult
	{
		// Token: 0x0400105A RID: 4186
		public string err_msg;

		// Token: 0x0400105B RID: 4187
		public ulong finished_bytes;

		// Token: 0x0400105C RID: 4188
		public uint finished_files;

		// Token: 0x0400105D RID: 4189
		public ulong total_bytes;

		// Token: 0x0400105E RID: 4190
		public uint total_files;

		// Token: 0x0400105F RID: 4191
		public SpaceWorkID id = new SpaceWorkID();

		// Token: 0x04001060 RID: 4192
		public uint err_code;
	}
}

using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003F5 RID: 1013
	public class AsyncListFileResult : EventBase
	{
		// Token: 0x04000F87 RID: 3975
		public List<RailStreamFileInfo> file_list = new List<RailStreamFileInfo>();

		// Token: 0x04000F88 RID: 3976
		public uint try_list_file_num;

		// Token: 0x04000F89 RID: 3977
		public uint all_file_num;

		// Token: 0x04000F8A RID: 3978
		public uint start_index;
	}
}

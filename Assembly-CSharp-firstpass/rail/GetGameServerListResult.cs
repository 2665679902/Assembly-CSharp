using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000361 RID: 865
	public class GetGameServerListResult : EventBase
	{
		// Token: 0x04000CD0 RID: 3280
		public List<GameServerInfo> server_info = new List<GameServerInfo>();

		// Token: 0x04000CD1 RID: 3281
		public uint total_num;

		// Token: 0x04000CD2 RID: 3282
		public uint start_index;

		// Token: 0x04000CD3 RID: 3283
		public uint end_index;
	}
}

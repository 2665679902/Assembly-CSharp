using System;

namespace rail
{
	// Token: 0x02000408 RID: 1032
	public class RailPlatformNotifyEventJoinGameByUser : EventBase
	{
		// Token: 0x04000FBA RID: 4026
		public RailID rail_id_to_join = new RailID();

		// Token: 0x04000FBB RID: 4027
		public string commandline_info;
	}
}

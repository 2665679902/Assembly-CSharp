using System;

namespace rail
{
	// Token: 0x020003C9 RID: 969
	public class KickOffMemberResult : EventBase
	{
		// Token: 0x04000F2F RID: 3887
		public ulong room_id;

		// Token: 0x04000F30 RID: 3888
		public RailID kicked_id = new RailID();
	}
}

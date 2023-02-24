using System;

namespace rail
{
	// Token: 0x020003D9 RID: 985
	public class SetMemberMetadataResult : EventBase
	{
		// Token: 0x04000F6F RID: 3951
		public ulong room_id;

		// Token: 0x04000F70 RID: 3952
		public RailID member_id = new RailID();
	}
}

using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003C4 RID: 964
	public class GetRoomMembersResult : EventBase
	{
		// Token: 0x04000F27 RID: 3879
		public List<RoomMemberInfo> member_infos = new List<RoomMemberInfo>();

		// Token: 0x04000F28 RID: 3880
		public ulong room_id;

		// Token: 0x04000F29 RID: 3881
		public uint member_num;
	}
}

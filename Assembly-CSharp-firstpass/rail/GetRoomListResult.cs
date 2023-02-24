using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003C3 RID: 963
	public class GetRoomListResult : EventBase
	{
		// Token: 0x04000F23 RID: 3875
		public List<RoomInfo> room_infos = new List<RoomInfo>();

		// Token: 0x04000F24 RID: 3876
		public uint total_room_num;

		// Token: 0x04000F25 RID: 3877
		public uint begin_index;

		// Token: 0x04000F26 RID: 3878
		public uint end_index;
	}
}

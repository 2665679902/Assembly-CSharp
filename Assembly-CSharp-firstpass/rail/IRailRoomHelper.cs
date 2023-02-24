using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003BA RID: 954
	public interface IRailRoomHelper
	{
		// Token: 0x06002F74 RID: 12148
		IRailRoom CreateRoom(RoomOptions options, string room_name, out RailResult result);

		// Token: 0x06002F75 RID: 12149
		IRailRoom AsyncCreateRoom(RoomOptions options, string room_name, string user_data);

		// Token: 0x06002F76 RID: 12150
		IRailRoom OpenRoom(ulong room_id, out RailResult result);

		// Token: 0x06002F77 RID: 12151
		IRailRoom AsyncOpenRoom(ulong room_id, string user_data);

		// Token: 0x06002F78 RID: 12152
		RailResult AsyncGetRoomList(uint start_index, uint end_index, List<RoomInfoListSorter> sorter, List<RoomInfoListFilter> filter, string user_data);

		// Token: 0x06002F79 RID: 12153
		RailResult AsyncGetRoomListByTags(uint start_index, uint end_index, List<RoomInfoListSorter> sorter, List<string> room_tags, string user_data);

		// Token: 0x06002F7A RID: 12154
		RailResult AsyncGetUserRoomList(string user_data);
	}
}

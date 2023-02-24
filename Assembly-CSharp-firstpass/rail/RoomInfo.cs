using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003D3 RID: 979
	public class RoomInfo
	{
		// Token: 0x04000F4A RID: 3914
		public bool has_password;

		// Token: 0x04000F4B RID: 3915
		public uint max_members;

		// Token: 0x04000F4C RID: 3916
		public string room_name;

		// Token: 0x04000F4D RID: 3917
		public RailID game_server_rail_id = new RailID();

		// Token: 0x04000F4E RID: 3918
		public uint create_time;

		// Token: 0x04000F4F RID: 3919
		public uint current_members;

		// Token: 0x04000F50 RID: 3920
		public EnumRoomType type;

		// Token: 0x04000F51 RID: 3921
		public bool is_joinable;

		// Token: 0x04000F52 RID: 3922
		public ulong room_id;

		// Token: 0x04000F53 RID: 3923
		public List<RailKeyValue> room_kvs = new List<RailKeyValue>();

		// Token: 0x04000F54 RID: 3924
		public string room_tag;

		// Token: 0x04000F55 RID: 3925
		public RailID owner_id = new RailID();
	}
}

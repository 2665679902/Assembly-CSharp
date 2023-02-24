using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003D7 RID: 983
	public class RoomMemberInfo
	{
		// Token: 0x04000F65 RID: 3941
		public string member_name;

		// Token: 0x04000F66 RID: 3942
		public uint member_index;

		// Token: 0x04000F67 RID: 3943
		public ulong room_id;

		// Token: 0x04000F68 RID: 3944
		public List<RailKeyValue> member_kvs = new List<RailKeyValue>();

		// Token: 0x04000F69 RID: 3945
		public RailID member_id = new RailID();
	}
}

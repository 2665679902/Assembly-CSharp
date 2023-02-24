using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003C2 RID: 962
	public class GetMemberMetadataResult : EventBase
	{
		// Token: 0x04000F20 RID: 3872
		public List<RailKeyValue> key_value = new List<RailKeyValue>();

		// Token: 0x04000F21 RID: 3873
		public ulong room_id;

		// Token: 0x04000F22 RID: 3874
		public RailID member_id = new RailID();
	}
}

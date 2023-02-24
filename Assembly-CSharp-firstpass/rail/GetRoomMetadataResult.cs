using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003C5 RID: 965
	public class GetRoomMetadataResult : EventBase
	{
		// Token: 0x04000F2A RID: 3882
		public List<RailKeyValue> key_value = new List<RailKeyValue>();

		// Token: 0x04000F2B RID: 3883
		public ulong room_id;
	}
}

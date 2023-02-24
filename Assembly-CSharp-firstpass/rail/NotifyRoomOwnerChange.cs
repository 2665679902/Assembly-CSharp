using System;

namespace rail
{
	// Token: 0x020003D0 RID: 976
	public class NotifyRoomOwnerChange : EventBase
	{
		// Token: 0x04000F41 RID: 3905
		public RailID old_owner_id = new RailID();

		// Token: 0x04000F42 RID: 3906
		public EnumRoomOwnerChangeReason reason;

		// Token: 0x04000F43 RID: 3907
		public ulong room_id;

		// Token: 0x04000F44 RID: 3908
		public RailID new_owner_id = new RailID();
	}
}

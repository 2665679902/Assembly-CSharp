using System;

namespace rail
{
	// Token: 0x020003CE RID: 974
	public class NotifyRoomMemberChange : EventBase
	{
		// Token: 0x04000F39 RID: 3897
		public RailID changer_id = new RailID();

		// Token: 0x04000F3A RID: 3898
		public RailID id_for_making_change = new RailID();

		// Token: 0x04000F3B RID: 3899
		public EnumRoomMemberActionStatus state_change;

		// Token: 0x04000F3C RID: 3900
		public ulong room_id;
	}
}

using System;

namespace rail
{
	// Token: 0x020003CF RID: 975
	public class NotifyRoomMemberKicked : EventBase
	{
		// Token: 0x04000F3D RID: 3901
		public RailID id_for_making_kick = new RailID();

		// Token: 0x04000F3E RID: 3902
		public uint due_to_kicker_lost_connect;

		// Token: 0x04000F3F RID: 3903
		public ulong room_id;

		// Token: 0x04000F40 RID: 3904
		public RailID kicked_id = new RailID();
	}
}

using System;

namespace rail
{
	// Token: 0x020003CD RID: 973
	public class NotifyRoomGameServerChange : EventBase
	{
		// Token: 0x04000F36 RID: 3894
		public RailID game_server_rail_id = new RailID();

		// Token: 0x04000F37 RID: 3895
		public ulong room_id;

		// Token: 0x04000F38 RID: 3896
		public ulong game_server_channel_id;
	}
}

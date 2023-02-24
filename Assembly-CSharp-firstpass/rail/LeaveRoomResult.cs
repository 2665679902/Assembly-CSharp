using System;

namespace rail
{
	// Token: 0x020003CA RID: 970
	public class LeaveRoomResult : EventBase
	{
		// Token: 0x04000F31 RID: 3889
		public EnumLeaveRoomReason reason;

		// Token: 0x04000F32 RID: 3890
		public ulong room_id;
	}
}

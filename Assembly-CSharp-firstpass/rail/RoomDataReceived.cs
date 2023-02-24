using System;

namespace rail
{
	// Token: 0x020003D2 RID: 978
	public class RoomDataReceived : EventBase
	{
		// Token: 0x04000F46 RID: 3910
		public uint data_len;

		// Token: 0x04000F47 RID: 3911
		public RailID remote_peer = new RailID();

		// Token: 0x04000F48 RID: 3912
		public uint message_type;

		// Token: 0x04000F49 RID: 3913
		public string data_buf;
	}
}

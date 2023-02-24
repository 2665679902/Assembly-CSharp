using System;

namespace rail
{
	// Token: 0x020003A8 RID: 936
	public class RailNetworkSessionState
	{
		// Token: 0x04000D68 RID: 3432
		public RailResult session_error;

		// Token: 0x04000D69 RID: 3433
		public ushort remote_port;

		// Token: 0x04000D6A RID: 3434
		public uint packets_in_send_buffer;

		// Token: 0x04000D6B RID: 3435
		public bool is_connecting;

		// Token: 0x04000D6C RID: 3436
		public uint bytes_in_send_buffer;

		// Token: 0x04000D6D RID: 3437
		public bool is_using_relay;

		// Token: 0x04000D6E RID: 3438
		public bool is_connection_active;

		// Token: 0x04000D6F RID: 3439
		public uint remote_ip;
	}
}

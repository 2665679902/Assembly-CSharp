using System;

namespace rail
{
	// Token: 0x020003A3 RID: 931
	public class CreateSessionFailed : EventBase
	{
		// Token: 0x04000D5E RID: 3422
		public RailID local_peer = new RailID();

		// Token: 0x04000D5F RID: 3423
		public RailID remote_peer = new RailID();
	}
}

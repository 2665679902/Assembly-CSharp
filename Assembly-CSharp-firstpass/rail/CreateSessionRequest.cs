using System;

namespace rail
{
	// Token: 0x020003A4 RID: 932
	public class CreateSessionRequest : EventBase
	{
		// Token: 0x04000D60 RID: 3424
		public RailID local_peer = new RailID();

		// Token: 0x04000D61 RID: 3425
		public RailID remote_peer = new RailID();
	}
}

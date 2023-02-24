using System;

namespace rail
{
	// Token: 0x020003A5 RID: 933
	public class NetworkCreateRawSessionFailed : EventBase
	{
		// Token: 0x04000D62 RID: 3426
		public RailID local_peer = new RailID();

		// Token: 0x04000D63 RID: 3427
		public RailGamePeer remote_game_peer = new RailGamePeer();
	}
}

using System;

namespace rail
{
	// Token: 0x020003A6 RID: 934
	public class NetworkCreateRawSessionRequest : EventBase
	{
		// Token: 0x04000D64 RID: 3428
		public RailID local_peer = new RailID();

		// Token: 0x04000D65 RID: 3429
		public RailGamePeer remote_game_peer = new RailGamePeer();
	}
}

using System;

namespace rail
{
	// Token: 0x02000406 RID: 1030
	public class RailPlatformNotifyEventJoinGameByGameServer : EventBase
	{
		// Token: 0x04000FB6 RID: 4022
		public string commandline_info;

		// Token: 0x04000FB7 RID: 4023
		public RailID gameserver_railid = new RailID();
	}
}

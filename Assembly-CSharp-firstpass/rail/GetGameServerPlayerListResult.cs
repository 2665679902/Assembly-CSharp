using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000363 RID: 867
	public class GetGameServerPlayerListResult : EventBase
	{
		// Token: 0x04000CD6 RID: 3286
		public RailID game_server_id = new RailID();

		// Token: 0x04000CD7 RID: 3287
		public List<GameServerPlayerInfo> server_player_info = new List<GameServerPlayerInfo>();
	}
}

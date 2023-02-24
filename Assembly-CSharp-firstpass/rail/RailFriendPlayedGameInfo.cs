using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000337 RID: 823
	public class RailFriendPlayedGameInfo
	{
		// Token: 0x04000C6C RID: 3180
		public bool in_room;

		// Token: 0x04000C6D RID: 3181
		public List<ulong> room_id_list = new List<ulong>();

		// Token: 0x04000C6E RID: 3182
		public RailID friend_id = new RailID();

		// Token: 0x04000C6F RID: 3183
		public List<ulong> game_server_id_list = new List<ulong>();

		// Token: 0x04000C70 RID: 3184
		public RailGameID game_id = new RailGameID();

		// Token: 0x04000C71 RID: 3185
		public bool in_game_server;

		// Token: 0x04000C72 RID: 3186
		public RailFriendPlayedGamePlayState friend_played_game_play_state;
	}
}

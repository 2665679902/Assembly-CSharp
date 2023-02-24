using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200035A RID: 858
	public class GameServerListFilter
	{
		// Token: 0x04000CB6 RID: 3254
		public string tags_not_contained;

		// Token: 0x04000CB7 RID: 3255
		public EnumRailOptionalValue filter_password;

		// Token: 0x04000CB8 RID: 3256
		public string filter_game_server_name;

		// Token: 0x04000CB9 RID: 3257
		public EnumRailOptionalValue filter_friends_created;

		// Token: 0x04000CBA RID: 3258
		public string tags_contained;

		// Token: 0x04000CBB RID: 3259
		public EnumRailOptionalValue filter_dedicated_server;

		// Token: 0x04000CBC RID: 3260
		public List<GameServerListFilterKey> filters = new List<GameServerListFilterKey>();

		// Token: 0x04000CBD RID: 3261
		public string filter_game_server_map;

		// Token: 0x04000CBE RID: 3262
		public string filter_game_server_host;

		// Token: 0x04000CBF RID: 3263
		public RailID owner_id = new RailID();
	}
}

using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000359 RID: 857
	public class GameServerInfo
	{
		// Token: 0x04000CA3 RID: 3235
		public List<RailKeyValue> server_kvs = new List<RailKeyValue>();

		// Token: 0x04000CA4 RID: 3236
		public RailID owner_rail_id = new RailID();

		// Token: 0x04000CA5 RID: 3237
		public string game_server_name;

		// Token: 0x04000CA6 RID: 3238
		public string server_fullname;

		// Token: 0x04000CA7 RID: 3239
		public bool is_dedicated;

		// Token: 0x04000CA8 RID: 3240
		public string server_info;

		// Token: 0x04000CA9 RID: 3241
		public string server_tags;

		// Token: 0x04000CAA RID: 3242
		public string spectator_host;

		// Token: 0x04000CAB RID: 3243
		public string server_description;

		// Token: 0x04000CAC RID: 3244
		public string server_host;

		// Token: 0x04000CAD RID: 3245
		public RailID game_server_rail_id = new RailID();

		// Token: 0x04000CAE RID: 3246
		public bool has_password;

		// Token: 0x04000CAF RID: 3247
		public string server_version;

		// Token: 0x04000CB0 RID: 3248
		public List<string> server_mods = new List<string>();

		// Token: 0x04000CB1 RID: 3249
		public uint bot_players;

		// Token: 0x04000CB2 RID: 3250
		public string game_server_map;

		// Token: 0x04000CB3 RID: 3251
		public uint max_players;

		// Token: 0x04000CB4 RID: 3252
		public uint current_players;

		// Token: 0x04000CB5 RID: 3253
		public bool is_friend_only;
	}
}

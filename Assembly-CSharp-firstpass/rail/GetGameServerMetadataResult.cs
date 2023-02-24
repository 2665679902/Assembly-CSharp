using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000362 RID: 866
	public class GetGameServerMetadataResult : EventBase
	{
		// Token: 0x04000CD4 RID: 3284
		public RailID game_server_id = new RailID();

		// Token: 0x04000CD5 RID: 3285
		public List<RailKeyValue> key_value = new List<RailKeyValue>();
	}
}

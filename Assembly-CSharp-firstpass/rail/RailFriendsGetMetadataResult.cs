using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200033D RID: 829
	public class RailFriendsGetMetadataResult : EventBase
	{
		// Token: 0x04000C7A RID: 3194
		public RailID friend_id = new RailID();

		// Token: 0x04000C7B RID: 3195
		public List<RailKeyValueResult> friend_kvs = new List<RailKeyValueResult>();
	}
}

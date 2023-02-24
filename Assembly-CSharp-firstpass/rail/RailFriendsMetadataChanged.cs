using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200033F RID: 831
	public class RailFriendsMetadataChanged : EventBase
	{
		// Token: 0x04000C7C RID: 3196
		public List<RailFriendMetadata> friends_changed_metadata = new List<RailFriendMetadata>();
	}
}

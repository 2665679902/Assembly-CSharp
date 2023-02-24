using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008E3 RID: 2275
	[Flags]
	public enum AuthScopeFlags
	{
		// Token: 0x04001EE6 RID: 7910
		NoFlags = 0,
		// Token: 0x04001EE7 RID: 7911
		BasicProfile = 1,
		// Token: 0x04001EE8 RID: 7912
		FriendsList = 2,
		// Token: 0x04001EE9 RID: 7913
		Presence = 4,
		// Token: 0x04001EEA RID: 7914
		FriendsManagement = 8
	}
}

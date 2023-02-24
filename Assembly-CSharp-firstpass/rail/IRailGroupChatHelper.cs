using System;

namespace rail
{
	// Token: 0x02000366 RID: 870
	public interface IRailGroupChatHelper
	{
		// Token: 0x06002EC4 RID: 11972
		RailResult AsyncQueryGroupsInfo(string user_data);

		// Token: 0x06002EC5 RID: 11973
		IRailGroupChat AsyncOpenGroupChat(string group_id, string user_data);
	}
}

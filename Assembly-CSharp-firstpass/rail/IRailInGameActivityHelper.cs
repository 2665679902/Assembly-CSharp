using System;

namespace rail
{
	// Token: 0x02000375 RID: 885
	public interface IRailInGameActivityHelper
	{
		// Token: 0x06002EE2 RID: 12002
		RailResult AsyncQueryGameActivity(string user_data);

		// Token: 0x06002EE3 RID: 12003
		RailResult AsyncOpenDefaultGameActivityWindow(string user_data);

		// Token: 0x06002EE4 RID: 12004
		RailResult AsyncOpenGameActivityWindow(ulong activity_id, string user_data);
	}
}

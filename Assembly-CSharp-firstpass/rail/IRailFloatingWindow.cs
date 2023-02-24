using System;

namespace rail
{
	// Token: 0x02000327 RID: 807
	public interface IRailFloatingWindow
	{
		// Token: 0x06002E2F RID: 11823
		RailResult AsyncShowRailFloatingWindow(EnumRailWindowType window_type, string user_data);

		// Token: 0x06002E30 RID: 11824
		RailResult AsyncCloseRailFloatingWindow(EnumRailWindowType window_type, string user_data);

		// Token: 0x06002E31 RID: 11825
		RailResult SetNotifyWindowPosition(EnumRailNotifyWindowType window_type, RailWindowLayout layout);

		// Token: 0x06002E32 RID: 11826
		RailResult AsyncShowStoreWindow(ulong id, RailStoreOptions options, string user_data);

		// Token: 0x06002E33 RID: 11827
		bool IsFloatingWindowAvailable();

		// Token: 0x06002E34 RID: 11828
		RailResult AsyncShowDefaultGameStoreWindow(string user_data);

		// Token: 0x06002E35 RID: 11829
		RailResult SetNotifyWindowEnable(EnumRailNotifyWindowType window_type, bool enable);
	}
}

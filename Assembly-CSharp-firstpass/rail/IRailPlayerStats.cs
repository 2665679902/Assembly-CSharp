using System;

namespace rail
{
	// Token: 0x020003EC RID: 1004
	public interface IRailPlayerStats : IRailComponent
	{
		// Token: 0x06002FB4 RID: 12212
		RailID GetRailID();

		// Token: 0x06002FB5 RID: 12213
		RailResult AsyncRequestStats(string user_data);

		// Token: 0x06002FB6 RID: 12214
		RailResult GetStatValue(string name, out int data);

		// Token: 0x06002FB7 RID: 12215
		RailResult GetStatValue(string name, out double data);

		// Token: 0x06002FB8 RID: 12216
		RailResult SetStatValue(string name, int data);

		// Token: 0x06002FB9 RID: 12217
		RailResult SetStatValue(string name, double data);

		// Token: 0x06002FBA RID: 12218
		RailResult UpdateAverageStatValue(string name, double data);

		// Token: 0x06002FBB RID: 12219
		RailResult AsyncStoreStats(string user_data);

		// Token: 0x06002FBC RID: 12220
		RailResult ResetAllStats();
	}
}

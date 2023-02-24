using System;

namespace rail
{
	// Token: 0x020003EB RID: 1003
	public interface IRailGlobalStats : IRailComponent
	{
		// Token: 0x06002FAF RID: 12207
		RailResult AsyncRequestGlobalStats(string user_data);

		// Token: 0x06002FB0 RID: 12208
		RailResult GetGlobalStatValue(string name, out long data);

		// Token: 0x06002FB1 RID: 12209
		RailResult GetGlobalStatValue(string name, out double data);

		// Token: 0x06002FB2 RID: 12210
		RailResult GetGlobalStatValueHistory(string name, long[] global_stats_data, uint data_size, out int num_global_stats);

		// Token: 0x06002FB3 RID: 12211
		RailResult GetGlobalStatValueHistory(string name, double[] global_stats_data, uint data_size, out int num_global_stats);
	}
}

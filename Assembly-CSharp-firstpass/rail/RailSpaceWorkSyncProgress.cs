using System;

namespace rail
{
	// Token: 0x02000431 RID: 1073
	public class RailSpaceWorkSyncProgress
	{
		// Token: 0x04001049 RID: 4169
		public float progress;

		// Token: 0x0400104A RID: 4170
		public ulong finished_bytes;

		// Token: 0x0400104B RID: 4171
		public ulong total_bytes;

		// Token: 0x0400104C RID: 4172
		public EnumRailSpaceWorkSyncState current_state;
	}
}

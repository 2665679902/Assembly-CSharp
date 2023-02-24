using System;

namespace rail
{
	// Token: 0x020002F2 RID: 754
	public class SplitAssetsToFinished : EventBase
	{
		// Token: 0x04000AB8 RID: 2744
		public ulong source_asset;

		// Token: 0x04000AB9 RID: 2745
		public uint to_quantity;

		// Token: 0x04000ABA RID: 2746
		public ulong split_to_asset_id;
	}
}

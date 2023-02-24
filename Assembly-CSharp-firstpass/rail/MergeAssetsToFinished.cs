using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002E9 RID: 745
	public class MergeAssetsToFinished : EventBase
	{
		// Token: 0x04000A9E RID: 2718
		public ulong merge_to_asset_id;

		// Token: 0x04000A9F RID: 2719
		public List<RailAssetItem> source_assets = new List<RailAssetItem>();
	}
}

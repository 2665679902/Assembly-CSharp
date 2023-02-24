using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002E8 RID: 744
	public class MergeAssetsFinished : EventBase
	{
		// Token: 0x04000A9C RID: 2716
		public List<RailAssetItem> source_assets = new List<RailAssetItem>();

		// Token: 0x04000A9D RID: 2717
		public ulong new_asset_id;
	}
}

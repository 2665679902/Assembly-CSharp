using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002E6 RID: 742
	public class ExchangeAssetsFinished : EventBase
	{
		// Token: 0x04000A97 RID: 2711
		public List<RailAssetItem> old_assets = new List<RailAssetItem>();

		// Token: 0x04000A98 RID: 2712
		public List<RailGeneratedAssetItem> new_asset_item_list = new List<RailGeneratedAssetItem>();
	}
}

using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002E7 RID: 743
	public class ExchangeAssetsToFinished : EventBase
	{
		// Token: 0x04000A99 RID: 2713
		public ulong exchange_to_asset_id;

		// Token: 0x04000A9A RID: 2714
		public RailProductItem to_product_info = new RailProductItem();

		// Token: 0x04000A9B RID: 2715
		public List<RailAssetItem> old_assets = new List<RailAssetItem>();
	}
}

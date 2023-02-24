using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000388 RID: 904
	public class RailInGamePurchasePurchaseProductsToAssetsResponse : EventBase
	{
		// Token: 0x04000D20 RID: 3360
		public string order_id;

		// Token: 0x04000D21 RID: 3361
		public List<RailAssetInfo> delivered_assets = new List<RailAssetInfo>();
	}
}

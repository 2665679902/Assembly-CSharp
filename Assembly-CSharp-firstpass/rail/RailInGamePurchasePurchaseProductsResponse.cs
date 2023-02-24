using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000387 RID: 903
	public class RailInGamePurchasePurchaseProductsResponse : EventBase
	{
		// Token: 0x04000D1E RID: 3358
		public string order_id;

		// Token: 0x04000D1F RID: 3359
		public List<RailProductItem> delivered_products = new List<RailProductItem>();
	}
}

using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000381 RID: 897
	public interface IRailInGamePurchase
	{
		// Token: 0x06002EF1 RID: 12017
		RailResult AsyncRequestAllPurchasableProducts(string user_data);

		// Token: 0x06002EF2 RID: 12018
		RailResult AsyncRequestAllProducts(string user_data);

		// Token: 0x06002EF3 RID: 12019
		RailResult GetProductInfo(uint product_id, RailPurchaseProductInfo product);

		// Token: 0x06002EF4 RID: 12020
		RailResult AsyncPurchaseProducts(List<RailProductItem> cart_items, string user_data);

		// Token: 0x06002EF5 RID: 12021
		RailResult AsyncFinishOrder(string order_id, string user_data);

		// Token: 0x06002EF6 RID: 12022
		RailResult AsyncPurchaseProductsToAssets(List<RailProductItem> cart_items, string user_data);
	}
}

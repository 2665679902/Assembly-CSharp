using System;

namespace rail
{
	// Token: 0x0200038C RID: 908
	public class RailPurchaseProductInfo
	{
		// Token: 0x04000D26 RID: 3366
		public string category;

		// Token: 0x04000D27 RID: 3367
		public float original_price;

		// Token: 0x04000D28 RID: 3368
		public string description;

		// Token: 0x04000D29 RID: 3369
		public RailDiscountInfo discount = new RailDiscountInfo();

		// Token: 0x04000D2A RID: 3370
		public bool is_purchasable;

		// Token: 0x04000D2B RID: 3371
		public string name;

		// Token: 0x04000D2C RID: 3372
		public string currency_type;

		// Token: 0x04000D2D RID: 3373
		public string product_thumbnail;

		// Token: 0x04000D2E RID: 3374
		public RailPurchaseProductExtraInfo extra_info = new RailPurchaseProductExtraInfo();

		// Token: 0x04000D2F RID: 3375
		public uint product_id;
	}
}

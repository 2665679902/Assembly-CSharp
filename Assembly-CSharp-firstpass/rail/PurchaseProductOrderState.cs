using System;

namespace rail
{
	// Token: 0x02000384 RID: 900
	public enum PurchaseProductOrderState
	{
		// Token: 0x04000D14 RID: 3348
		kPurchaseProductOrderStateInvalid,
		// Token: 0x04000D15 RID: 3349
		kPurchaseProductOrderStateCreateOrderOk = 100,
		// Token: 0x04000D16 RID: 3350
		kPurchaseProductOrderStatePayOk = 200,
		// Token: 0x04000D17 RID: 3351
		kPurchaseProductOrderStateDeliverOk = 300
	}
}

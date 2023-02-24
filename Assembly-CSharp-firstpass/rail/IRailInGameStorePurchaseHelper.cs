using System;

namespace rail
{
	// Token: 0x0200038D RID: 909
	public interface IRailInGameStorePurchaseHelper
	{
		// Token: 0x06002EFF RID: 12031
		RailResult AsyncShowPaymentWindow(string order_id, string user_data);
	}
}

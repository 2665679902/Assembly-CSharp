using System;

namespace rail
{
	// Token: 0x0200037B RID: 891
	public interface IRailInGameCoin
	{
		// Token: 0x06002EEA RID: 12010
		RailResult AsyncRequestCoinInfo(string user_data);

		// Token: 0x06002EEB RID: 12011
		RailResult AsyncPurchaseCoins(RailCoins purchase_info, string user_data);
	}
}

using System;

namespace rail
{
	// Token: 0x0200037C RID: 892
	public class RailCoinInfo
	{
		// Token: 0x04000CFA RID: 3322
		public string name;

		// Token: 0x04000CFB RID: 3323
		public string icon_url;

		// Token: 0x04000CFC RID: 3324
		public string description;

		// Token: 0x04000CFD RID: 3325
		public RailCurrencyExchangeCoinRate exchange_rate = new RailCurrencyExchangeCoinRate();

		// Token: 0x04000CFE RID: 3326
		public uint coin_class_id;

		// Token: 0x04000CFF RID: 3327
		public string metadata;
	}
}

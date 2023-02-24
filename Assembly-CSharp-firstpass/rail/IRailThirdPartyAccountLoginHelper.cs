using System;

namespace rail
{
	// Token: 0x0200040E RID: 1038
	public interface IRailThirdPartyAccountLoginHelper
	{
		// Token: 0x06003003 RID: 12291
		RailResult AsyncAutoLogin(string user_data);

		// Token: 0x06003004 RID: 12292
		RailResult AsyncLogin(RailThirdPartyAccountLoginOptions options, string user_data);

		// Token: 0x06003005 RID: 12293
		RailResult GetAccountInfo(RailThirdPartyAccountInfo account_info);
	}
}

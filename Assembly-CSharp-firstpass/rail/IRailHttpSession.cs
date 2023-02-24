using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200036B RID: 875
	public interface IRailHttpSession : IRailComponent
	{
		// Token: 0x06002ED3 RID: 11987
		RailResult SetRequestMethod(RailHttpSessionMethod method);

		// Token: 0x06002ED4 RID: 11988
		RailResult SetParameters(List<RailKeyValue> parameters);

		// Token: 0x06002ED5 RID: 11989
		RailResult SetPostBodyContent(string body_content);

		// Token: 0x06002ED6 RID: 11990
		RailResult SetRequestTimeOut(uint timeout_secs);

		// Token: 0x06002ED7 RID: 11991
		RailResult SetRequestHeaders(List<string> headers);

		// Token: 0x06002ED8 RID: 11992
		RailResult AsyncSendRequest(string url, string user_data);
	}
}

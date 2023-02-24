using System;

namespace rail
{
	// Token: 0x0200036C RID: 876
	public interface IRailHttpSessionHelper
	{
		// Token: 0x06002ED9 RID: 11993
		IRailHttpSession CreateHttpSession();

		// Token: 0x06002EDA RID: 11994
		IRailHttpResponse CreateHttpResponse(string http_response_data);
	}
}

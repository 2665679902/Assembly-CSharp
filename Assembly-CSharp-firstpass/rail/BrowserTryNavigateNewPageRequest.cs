using System;

namespace rail
{
	// Token: 0x020002FE RID: 766
	public class BrowserTryNavigateNewPageRequest : EventBase
	{
		// Token: 0x04000AD6 RID: 2774
		public string url;

		// Token: 0x04000AD7 RID: 2775
		public string target_type;

		// Token: 0x04000AD8 RID: 2776
		public bool is_redirect_request;
	}
}

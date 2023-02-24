using System;

namespace rail
{
	// Token: 0x020002F7 RID: 759
	public interface IRailBrowserHelper
	{
		// Token: 0x06002DBC RID: 11708
		IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data, CreateBrowserOptions options, out RailResult result);

		// Token: 0x06002DBD RID: 11709
		IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data, CreateBrowserOptions options);

		// Token: 0x06002DBE RID: 11710
		IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data);

		// Token: 0x06002DBF RID: 11711
		IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data, CreateCustomerDrawBrowserOptions options, out RailResult result);

		// Token: 0x06002DC0 RID: 11712
		IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data, CreateCustomerDrawBrowserOptions options);

		// Token: 0x06002DC1 RID: 11713
		IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data);

		// Token: 0x06002DC2 RID: 11714
		RailResult NavigateWebPage(string url, bool display_in_new_tab);
	}
}

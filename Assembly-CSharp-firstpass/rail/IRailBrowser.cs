using System;

namespace rail
{
	// Token: 0x020002F6 RID: 758
	public interface IRailBrowser : IRailComponent
	{
		// Token: 0x06002DB4 RID: 11700
		bool GetCurrentUrl(out string url);

		// Token: 0x06002DB5 RID: 11701
		bool ReloadWithUrl(string new_url);

		// Token: 0x06002DB6 RID: 11702
		bool ReloadWithUrl();

		// Token: 0x06002DB7 RID: 11703
		void StopLoad();

		// Token: 0x06002DB8 RID: 11704
		bool AddJavascriptEventListener(string event_name);

		// Token: 0x06002DB9 RID: 11705
		bool RemoveAllJavascriptEventListener();

		// Token: 0x06002DBA RID: 11706
		void AllowNavigateNewPage(bool allow);

		// Token: 0x06002DBB RID: 11707
		void Close();
	}
}

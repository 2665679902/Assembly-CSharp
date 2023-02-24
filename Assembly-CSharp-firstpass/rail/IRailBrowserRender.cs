using System;

namespace rail
{
	// Token: 0x020002F8 RID: 760
	public interface IRailBrowserRender : IRailComponent
	{
		// Token: 0x06002DC3 RID: 11715
		bool GetCurrentUrl(out string url);

		// Token: 0x06002DC4 RID: 11716
		bool ReloadWithUrl(string new_url);

		// Token: 0x06002DC5 RID: 11717
		bool ReloadWithUrl();

		// Token: 0x06002DC6 RID: 11718
		void StopLoad();

		// Token: 0x06002DC7 RID: 11719
		bool AddJavascriptEventListener(string event_name);

		// Token: 0x06002DC8 RID: 11720
		bool RemoveAllJavascriptEventListener();

		// Token: 0x06002DC9 RID: 11721
		void AllowNavigateNewPage(bool allow);

		// Token: 0x06002DCA RID: 11722
		void Close();

		// Token: 0x06002DCB RID: 11723
		void UpdateCustomDrawWindowPos(int content_offset_x, int content_offset_y, uint content_window_width, uint content_window_height);

		// Token: 0x06002DCC RID: 11724
		void SetBrowserActive(bool active);

		// Token: 0x06002DCD RID: 11725
		void GoBack();

		// Token: 0x06002DCE RID: 11726
		void GoForward();

		// Token: 0x06002DCF RID: 11727
		bool ExecuteJavascript(string event_name, string event_value);

		// Token: 0x06002DD0 RID: 11728
		void DispatchWindowsMessage(uint window_msg, uint w_param, uint l_param);

		// Token: 0x06002DD1 RID: 11729
		void DispatchMouseMessage(EnumRailMouseActionType button_action, uint user_define_mouse_key, uint x_pos, uint y_pos);

		// Token: 0x06002DD2 RID: 11730
		void MouseWheel(int delta, uint user_define_mouse_key, uint x_pos, uint y_pos);

		// Token: 0x06002DD3 RID: 11731
		void SetFocus(bool has_focus);

		// Token: 0x06002DD4 RID: 11732
		void KeyDown(uint key_code);

		// Token: 0x06002DD5 RID: 11733
		void KeyUp(uint key_code);

		// Token: 0x06002DD6 RID: 11734
		void KeyChar(uint key_code, bool is_uinchar);
	}
}

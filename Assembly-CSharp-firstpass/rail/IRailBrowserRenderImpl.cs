using System;

namespace rail
{
	// Token: 0x0200029D RID: 669
	public class IRailBrowserRenderImpl : RailObject, IRailBrowserRender, IRailComponent
	{
		// Token: 0x06002825 RID: 10277 RVA: 0x0004FA1B File Offset: 0x0004DC1B
		internal IRailBrowserRenderImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0004FA2C File Offset: 0x0004DC2C
		~IRailBrowserRenderImpl()
		{
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0004FA54 File Offset: 0x0004DC54
		public virtual bool GetCurrentUrl(out string url)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailBrowserRender_GetCurrentUrl(this.swigCPtr_, intPtr);
			}
			finally
			{
				url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x0004FA9C File Offset: 0x0004DC9C
		public virtual bool ReloadWithUrl(string new_url)
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_ReloadWithUrl__SWIG_0(this.swigCPtr_, new_url);
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0004FAAA File Offset: 0x0004DCAA
		public virtual bool ReloadWithUrl()
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_ReloadWithUrl__SWIG_1(this.swigCPtr_);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x0004FAB7 File Offset: 0x0004DCB7
		public virtual void StopLoad()
		{
			RAIL_API_PINVOKE.IRailBrowserRender_StopLoad(this.swigCPtr_);
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x0004FAC4 File Offset: 0x0004DCC4
		public virtual bool AddJavascriptEventListener(string event_name)
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_AddJavascriptEventListener(this.swigCPtr_, event_name);
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0004FAD2 File Offset: 0x0004DCD2
		public virtual bool RemoveAllJavascriptEventListener()
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_RemoveAllJavascriptEventListener(this.swigCPtr_);
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0004FADF File Offset: 0x0004DCDF
		public virtual void AllowNavigateNewPage(bool allow)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_AllowNavigateNewPage(this.swigCPtr_, allow);
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0004FAED File Offset: 0x0004DCED
		public virtual void Close()
		{
			RAIL_API_PINVOKE.IRailBrowserRender_Close(this.swigCPtr_);
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0004FAFA File Offset: 0x0004DCFA
		public virtual void UpdateCustomDrawWindowPos(int content_offset_x, int content_offset_y, uint content_window_width, uint content_window_height)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_UpdateCustomDrawWindowPos(this.swigCPtr_, content_offset_x, content_offset_y, content_window_width, content_window_height);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0004FB0C File Offset: 0x0004DD0C
		public virtual void SetBrowserActive(bool active)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_SetBrowserActive(this.swigCPtr_, active);
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x0004FB1A File Offset: 0x0004DD1A
		public virtual void GoBack()
		{
			RAIL_API_PINVOKE.IRailBrowserRender_GoBack(this.swigCPtr_);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0004FB27 File Offset: 0x0004DD27
		public virtual void GoForward()
		{
			RAIL_API_PINVOKE.IRailBrowserRender_GoForward(this.swigCPtr_);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0004FB34 File Offset: 0x0004DD34
		public virtual bool ExecuteJavascript(string event_name, string event_value)
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_ExecuteJavascript(this.swigCPtr_, event_name, event_value);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0004FB43 File Offset: 0x0004DD43
		public virtual void DispatchWindowsMessage(uint window_msg, uint w_param, uint l_param)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_DispatchWindowsMessage(this.swigCPtr_, window_msg, w_param, l_param);
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0004FB53 File Offset: 0x0004DD53
		public virtual void DispatchMouseMessage(EnumRailMouseActionType button_action, uint user_define_mouse_key, uint x_pos, uint y_pos)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_DispatchMouseMessage(this.swigCPtr_, (int)button_action, user_define_mouse_key, x_pos, y_pos);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0004FB65 File Offset: 0x0004DD65
		public virtual void MouseWheel(int delta, uint user_define_mouse_key, uint x_pos, uint y_pos)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_MouseWheel(this.swigCPtr_, delta, user_define_mouse_key, x_pos, y_pos);
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0004FB77 File Offset: 0x0004DD77
		public virtual void SetFocus(bool has_focus)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_SetFocus(this.swigCPtr_, has_focus);
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x0004FB85 File Offset: 0x0004DD85
		public virtual void KeyDown(uint key_code)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_KeyDown(this.swigCPtr_, key_code);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x0004FB93 File Offset: 0x0004DD93
		public virtual void KeyUp(uint key_code)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_KeyUp(this.swigCPtr_, key_code);
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x0004FBA1 File Offset: 0x0004DDA1
		public virtual void KeyChar(uint key_code, bool is_uinchar)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_KeyChar(this.swigCPtr_, key_code, is_uinchar);
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x0004FBB0 File Offset: 0x0004DDB0
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0004FBBD File Offset: 0x0004DDBD
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

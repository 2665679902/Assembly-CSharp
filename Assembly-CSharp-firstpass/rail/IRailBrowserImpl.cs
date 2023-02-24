using System;

namespace rail
{
	// Token: 0x0200029B RID: 667
	public class IRailBrowserImpl : RailObject, IRailBrowser, IRailComponent
	{
		// Token: 0x06002810 RID: 10256 RVA: 0x0004F6C2 File Offset: 0x0004D8C2
		internal IRailBrowserImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x0004F6D4 File Offset: 0x0004D8D4
		~IRailBrowserImpl()
		{
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x0004F6FC File Offset: 0x0004D8FC
		public virtual bool GetCurrentUrl(out string url)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailBrowser_GetCurrentUrl(this.swigCPtr_, intPtr);
			}
			finally
			{
				url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x0004F744 File Offset: 0x0004D944
		public virtual bool ReloadWithUrl(string new_url)
		{
			return RAIL_API_PINVOKE.IRailBrowser_ReloadWithUrl__SWIG_0(this.swigCPtr_, new_url);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x0004F752 File Offset: 0x0004D952
		public virtual bool ReloadWithUrl()
		{
			return RAIL_API_PINVOKE.IRailBrowser_ReloadWithUrl__SWIG_1(this.swigCPtr_);
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0004F75F File Offset: 0x0004D95F
		public virtual void StopLoad()
		{
			RAIL_API_PINVOKE.IRailBrowser_StopLoad(this.swigCPtr_);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0004F76C File Offset: 0x0004D96C
		public virtual bool AddJavascriptEventListener(string event_name)
		{
			return RAIL_API_PINVOKE.IRailBrowser_AddJavascriptEventListener(this.swigCPtr_, event_name);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x0004F77A File Offset: 0x0004D97A
		public virtual bool RemoveAllJavascriptEventListener()
		{
			return RAIL_API_PINVOKE.IRailBrowser_RemoveAllJavascriptEventListener(this.swigCPtr_);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x0004F787 File Offset: 0x0004D987
		public virtual void AllowNavigateNewPage(bool allow)
		{
			RAIL_API_PINVOKE.IRailBrowser_AllowNavigateNewPage(this.swigCPtr_, allow);
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0004F795 File Offset: 0x0004D995
		public virtual void Close()
		{
			RAIL_API_PINVOKE.IRailBrowser_Close(this.swigCPtr_);
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0004F7A2 File Offset: 0x0004D9A2
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0004F7AF File Offset: 0x0004D9AF
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

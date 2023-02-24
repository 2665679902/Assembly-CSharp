using System;

namespace rail
{
	// Token: 0x0200029C RID: 668
	public class IRailBrowserHelperImpl : RailObject, IRailBrowserHelper
	{
		// Token: 0x0600281C RID: 10268 RVA: 0x0004F7BC File Offset: 0x0004D9BC
		internal IRailBrowserHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x0004F7CC File Offset: 0x0004D9CC
		~IRailBrowserHelperImpl()
		{
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x0004F7F4 File Offset: 0x0004D9F4
		public virtual IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data, CreateBrowserOptions options, out RailResult result)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateBrowserOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailBrowser railBrowser;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailBrowserHelper_AsyncCreateBrowser__SWIG_0(this.swigCPtr_, url, window_width, window_height, user_data, intPtr, out result);
				railBrowser = ((intPtr2 == IntPtr.Zero) ? null : new IRailBrowserImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateBrowserOptions(intPtr);
			}
			return railBrowser;
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x0004F864 File Offset: 0x0004DA64
		public virtual IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data, CreateBrowserOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateBrowserOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailBrowser railBrowser;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailBrowserHelper_AsyncCreateBrowser__SWIG_1(this.swigCPtr_, url, window_width, window_height, user_data, intPtr);
				railBrowser = ((intPtr2 == IntPtr.Zero) ? null : new IRailBrowserImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateBrowserOptions(intPtr);
			}
			return railBrowser;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x0004F8D4 File Offset: 0x0004DAD4
		public virtual IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailBrowserHelper_AsyncCreateBrowser__SWIG_2(this.swigCPtr_, url, window_width, window_height, user_data);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailBrowserImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x0004F908 File Offset: 0x0004DB08
		public virtual IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data, CreateCustomerDrawBrowserOptions options, out RailResult result)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateCustomerDrawBrowserOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailBrowserRender railBrowserRender;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_0(this.swigCPtr_, url, user_data, intPtr, out result);
				railBrowserRender = ((intPtr2 == IntPtr.Zero) ? null : new IRailBrowserRenderImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateCustomerDrawBrowserOptions(intPtr);
			}
			return railBrowserRender;
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x0004F974 File Offset: 0x0004DB74
		public virtual IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data, CreateCustomerDrawBrowserOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateCustomerDrawBrowserOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailBrowserRender railBrowserRender;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_1(this.swigCPtr_, url, user_data, intPtr);
				railBrowserRender = ((intPtr2 == IntPtr.Zero) ? null : new IRailBrowserRenderImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateCustomerDrawBrowserOptions(intPtr);
			}
			return railBrowserRender;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x0004F9DC File Offset: 0x0004DBDC
		public virtual IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_2(this.swigCPtr_, url, user_data);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailBrowserRenderImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x0004FA0C File Offset: 0x0004DC0C
		public virtual RailResult NavigateWebPage(string url, bool display_in_new_tab)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailBrowserHelper_NavigateWebPage(this.swigCPtr_, url, display_in_new_tab);
		}
	}
}

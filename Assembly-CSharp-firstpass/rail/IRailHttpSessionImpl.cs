using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002AB RID: 683
	public class IRailHttpSessionImpl : RailObject, IRailHttpSession, IRailComponent
	{
		// Token: 0x06002911 RID: 10513 RVA: 0x00051EB6 File Offset: 0x000500B6
		internal IRailHttpSessionImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x00051EC8 File Offset: 0x000500C8
		~IRailHttpSessionImpl()
		{
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00051EF0 File Offset: 0x000500F0
		public virtual RailResult SetRequestMethod(RailHttpSessionMethod method)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetRequestMethod(this.swigCPtr_, (int)method);
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x00051F00 File Offset: 0x00050100
		public virtual RailResult SetParameters(List<RailKeyValue> parameters)
		{
			IntPtr intPtr = ((parameters == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (parameters != null)
			{
				RailConverter.Csharp2Cpp(parameters, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetParameters(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x00051F50 File Offset: 0x00050150
		public virtual RailResult SetPostBodyContent(string body_content)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetPostBodyContent(this.swigCPtr_, body_content);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x00051F5E File Offset: 0x0005015E
		public virtual RailResult SetRequestTimeOut(uint timeout_secs)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetRequestTimeOut(this.swigCPtr_, timeout_secs);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x00051F6C File Offset: 0x0005016C
		public virtual RailResult SetRequestHeaders(List<string> headers)
		{
			IntPtr intPtr = ((headers == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (headers != null)
			{
				RailConverter.Csharp2Cpp(headers, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetRequestHeaders(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x00051FBC File Offset: 0x000501BC
		public virtual RailResult AsyncSendRequest(string url, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailHttpSession_AsyncSendRequest(this.swigCPtr_, url, user_data);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x00051FCB File Offset: 0x000501CB
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00051FD8 File Offset: 0x000501D8
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

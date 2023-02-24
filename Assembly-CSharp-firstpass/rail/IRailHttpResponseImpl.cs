using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002AA RID: 682
	public class IRailHttpResponseImpl : RailObject, IRailHttpResponse, IRailComponent
	{
		// Token: 0x06002903 RID: 10499 RVA: 0x00051D78 File Offset: 0x0004FF78
		internal IRailHttpResponseImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x00051D88 File Offset: 0x0004FF88
		~IRailHttpResponseImpl()
		{
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x00051DB0 File Offset: 0x0004FFB0
		public virtual int GetHttpResponseCode()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetHttpResponseCode(this.swigCPtr_);
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00051DC0 File Offset: 0x0004FFC0
		public virtual RailResult GetResponseHeaderKeys(List<string> header_keys)
		{
			IntPtr intPtr = ((header_keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailHttpResponse_GetResponseHeaderKeys(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (header_keys != null)
				{
					RailConverter.Cpp2Csharp(intPtr, header_keys);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x00051E10 File Offset: 0x00050010
		public virtual string GetResponseHeaderValue(string header_key)
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailHttpResponse_GetResponseHeaderValue(this.swigCPtr_, header_key));
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x00051E23 File Offset: 0x00050023
		public virtual string GetResponseBodyData()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailHttpResponse_GetResponseBodyData(this.swigCPtr_));
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x00051E35 File Offset: 0x00050035
		public virtual uint GetContentLength()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetContentLength(this.swigCPtr_);
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x00051E42 File Offset: 0x00050042
		public virtual string GetContentType()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailHttpResponse_GetContentType(this.swigCPtr_));
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x00051E54 File Offset: 0x00050054
		public virtual string GetContentRange()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailHttpResponse_GetContentRange(this.swigCPtr_));
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00051E66 File Offset: 0x00050066
		public virtual string GetContentLanguage()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailHttpResponse_GetContentLanguage(this.swigCPtr_));
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00051E78 File Offset: 0x00050078
		public virtual string GetContentEncoding()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailHttpResponse_GetContentEncoding(this.swigCPtr_));
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00051E8A File Offset: 0x0005008A
		public virtual string GetLastModified()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailHttpResponse_GetLastModified(this.swigCPtr_));
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00051E9C File Offset: 0x0005009C
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x00051EA9 File Offset: 0x000500A9
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

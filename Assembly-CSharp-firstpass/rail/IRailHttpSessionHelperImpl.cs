using System;

namespace rail
{
	// Token: 0x020002AC RID: 684
	public class IRailHttpSessionHelperImpl : RailObject, IRailHttpSessionHelper
	{
		// Token: 0x0600291B RID: 10523 RVA: 0x00051FE5 File Offset: 0x000501E5
		internal IRailHttpSessionHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x00051FF4 File Offset: 0x000501F4
		~IRailHttpSessionHelperImpl()
		{
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0005201C File Offset: 0x0005021C
		public virtual IRailHttpSession CreateHttpSession()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailHttpSessionHelper_CreateHttpSession(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailHttpSessionImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x0005204C File Offset: 0x0005024C
		public virtual IRailHttpResponse CreateHttpResponse(string http_response_data)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailHttpSessionHelper_CreateHttpResponse(this.swigCPtr_, http_response_data);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailHttpResponseImpl(intPtr);
			}
			return null;
		}
	}
}

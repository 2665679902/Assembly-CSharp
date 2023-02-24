using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002BD RID: 701
	public class IRailSmallObjectServiceHelperImpl : RailObject, IRailSmallObjectServiceHelper
	{
		// Token: 0x060029D6 RID: 10710 RVA: 0x000541CD File Offset: 0x000523CD
		internal IRailSmallObjectServiceHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000541DC File Offset: 0x000523DC
		~IRailSmallObjectServiceHelperImpl()
		{
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x00054204 File Offset: 0x00052404
		public virtual RailResult AsyncDownloadObjects(List<uint> indexes, string user_data)
		{
			IntPtr intPtr = ((indexes == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayuint32_t__SWIG_0());
			if (indexes != null)
			{
				RailConverter.Csharp2Cpp(indexes, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSmallObjectServiceHelper_AsyncDownloadObjects(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayuint32_t(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x00054254 File Offset: 0x00052454
		public virtual RailResult GetObjectContent(uint index, out string content)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSmallObjectServiceHelper_GetObjectContent(this.swigCPtr_, index, intPtr);
			}
			finally
			{
				content = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x0005429C File Offset: 0x0005249C
		public virtual RailResult AsyncQueryObjectState(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSmallObjectServiceHelper_AsyncQueryObjectState(this.swigCPtr_, user_data);
		}
	}
}

using System;

namespace rail
{
	// Token: 0x020002C4 RID: 708
	public class IRailThirdPartyAccountLoginHelperImpl : RailObject, IRailThirdPartyAccountLoginHelper
	{
		// Token: 0x06002A46 RID: 10822 RVA: 0x0005533D File Offset: 0x0005353D
		internal IRailThirdPartyAccountLoginHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x0005534C File Offset: 0x0005354C
		~IRailThirdPartyAccountLoginHelperImpl()
		{
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00055374 File Offset: 0x00053574
		public virtual RailResult AsyncAutoLogin(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailThirdPartyAccountLoginHelper_AsyncAutoLogin(this.swigCPtr_, user_data);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00055384 File Offset: 0x00053584
		public virtual RailResult AsyncLogin(RailThirdPartyAccountLoginOptions options, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailThirdPartyAccountLoginOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailThirdPartyAccountLoginHelper_AsyncLogin(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailThirdPartyAccountLoginOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000553D4 File Offset: 0x000535D4
		public virtual RailResult GetAccountInfo(RailThirdPartyAccountInfo account_info)
		{
			IntPtr intPtr = ((account_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailThirdPartyAccountInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailThirdPartyAccountLoginHelper_GetAccountInfo(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (account_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr, account_info);
				}
				RAIL_API_PINVOKE.delete_RailThirdPartyAccountInfo(intPtr);
			}
			return railResult;
		}
	}
}

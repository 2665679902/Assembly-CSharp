using System;

namespace rail
{
	// Token: 0x020002AF RID: 687
	public class IRailInGameCoinImpl : RailObject, IRailInGameCoin
	{
		// Token: 0x06002928 RID: 10536 RVA: 0x000521B7 File Offset: 0x000503B7
		internal IRailInGameCoinImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000521C8 File Offset: 0x000503C8
		~IRailInGameCoinImpl()
		{
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000521F0 File Offset: 0x000503F0
		public virtual RailResult AsyncRequestCoinInfo(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGameCoin_AsyncRequestCoinInfo(this.swigCPtr_, user_data);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00052200 File Offset: 0x00050400
		public virtual RailResult AsyncPurchaseCoins(RailCoins purchase_info, string user_data)
		{
			IntPtr intPtr = ((purchase_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailCoins__SWIG_0());
			if (purchase_info != null)
			{
				RailConverter.Csharp2Cpp(purchase_info, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailInGameCoin_AsyncPurchaseCoins(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailCoins(intPtr);
			}
			return railResult;
		}
	}
}

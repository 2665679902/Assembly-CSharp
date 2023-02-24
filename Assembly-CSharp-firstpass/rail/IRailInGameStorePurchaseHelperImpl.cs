using System;

namespace rail
{
	// Token: 0x020002B1 RID: 689
	public class IRailInGameStorePurchaseHelperImpl : RailObject, IRailInGameStorePurchaseHelper
	{
		// Token: 0x06002934 RID: 10548 RVA: 0x000523A4 File Offset: 0x000505A4
		internal IRailInGameStorePurchaseHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000523B4 File Offset: 0x000505B4
		~IRailInGameStorePurchaseHelperImpl()
		{
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000523DC File Offset: 0x000505DC
		public virtual RailResult AsyncShowPaymentWindow(string order_id, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGameStorePurchaseHelper_AsyncShowPaymentWindow(this.swigCPtr_, order_id, user_data);
		}
	}
}

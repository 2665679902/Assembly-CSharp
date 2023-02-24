using System;

namespace rail
{
	// Token: 0x02000297 RID: 663
	public class IRailAntiAddictionHelperImpl : RailObject, IRailAntiAddictionHelper
	{
		// Token: 0x060027F1 RID: 10225 RVA: 0x0004F092 File Offset: 0x0004D292
		internal IRailAntiAddictionHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x0004F0A4 File Offset: 0x0004D2A4
		~IRailAntiAddictionHelperImpl()
		{
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x0004F0CC File Offset: 0x0004D2CC
		public virtual RailResult AsyncQueryGameOnlineTime(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAntiAddictionHelper_AsyncQueryGameOnlineTime(this.swigCPtr_, user_data);
		}
	}
}

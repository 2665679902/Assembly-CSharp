using System;

namespace rail
{
	// Token: 0x020002AE RID: 686
	public class IRailInGameActivityHelperImpl : RailObject, IRailInGameActivityHelper
	{
		// Token: 0x06002923 RID: 10531 RVA: 0x00052154 File Offset: 0x00050354
		internal IRailInGameActivityHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x00052164 File Offset: 0x00050364
		~IRailInGameActivityHelperImpl()
		{
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x0005218C File Offset: 0x0005038C
		public virtual RailResult AsyncQueryGameActivity(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGameActivityHelper_AsyncQueryGameActivity(this.swigCPtr_, user_data);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x0005219A File Offset: 0x0005039A
		public virtual RailResult AsyncOpenDefaultGameActivityWindow(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGameActivityHelper_AsyncOpenDefaultGameActivityWindow(this.swigCPtr_, user_data);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000521A8 File Offset: 0x000503A8
		public virtual RailResult AsyncOpenGameActivityWindow(ulong activity_id, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGameActivityHelper_AsyncOpenGameActivityWindow(this.swigCPtr_, activity_id, user_data);
		}
	}
}

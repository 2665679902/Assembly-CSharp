using System;

namespace rail
{
	// Token: 0x020002A9 RID: 681
	public class IRailGroupChatHelperImpl : RailObject, IRailGroupChatHelper
	{
		// Token: 0x060028FF RID: 10495 RVA: 0x00051CFF File Offset: 0x0004FEFF
		internal IRailGroupChatHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x00051D10 File Offset: 0x0004FF10
		~IRailGroupChatHelperImpl()
		{
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x00051D38 File Offset: 0x0004FF38
		public virtual RailResult AsyncQueryGroupsInfo(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGroupChatHelper_AsyncQueryGroupsInfo(this.swigCPtr_, user_data);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x00051D48 File Offset: 0x0004FF48
		public virtual IRailGroupChat AsyncOpenGroupChat(string group_id, string user_data)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGroupChatHelper_AsyncOpenGroupChat(this.swigCPtr_, group_id, user_data);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGroupChatImpl(intPtr);
			}
			return null;
		}
	}
}

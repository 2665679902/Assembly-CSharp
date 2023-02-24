using System;

namespace rail
{
	// Token: 0x020002A8 RID: 680
	public class IRailGroupChatImpl : RailObject, IRailGroupChat, IRailComponent
	{
		// Token: 0x060028F9 RID: 10489 RVA: 0x00051C4E File Offset: 0x0004FE4E
		internal IRailGroupChatImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x00051C60 File Offset: 0x0004FE60
		~IRailGroupChatImpl()
		{
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x00051C88 File Offset: 0x0004FE88
		public virtual RailResult GetGroupInfo(RailGroupInfo group_info)
		{
			IntPtr intPtr = ((group_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGroupInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGroupChat_GetGroupInfo(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (group_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr, group_info);
				}
				RAIL_API_PINVOKE.delete_RailGroupInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x00051CD8 File Offset: 0x0004FED8
		public virtual RailResult OpenGroupWindow()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGroupChat_OpenGroupWindow(this.swigCPtr_);
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x00051CE5 File Offset: 0x0004FEE5
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x00051CF2 File Offset: 0x0004FEF2
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

using System;

namespace rail
{
	// Token: 0x020002A6 RID: 678
	public class IRailGlobalAchievementImpl : RailObject, IRailGlobalAchievement, IRailComponent
	{
		// Token: 0x060028E9 RID: 10473 RVA: 0x00051AEC File Offset: 0x0004FCEC
		internal IRailGlobalAchievementImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x00051AFC File Offset: 0x0004FCFC
		~IRailGlobalAchievementImpl()
		{
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x00051B24 File Offset: 0x0004FD24
		public virtual RailResult AsyncRequestAchievement(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalAchievement_AsyncRequestAchievement(this.swigCPtr_, user_data);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x00051B32 File Offset: 0x0004FD32
		public virtual RailResult GetGlobalAchievedPercent(string name, out double percent)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalAchievement_GetGlobalAchievedPercent(this.swigCPtr_, name, out percent);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x00051B44 File Offset: 0x0004FD44
		public virtual RailResult GetGlobalAchievedPercentDescending(int index, out string name, out double percent)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGlobalAchievement_GetGlobalAchievedPercentDescending(this.swigCPtr_, index, intPtr, out percent);
			}
			finally
			{
				name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x00051B90 File Offset: 0x0004FD90
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x00051B9D File Offset: 0x0004FD9D
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

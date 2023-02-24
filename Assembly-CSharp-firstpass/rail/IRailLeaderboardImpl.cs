using System;

namespace rail
{
	// Token: 0x020002B2 RID: 690
	public class IRailLeaderboardImpl : RailObject, IRailLeaderboard, IRailComponent
	{
		// Token: 0x06002937 RID: 10551 RVA: 0x000523EB File Offset: 0x000505EB
		internal IRailLeaderboardImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000523FC File Offset: 0x000505FC
		~IRailLeaderboardImpl()
		{
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x00052424 File Offset: 0x00050624
		public virtual string GetLeaderboardName()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardName(this.swigCPtr_));
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x00052436 File Offset: 0x00050636
		public virtual string GetLeaderboardDisplayName()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardDisplayName(this.swigCPtr_));
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x00052448 File Offset: 0x00050648
		public virtual int GetTotalEntriesCount()
		{
			return RAIL_API_PINVOKE.IRailLeaderboard_GetTotalEntriesCount(this.swigCPtr_);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x00052455 File Offset: 0x00050655
		public virtual RailResult AsyncGetLeaderboard(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_AsyncGetLeaderboard(this.swigCPtr_, user_data);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x00052464 File Offset: 0x00050664
		public virtual RailResult GetLeaderboardParameters(LeaderboardParameters param)
		{
			IntPtr intPtr = ((param == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_LeaderboardParameters__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardParameters(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (param != null)
				{
					RailConverter.Cpp2Csharp(intPtr, param);
				}
				RAIL_API_PINVOKE.delete_LeaderboardParameters(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000524B4 File Offset: 0x000506B4
		public virtual IRailLeaderboardEntries CreateLeaderboardEntries()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboard_CreateLeaderboardEntries(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailLeaderboardEntriesImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000524E4 File Offset: 0x000506E4
		public virtual RailResult AsyncUploadLeaderboard(UploadLeaderboardParam update_param, string user_data)
		{
			IntPtr intPtr = ((update_param == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_UploadLeaderboardParam__SWIG_0());
			if (update_param != null)
			{
				RailConverter.Csharp2Cpp(update_param, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_AsyncUploadLeaderboard(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_UploadLeaderboardParam(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x00052534 File Offset: 0x00050734
		public virtual RailResult GetLeaderboardSortType(out int sort_type)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardSortType(this.swigCPtr_, out sort_type);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x00052542 File Offset: 0x00050742
		public virtual RailResult GetLeaderboardDisplayType(out int display_type)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_GetLeaderboardDisplayType(this.swigCPtr_, out display_type);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x00052550 File Offset: 0x00050750
		public virtual RailResult AsyncAttachSpaceWork(SpaceWorkID spacework_id, string user_data)
		{
			IntPtr intPtr = ((spacework_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0());
			if (spacework_id != null)
			{
				RailConverter.Csharp2Cpp(spacework_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboard_AsyncAttachSpaceWork(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000525AC File Offset: 0x000507AC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000525B9 File Offset: 0x000507B9
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

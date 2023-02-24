using System;

namespace rail
{
	// Token: 0x020002B3 RID: 691
	public class IRailLeaderboardEntriesImpl : RailObject, IRailLeaderboardEntries, IRailComponent
	{
		// Token: 0x06002945 RID: 10565 RVA: 0x000525C6 File Offset: 0x000507C6
		internal IRailLeaderboardEntriesImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000525D8 File Offset: 0x000507D8
		~IRailLeaderboardEntriesImpl()
		{
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x00052600 File Offset: 0x00050800
		public virtual RailID GetRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboardEntries_GetRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x00052625 File Offset: 0x00050825
		public virtual string GetLeaderboardName()
		{
			return UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.IRailLeaderboardEntries_GetLeaderboardName(this.swigCPtr_));
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x00052638 File Offset: 0x00050838
		public virtual RailResult AsyncRequestLeaderboardEntries(RailID player, RequestLeaderboardEntryParam param, string user_data)
		{
			IntPtr intPtr = ((player == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player != null)
			{
				RailConverter.Csharp2Cpp(player, intPtr);
			}
			IntPtr intPtr2 = ((param == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RequestLeaderboardEntryParam__SWIG_0());
			if (param != null)
			{
				RailConverter.Csharp2Cpp(param, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboardEntries_AsyncRequestLeaderboardEntries(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RequestLeaderboardEntryParam(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000526B8 File Offset: 0x000508B8
		public virtual RequestLeaderboardEntryParam GetEntriesParam()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboardEntries_GetEntriesParam(this.swigCPtr_);
			RequestLeaderboardEntryParam requestLeaderboardEntryParam = new RequestLeaderboardEntryParam();
			RailConverter.Cpp2Csharp(intPtr, requestLeaderboardEntryParam);
			return requestLeaderboardEntryParam;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000526DD File Offset: 0x000508DD
		public virtual int GetEntriesCount()
		{
			return RAIL_API_PINVOKE.IRailLeaderboardEntries_GetEntriesCount(this.swigCPtr_);
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000526EC File Offset: 0x000508EC
		public virtual RailResult GetLeaderboardEntry(int index, LeaderboardEntry leaderboard_entry)
		{
			IntPtr intPtr = ((leaderboard_entry == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_LeaderboardEntry__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailLeaderboardEntries_GetLeaderboardEntry(this.swigCPtr_, index, intPtr);
			}
			finally
			{
				if (leaderboard_entry != null)
				{
					RailConverter.Cpp2Csharp(intPtr, leaderboard_entry);
				}
				RAIL_API_PINVOKE.delete_LeaderboardEntry(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x0005273C File Offset: 0x0005093C
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x00052749 File Offset: 0x00050949
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

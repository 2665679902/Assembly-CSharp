using System;

namespace rail
{
	// Token: 0x020002BF RID: 703
	public class IRailStatisticHelperImpl : RailObject, IRailStatisticHelper
	{
		// Token: 0x06002A15 RID: 10773 RVA: 0x00054C6E File Offset: 0x00052E6E
		internal IRailStatisticHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x00054C80 File Offset: 0x00052E80
		~IRailStatisticHelperImpl()
		{
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x00054CA8 File Offset: 0x00052EA8
		public virtual IRailPlayerStats CreatePlayerStats(RailID player)
		{
			IntPtr intPtr = ((player == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player != null)
			{
				RailConverter.Csharp2Cpp(player, intPtr);
			}
			IRailPlayerStats railPlayerStats;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailStatisticHelper_CreatePlayerStats(this.swigCPtr_, intPtr);
				railPlayerStats = ((intPtr2 == IntPtr.Zero) ? null : new IRailPlayerStatsImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railPlayerStats;
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x00054D1C File Offset: 0x00052F1C
		public virtual IRailGlobalStats GetGlobalStats()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailStatisticHelper_GetGlobalStats(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGlobalStatsImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x00054D4A File Offset: 0x00052F4A
		public virtual RailResult AsyncGetNumberOfPlayer(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailStatisticHelper_AsyncGetNumberOfPlayer(this.swigCPtr_, user_data);
		}
	}
}

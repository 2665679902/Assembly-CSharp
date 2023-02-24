using System;

namespace rail
{
	// Token: 0x020002B4 RID: 692
	public class IRailLeaderboardHelperImpl : RailObject, IRailLeaderboardHelper
	{
		// Token: 0x0600294F RID: 10575 RVA: 0x00052756 File Offset: 0x00050956
		internal IRailLeaderboardHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x00052768 File Offset: 0x00050968
		~IRailLeaderboardHelperImpl()
		{
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x00052790 File Offset: 0x00050990
		public virtual IRailLeaderboard OpenLeaderboard(string leaderboard_name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboardHelper_OpenLeaderboard(this.swigCPtr_, leaderboard_name);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailLeaderboardImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000527C0 File Offset: 0x000509C0
		public virtual IRailLeaderboard AsyncCreateLeaderboard(string leaderboard_name, LeaderboardSortType sort_type, LeaderboardDisplayType display_type, string user_data, out RailResult result)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailLeaderboardHelper_AsyncCreateLeaderboard(this.swigCPtr_, leaderboard_name, (int)sort_type, (int)display_type, user_data, out result);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailLeaderboardImpl(intPtr);
			}
			return null;
		}
	}
}

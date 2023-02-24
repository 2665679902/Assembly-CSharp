using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007FD RID: 2045
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserScoresQueryStatInfoInternal : IDisposable
	{
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x000918B4 File Offset: 0x0008FAB4
		// (set) Token: 0x0600498E RID: 18830 RVA: 0x000918D6 File Offset: 0x0008FAD6
		public int ApiVersion
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ApiVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ApiVersion, value);
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x000918E8 File Offset: 0x0008FAE8
		// (set) Token: 0x06004990 RID: 18832 RVA: 0x0009190A File Offset: 0x0008FB0A
		public string StatName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_StatName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_StatName, value);
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06004991 RID: 18833 RVA: 0x0009191C File Offset: 0x0008FB1C
		// (set) Token: 0x06004992 RID: 18834 RVA: 0x0009193E File Offset: 0x0008FB3E
		public LeaderboardAggregation Aggregation
		{
			get
			{
				LeaderboardAggregation @default = Helper.GetDefault<LeaderboardAggregation>();
				Helper.TryMarshalGet<LeaderboardAggregation>(this.m_Aggregation, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<LeaderboardAggregation>(ref this.m_Aggregation, value);
			}
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x0009194D File Offset: 0x0008FB4D
		public void Dispose()
		{
		}

		// Token: 0x04001C5F RID: 7263
		private int m_ApiVersion;

		// Token: 0x04001C60 RID: 7264
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_StatName;

		// Token: 0x04001C61 RID: 7265
		private LeaderboardAggregation m_Aggregation;
	}
}

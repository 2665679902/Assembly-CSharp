using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007F9 RID: 2041
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardRanksOptionsInternal : IDisposable
	{
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x0009169C File Offset: 0x0008F89C
		// (set) Token: 0x0600496E RID: 18798 RVA: 0x000916BE File Offset: 0x0008F8BE
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

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600496F RID: 18799 RVA: 0x000916D0 File Offset: 0x0008F8D0
		// (set) Token: 0x06004970 RID: 18800 RVA: 0x000916F2 File Offset: 0x0008F8F2
		public string LeaderboardId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LeaderboardId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LeaderboardId, value);
			}
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x00091701 File Offset: 0x0008F901
		public void Dispose()
		{
		}

		// Token: 0x04001C50 RID: 7248
		private int m_ApiVersion;

		// Token: 0x04001C51 RID: 7249
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LeaderboardId;
	}
}

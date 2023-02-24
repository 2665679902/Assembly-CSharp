using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D3 RID: 2003
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal : IDisposable
	{
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600488D RID: 18573 RVA: 0x00090878 File Offset: 0x0008EA78
		// (set) Token: 0x0600488E RID: 18574 RVA: 0x0009089A File Offset: 0x0008EA9A
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

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x000908AC File Offset: 0x0008EAAC
		// (set) Token: 0x06004890 RID: 18576 RVA: 0x000908CE File Offset: 0x0008EACE
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

		// Token: 0x06004891 RID: 18577 RVA: 0x000908DD File Offset: 0x0008EADD
		public void Dispose()
		{
		}

		// Token: 0x04001BF8 RID: 7160
		private int m_ApiVersion;

		// Token: 0x04001BF9 RID: 7161
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LeaderboardId;
	}
}

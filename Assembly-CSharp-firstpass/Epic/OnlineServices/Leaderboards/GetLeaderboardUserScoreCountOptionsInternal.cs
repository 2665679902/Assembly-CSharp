using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007E3 RID: 2019
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLeaderboardUserScoreCountOptionsInternal : IDisposable
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x00090DAC File Offset: 0x0008EFAC
		// (set) Token: 0x060048E6 RID: 18662 RVA: 0x00090DCE File Offset: 0x0008EFCE
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

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x00090DE0 File Offset: 0x0008EFE0
		// (set) Token: 0x060048E8 RID: 18664 RVA: 0x00090E02 File Offset: 0x0008F002
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

		// Token: 0x060048E9 RID: 18665 RVA: 0x00090E11 File Offset: 0x0008F011
		public void Dispose()
		{
		}

		// Token: 0x04001C18 RID: 7192
		private int m_ApiVersion;

		// Token: 0x04001C19 RID: 7193
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_StatName;
	}
}

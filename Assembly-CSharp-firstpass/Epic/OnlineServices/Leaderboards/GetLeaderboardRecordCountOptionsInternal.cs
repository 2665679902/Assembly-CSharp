using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007E1 RID: 2017
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLeaderboardRecordCountOptionsInternal : IDisposable
	{
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060048DE RID: 18654 RVA: 0x00090D5C File Offset: 0x0008EF5C
		// (set) Token: 0x060048DF RID: 18655 RVA: 0x00090D7E File Offset: 0x0008EF7E
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

		// Token: 0x060048E0 RID: 18656 RVA: 0x00090D8D File Offset: 0x0008EF8D
		public void Dispose()
		{
		}

		// Token: 0x04001C16 RID: 7190
		private int m_ApiVersion;
	}
}

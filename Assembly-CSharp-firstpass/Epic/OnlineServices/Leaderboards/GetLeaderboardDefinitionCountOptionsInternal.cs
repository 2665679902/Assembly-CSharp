using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007DF RID: 2015
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLeaderboardDefinitionCountOptionsInternal : IDisposable
	{
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x00090D1C File Offset: 0x0008EF1C
		// (set) Token: 0x060048DA RID: 18650 RVA: 0x00090D3E File Offset: 0x0008EF3E
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

		// Token: 0x060048DB RID: 18651 RVA: 0x00090D4D File Offset: 0x0008EF4D
		public void Dispose()
		{
		}

		// Token: 0x04001C15 RID: 7189
		private int m_ApiVersion;
	}
}

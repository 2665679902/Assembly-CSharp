using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D1 RID: 2001
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardDefinitionByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06004884 RID: 18564 RVA: 0x000907F4 File Offset: 0x0008E9F4
		// (set) Token: 0x06004885 RID: 18565 RVA: 0x00090816 File Offset: 0x0008EA16
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

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06004886 RID: 18566 RVA: 0x00090828 File Offset: 0x0008EA28
		// (set) Token: 0x06004887 RID: 18567 RVA: 0x0009084A File Offset: 0x0008EA4A
		public uint LeaderboardIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_LeaderboardIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_LeaderboardIndex, value);
			}
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x00090859 File Offset: 0x0008EA59
		public void Dispose()
		{
		}

		// Token: 0x04001BF5 RID: 7157
		private int m_ApiVersion;

		// Token: 0x04001BF6 RID: 7158
		private uint m_LeaderboardIndex;
	}
}

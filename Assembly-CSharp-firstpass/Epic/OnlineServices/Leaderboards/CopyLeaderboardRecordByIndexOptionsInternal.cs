using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D5 RID: 2005
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardRecordByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06004896 RID: 18582 RVA: 0x000908FC File Offset: 0x0008EAFC
		// (set) Token: 0x06004897 RID: 18583 RVA: 0x0009091E File Offset: 0x0008EB1E
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

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x00090930 File Offset: 0x0008EB30
		// (set) Token: 0x06004899 RID: 18585 RVA: 0x00090952 File Offset: 0x0008EB52
		public uint LeaderboardRecordIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_LeaderboardRecordIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_LeaderboardRecordIndex, value);
			}
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x00090961 File Offset: 0x0008EB61
		public void Dispose()
		{
		}

		// Token: 0x04001BFB RID: 7163
		private int m_ApiVersion;

		// Token: 0x04001BFC RID: 7164
		private uint m_LeaderboardRecordIndex;
	}
}

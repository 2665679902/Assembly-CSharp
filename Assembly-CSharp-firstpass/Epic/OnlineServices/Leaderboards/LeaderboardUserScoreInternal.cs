using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007E8 RID: 2024
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardUserScoreInternal : IDisposable
	{
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06004905 RID: 18693 RVA: 0x00090F94 File Offset: 0x0008F194
		// (set) Token: 0x06004906 RID: 18694 RVA: 0x00090FB6 File Offset: 0x0008F1B6
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

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x00090FC8 File Offset: 0x0008F1C8
		// (set) Token: 0x06004908 RID: 18696 RVA: 0x00090FEA File Offset: 0x0008F1EA
		public ProductUserId UserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06004909 RID: 18697 RVA: 0x00090FFC File Offset: 0x0008F1FC
		// (set) Token: 0x0600490A RID: 18698 RVA: 0x0009101E File Offset: 0x0008F21E
		public int Score
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_Score, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_Score, value);
			}
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x0009102D File Offset: 0x0008F22D
		public void Dispose()
		{
		}

		// Token: 0x04001C2A RID: 7210
		private int m_ApiVersion;

		// Token: 0x04001C2B RID: 7211
		private IntPtr m_UserId;

		// Token: 0x04001C2C RID: 7212
		private int m_Score;
	}
}

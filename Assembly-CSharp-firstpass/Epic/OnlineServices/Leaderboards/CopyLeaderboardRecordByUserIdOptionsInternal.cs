using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D7 RID: 2007
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardRecordByUserIdOptionsInternal : IDisposable
	{
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600489F RID: 18591 RVA: 0x00090980 File Offset: 0x0008EB80
		// (set) Token: 0x060048A0 RID: 18592 RVA: 0x000909A2 File Offset: 0x0008EBA2
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

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060048A1 RID: 18593 RVA: 0x000909B4 File Offset: 0x0008EBB4
		// (set) Token: 0x060048A2 RID: 18594 RVA: 0x000909D6 File Offset: 0x0008EBD6
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

		// Token: 0x060048A3 RID: 18595 RVA: 0x000909E5 File Offset: 0x0008EBE5
		public void Dispose()
		{
		}

		// Token: 0x04001BFE RID: 7166
		private int m_ApiVersion;

		// Token: 0x04001BFF RID: 7167
		private IntPtr m_UserId;
	}
}

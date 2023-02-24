using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007E6 RID: 2022
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardRecordInternal : IDisposable
	{
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x00090E64 File Offset: 0x0008F064
		// (set) Token: 0x060048F5 RID: 18677 RVA: 0x00090E86 File Offset: 0x0008F086
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

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x00090E98 File Offset: 0x0008F098
		// (set) Token: 0x060048F7 RID: 18679 RVA: 0x00090EBA File Offset: 0x0008F0BA
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

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x00090ECC File Offset: 0x0008F0CC
		// (set) Token: 0x060048F9 RID: 18681 RVA: 0x00090EEE File Offset: 0x0008F0EE
		public uint Rank
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_Rank, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_Rank, value);
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060048FA RID: 18682 RVA: 0x00090F00 File Offset: 0x0008F100
		// (set) Token: 0x060048FB RID: 18683 RVA: 0x00090F22 File Offset: 0x0008F122
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

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x00090F34 File Offset: 0x0008F134
		// (set) Token: 0x060048FD RID: 18685 RVA: 0x00090F56 File Offset: 0x0008F156
		public string UserDisplayName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_UserDisplayName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_UserDisplayName, value);
			}
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x00090F65 File Offset: 0x0008F165
		public void Dispose()
		{
		}

		// Token: 0x04001C23 RID: 7203
		private int m_ApiVersion;

		// Token: 0x04001C24 RID: 7204
		private IntPtr m_UserId;

		// Token: 0x04001C25 RID: 7205
		private uint m_Rank;

		// Token: 0x04001C26 RID: 7206
		private int m_Score;

		// Token: 0x04001C27 RID: 7207
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_UserDisplayName;
	}
}

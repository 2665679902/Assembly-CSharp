using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007BD RID: 1981
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PromoteMemberOptionsInternal : IDisposable
	{
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600480E RID: 18446 RVA: 0x000900A4 File Offset: 0x0008E2A4
		// (set) Token: 0x0600480F RID: 18447 RVA: 0x000900C6 File Offset: 0x0008E2C6
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

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06004810 RID: 18448 RVA: 0x000900D8 File Offset: 0x0008E2D8
		// (set) Token: 0x06004811 RID: 18449 RVA: 0x000900FA File Offset: 0x0008E2FA
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x0009010C File Offset: 0x0008E30C
		// (set) Token: 0x06004813 RID: 18451 RVA: 0x0009012E File Offset: 0x0008E32E
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x00090140 File Offset: 0x0008E340
		// (set) Token: 0x06004815 RID: 18453 RVA: 0x00090162 File Offset: 0x0008E362
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x00090171 File Offset: 0x0008E371
		public void Dispose()
		{
		}

		// Token: 0x04001BC1 RID: 7105
		private int m_ApiVersion;

		// Token: 0x04001BC2 RID: 7106
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;

		// Token: 0x04001BC3 RID: 7107
		private IntPtr m_LocalUserId;

		// Token: 0x04001BC4 RID: 7108
		private IntPtr m_TargetUserId;
	}
}

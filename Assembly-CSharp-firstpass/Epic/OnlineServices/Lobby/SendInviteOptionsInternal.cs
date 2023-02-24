using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C9 RID: 1993
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteOptionsInternal : IDisposable
	{
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x0009050C File Offset: 0x0008E70C
		// (set) Token: 0x06004857 RID: 18519 RVA: 0x0009052E File Offset: 0x0008E72E
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

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x00090540 File Offset: 0x0008E740
		// (set) Token: 0x06004859 RID: 18521 RVA: 0x00090562 File Offset: 0x0008E762
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

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x00090574 File Offset: 0x0008E774
		// (set) Token: 0x0600485B RID: 18523 RVA: 0x00090596 File Offset: 0x0008E796
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

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x000905A8 File Offset: 0x0008E7A8
		// (set) Token: 0x0600485D RID: 18525 RVA: 0x000905CA File Offset: 0x0008E7CA
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

		// Token: 0x0600485E RID: 18526 RVA: 0x000905D9 File Offset: 0x0008E7D9
		public void Dispose()
		{
		}

		// Token: 0x04001BE2 RID: 7138
		private int m_ApiVersion;

		// Token: 0x04001BE3 RID: 7139
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;

		// Token: 0x04001BE4 RID: 7140
		private IntPtr m_LocalUserId;

		// Token: 0x04001BE5 RID: 7141
		private IntPtr m_TargetUserId;
	}
}

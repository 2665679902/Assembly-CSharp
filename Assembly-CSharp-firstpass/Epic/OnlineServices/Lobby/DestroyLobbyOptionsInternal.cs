using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000740 RID: 1856
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroyLobbyOptionsInternal : IDisposable
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06004535 RID: 17717 RVA: 0x0008D458 File Offset: 0x0008B658
		// (set) Token: 0x06004536 RID: 17718 RVA: 0x0008D47A File Offset: 0x0008B67A
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

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x0008D48C File Offset: 0x0008B68C
		// (set) Token: 0x06004538 RID: 17720 RVA: 0x0008D4AE File Offset: 0x0008B6AE
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

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06004539 RID: 17721 RVA: 0x0008D4C0 File Offset: 0x0008B6C0
		// (set) Token: 0x0600453A RID: 17722 RVA: 0x0008D4E2 File Offset: 0x0008B6E2
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

		// Token: 0x0600453B RID: 17723 RVA: 0x0008D4F1 File Offset: 0x0008B6F1
		public void Dispose()
		{
		}

		// Token: 0x04001ABD RID: 6845
		private int m_ApiVersion;

		// Token: 0x04001ABE RID: 6846
		private IntPtr m_LocalUserId;

		// Token: 0x04001ABF RID: 6847
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}

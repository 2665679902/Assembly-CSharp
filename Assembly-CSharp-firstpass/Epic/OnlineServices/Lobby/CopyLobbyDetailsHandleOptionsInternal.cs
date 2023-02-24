using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000736 RID: 1846
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLobbyDetailsHandleOptionsInternal : IDisposable
	{
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x0008D058 File Offset: 0x0008B258
		// (set) Token: 0x060044F5 RID: 17653 RVA: 0x0008D07A File Offset: 0x0008B27A
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

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060044F6 RID: 17654 RVA: 0x0008D08C File Offset: 0x0008B28C
		// (set) Token: 0x060044F7 RID: 17655 RVA: 0x0008D0AE File Offset: 0x0008B2AE
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

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x0008D0C0 File Offset: 0x0008B2C0
		// (set) Token: 0x060044F9 RID: 17657 RVA: 0x0008D0E2 File Offset: 0x0008B2E2
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

		// Token: 0x060044FA RID: 17658 RVA: 0x0008D0F1 File Offset: 0x0008B2F1
		public void Dispose()
		{
		}

		// Token: 0x04001AA0 RID: 6816
		private int m_ApiVersion;

		// Token: 0x04001AA1 RID: 6817
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;

		// Token: 0x04001AA2 RID: 6818
		private IntPtr m_LocalUserId;
	}
}

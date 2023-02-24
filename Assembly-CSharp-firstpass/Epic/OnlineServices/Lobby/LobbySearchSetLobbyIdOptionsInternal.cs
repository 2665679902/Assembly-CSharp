using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000791 RID: 1937
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetLobbyIdOptionsInternal : IDisposable
	{
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600474F RID: 18255 RVA: 0x0008FCF8 File Offset: 0x0008DEF8
		// (set) Token: 0x06004750 RID: 18256 RVA: 0x0008FD1A File Offset: 0x0008DF1A
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

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06004751 RID: 18257 RVA: 0x0008FD2C File Offset: 0x0008DF2C
		// (set) Token: 0x06004752 RID: 18258 RVA: 0x0008FD4E File Offset: 0x0008DF4E
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

		// Token: 0x06004753 RID: 18259 RVA: 0x0008FD5D File Offset: 0x0008DF5D
		public void Dispose()
		{
		}

		// Token: 0x04001BA7 RID: 7079
		private int m_ApiVersion;

		// Token: 0x04001BA8 RID: 7080
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}

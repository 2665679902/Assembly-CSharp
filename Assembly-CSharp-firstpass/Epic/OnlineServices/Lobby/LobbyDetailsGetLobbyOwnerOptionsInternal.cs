using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000762 RID: 1890
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetLobbyOwnerOptionsInternal : IDisposable
	{
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x0008E1E8 File Offset: 0x0008C3E8
		// (set) Token: 0x060045FF RID: 17919 RVA: 0x0008E20A File Offset: 0x0008C40A
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

		// Token: 0x06004600 RID: 17920 RVA: 0x0008E219 File Offset: 0x0008C419
		public void Dispose()
		{
		}

		// Token: 0x04001B08 RID: 6920
		private int m_ApiVersion;
	}
}

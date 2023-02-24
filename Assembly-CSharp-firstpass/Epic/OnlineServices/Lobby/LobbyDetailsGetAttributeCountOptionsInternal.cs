using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000760 RID: 1888
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetAttributeCountOptionsInternal : IDisposable
	{
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x0008E1A8 File Offset: 0x0008C3A8
		// (set) Token: 0x060045FA RID: 17914 RVA: 0x0008E1CA File Offset: 0x0008C3CA
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

		// Token: 0x060045FB RID: 17915 RVA: 0x0008E1D9 File Offset: 0x0008C3D9
		public void Dispose()
		{
		}

		// Token: 0x04001B07 RID: 6919
		private int m_ApiVersion;
	}
}

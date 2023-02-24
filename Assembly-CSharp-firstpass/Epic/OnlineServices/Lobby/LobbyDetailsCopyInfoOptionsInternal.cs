using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200075A RID: 1882
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyInfoOptionsInternal : IDisposable
	{
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x0008DFD8 File Offset: 0x0008C1D8
		// (set) Token: 0x060045DB RID: 17883 RVA: 0x0008DFFA File Offset: 0x0008C1FA
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

		// Token: 0x060045DC RID: 17884 RVA: 0x0008E009 File Offset: 0x0008C209
		public void Dispose()
		{
		}

		// Token: 0x04001AFC RID: 6908
		private int m_ApiVersion;
	}
}

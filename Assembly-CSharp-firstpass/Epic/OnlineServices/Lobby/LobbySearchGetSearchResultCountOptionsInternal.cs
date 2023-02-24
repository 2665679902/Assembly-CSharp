using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200078B RID: 1931
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchGetSearchResultCountOptionsInternal : IDisposable
	{
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06004733 RID: 18227 RVA: 0x0008FBE0 File Offset: 0x0008DDE0
		// (set) Token: 0x06004734 RID: 18228 RVA: 0x0008FC02 File Offset: 0x0008DE02
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

		// Token: 0x06004735 RID: 18229 RVA: 0x0008FC11 File Offset: 0x0008DE11
		public void Dispose()
		{
		}

		// Token: 0x04001BA0 RID: 7072
		private int m_ApiVersion;
	}
}

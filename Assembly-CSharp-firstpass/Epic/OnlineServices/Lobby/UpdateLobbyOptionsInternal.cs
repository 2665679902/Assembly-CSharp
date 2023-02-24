using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007CF RID: 1999
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateLobbyOptionsInternal : IDisposable
	{
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x00090770 File Offset: 0x0008E970
		// (set) Token: 0x0600487C RID: 18556 RVA: 0x00090792 File Offset: 0x0008E992
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

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x000907A4 File Offset: 0x0008E9A4
		// (set) Token: 0x0600487E RID: 18558 RVA: 0x000907C6 File Offset: 0x0008E9C6
		public LobbyModification LobbyModificationHandle
		{
			get
			{
				LobbyModification @default = Helper.GetDefault<LobbyModification>();
				Helper.TryMarshalGet<LobbyModification>(this.m_LobbyModificationHandle, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyModificationHandle, value);
			}
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x000907D5 File Offset: 0x0008E9D5
		public void Dispose()
		{
		}

		// Token: 0x04001BF2 RID: 7154
		private int m_ApiVersion;

		// Token: 0x04001BF3 RID: 7155
		private IntPtr m_LobbyModificationHandle;
	}
}

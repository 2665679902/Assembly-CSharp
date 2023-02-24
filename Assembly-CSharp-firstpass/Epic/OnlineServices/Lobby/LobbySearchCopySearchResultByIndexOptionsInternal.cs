using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000785 RID: 1925
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchCopySearchResultByIndexOptionsInternal : IDisposable
	{
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600471B RID: 18203 RVA: 0x0008FA6C File Offset: 0x0008DC6C
		// (set) Token: 0x0600471C RID: 18204 RVA: 0x0008FA8E File Offset: 0x0008DC8E
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

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600471D RID: 18205 RVA: 0x0008FAA0 File Offset: 0x0008DCA0
		// (set) Token: 0x0600471E RID: 18206 RVA: 0x0008FAC2 File Offset: 0x0008DCC2
		public uint LobbyIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_LobbyIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_LobbyIndex, value);
			}
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x0008FAD1 File Offset: 0x0008DCD1
		public void Dispose()
		{
		}

		// Token: 0x04001B97 RID: 7063
		private int m_ApiVersion;

		// Token: 0x04001B98 RID: 7064
		private uint m_LobbyIndex;
	}
}

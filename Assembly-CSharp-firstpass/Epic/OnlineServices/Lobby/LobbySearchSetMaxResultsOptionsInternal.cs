using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000793 RID: 1939
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetMaxResultsOptionsInternal : IDisposable
	{
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06004758 RID: 18264 RVA: 0x0008FD7C File Offset: 0x0008DF7C
		// (set) Token: 0x06004759 RID: 18265 RVA: 0x0008FD9E File Offset: 0x0008DF9E
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

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600475A RID: 18266 RVA: 0x0008FDB0 File Offset: 0x0008DFB0
		// (set) Token: 0x0600475B RID: 18267 RVA: 0x0008FDD2 File Offset: 0x0008DFD2
		public uint MaxResults
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MaxResults, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MaxResults, value);
			}
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x0008FDE1 File Offset: 0x0008DFE1
		public void Dispose()
		{
		}

		// Token: 0x04001BAA RID: 7082
		private int m_ApiVersion;

		// Token: 0x04001BAB RID: 7083
		private uint m_MaxResults;
	}
}

using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000720 RID: 1824
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyJoinLobbyAcceptedOptionsInternal : IDisposable
	{
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x0008C92C File Offset: 0x0008AB2C
		// (set) Token: 0x0600448D RID: 17549 RVA: 0x0008C94E File Offset: 0x0008AB4E
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

		// Token: 0x0600448E RID: 17550 RVA: 0x0008C95D File Offset: 0x0008AB5D
		public void Dispose()
		{
		}

		// Token: 0x04001A7E RID: 6782
		private int m_ApiVersion;
	}
}

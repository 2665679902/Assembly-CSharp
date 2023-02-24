using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000728 RID: 1832
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyMemberUpdateReceivedOptionsInternal : IDisposable
	{
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x0008CA2C File Offset: 0x0008AC2C
		// (set) Token: 0x060044A1 RID: 17569 RVA: 0x0008CA4E File Offset: 0x0008AC4E
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

		// Token: 0x060044A2 RID: 17570 RVA: 0x0008CA5D File Offset: 0x0008AC5D
		public void Dispose()
		{
		}

		// Token: 0x04001A82 RID: 6786
		private int m_ApiVersion;
	}
}

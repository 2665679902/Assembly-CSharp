using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200072A RID: 1834
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyUpdateReceivedOptionsInternal : IDisposable
	{
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060044A5 RID: 17573 RVA: 0x0008CA6C File Offset: 0x0008AC6C
		// (set) Token: 0x060044A6 RID: 17574 RVA: 0x0008CA8E File Offset: 0x0008AC8E
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

		// Token: 0x060044A7 RID: 17575 RVA: 0x0008CA9D File Offset: 0x0008AC9D
		public void Dispose()
		{
		}

		// Token: 0x04001A83 RID: 6787
		private int m_ApiVersion;
	}
}

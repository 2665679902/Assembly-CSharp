using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000722 RID: 1826
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyInviteAcceptedOptionsInternal : IDisposable
	{
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06004491 RID: 17553 RVA: 0x0008C96C File Offset: 0x0008AB6C
		// (set) Token: 0x06004492 RID: 17554 RVA: 0x0008C98E File Offset: 0x0008AB8E
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

		// Token: 0x06004493 RID: 17555 RVA: 0x0008C99D File Offset: 0x0008AB9D
		public void Dispose()
		{
		}

		// Token: 0x04001A7F RID: 6783
		private int m_ApiVersion;
	}
}

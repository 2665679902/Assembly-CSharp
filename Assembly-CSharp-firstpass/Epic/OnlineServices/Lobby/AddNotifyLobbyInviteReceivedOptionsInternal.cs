using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000724 RID: 1828
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyInviteReceivedOptionsInternal : IDisposable
	{
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x0008C9AC File Offset: 0x0008ABAC
		// (set) Token: 0x06004497 RID: 17559 RVA: 0x0008C9CE File Offset: 0x0008ABCE
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

		// Token: 0x06004498 RID: 17560 RVA: 0x0008C9DD File Offset: 0x0008ABDD
		public void Dispose()
		{
		}

		// Token: 0x04001A80 RID: 6784
		private int m_ApiVersion;
	}
}

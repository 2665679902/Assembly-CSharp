using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000726 RID: 1830
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyMemberStatusReceivedOptionsInternal : IDisposable
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x0008C9EC File Offset: 0x0008ABEC
		// (set) Token: 0x0600449C RID: 17564 RVA: 0x0008CA0E File Offset: 0x0008AC0E
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

		// Token: 0x0600449D RID: 17565 RVA: 0x0008CA1D File Offset: 0x0008AC1D
		public void Dispose()
		{
		}

		// Token: 0x04001A81 RID: 6785
		private int m_ApiVersion;
	}
}

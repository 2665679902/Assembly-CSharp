using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000768 RID: 1896
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetMemberCountOptionsInternal : IDisposable
	{
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06004615 RID: 17941 RVA: 0x0008E330 File Offset: 0x0008C530
		// (set) Token: 0x06004616 RID: 17942 RVA: 0x0008E352 File Offset: 0x0008C552
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

		// Token: 0x06004617 RID: 17943 RVA: 0x0008E361 File Offset: 0x0008C561
		public void Dispose()
		{
		}

		// Token: 0x04001B0F RID: 6927
		private int m_ApiVersion;
	}
}

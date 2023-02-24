using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200063D RID: 1597
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetMaxPlayersOptionsInternal : IDisposable
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06003EA3 RID: 16035 RVA: 0x000864C8 File Offset: 0x000846C8
		// (set) Token: 0x06003EA4 RID: 16036 RVA: 0x000864EA File Offset: 0x000846EA
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

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x000864FC File Offset: 0x000846FC
		// (set) Token: 0x06003EA6 RID: 16038 RVA: 0x0008651E File Offset: 0x0008471E
		public uint MaxPlayers
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MaxPlayers, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MaxPlayers, value);
			}
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x0008652D File Offset: 0x0008472D
		public void Dispose()
		{
		}

		// Token: 0x040017CC RID: 6092
		private int m_ApiVersion;

		// Token: 0x040017CD RID: 6093
		private uint m_MaxPlayers;
	}
}

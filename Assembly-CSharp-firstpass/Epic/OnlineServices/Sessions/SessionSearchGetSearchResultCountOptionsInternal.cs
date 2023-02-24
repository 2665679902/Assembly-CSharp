using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000648 RID: 1608
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchGetSearchResultCountOptionsInternal : IDisposable
	{
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x0008696C File Offset: 0x00084B6C
		// (set) Token: 0x06003EE2 RID: 16098 RVA: 0x0008698E File Offset: 0x00084B8E
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

		// Token: 0x06003EE3 RID: 16099 RVA: 0x0008699D File Offset: 0x00084B9D
		public void Dispose()
		{
		}

		// Token: 0x040017DB RID: 6107
		private int m_ApiVersion;
	}
}

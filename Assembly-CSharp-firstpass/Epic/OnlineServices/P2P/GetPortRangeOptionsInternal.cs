using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006EE RID: 1774
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPortRangeOptionsInternal : IDisposable
	{
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x0008B4E0 File Offset: 0x000896E0
		// (set) Token: 0x06004368 RID: 17256 RVA: 0x0008B502 File Offset: 0x00089702
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

		// Token: 0x06004369 RID: 17257 RVA: 0x0008B511 File Offset: 0x00089711
		public void Dispose()
		{
		}

		// Token: 0x040019D6 RID: 6614
		private int m_ApiVersion;
	}
}

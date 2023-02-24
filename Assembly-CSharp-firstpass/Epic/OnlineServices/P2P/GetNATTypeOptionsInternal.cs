using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006EA RID: 1770
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetNATTypeOptionsInternal : IDisposable
	{
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x0008B3CC File Offset: 0x000895CC
		// (set) Token: 0x06004356 RID: 17238 RVA: 0x0008B3EE File Offset: 0x000895EE
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

		// Token: 0x06004357 RID: 17239 RVA: 0x0008B3FD File Offset: 0x000895FD
		public void Dispose()
		{
		}

		// Token: 0x040019D0 RID: 6608
		private int m_ApiVersion;
	}
}

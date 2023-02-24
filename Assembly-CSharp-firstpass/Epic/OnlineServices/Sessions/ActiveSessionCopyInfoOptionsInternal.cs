using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005BC RID: 1468
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionCopyInfoOptionsInternal : IDisposable
	{
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x00083F20 File Offset: 0x00082120
		// (set) Token: 0x06003BF5 RID: 15349 RVA: 0x00083F42 File Offset: 0x00082142
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

		// Token: 0x06003BF6 RID: 15350 RVA: 0x00083F51 File Offset: 0x00082151
		public void Dispose()
		{
		}

		// Token: 0x040016E0 RID: 5856
		private int m_ApiVersion;
	}
}

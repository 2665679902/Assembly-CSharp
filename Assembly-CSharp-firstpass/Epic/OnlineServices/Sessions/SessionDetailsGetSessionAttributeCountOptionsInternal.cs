using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000626 RID: 1574
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsGetSessionAttributeCountOptionsInternal : IDisposable
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x00085A44 File Offset: 0x00083C44
		// (set) Token: 0x06003E03 RID: 15875 RVA: 0x00085A66 File Offset: 0x00083C66
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

		// Token: 0x06003E04 RID: 15876 RVA: 0x00085A75 File Offset: 0x00083C75
		public void Dispose()
		{
		}

		// Token: 0x04001790 RID: 6032
		private int m_ApiVersion;
	}
}

using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000701 RID: 1793
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryNATTypeOptionsInternal : IDisposable
	{
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060043D7 RID: 17367 RVA: 0x0008BCB4 File Offset: 0x00089EB4
		// (set) Token: 0x060043D8 RID: 17368 RVA: 0x0008BCD6 File Offset: 0x00089ED6
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

		// Token: 0x060043D9 RID: 17369 RVA: 0x0008BCE5 File Offset: 0x00089EE5
		public void Dispose()
		{
		}

		// Token: 0x04001A0A RID: 6666
		private int m_ApiVersion;
	}
}

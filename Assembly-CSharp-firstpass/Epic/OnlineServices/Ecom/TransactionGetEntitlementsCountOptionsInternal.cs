using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000885 RID: 2181
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransactionGetEntitlementsCountOptionsInternal : IDisposable
	{
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06004D77 RID: 19831 RVA: 0x00095888 File Offset: 0x00093A88
		// (set) Token: 0x06004D78 RID: 19832 RVA: 0x000958AA File Offset: 0x00093AAA
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

		// Token: 0x06004D79 RID: 19833 RVA: 0x000958B9 File Offset: 0x00093AB9
		public void Dispose()
		{
		}

		// Token: 0x04001E09 RID: 7689
		private int m_ApiVersion;
	}
}

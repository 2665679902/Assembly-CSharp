using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008DB RID: 2267
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlinkAccountOptionsInternal : IDisposable
	{
		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x000976CC File Offset: 0x000958CC
		// (set) Token: 0x06004F77 RID: 20343 RVA: 0x000976EE File Offset: 0x000958EE
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

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x00097700 File Offset: 0x00095900
		// (set) Token: 0x06004F79 RID: 20345 RVA: 0x00097722 File Offset: 0x00095922
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00097731 File Offset: 0x00095931
		public void Dispose()
		{
		}

		// Token: 0x04001ED1 RID: 7889
		private int m_ApiVersion;

		// Token: 0x04001ED2 RID: 7890
		private IntPtr m_LocalUserId;
	}
}

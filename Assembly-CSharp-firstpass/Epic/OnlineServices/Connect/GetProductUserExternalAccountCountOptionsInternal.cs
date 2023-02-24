using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008A9 RID: 2217
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProductUserExternalAccountCountOptionsInternal : IDisposable
	{
		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x00096B04 File Offset: 0x00094D04
		// (set) Token: 0x06004E65 RID: 20069 RVA: 0x00096B26 File Offset: 0x00094D26
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

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x00096B38 File Offset: 0x00094D38
		// (set) Token: 0x06004E67 RID: 20071 RVA: 0x00096B5A File Offset: 0x00094D5A
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x00096B69 File Offset: 0x00094D69
		public void Dispose()
		{
		}

		// Token: 0x04001E78 RID: 7800
		private int m_ApiVersion;

		// Token: 0x04001E79 RID: 7801
		private IntPtr m_TargetUserId;
	}
}

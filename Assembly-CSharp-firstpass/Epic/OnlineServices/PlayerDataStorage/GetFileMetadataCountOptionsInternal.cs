using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006A8 RID: 1704
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFileMetadataCountOptionsInternal : IDisposable
	{
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x00089508 File Offset: 0x00087708
		// (set) Token: 0x06004158 RID: 16728 RVA: 0x0008952A File Offset: 0x0008772A
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

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x0008953C File Offset: 0x0008773C
		// (set) Token: 0x0600415A RID: 16730 RVA: 0x0008955E File Offset: 0x0008775E
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

		// Token: 0x0600415B RID: 16731 RVA: 0x0008956D File Offset: 0x0008776D
		public void Dispose()
		{
		}

		// Token: 0x04001909 RID: 6409
		private int m_ApiVersion;

		// Token: 0x0400190A RID: 6410
		private IntPtr m_LocalUserId;
	}
}

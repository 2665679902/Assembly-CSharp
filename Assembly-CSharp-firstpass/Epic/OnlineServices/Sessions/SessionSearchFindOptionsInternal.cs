using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000646 RID: 1606
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchFindOptionsInternal : IDisposable
	{
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06003EDA RID: 16090 RVA: 0x000868F8 File Offset: 0x00084AF8
		// (set) Token: 0x06003EDB RID: 16091 RVA: 0x0008691A File Offset: 0x00084B1A
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

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x0008692C File Offset: 0x00084B2C
		// (set) Token: 0x06003EDD RID: 16093 RVA: 0x0008694E File Offset: 0x00084B4E
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

		// Token: 0x06003EDE RID: 16094 RVA: 0x0008695D File Offset: 0x00084B5D
		public void Dispose()
		{
		}

		// Token: 0x040017D9 RID: 6105
		private int m_ApiVersion;

		// Token: 0x040017DA RID: 6106
		private IntPtr m_LocalUserId;
	}
}

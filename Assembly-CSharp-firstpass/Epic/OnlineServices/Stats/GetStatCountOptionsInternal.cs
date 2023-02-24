using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005A8 RID: 1448
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetStatCountOptionsInternal : IDisposable
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x0008353C File Offset: 0x0008173C
		// (set) Token: 0x06003B5E RID: 15198 RVA: 0x0008355E File Offset: 0x0008175E
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

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x00083570 File Offset: 0x00081770
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x00083592 File Offset: 0x00081792
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

		// Token: 0x06003B61 RID: 15201 RVA: 0x000835A1 File Offset: 0x000817A1
		public void Dispose()
		{
		}

		// Token: 0x040016A1 RID: 5793
		private int m_ApiVersion;

		// Token: 0x040016A2 RID: 5794
		private IntPtr m_TargetUserId;
	}
}

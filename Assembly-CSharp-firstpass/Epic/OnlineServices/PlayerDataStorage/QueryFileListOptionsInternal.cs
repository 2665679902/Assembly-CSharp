using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C2 RID: 1730
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListOptionsInternal : IDisposable
	{
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060041E7 RID: 16871 RVA: 0x00089C60 File Offset: 0x00087E60
		// (set) Token: 0x060041E8 RID: 16872 RVA: 0x00089C82 File Offset: 0x00087E82
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

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x00089C94 File Offset: 0x00087E94
		// (set) Token: 0x060041EA RID: 16874 RVA: 0x00089CB6 File Offset: 0x00087EB6
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

		// Token: 0x060041EB RID: 16875 RVA: 0x00089CC5 File Offset: 0x00087EC5
		public void Dispose()
		{
		}

		// Token: 0x04001926 RID: 6438
		private int m_ApiVersion;

		// Token: 0x04001927 RID: 6439
		private IntPtr m_LocalUserId;
	}
}

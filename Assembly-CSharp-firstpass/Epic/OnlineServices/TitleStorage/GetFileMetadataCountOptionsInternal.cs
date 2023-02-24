using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000585 RID: 1413
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFileMetadataCountOptionsInternal : IDisposable
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x00082860 File Offset: 0x00080A60
		// (set) Token: 0x06003A7F RID: 14975 RVA: 0x00082882 File Offset: 0x00080A82
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

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06003A80 RID: 14976 RVA: 0x00082894 File Offset: 0x00080A94
		// (set) Token: 0x06003A81 RID: 14977 RVA: 0x000828B6 File Offset: 0x00080AB6
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

		// Token: 0x06003A82 RID: 14978 RVA: 0x000828C5 File Offset: 0x00080AC5
		public void Dispose()
		{
		}

		// Token: 0x0400164E RID: 5710
		private int m_ApiVersion;

		// Token: 0x0400164F RID: 5711
		private IntPtr m_LocalUserId;
	}
}

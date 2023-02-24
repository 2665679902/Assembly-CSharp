using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000597 RID: 1431
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListOptionsInternal : IDisposable
	{
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x00082A88 File Offset: 0x00080C88
		// (set) Token: 0x06003AD3 RID: 15059 RVA: 0x00082AAA File Offset: 0x00080CAA
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

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x00082ABC File Offset: 0x00080CBC
		// (set) Token: 0x06003AD5 RID: 15061 RVA: 0x00082ADE File Offset: 0x00080CDE
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

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x00082AF0 File Offset: 0x00080CF0
		// (set) Token: 0x06003AD7 RID: 15063 RVA: 0x00082B18 File Offset: 0x00080D18
		public string[] ListOfTags
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_ListOfTags, out @default, this.m_ListOfTagsCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ListOfTags, value, out this.m_ListOfTagsCount);
			}
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x00082B2D File Offset: 0x00080D2D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ListOfTags);
		}

		// Token: 0x04001660 RID: 5728
		private int m_ApiVersion;

		// Token: 0x04001661 RID: 5729
		private IntPtr m_LocalUserId;

		// Token: 0x04001662 RID: 5730
		private IntPtr m_ListOfTags;

		// Token: 0x04001663 RID: 5731
		private uint m_ListOfTagsCount;
	}
}

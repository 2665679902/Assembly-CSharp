using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000579 RID: 1401
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataAtIndexOptionsInternal : IDisposable
	{
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x00082384 File Offset: 0x00080584
		// (set) Token: 0x06003A31 RID: 14897 RVA: 0x000823A6 File Offset: 0x000805A6
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

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06003A32 RID: 14898 RVA: 0x000823B8 File Offset: 0x000805B8
		// (set) Token: 0x06003A33 RID: 14899 RVA: 0x000823DA File Offset: 0x000805DA
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

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x000823EC File Offset: 0x000805EC
		// (set) Token: 0x06003A35 RID: 14901 RVA: 0x0008240E File Offset: 0x0008060E
		public uint Index
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_Index, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_Index, value);
			}
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x0008241D File Offset: 0x0008061D
		public void Dispose()
		{
		}

		// Token: 0x0400162B RID: 5675
		private int m_ApiVersion;

		// Token: 0x0400162C RID: 5676
		private IntPtr m_LocalUserId;

		// Token: 0x0400162D RID: 5677
		private uint m_Index;
	}
}

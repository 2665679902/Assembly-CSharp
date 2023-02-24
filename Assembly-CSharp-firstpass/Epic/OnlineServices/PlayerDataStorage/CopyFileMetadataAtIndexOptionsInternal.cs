using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000698 RID: 1688
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataAtIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x00088E2C File Offset: 0x0008702C
		// (set) Token: 0x060040EA RID: 16618 RVA: 0x00088E4E File Offset: 0x0008704E
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

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060040EB RID: 16619 RVA: 0x00088E60 File Offset: 0x00087060
		// (set) Token: 0x060040EC RID: 16620 RVA: 0x00088E82 File Offset: 0x00087082
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

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060040ED RID: 16621 RVA: 0x00088E94 File Offset: 0x00087094
		// (set) Token: 0x060040EE RID: 16622 RVA: 0x00088EB6 File Offset: 0x000870B6
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

		// Token: 0x060040EF RID: 16623 RVA: 0x00088EC5 File Offset: 0x000870C5
		public void Dispose()
		{
		}

		// Token: 0x040018D7 RID: 6359
		private int m_ApiVersion;

		// Token: 0x040018D8 RID: 6360
		private IntPtr m_LocalUserId;

		// Token: 0x040018D9 RID: 6361
		private uint m_Index;
	}
}

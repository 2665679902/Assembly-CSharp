using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D7 RID: 2263
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransferDeviceIdAccountOptionsInternal : IDisposable
	{
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004F5E RID: 20318 RVA: 0x00097530 File Offset: 0x00095730
		// (set) Token: 0x06004F5F RID: 20319 RVA: 0x00097552 File Offset: 0x00095752
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

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004F60 RID: 20320 RVA: 0x00097564 File Offset: 0x00095764
		// (set) Token: 0x06004F61 RID: 20321 RVA: 0x00097586 File Offset: 0x00095786
		public ProductUserId PrimaryLocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_PrimaryLocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_PrimaryLocalUserId, value);
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004F62 RID: 20322 RVA: 0x00097598 File Offset: 0x00095798
		// (set) Token: 0x06004F63 RID: 20323 RVA: 0x000975BA File Offset: 0x000957BA
		public ProductUserId LocalDeviceUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalDeviceUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalDeviceUserId, value);
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004F64 RID: 20324 RVA: 0x000975CC File Offset: 0x000957CC
		// (set) Token: 0x06004F65 RID: 20325 RVA: 0x000975EE File Offset: 0x000957EE
		public ProductUserId ProductUserIdToPreserve
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_ProductUserIdToPreserve, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductUserIdToPreserve, value);
			}
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x000975FD File Offset: 0x000957FD
		public void Dispose()
		{
		}

		// Token: 0x04001EC6 RID: 7878
		private int m_ApiVersion;

		// Token: 0x04001EC7 RID: 7879
		private IntPtr m_PrimaryLocalUserId;

		// Token: 0x04001EC8 RID: 7880
		private IntPtr m_LocalDeviceUserId;

		// Token: 0x04001EC9 RID: 7881
		private IntPtr m_ProductUserIdToPreserve;
	}
}

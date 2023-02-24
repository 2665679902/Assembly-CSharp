using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000599 RID: 1433
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileOptionsInternal : IDisposable
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x00082B68 File Offset: 0x00080D68
		// (set) Token: 0x06003AE0 RID: 15072 RVA: 0x00082B8A File Offset: 0x00080D8A
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

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x00082B9C File Offset: 0x00080D9C
		// (set) Token: 0x06003AE2 RID: 15074 RVA: 0x00082BBE File Offset: 0x00080DBE
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

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x00082BD0 File Offset: 0x00080DD0
		// (set) Token: 0x06003AE4 RID: 15076 RVA: 0x00082BF2 File Offset: 0x00080DF2
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Filename, value);
			}
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x00082C01 File Offset: 0x00080E01
		public void Dispose()
		{
		}

		// Token: 0x04001666 RID: 5734
		private int m_ApiVersion;

		// Token: 0x04001667 RID: 5735
		private IntPtr m_LocalUserId;

		// Token: 0x04001668 RID: 5736
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}

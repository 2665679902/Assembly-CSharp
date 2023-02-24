using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000892 RID: 2194
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserExternalAccountByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004DE7 RID: 19943 RVA: 0x00096368 File Offset: 0x00094568
		// (set) Token: 0x06004DE8 RID: 19944 RVA: 0x0009638A File Offset: 0x0009458A
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

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004DE9 RID: 19945 RVA: 0x0009639C File Offset: 0x0009459C
		// (set) Token: 0x06004DEA RID: 19946 RVA: 0x000963BE File Offset: 0x000945BE
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

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004DEB RID: 19947 RVA: 0x000963D0 File Offset: 0x000945D0
		// (set) Token: 0x06004DEC RID: 19948 RVA: 0x000963F2 File Offset: 0x000945F2
		public uint ExternalAccountInfoIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_ExternalAccountInfoIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_ExternalAccountInfoIndex, value);
			}
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x00096401 File Offset: 0x00094601
		public void Dispose()
		{
		}

		// Token: 0x04001E38 RID: 7736
		private int m_ApiVersion;

		// Token: 0x04001E39 RID: 7737
		private IntPtr m_TargetUserId;

		// Token: 0x04001E3A RID: 7738
		private uint m_ExternalAccountInfoIndex;
	}
}

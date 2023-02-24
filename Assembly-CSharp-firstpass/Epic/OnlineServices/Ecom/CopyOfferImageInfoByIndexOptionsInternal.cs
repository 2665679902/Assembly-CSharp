using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000841 RID: 2113
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferImageInfoByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06004B99 RID: 19353 RVA: 0x000938C4 File Offset: 0x00091AC4
		// (set) Token: 0x06004B9A RID: 19354 RVA: 0x000938E6 File Offset: 0x00091AE6
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

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06004B9B RID: 19355 RVA: 0x000938F8 File Offset: 0x00091AF8
		// (set) Token: 0x06004B9C RID: 19356 RVA: 0x0009391A File Offset: 0x00091B1A
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06004B9D RID: 19357 RVA: 0x0009392C File Offset: 0x00091B2C
		// (set) Token: 0x06004B9E RID: 19358 RVA: 0x0009394E File Offset: 0x00091B4E
		public string OfferId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_OfferId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_OfferId, value);
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06004B9F RID: 19359 RVA: 0x00093960 File Offset: 0x00091B60
		// (set) Token: 0x06004BA0 RID: 19360 RVA: 0x00093982 File Offset: 0x00091B82
		public uint ImageInfoIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_ImageInfoIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_ImageInfoIndex, value);
			}
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x00093991 File Offset: 0x00091B91
		public void Dispose()
		{
		}

		// Token: 0x04001D3B RID: 7483
		private int m_ApiVersion;

		// Token: 0x04001D3C RID: 7484
		private IntPtr m_LocalUserId;

		// Token: 0x04001D3D RID: 7485
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OfferId;

		// Token: 0x04001D3E RID: 7486
		private uint m_ImageInfoIndex;
	}
}

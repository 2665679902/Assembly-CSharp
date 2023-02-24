using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200083D RID: 2109
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferByIdOptionsInternal : IDisposable
	{
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06004B7D RID: 19325 RVA: 0x00093720 File Offset: 0x00091920
		// (set) Token: 0x06004B7E RID: 19326 RVA: 0x00093742 File Offset: 0x00091942
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

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06004B7F RID: 19327 RVA: 0x00093754 File Offset: 0x00091954
		// (set) Token: 0x06004B80 RID: 19328 RVA: 0x00093776 File Offset: 0x00091976
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

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06004B81 RID: 19329 RVA: 0x00093788 File Offset: 0x00091988
		// (set) Token: 0x06004B82 RID: 19330 RVA: 0x000937AA File Offset: 0x000919AA
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

		// Token: 0x06004B83 RID: 19331 RVA: 0x000937B9 File Offset: 0x000919B9
		public void Dispose()
		{
		}

		// Token: 0x04001D30 RID: 7472
		private int m_ApiVersion;

		// Token: 0x04001D31 RID: 7473
		private IntPtr m_LocalUserId;

		// Token: 0x04001D32 RID: 7474
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OfferId;
	}
}

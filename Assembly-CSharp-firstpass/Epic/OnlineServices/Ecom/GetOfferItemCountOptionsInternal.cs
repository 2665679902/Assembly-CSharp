using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000859 RID: 2137
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOfferItemCountOptionsInternal : IDisposable
	{
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x00094AF8 File Offset: 0x00092CF8
		// (set) Token: 0x06004C77 RID: 19575 RVA: 0x00094B1A File Offset: 0x00092D1A
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

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x00094B2C File Offset: 0x00092D2C
		// (set) Token: 0x06004C79 RID: 19577 RVA: 0x00094B4E File Offset: 0x00092D4E
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

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x00094B60 File Offset: 0x00092D60
		// (set) Token: 0x06004C7B RID: 19579 RVA: 0x00094B82 File Offset: 0x00092D82
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

		// Token: 0x06004C7C RID: 19580 RVA: 0x00094B91 File Offset: 0x00092D91
		public void Dispose()
		{
		}

		// Token: 0x04001DA9 RID: 7593
		private int m_ApiVersion;

		// Token: 0x04001DAA RID: 7594
		private IntPtr m_LocalUserId;

		// Token: 0x04001DAB RID: 7595
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OfferId;
	}
}

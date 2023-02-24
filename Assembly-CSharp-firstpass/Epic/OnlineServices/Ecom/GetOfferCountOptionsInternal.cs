using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000855 RID: 2133
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOfferCountOptionsInternal : IDisposable
	{
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06004C5E RID: 19550 RVA: 0x0009499C File Offset: 0x00092B9C
		// (set) Token: 0x06004C5F RID: 19551 RVA: 0x000949BE File Offset: 0x00092BBE
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

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06004C60 RID: 19552 RVA: 0x000949D0 File Offset: 0x00092BD0
		// (set) Token: 0x06004C61 RID: 19553 RVA: 0x000949F2 File Offset: 0x00092BF2
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

		// Token: 0x06004C62 RID: 19554 RVA: 0x00094A01 File Offset: 0x00092C01
		public void Dispose()
		{
		}

		// Token: 0x04001DA0 RID: 7584
		private int m_ApiVersion;

		// Token: 0x04001DA1 RID: 7585
		private IntPtr m_LocalUserId;
	}
}

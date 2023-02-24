using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200085B RID: 2139
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetTransactionCountOptionsInternal : IDisposable
	{
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x00094BB0 File Offset: 0x00092DB0
		// (set) Token: 0x06004C82 RID: 19586 RVA: 0x00094BD2 File Offset: 0x00092DD2
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

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004C83 RID: 19587 RVA: 0x00094BE4 File Offset: 0x00092DE4
		// (set) Token: 0x06004C84 RID: 19588 RVA: 0x00094C06 File Offset: 0x00092E06
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

		// Token: 0x06004C85 RID: 19589 RVA: 0x00094C15 File Offset: 0x00092E15
		public void Dispose()
		{
		}

		// Token: 0x04001DAD RID: 7597
		private int m_ApiVersion;

		// Token: 0x04001DAE RID: 7598
		private IntPtr m_LocalUserId;
	}
}

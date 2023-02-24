using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000844 RID: 2116
	public class CopyTransactionByIdOptions
	{
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x00093AA3 File Offset: 0x00091CA3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x00093AA6 File Offset: 0x00091CA6
		// (set) Token: 0x06004BB5 RID: 19381 RVA: 0x00093AAE File Offset: 0x00091CAE
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06004BB6 RID: 19382 RVA: 0x00093AB7 File Offset: 0x00091CB7
		// (set) Token: 0x06004BB7 RID: 19383 RVA: 0x00093ABF File Offset: 0x00091CBF
		public string TransactionId { get; set; }
	}
}

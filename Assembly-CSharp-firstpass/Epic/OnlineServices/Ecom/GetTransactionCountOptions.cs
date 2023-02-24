using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200085A RID: 2138
	public class GetTransactionCountOptions
	{
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06004C7D RID: 19581 RVA: 0x00094B93 File Offset: 0x00092D93
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x00094B96 File Offset: 0x00092D96
		// (set) Token: 0x06004C7F RID: 19583 RVA: 0x00094B9E File Offset: 0x00092D9E
		public EpicAccountId LocalUserId { get; set; }
	}
}

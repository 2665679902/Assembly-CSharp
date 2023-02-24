using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000858 RID: 2136
	public class GetOfferItemCountOptions
	{
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06004C70 RID: 19568 RVA: 0x00094ACB File Offset: 0x00092CCB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06004C71 RID: 19569 RVA: 0x00094ACE File Offset: 0x00092CCE
		// (set) Token: 0x06004C72 RID: 19570 RVA: 0x00094AD6 File Offset: 0x00092CD6
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06004C73 RID: 19571 RVA: 0x00094ADF File Offset: 0x00092CDF
		// (set) Token: 0x06004C74 RID: 19572 RVA: 0x00094AE7 File Offset: 0x00092CE7
		public string OfferId { get; set; }
	}
}

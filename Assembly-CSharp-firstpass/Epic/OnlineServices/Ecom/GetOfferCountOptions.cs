using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000854 RID: 2132
	public class GetOfferCountOptions
	{
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06004C5A RID: 19546 RVA: 0x0009497F File Offset: 0x00092B7F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004C5B RID: 19547 RVA: 0x00094982 File Offset: 0x00092B82
		// (set) Token: 0x06004C5C RID: 19548 RVA: 0x0009498A File Offset: 0x00092B8A
		public EpicAccountId LocalUserId { get; set; }
	}
}

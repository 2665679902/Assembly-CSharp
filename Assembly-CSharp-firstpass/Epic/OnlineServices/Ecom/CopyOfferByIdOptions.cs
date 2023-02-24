using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200083C RID: 2108
	public class CopyOfferByIdOptions
	{
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06004B77 RID: 19319 RVA: 0x000936F3 File Offset: 0x000918F3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06004B78 RID: 19320 RVA: 0x000936F6 File Offset: 0x000918F6
		// (set) Token: 0x06004B79 RID: 19321 RVA: 0x000936FE File Offset: 0x000918FE
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06004B7A RID: 19322 RVA: 0x00093707 File Offset: 0x00091907
		// (set) Token: 0x06004B7B RID: 19323 RVA: 0x0009370F File Offset: 0x0009190F
		public string OfferId { get; set; }
	}
}

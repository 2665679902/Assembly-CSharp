using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000852 RID: 2130
	public class GetItemReleaseCountOptions
	{
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06004C4D RID: 19533 RVA: 0x000948B7 File Offset: 0x00092AB7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06004C4E RID: 19534 RVA: 0x000948BA File Offset: 0x00092ABA
		// (set) Token: 0x06004C4F RID: 19535 RVA: 0x000948C2 File Offset: 0x00092AC2
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06004C50 RID: 19536 RVA: 0x000948CB File Offset: 0x00092ACB
		// (set) Token: 0x06004C51 RID: 19537 RVA: 0x000948D3 File Offset: 0x00092AD3
		public string ItemId { get; set; }
	}
}

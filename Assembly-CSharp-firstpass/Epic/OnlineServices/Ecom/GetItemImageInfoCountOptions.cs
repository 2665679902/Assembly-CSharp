using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000850 RID: 2128
	public class GetItemImageInfoCountOptions
	{
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x000947EF File Offset: 0x000929EF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06004C41 RID: 19521 RVA: 0x000947F2 File Offset: 0x000929F2
		// (set) Token: 0x06004C42 RID: 19522 RVA: 0x000947FA File Offset: 0x000929FA
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06004C43 RID: 19523 RVA: 0x00094803 File Offset: 0x00092A03
		// (set) Token: 0x06004C44 RID: 19524 RVA: 0x0009480B File Offset: 0x00092A0B
		public string ItemId { get; set; }
	}
}

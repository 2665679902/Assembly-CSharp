using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200082E RID: 2094
	public class CheckoutOptions
	{
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004B0C RID: 19212 RVA: 0x00093043 File Offset: 0x00091243
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004B0D RID: 19213 RVA: 0x00093046 File Offset: 0x00091246
		// (set) Token: 0x06004B0E RID: 19214 RVA: 0x0009304E File Offset: 0x0009124E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06004B0F RID: 19215 RVA: 0x00093057 File Offset: 0x00091257
		// (set) Token: 0x06004B10 RID: 19216 RVA: 0x0009305F File Offset: 0x0009125F
		public string OverrideCatalogNamespace { get; set; }

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06004B11 RID: 19217 RVA: 0x00093068 File Offset: 0x00091268
		// (set) Token: 0x06004B12 RID: 19218 RVA: 0x00093070 File Offset: 0x00091270
		public CheckoutEntry[] Entries { get; set; }
	}
}

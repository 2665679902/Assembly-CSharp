using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000826 RID: 2086
	public class CatalogOffer
	{
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06004A9F RID: 19103 RVA: 0x0009290B File Offset: 0x00090B0B
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06004AA0 RID: 19104 RVA: 0x0009290E File Offset: 0x00090B0E
		// (set) Token: 0x06004AA1 RID: 19105 RVA: 0x00092916 File Offset: 0x00090B16
		public int ServerIndex { get; set; }

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06004AA2 RID: 19106 RVA: 0x0009291F File Offset: 0x00090B1F
		// (set) Token: 0x06004AA3 RID: 19107 RVA: 0x00092927 File Offset: 0x00090B27
		public string CatalogNamespace { get; set; }

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x00092930 File Offset: 0x00090B30
		// (set) Token: 0x06004AA5 RID: 19109 RVA: 0x00092938 File Offset: 0x00090B38
		public string Id { get; set; }

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x00092941 File Offset: 0x00090B41
		// (set) Token: 0x06004AA7 RID: 19111 RVA: 0x00092949 File Offset: 0x00090B49
		public string TitleText { get; set; }

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06004AA8 RID: 19112 RVA: 0x00092952 File Offset: 0x00090B52
		// (set) Token: 0x06004AA9 RID: 19113 RVA: 0x0009295A File Offset: 0x00090B5A
		public string DescriptionText { get; set; }

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06004AAA RID: 19114 RVA: 0x00092963 File Offset: 0x00090B63
		// (set) Token: 0x06004AAB RID: 19115 RVA: 0x0009296B File Offset: 0x00090B6B
		public string LongDescriptionText { get; set; }

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06004AAC RID: 19116 RVA: 0x00092974 File Offset: 0x00090B74
		// (set) Token: 0x06004AAD RID: 19117 RVA: 0x0009297C File Offset: 0x00090B7C
		public string TechnicalDetailsText_DEPRECATED { get; set; }

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06004AAE RID: 19118 RVA: 0x00092985 File Offset: 0x00090B85
		// (set) Token: 0x06004AAF RID: 19119 RVA: 0x0009298D File Offset: 0x00090B8D
		public string CurrencyCode { get; set; }

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x00092996 File Offset: 0x00090B96
		// (set) Token: 0x06004AB1 RID: 19121 RVA: 0x0009299E File Offset: 0x00090B9E
		public Result PriceResult { get; set; }

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06004AB2 RID: 19122 RVA: 0x000929A7 File Offset: 0x00090BA7
		// (set) Token: 0x06004AB3 RID: 19123 RVA: 0x000929AF File Offset: 0x00090BAF
		public uint OriginalPrice { get; set; }

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06004AB4 RID: 19124 RVA: 0x000929B8 File Offset: 0x00090BB8
		// (set) Token: 0x06004AB5 RID: 19125 RVA: 0x000929C0 File Offset: 0x00090BC0
		public uint CurrentPrice { get; set; }

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06004AB6 RID: 19126 RVA: 0x000929C9 File Offset: 0x00090BC9
		// (set) Token: 0x06004AB7 RID: 19127 RVA: 0x000929D1 File Offset: 0x00090BD1
		public byte DiscountPercentage { get; set; }

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x000929DA File Offset: 0x00090BDA
		// (set) Token: 0x06004AB9 RID: 19129 RVA: 0x000929E2 File Offset: 0x00090BE2
		public long ExpirationTimestamp { get; set; }

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x000929EB File Offset: 0x00090BEB
		// (set) Token: 0x06004ABB RID: 19131 RVA: 0x000929F3 File Offset: 0x00090BF3
		public uint PurchasedCount { get; set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06004ABC RID: 19132 RVA: 0x000929FC File Offset: 0x00090BFC
		// (set) Token: 0x06004ABD RID: 19133 RVA: 0x00092A04 File Offset: 0x00090C04
		public int PurchaseLimit { get; set; }

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06004ABE RID: 19134 RVA: 0x00092A0D File Offset: 0x00090C0D
		// (set) Token: 0x06004ABF RID: 19135 RVA: 0x00092A15 File Offset: 0x00090C15
		public bool AvailableForPurchase { get; set; }
	}
}

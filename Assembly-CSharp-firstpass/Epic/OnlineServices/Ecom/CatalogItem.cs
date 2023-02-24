using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000824 RID: 2084
	public class CatalogItem
	{
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06004A72 RID: 19058 RVA: 0x0009261B File Offset: 0x0009081B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x0009261E File Offset: 0x0009081E
		// (set) Token: 0x06004A74 RID: 19060 RVA: 0x00092626 File Offset: 0x00090826
		public string CatalogNamespace { get; set; }

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06004A75 RID: 19061 RVA: 0x0009262F File Offset: 0x0009082F
		// (set) Token: 0x06004A76 RID: 19062 RVA: 0x00092637 File Offset: 0x00090837
		public string Id { get; set; }

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06004A77 RID: 19063 RVA: 0x00092640 File Offset: 0x00090840
		// (set) Token: 0x06004A78 RID: 19064 RVA: 0x00092648 File Offset: 0x00090848
		public string EntitlementName { get; set; }

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x00092651 File Offset: 0x00090851
		// (set) Token: 0x06004A7A RID: 19066 RVA: 0x00092659 File Offset: 0x00090859
		public string TitleText { get; set; }

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004A7B RID: 19067 RVA: 0x00092662 File Offset: 0x00090862
		// (set) Token: 0x06004A7C RID: 19068 RVA: 0x0009266A File Offset: 0x0009086A
		public string DescriptionText { get; set; }

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x00092673 File Offset: 0x00090873
		// (set) Token: 0x06004A7E RID: 19070 RVA: 0x0009267B File Offset: 0x0009087B
		public string LongDescriptionText { get; set; }

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06004A7F RID: 19071 RVA: 0x00092684 File Offset: 0x00090884
		// (set) Token: 0x06004A80 RID: 19072 RVA: 0x0009268C File Offset: 0x0009088C
		public string TechnicalDetailsText { get; set; }

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06004A81 RID: 19073 RVA: 0x00092695 File Offset: 0x00090895
		// (set) Token: 0x06004A82 RID: 19074 RVA: 0x0009269D File Offset: 0x0009089D
		public string DeveloperText { get; set; }

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x000926A6 File Offset: 0x000908A6
		// (set) Token: 0x06004A84 RID: 19076 RVA: 0x000926AE File Offset: 0x000908AE
		public EcomItemType ItemType { get; set; }

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x000926B7 File Offset: 0x000908B7
		// (set) Token: 0x06004A86 RID: 19078 RVA: 0x000926BF File Offset: 0x000908BF
		public long EntitlementEndTimestamp { get; set; }
	}
}

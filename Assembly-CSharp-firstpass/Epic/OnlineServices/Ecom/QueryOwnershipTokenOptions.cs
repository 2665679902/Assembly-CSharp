using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200087B RID: 2171
	public class QueryOwnershipTokenOptions
	{
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06004D39 RID: 19769 RVA: 0x00095472 File Offset: 0x00093672
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004D3A RID: 19770 RVA: 0x00095475 File Offset: 0x00093675
		// (set) Token: 0x06004D3B RID: 19771 RVA: 0x0009547D File Offset: 0x0009367D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06004D3C RID: 19772 RVA: 0x00095486 File Offset: 0x00093686
		// (set) Token: 0x06004D3D RID: 19773 RVA: 0x0009548E File Offset: 0x0009368E
		public string[] CatalogItemIds { get; set; }

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06004D3E RID: 19774 RVA: 0x00095497 File Offset: 0x00093697
		// (set) Token: 0x06004D3F RID: 19775 RVA: 0x0009549F File Offset: 0x0009369F
		public string CatalogNamespace { get; set; }
	}
}

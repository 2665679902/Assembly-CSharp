using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005C9 RID: 1481
	public class AttributeData
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x00084237 File Offset: 0x00082437
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x0008423A File Offset: 0x0008243A
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x00084242 File Offset: 0x00082442
		public string Key { get; set; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x0008424B File Offset: 0x0008244B
		// (set) Token: 0x06003C2D RID: 15405 RVA: 0x00084253 File Offset: 0x00082453
		public AttributeDataValue Value { get; set; }
	}
}

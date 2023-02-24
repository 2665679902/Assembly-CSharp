using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005AD RID: 1453
	public class IngestStatOptions
	{
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x0008374E File Offset: 0x0008194E
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06003B7E RID: 15230 RVA: 0x00083751 File Offset: 0x00081951
		// (set) Token: 0x06003B7F RID: 15231 RVA: 0x00083759 File Offset: 0x00081959
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06003B80 RID: 15232 RVA: 0x00083762 File Offset: 0x00081962
		// (set) Token: 0x06003B81 RID: 15233 RVA: 0x0008376A File Offset: 0x0008196A
		public IngestData[] Stats { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x00083773 File Offset: 0x00081973
		// (set) Token: 0x06003B83 RID: 15235 RVA: 0x0008377B File Offset: 0x0008197B
		public ProductUserId TargetUserId { get; set; }
	}
}

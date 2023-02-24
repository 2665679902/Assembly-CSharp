using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005A5 RID: 1445
	public class CopyStatByNameOptions
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06003B4C RID: 15180 RVA: 0x00083457 File Offset: 0x00081657
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x0008345A File Offset: 0x0008165A
		// (set) Token: 0x06003B4E RID: 15182 RVA: 0x00083462 File Offset: 0x00081662
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x0008346B File Offset: 0x0008166B
		// (set) Token: 0x06003B50 RID: 15184 RVA: 0x00083473 File Offset: 0x00081673
		public string Name { get; set; }
	}
}

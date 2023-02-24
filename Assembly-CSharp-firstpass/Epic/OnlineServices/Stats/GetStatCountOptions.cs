using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005A7 RID: 1447
	public class GetStatCountOptions
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x0008351F File Offset: 0x0008171F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x00083522 File Offset: 0x00081722
		// (set) Token: 0x06003B5B RID: 15195 RVA: 0x0008352A File Offset: 0x0008172A
		public ProductUserId TargetUserId { get; set; }
	}
}

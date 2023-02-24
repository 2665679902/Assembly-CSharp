using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000711 RID: 1809
	public class EndPlayerSessionOptions
	{
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x0008C557 File Offset: 0x0008A757
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x0008C55A File Offset: 0x0008A75A
		// (set) Token: 0x06004456 RID: 17494 RVA: 0x0008C562 File Offset: 0x0008A762
		public EndPlayerSessionOptionsAccountId AccountId { get; set; }
	}
}

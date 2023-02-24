using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000948 RID: 2376
	public class QueryDefinitionsOptions
	{
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x0600525F RID: 21087 RVA: 0x0009A51B File Offset: 0x0009871B
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06005260 RID: 21088 RVA: 0x0009A51E File Offset: 0x0009871E
		// (set) Token: 0x06005261 RID: 21089 RVA: 0x0009A526 File Offset: 0x00098726
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06005262 RID: 21090 RVA: 0x0009A52F File Offset: 0x0009872F
		// (set) Token: 0x06005263 RID: 21091 RVA: 0x0009A537 File Offset: 0x00098737
		public EpicAccountId EpicUserId_DEPRECATED { get; set; }

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06005264 RID: 21092 RVA: 0x0009A540 File Offset: 0x00098740
		// (set) Token: 0x06005265 RID: 21093 RVA: 0x0009A548 File Offset: 0x00098748
		public string[] HiddenAchievementIds_DEPRECATED { get; set; }
	}
}

using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000534 RID: 1332
	public class PageResult
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x000808C7 File Offset: 0x0007EAC7
		// (set) Token: 0x0600387C RID: 14460 RVA: 0x000808CF File Offset: 0x0007EACF
		public int StartIndex { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x000808D8 File Offset: 0x0007EAD8
		// (set) Token: 0x0600387E RID: 14462 RVA: 0x000808E0 File Offset: 0x0007EAE0
		public int Count { get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x000808E9 File Offset: 0x0007EAE9
		// (set) Token: 0x06003880 RID: 14464 RVA: 0x000808F1 File Offset: 0x0007EAF1
		public int TotalCount { get; set; }
	}
}

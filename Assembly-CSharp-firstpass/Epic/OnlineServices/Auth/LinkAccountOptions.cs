using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008F0 RID: 2288
	public class LinkAccountOptions
	{
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06004FED RID: 20461 RVA: 0x00098002 File Offset: 0x00096202
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x00098005 File Offset: 0x00096205
		// (set) Token: 0x06004FEF RID: 20463 RVA: 0x0009800D File Offset: 0x0009620D
		public LinkAccountFlags LinkAccountFlags { get; set; }

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06004FF0 RID: 20464 RVA: 0x00098016 File Offset: 0x00096216
		// (set) Token: 0x06004FF1 RID: 20465 RVA: 0x0009801E File Offset: 0x0009621E
		public ContinuanceToken ContinuanceToken { get; set; }

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004FF2 RID: 20466 RVA: 0x00098027 File Offset: 0x00096227
		// (set) Token: 0x06004FF3 RID: 20467 RVA: 0x0009802F File Offset: 0x0009622F
		public EpicAccountId LocalUserId { get; set; }
	}
}

using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008AE RID: 2222
	public class LinkAccountOptions
	{
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x00096D2A File Offset: 0x00094F2A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004E86 RID: 20102 RVA: 0x00096D2D File Offset: 0x00094F2D
		// (set) Token: 0x06004E87 RID: 20103 RVA: 0x00096D35 File Offset: 0x00094F35
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x00096D3E File Offset: 0x00094F3E
		// (set) Token: 0x06004E89 RID: 20105 RVA: 0x00096D46 File Offset: 0x00094F46
		public ContinuanceToken ContinuanceToken { get; set; }
	}
}

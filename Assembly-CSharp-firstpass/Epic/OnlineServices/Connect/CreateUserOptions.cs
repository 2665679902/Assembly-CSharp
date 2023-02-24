using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200089B RID: 2203
	public class CreateUserOptions
	{
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004E13 RID: 19987 RVA: 0x00096636 File Offset: 0x00094836
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004E14 RID: 19988 RVA: 0x00096639 File Offset: 0x00094839
		// (set) Token: 0x06004E15 RID: 19989 RVA: 0x00096641 File Offset: 0x00094841
		public ContinuanceToken ContinuanceToken { get; set; }
	}
}

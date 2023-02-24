using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008DA RID: 2266
	public class UnlinkAccountOptions
	{
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x000976AE File Offset: 0x000958AE
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06004F73 RID: 20339 RVA: 0x000976B1 File Offset: 0x000958B1
		// (set) Token: 0x06004F74 RID: 20340 RVA: 0x000976B9 File Offset: 0x000958B9
		public ProductUserId LocalUserId { get; set; }
	}
}

using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008F5 RID: 2293
	public class LoginOptions
	{
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06005012 RID: 20498 RVA: 0x0009825E File Offset: 0x0009645E
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06005013 RID: 20499 RVA: 0x00098261 File Offset: 0x00096461
		// (set) Token: 0x06005014 RID: 20500 RVA: 0x00098269 File Offset: 0x00096469
		public Credentials Credentials { get; set; }

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06005015 RID: 20501 RVA: 0x00098272 File Offset: 0x00096472
		// (set) Token: 0x06005016 RID: 20502 RVA: 0x0009827A File Offset: 0x0009647A
		public AuthScopeFlags ScopeFlags { get; set; }
	}
}

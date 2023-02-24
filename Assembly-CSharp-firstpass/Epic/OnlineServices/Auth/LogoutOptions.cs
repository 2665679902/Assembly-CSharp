using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008FB RID: 2299
	public class LogoutOptions
	{
		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06005038 RID: 20536 RVA: 0x000984C6 File Offset: 0x000966C6
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06005039 RID: 20537 RVA: 0x000984C9 File Offset: 0x000966C9
		// (set) Token: 0x0600503A RID: 20538 RVA: 0x000984D1 File Offset: 0x000966D1
		public EpicAccountId LocalUserId { get; set; }
	}
}

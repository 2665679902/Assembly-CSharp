using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008EB RID: 2283
	public class DeletePersistentAuthOptions
	{
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004FD6 RID: 20438 RVA: 0x00097E9A File Offset: 0x0009609A
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004FD7 RID: 20439 RVA: 0x00097E9D File Offset: 0x0009609D
		// (set) Token: 0x06004FD8 RID: 20440 RVA: 0x00097EA5 File Offset: 0x000960A5
		public string RefreshToken { get; set; }
	}
}

using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200090F RID: 2319
	public class VerifyUserAuthOptions
	{
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x060050BB RID: 20667 RVA: 0x00098A0A File Offset: 0x00096C0A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x00098A0D File Offset: 0x00096C0D
		// (set) Token: 0x060050BD RID: 20669 RVA: 0x00098A15 File Offset: 0x00096C15
		public Token AuthToken { get; set; }
	}
}

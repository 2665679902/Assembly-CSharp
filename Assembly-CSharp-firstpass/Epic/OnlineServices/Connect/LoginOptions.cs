using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008B2 RID: 2226
	public class LoginOptions
	{
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004EA0 RID: 20128 RVA: 0x00096ED6 File Offset: 0x000950D6
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004EA1 RID: 20129 RVA: 0x00096ED9 File Offset: 0x000950D9
		// (set) Token: 0x06004EA2 RID: 20130 RVA: 0x00096EE1 File Offset: 0x000950E1
		public Credentials Credentials { get; set; }

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004EA3 RID: 20131 RVA: 0x00096EEA File Offset: 0x000950EA
		// (set) Token: 0x06004EA4 RID: 20132 RVA: 0x00096EF2 File Offset: 0x000950F2
		public UserLoginInfo UserLoginInfo { get; set; }
	}
}

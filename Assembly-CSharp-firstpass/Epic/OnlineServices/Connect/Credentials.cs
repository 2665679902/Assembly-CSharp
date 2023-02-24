using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200089D RID: 2205
	public class Credentials
	{
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004E1C RID: 19996 RVA: 0x000966BB File Offset: 0x000948BB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06004E1D RID: 19997 RVA: 0x000966BE File Offset: 0x000948BE
		// (set) Token: 0x06004E1E RID: 19998 RVA: 0x000966C6 File Offset: 0x000948C6
		public string Token { get; set; }

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06004E1F RID: 19999 RVA: 0x000966CF File Offset: 0x000948CF
		// (set) Token: 0x06004E20 RID: 20000 RVA: 0x000966D7 File Offset: 0x000948D7
		public ExternalCredentialType Type { get; set; }
	}
}

using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000638 RID: 1592
	public class SessionModificationSetInvitesAllowedOptions
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000863A3 File Offset: 0x000845A3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06003E8E RID: 16014 RVA: 0x000863A6 File Offset: 0x000845A6
		// (set) Token: 0x06003E8F RID: 16015 RVA: 0x000863AE File Offset: 0x000845AE
		public bool InvitesAllowed { get; set; }
	}
}

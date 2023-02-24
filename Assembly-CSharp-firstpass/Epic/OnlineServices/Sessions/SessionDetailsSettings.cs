using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000629 RID: 1577
	public class SessionDetailsSettings
	{
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06003E1A RID: 15898 RVA: 0x00085BD7 File Offset: 0x00083DD7
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06003E1B RID: 15899 RVA: 0x00085BDA File Offset: 0x00083DDA
		// (set) Token: 0x06003E1C RID: 15900 RVA: 0x00085BE2 File Offset: 0x00083DE2
		public string BucketId { get; set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x00085BEB File Offset: 0x00083DEB
		// (set) Token: 0x06003E1E RID: 15902 RVA: 0x00085BF3 File Offset: 0x00083DF3
		public uint NumPublicConnections { get; set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06003E1F RID: 15903 RVA: 0x00085BFC File Offset: 0x00083DFC
		// (set) Token: 0x06003E20 RID: 15904 RVA: 0x00085C04 File Offset: 0x00083E04
		public bool AllowJoinInProgress { get; set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06003E21 RID: 15905 RVA: 0x00085C0D File Offset: 0x00083E0D
		// (set) Token: 0x06003E22 RID: 15906 RVA: 0x00085C15 File Offset: 0x00083E15
		public OnlineSessionPermissionLevel PermissionLevel { get; set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06003E23 RID: 15907 RVA: 0x00085C1E File Offset: 0x00083E1E
		// (set) Token: 0x06003E24 RID: 15908 RVA: 0x00085C26 File Offset: 0x00083E26
		public bool InvitesAllowed { get; set; }
	}
}

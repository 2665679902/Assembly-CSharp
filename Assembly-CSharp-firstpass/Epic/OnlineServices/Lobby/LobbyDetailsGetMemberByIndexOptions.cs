using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000765 RID: 1893
	public class LobbyDetailsGetMemberByIndexOptions
	{
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600460A RID: 17930 RVA: 0x0008E29F File Offset: 0x0008C49F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x0008E2A2 File Offset: 0x0008C4A2
		// (set) Token: 0x0600460C RID: 17932 RVA: 0x0008E2AA File Offset: 0x0008C4AA
		public uint MemberIndex { get; set; }
	}
}

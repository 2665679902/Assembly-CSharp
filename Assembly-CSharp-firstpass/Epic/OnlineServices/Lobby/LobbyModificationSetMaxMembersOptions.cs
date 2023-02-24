using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200077E RID: 1918
	public class LobbyModificationSetMaxMembersOptions
	{
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060046F1 RID: 18161 RVA: 0x0008F71F File Offset: 0x0008D91F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060046F2 RID: 18162 RVA: 0x0008F722 File Offset: 0x0008D922
		// (set) Token: 0x060046F3 RID: 18163 RVA: 0x0008F72A File Offset: 0x0008D92A
		public uint MaxMembers { get; set; }
	}
}

using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200074D RID: 1869
	public class KickMemberOptions
	{
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06004584 RID: 17796 RVA: 0x0008D95A File Offset: 0x0008BB5A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x0008D95D File Offset: 0x0008BB5D
		// (set) Token: 0x06004586 RID: 17798 RVA: 0x0008D965 File Offset: 0x0008BB65
		public string LobbyId { get; set; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x0008D96E File Offset: 0x0008BB6E
		// (set) Token: 0x06004588 RID: 17800 RVA: 0x0008D976 File Offset: 0x0008BB76
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x0008D97F File Offset: 0x0008BB7F
		// (set) Token: 0x0600458A RID: 17802 RVA: 0x0008D987 File Offset: 0x0008BB87
		public ProductUserId TargetUserId { get; set; }
	}
}

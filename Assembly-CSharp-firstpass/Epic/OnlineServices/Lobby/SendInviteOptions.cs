using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C8 RID: 1992
	public class SendInviteOptions
	{
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x0600484E RID: 18510 RVA: 0x000904CE File Offset: 0x0008E6CE
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600484F RID: 18511 RVA: 0x000904D1 File Offset: 0x0008E6D1
		// (set) Token: 0x06004850 RID: 18512 RVA: 0x000904D9 File Offset: 0x0008E6D9
		public string LobbyId { get; set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06004851 RID: 18513 RVA: 0x000904E2 File Offset: 0x0008E6E2
		// (set) Token: 0x06004852 RID: 18514 RVA: 0x000904EA File Offset: 0x0008E6EA
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x000904F3 File Offset: 0x0008E6F3
		// (set) Token: 0x06004854 RID: 18516 RVA: 0x000904FB File Offset: 0x0008E6FB
		public ProductUserId TargetUserId { get; set; }
	}
}

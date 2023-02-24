using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000731 RID: 1841
	public class CopyLobbyDetailsHandleByInviteIdOptions
	{
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x0008CF22 File Offset: 0x0008B122
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x0008CF25 File Offset: 0x0008B125
		// (set) Token: 0x060044DE RID: 17630 RVA: 0x0008CF2D File Offset: 0x0008B12D
		public string InviteId { get; set; }
	}
}

using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000611 RID: 1553
	public class RegisterPlayersOptions
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x0008531A File Offset: 0x0008351A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06003D95 RID: 15765 RVA: 0x0008531D File Offset: 0x0008351D
		// (set) Token: 0x06003D96 RID: 15766 RVA: 0x00085325 File Offset: 0x00083525
		public string SessionName { get; set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06003D97 RID: 15767 RVA: 0x0008532E File Offset: 0x0008352E
		// (set) Token: 0x06003D98 RID: 15768 RVA: 0x00085336 File Offset: 0x00083536
		public ProductUserId[] PlayersToRegister { get; set; }
	}
}

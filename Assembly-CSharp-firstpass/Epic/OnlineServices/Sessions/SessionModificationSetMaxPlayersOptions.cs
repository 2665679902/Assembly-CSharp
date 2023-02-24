using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200063C RID: 1596
	public class SessionModificationSetMaxPlayersOptions
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x000864AB File Offset: 0x000846AB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x000864AE File Offset: 0x000846AE
		// (set) Token: 0x06003EA1 RID: 16033 RVA: 0x000864B6 File Offset: 0x000846B6
		public uint MaxPlayers { get; set; }
	}
}

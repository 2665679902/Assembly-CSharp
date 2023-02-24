using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200065C RID: 1628
	public class UnregisterPlayersOptions
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x000877D2 File Offset: 0x000859D2
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x000877D5 File Offset: 0x000859D5
		// (set) Token: 0x06003F80 RID: 16256 RVA: 0x000877DD File Offset: 0x000859DD
		public string SessionName { get; set; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06003F81 RID: 16257 RVA: 0x000877E6 File Offset: 0x000859E6
		// (set) Token: 0x06003F82 RID: 16258 RVA: 0x000877EE File Offset: 0x000859EE
		public ProductUserId[] PlayersToUnregister { get; set; }
	}
}

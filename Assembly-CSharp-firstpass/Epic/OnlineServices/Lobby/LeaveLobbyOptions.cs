using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000751 RID: 1873
	public class LeaveLobbyOptions
	{
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060045A0 RID: 17824 RVA: 0x0008DB16 File Offset: 0x0008BD16
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060045A1 RID: 17825 RVA: 0x0008DB19 File Offset: 0x0008BD19
		// (set) Token: 0x060045A2 RID: 17826 RVA: 0x0008DB21 File Offset: 0x0008BD21
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060045A3 RID: 17827 RVA: 0x0008DB2A File Offset: 0x0008BD2A
		// (set) Token: 0x060045A4 RID: 17828 RVA: 0x0008DB32 File Offset: 0x0008BD32
		public string LobbyId { get; set; }
	}
}

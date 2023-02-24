using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000780 RID: 1920
	public class LobbyModificationSetPermissionLevelOptions
	{
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x0008F7A3 File Offset: 0x0008D9A3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x0008F7A6 File Offset: 0x0008D9A6
		// (set) Token: 0x060046FC RID: 18172 RVA: 0x0008F7AE File Offset: 0x0008D9AE
		public LobbyPermissionLevel PermissionLevel { get; set; }
	}
}

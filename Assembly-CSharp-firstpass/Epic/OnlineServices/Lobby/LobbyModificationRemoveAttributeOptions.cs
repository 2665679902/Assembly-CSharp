using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200077A RID: 1914
	public class LobbyModificationRemoveAttributeOptions
	{
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060046DF RID: 18143 RVA: 0x0008F617 File Offset: 0x0008D817
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060046E0 RID: 18144 RVA: 0x0008F61A File Offset: 0x0008D81A
		// (set) Token: 0x060046E1 RID: 18145 RVA: 0x0008F622 File Offset: 0x0008D822
		public string Key { get; set; }
	}
}

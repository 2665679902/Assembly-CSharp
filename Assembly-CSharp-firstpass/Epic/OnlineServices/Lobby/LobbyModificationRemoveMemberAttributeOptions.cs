using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200077C RID: 1916
	public class LobbyModificationRemoveMemberAttributeOptions
	{
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x0008F69B File Offset: 0x0008D89B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x0008F69E File Offset: 0x0008D89E
		// (set) Token: 0x060046EA RID: 18154 RVA: 0x0008F6A6 File Offset: 0x0008D8A6
		public string Key { get; set; }
	}
}

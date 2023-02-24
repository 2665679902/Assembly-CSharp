using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000784 RID: 1924
	public class LobbySearchCopySearchResultByIndexOptions
	{
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06004717 RID: 18199 RVA: 0x0008FA50 File Offset: 0x0008DC50
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06004718 RID: 18200 RVA: 0x0008FA53 File Offset: 0x0008DC53
		// (set) Token: 0x06004719 RID: 18201 RVA: 0x0008FA5B File Offset: 0x0008DC5B
		public uint LobbyIndex { get; set; }
	}
}

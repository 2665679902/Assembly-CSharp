using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000788 RID: 1928
	public class LobbySearchFindOptions
	{
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06004728 RID: 18216 RVA: 0x0008FB4E File Offset: 0x0008DD4E
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06004729 RID: 18217 RVA: 0x0008FB51 File Offset: 0x0008DD51
		// (set) Token: 0x0600472A RID: 18218 RVA: 0x0008FB59 File Offset: 0x0008DD59
		public ProductUserId LocalUserId { get; set; }
	}
}

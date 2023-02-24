using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200075D RID: 1885
	public class LobbyDetailsCopyMemberAttributeByKeyOptions
	{
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060045EA RID: 17898 RVA: 0x0008E0D3 File Offset: 0x0008C2D3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060045EB RID: 17899 RVA: 0x0008E0D6 File Offset: 0x0008C2D6
		// (set) Token: 0x060045EC RID: 17900 RVA: 0x0008E0DE File Offset: 0x0008C2DE
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x0008E0E7 File Offset: 0x0008C2E7
		// (set) Token: 0x060045EE RID: 17902 RVA: 0x0008E0EF File Offset: 0x0008C2EF
		public string AttrKey { get; set; }
	}
}

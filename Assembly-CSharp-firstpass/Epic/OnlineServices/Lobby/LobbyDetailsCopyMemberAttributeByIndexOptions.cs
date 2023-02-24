using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200075B RID: 1883
	public class LobbyDetailsCopyMemberAttributeByIndexOptions
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060045DD RID: 17885 RVA: 0x0008E00B File Offset: 0x0008C20B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060045DE RID: 17886 RVA: 0x0008E00E File Offset: 0x0008C20E
		// (set) Token: 0x060045DF RID: 17887 RVA: 0x0008E016 File Offset: 0x0008C216
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x0008E01F File Offset: 0x0008C21F
		// (set) Token: 0x060045E1 RID: 17889 RVA: 0x0008E027 File Offset: 0x0008C227
		public uint AttrIndex { get; set; }
	}
}

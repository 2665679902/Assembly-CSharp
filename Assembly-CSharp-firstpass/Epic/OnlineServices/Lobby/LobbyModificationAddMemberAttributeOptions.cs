using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000778 RID: 1912
	public class LobbyModificationAddMemberAttributeOptions
	{
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x0008F543 File Offset: 0x0008D743
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060046D3 RID: 18131 RVA: 0x0008F546 File Offset: 0x0008D746
		// (set) Token: 0x060046D4 RID: 18132 RVA: 0x0008F54E File Offset: 0x0008D74E
		public AttributeData Attribute { get; set; }

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x0008F557 File Offset: 0x0008D757
		// (set) Token: 0x060046D6 RID: 18134 RVA: 0x0008F55F File Offset: 0x0008D75F
		public LobbyAttributeVisibility Visibility { get; set; }
	}
}

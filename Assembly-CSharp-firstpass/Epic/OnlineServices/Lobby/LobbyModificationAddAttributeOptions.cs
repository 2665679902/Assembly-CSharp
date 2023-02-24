using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000776 RID: 1910
	public class LobbyModificationAddAttributeOptions
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060046C5 RID: 18117 RVA: 0x0008F46D File Offset: 0x0008D66D
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060046C6 RID: 18118 RVA: 0x0008F470 File Offset: 0x0008D670
		// (set) Token: 0x060046C7 RID: 18119 RVA: 0x0008F478 File Offset: 0x0008D678
		public AttributeData Attribute { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x0008F481 File Offset: 0x0008D681
		// (set) Token: 0x060046C9 RID: 18121 RVA: 0x0008F489 File Offset: 0x0008D689
		public LobbyAttributeVisibility Visibility { get; set; }
	}
}

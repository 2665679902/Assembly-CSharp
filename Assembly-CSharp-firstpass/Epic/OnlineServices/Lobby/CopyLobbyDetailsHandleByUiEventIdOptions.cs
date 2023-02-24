using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000733 RID: 1843
	public class CopyLobbyDetailsHandleByUiEventIdOptions
	{
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x0008CFA7 File Offset: 0x0008B1A7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x0008CFAA File Offset: 0x0008B1AA
		// (set) Token: 0x060044E7 RID: 17639 RVA: 0x0008CFB2 File Offset: 0x0008B1B2
		public ulong UiEventId { get; set; }
	}
}

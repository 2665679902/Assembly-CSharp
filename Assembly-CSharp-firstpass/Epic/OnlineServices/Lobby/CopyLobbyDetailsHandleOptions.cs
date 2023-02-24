using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000735 RID: 1845
	public class CopyLobbyDetailsHandleOptions
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x0008D02B File Offset: 0x0008B22B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x0008D02E File Offset: 0x0008B22E
		// (set) Token: 0x060044F0 RID: 17648 RVA: 0x0008D036 File Offset: 0x0008B236
		public string LobbyId { get; set; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x0008D03F File Offset: 0x0008B23F
		// (set) Token: 0x060044F2 RID: 17650 RVA: 0x0008D047 File Offset: 0x0008B247
		public ProductUserId LocalUserId { get; set; }
	}
}

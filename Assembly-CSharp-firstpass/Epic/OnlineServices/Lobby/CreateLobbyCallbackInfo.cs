using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000737 RID: 1847
	public class CreateLobbyCallbackInfo
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060044FB RID: 17659 RVA: 0x0008D0F3 File Offset: 0x0008B2F3
		// (set) Token: 0x060044FC RID: 17660 RVA: 0x0008D0FB File Offset: 0x0008B2FB
		public Result ResultCode { get; set; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060044FD RID: 17661 RVA: 0x0008D104 File Offset: 0x0008B304
		// (set) Token: 0x060044FE RID: 17662 RVA: 0x0008D10C File Offset: 0x0008B30C
		public object ClientData { get; set; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060044FF RID: 17663 RVA: 0x0008D115 File Offset: 0x0008B315
		// (set) Token: 0x06004500 RID: 17664 RVA: 0x0008D11D File Offset: 0x0008B31D
		public string LobbyId { get; set; }
	}
}

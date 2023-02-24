using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007BA RID: 1978
	public class PromoteMemberCallbackInfo
	{
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x0008FFB6 File Offset: 0x0008E1B6
		// (set) Token: 0x060047FC RID: 18428 RVA: 0x0008FFBE File Offset: 0x0008E1BE
		public Result ResultCode { get; set; }

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060047FD RID: 18429 RVA: 0x0008FFC7 File Offset: 0x0008E1C7
		// (set) Token: 0x060047FE RID: 18430 RVA: 0x0008FFCF File Offset: 0x0008E1CF
		public object ClientData { get; set; }

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060047FF RID: 18431 RVA: 0x0008FFD8 File Offset: 0x0008E1D8
		// (set) Token: 0x06004800 RID: 18432 RVA: 0x0008FFE0 File Offset: 0x0008E1E0
		public string LobbyId { get; set; }
	}
}

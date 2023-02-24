using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200060F RID: 1551
	public class RegisterPlayersCallbackInfo
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x0008529F File Offset: 0x0008349F
		// (set) Token: 0x06003D8D RID: 15757 RVA: 0x000852A7 File Offset: 0x000834A7
		public Result ResultCode { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x000852B0 File Offset: 0x000834B0
		// (set) Token: 0x06003D8F RID: 15759 RVA: 0x000852B8 File Offset: 0x000834B8
		public object ClientData { get; set; }
	}
}

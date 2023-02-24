using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200069F RID: 1695
	public class DuplicateFileCallbackInfo
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06004115 RID: 16661 RVA: 0x00089107 File Offset: 0x00087307
		// (set) Token: 0x06004116 RID: 16662 RVA: 0x0008910F File Offset: 0x0008730F
		public Result ResultCode { get; set; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x00089118 File Offset: 0x00087318
		// (set) Token: 0x06004118 RID: 16664 RVA: 0x00089120 File Offset: 0x00087320
		public object ClientData { get; set; }

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x00089129 File Offset: 0x00087329
		// (set) Token: 0x0600411A RID: 16666 RVA: 0x00089131 File Offset: 0x00087331
		public ProductUserId LocalUserId { get; set; }
	}
}

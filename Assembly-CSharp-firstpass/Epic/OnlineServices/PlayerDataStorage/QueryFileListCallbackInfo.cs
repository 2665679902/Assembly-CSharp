using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006BF RID: 1727
	public class QueryFileListCallbackInfo
	{
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x00089B5E File Offset: 0x00087D5E
		// (set) Token: 0x060041D6 RID: 16854 RVA: 0x00089B66 File Offset: 0x00087D66
		public Result ResultCode { get; set; }

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060041D7 RID: 16855 RVA: 0x00089B6F File Offset: 0x00087D6F
		// (set) Token: 0x060041D8 RID: 16856 RVA: 0x00089B77 File Offset: 0x00087D77
		public object ClientData { get; set; }

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060041D9 RID: 16857 RVA: 0x00089B80 File Offset: 0x00087D80
		// (set) Token: 0x060041DA RID: 16858 RVA: 0x00089B88 File Offset: 0x00087D88
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060041DB RID: 16859 RVA: 0x00089B91 File Offset: 0x00087D91
		// (set) Token: 0x060041DC RID: 16860 RVA: 0x00089B99 File Offset: 0x00087D99
		public uint FileCount { get; set; }
	}
}

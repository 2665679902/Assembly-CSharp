using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008ED RID: 2285
	public class LinkAccountCallbackInfo
	{
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06004FDF RID: 20447 RVA: 0x00097F1F File Offset: 0x0009611F
		// (set) Token: 0x06004FE0 RID: 20448 RVA: 0x00097F27 File Offset: 0x00096127
		public Result ResultCode { get; set; }

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004FE1 RID: 20449 RVA: 0x00097F30 File Offset: 0x00096130
		// (set) Token: 0x06004FE2 RID: 20450 RVA: 0x00097F38 File Offset: 0x00096138
		public object ClientData { get; set; }

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004FE3 RID: 20451 RVA: 0x00097F41 File Offset: 0x00096141
		// (set) Token: 0x06004FE4 RID: 20452 RVA: 0x00097F49 File Offset: 0x00096149
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004FE5 RID: 20453 RVA: 0x00097F52 File Offset: 0x00096152
		// (set) Token: 0x06004FE6 RID: 20454 RVA: 0x00097F5A File Offset: 0x0009615A
		public PinGrantInfo PinGrantInfo { get; set; }
	}
}

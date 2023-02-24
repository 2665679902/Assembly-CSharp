using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000573 RID: 1395
	public class ShowFriendsCallbackInfo
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x00081F23 File Offset: 0x00080123
		// (set) Token: 0x060039FD RID: 14845 RVA: 0x00081F2B File Offset: 0x0008012B
		public Result ResultCode { get; set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x00081F34 File Offset: 0x00080134
		// (set) Token: 0x060039FF RID: 14847 RVA: 0x00081F3C File Offset: 0x0008013C
		public object ClientData { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x00081F45 File Offset: 0x00080145
		// (set) Token: 0x06003A01 RID: 14849 RVA: 0x00081F4D File Offset: 0x0008014D
		public EpicAccountId LocalUserId { get; set; }
	}
}

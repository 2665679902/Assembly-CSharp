using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000561 RID: 1377
	public class HideFriendsCallbackInfo
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x00081C37 File Offset: 0x0007FE37
		// (set) Token: 0x060039B4 RID: 14772 RVA: 0x00081C3F File Offset: 0x0007FE3F
		public Result ResultCode { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x00081C48 File Offset: 0x0007FE48
		// (set) Token: 0x060039B6 RID: 14774 RVA: 0x00081C50 File Offset: 0x0007FE50
		public object ClientData { get; set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060039B7 RID: 14775 RVA: 0x00081C59 File Offset: 0x0007FE59
		// (set) Token: 0x060039B8 RID: 14776 RVA: 0x00081C61 File Offset: 0x0007FE61
		public EpicAccountId LocalUserId { get; set; }
	}
}

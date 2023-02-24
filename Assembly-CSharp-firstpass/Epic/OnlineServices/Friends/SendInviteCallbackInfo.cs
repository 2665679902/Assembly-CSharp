using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000820 RID: 2080
	public class SendInviteCallbackInfo
	{
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x0009246F File Offset: 0x0009066F
		// (set) Token: 0x06004A58 RID: 19032 RVA: 0x00092477 File Offset: 0x00090677
		public Result ResultCode { get; set; }

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06004A59 RID: 19033 RVA: 0x00092480 File Offset: 0x00090680
		// (set) Token: 0x06004A5A RID: 19034 RVA: 0x00092488 File Offset: 0x00090688
		public object ClientData { get; set; }

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06004A5B RID: 19035 RVA: 0x00092491 File Offset: 0x00090691
		// (set) Token: 0x06004A5C RID: 19036 RVA: 0x00092499 File Offset: 0x00090699
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x000924A2 File Offset: 0x000906A2
		// (set) Token: 0x06004A5E RID: 19038 RVA: 0x000924AA File Offset: 0x000906AA
		public EpicAccountId TargetUserId { get; set; }
	}
}

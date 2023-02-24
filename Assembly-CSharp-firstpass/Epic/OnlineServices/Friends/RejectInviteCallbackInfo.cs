using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200081C RID: 2076
	public class RejectInviteCallbackInfo
	{
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06004A3C RID: 19004 RVA: 0x000922C3 File Offset: 0x000904C3
		// (set) Token: 0x06004A3D RID: 19005 RVA: 0x000922CB File Offset: 0x000904CB
		public Result ResultCode { get; set; }

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x000922D4 File Offset: 0x000904D4
		// (set) Token: 0x06004A3F RID: 19007 RVA: 0x000922DC File Offset: 0x000904DC
		public object ClientData { get; set; }

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06004A40 RID: 19008 RVA: 0x000922E5 File Offset: 0x000904E5
		// (set) Token: 0x06004A41 RID: 19009 RVA: 0x000922ED File Offset: 0x000904ED
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06004A42 RID: 19010 RVA: 0x000922F6 File Offset: 0x000904F6
		// (set) Token: 0x06004A43 RID: 19011 RVA: 0x000922FE File Offset: 0x000904FE
		public EpicAccountId TargetUserId { get; set; }
	}
}

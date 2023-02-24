using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x020007FE RID: 2046
	public class AcceptInviteCallbackInfo
	{
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06004994 RID: 18836 RVA: 0x0009194F File Offset: 0x0008FB4F
		// (set) Token: 0x06004995 RID: 18837 RVA: 0x00091957 File Offset: 0x0008FB57
		public Result ResultCode { get; set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x00091960 File Offset: 0x0008FB60
		// (set) Token: 0x06004997 RID: 18839 RVA: 0x00091968 File Offset: 0x0008FB68
		public object ClientData { get; set; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06004998 RID: 18840 RVA: 0x00091971 File Offset: 0x0008FB71
		// (set) Token: 0x06004999 RID: 18841 RVA: 0x00091979 File Offset: 0x0008FB79
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600499A RID: 18842 RVA: 0x00091982 File Offset: 0x0008FB82
		// (set) Token: 0x0600499B RID: 18843 RVA: 0x0009198A File Offset: 0x0008FB8A
		public EpicAccountId TargetUserId { get; set; }
	}
}

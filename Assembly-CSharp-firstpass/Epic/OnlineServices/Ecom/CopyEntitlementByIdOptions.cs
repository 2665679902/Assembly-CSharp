using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000830 RID: 2096
	public class CopyEntitlementByIdOptions
	{
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004B1D RID: 19229 RVA: 0x0009316B File Offset: 0x0009136B
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06004B1E RID: 19230 RVA: 0x0009316E File Offset: 0x0009136E
		// (set) Token: 0x06004B1F RID: 19231 RVA: 0x00093176 File Offset: 0x00091376
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06004B20 RID: 19232 RVA: 0x0009317F File Offset: 0x0009137F
		// (set) Token: 0x06004B21 RID: 19233 RVA: 0x00093187 File Offset: 0x00091387
		public string EntitlementId { get; set; }
	}
}

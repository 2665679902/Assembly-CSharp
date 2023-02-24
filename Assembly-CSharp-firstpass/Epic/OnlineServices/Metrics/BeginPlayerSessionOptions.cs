using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x0200070D RID: 1805
	public class BeginPlayerSessionOptions
	{
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x0600442B RID: 17451 RVA: 0x0008C21D File Offset: 0x0008A41D
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x0008C220 File Offset: 0x0008A420
		// (set) Token: 0x0600442D RID: 17453 RVA: 0x0008C228 File Offset: 0x0008A428
		public BeginPlayerSessionOptionsAccountId AccountId { get; set; }

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600442E RID: 17454 RVA: 0x0008C231 File Offset: 0x0008A431
		// (set) Token: 0x0600442F RID: 17455 RVA: 0x0008C239 File Offset: 0x0008A439
		public string DisplayName { get; set; }

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x0008C242 File Offset: 0x0008A442
		// (set) Token: 0x06004431 RID: 17457 RVA: 0x0008C24A File Offset: 0x0008A44A
		public UserControllerType ControllerType { get; set; }

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x0008C253 File Offset: 0x0008A453
		// (set) Token: 0x06004433 RID: 17459 RVA: 0x0008C25B File Offset: 0x0008A45B
		public string ServerIp { get; set; }

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x0008C264 File Offset: 0x0008A464
		// (set) Token: 0x06004435 RID: 17461 RVA: 0x0008C26C File Offset: 0x0008A46C
		public string GameSessionId { get; set; }
	}
}

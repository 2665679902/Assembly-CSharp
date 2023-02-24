using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000909 RID: 2313
	public class PinGrantInfo
	{
		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06005071 RID: 20593 RVA: 0x0009854B File Offset: 0x0009674B
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x0009854E File Offset: 0x0009674E
		// (set) Token: 0x06005073 RID: 20595 RVA: 0x00098556 File Offset: 0x00096756
		public string UserCode { get; set; }

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x0009855F File Offset: 0x0009675F
		// (set) Token: 0x06005075 RID: 20597 RVA: 0x00098567 File Offset: 0x00096767
		public string VerificationURI { get; set; }

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06005076 RID: 20598 RVA: 0x00098570 File Offset: 0x00096770
		// (set) Token: 0x06005077 RID: 20599 RVA: 0x00098578 File Offset: 0x00096778
		public int ExpiresIn { get; set; }

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06005078 RID: 20600 RVA: 0x00098581 File Offset: 0x00096781
		// (set) Token: 0x06005079 RID: 20601 RVA: 0x00098589 File Offset: 0x00096789
		public string VerificationURIComplete { get; set; }
	}
}

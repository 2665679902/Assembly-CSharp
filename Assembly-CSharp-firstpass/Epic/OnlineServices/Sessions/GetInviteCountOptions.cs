using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E3 RID: 1507
	public class GetInviteCountOptions
	{
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x00084CDB File Offset: 0x00082EDB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x00084CDE File Offset: 0x00082EDE
		// (set) Token: 0x06003CC7 RID: 15559 RVA: 0x00084CE6 File Offset: 0x00082EE6
		public ProductUserId LocalUserId { get; set; }
	}
}

using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000619 RID: 1561
	public class SendInviteOptions
	{
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000855BA File Offset: 0x000837BA
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06003DBF RID: 15807 RVA: 0x000855BD File Offset: 0x000837BD
		// (set) Token: 0x06003DC0 RID: 15808 RVA: 0x000855C5 File Offset: 0x000837C5
		public string SessionName { get; set; }

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x000855CE File Offset: 0x000837CE
		// (set) Token: 0x06003DC2 RID: 15810 RVA: 0x000855D6 File Offset: 0x000837D6
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x000855DF File Offset: 0x000837DF
		// (set) Token: 0x06003DC4 RID: 15812 RVA: 0x000855E7 File Offset: 0x000837E7
		public ProductUserId TargetUserId { get; set; }
	}
}

using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200068E RID: 1678
	public class QueryPresenceOptions
	{
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x00088BBE File Offset: 0x00086DBE
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x00088BC1 File Offset: 0x00086DC1
		// (set) Token: 0x060040B8 RID: 16568 RVA: 0x00088BC9 File Offset: 0x00086DC9
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x00088BD2 File Offset: 0x00086DD2
		// (set) Token: 0x060040BA RID: 16570 RVA: 0x00088BDA File Offset: 0x00086DDA
		public EpicAccountId TargetUserId { get; set; }
	}
}

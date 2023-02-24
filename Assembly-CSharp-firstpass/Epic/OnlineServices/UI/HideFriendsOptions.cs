using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000563 RID: 1379
	public class HideFriendsOptions
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x00081CE6 File Offset: 0x0007FEE6
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x00081CE9 File Offset: 0x0007FEE9
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x00081CF1 File Offset: 0x0007FEF1
		public EpicAccountId LocalUserId { get; set; }
	}
}

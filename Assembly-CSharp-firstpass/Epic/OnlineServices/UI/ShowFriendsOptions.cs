using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000575 RID: 1397
	public class ShowFriendsOptions
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x00081FD2 File Offset: 0x000801D2
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x00081FD5 File Offset: 0x000801D5
		// (set) Token: 0x06003A09 RID: 14857 RVA: 0x00081FDD File Offset: 0x000801DD
		public EpicAccountId LocalUserId { get; set; }
	}
}

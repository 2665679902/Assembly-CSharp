using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000808 RID: 2056
	public class GetFriendsCountOptions
	{
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060049D9 RID: 18905 RVA: 0x00091F2B File Offset: 0x0009012B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x00091F2E File Offset: 0x0009012E
		// (set) Token: 0x060049DB RID: 18907 RVA: 0x00091F36 File Offset: 0x00090136
		public EpicAccountId LocalUserId { get; set; }
	}
}

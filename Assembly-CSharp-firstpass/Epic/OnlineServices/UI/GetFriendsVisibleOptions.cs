using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200055D RID: 1373
	public class GetFriendsVisibleOptions
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060039A5 RID: 14757 RVA: 0x00081B73 File Offset: 0x0007FD73
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x00081B76 File Offset: 0x0007FD76
		// (set) Token: 0x060039A7 RID: 14759 RVA: 0x00081B7E File Offset: 0x0007FD7E
		public EpicAccountId LocalUserId { get; set; }
	}
}

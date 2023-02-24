using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200081A RID: 2074
	public class QueryFriendsOptions
	{
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06004A33 RID: 18995 RVA: 0x0009223E File Offset: 0x0009043E
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x00092241 File Offset: 0x00090441
		// (set) Token: 0x06004A35 RID: 18997 RVA: 0x00092249 File Offset: 0x00090449
		public EpicAccountId LocalUserId { get; set; }
	}
}

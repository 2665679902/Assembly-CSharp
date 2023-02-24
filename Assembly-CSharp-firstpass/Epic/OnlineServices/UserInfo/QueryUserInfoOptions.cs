using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000554 RID: 1364
	public class QueryUserInfoOptions
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x0008150A File Offset: 0x0007F70A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06003958 RID: 14680 RVA: 0x0008150D File Offset: 0x0007F70D
		// (set) Token: 0x06003959 RID: 14681 RVA: 0x00081515 File Offset: 0x0007F715
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600395A RID: 14682 RVA: 0x0008151E File Offset: 0x0007F71E
		// (set) Token: 0x0600395B RID: 14683 RVA: 0x00081526 File Offset: 0x0007F726
		public EpicAccountId TargetUserId { get; set; }
	}
}

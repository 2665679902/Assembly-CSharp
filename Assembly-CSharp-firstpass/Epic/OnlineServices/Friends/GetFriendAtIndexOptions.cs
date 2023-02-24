using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000806 RID: 2054
	public class GetFriendAtIndexOptions
	{
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x00091E60 File Offset: 0x00090060
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x00091E63 File Offset: 0x00090063
		// (set) Token: 0x060049CE RID: 18894 RVA: 0x00091E6B File Offset: 0x0009006B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x00091E74 File Offset: 0x00090074
		// (set) Token: 0x060049D0 RID: 18896 RVA: 0x00091E7C File Offset: 0x0009007C
		public int Index { get; set; }
	}
}

using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000822 RID: 2082
	public class SendInviteOptions
	{
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x00092552 File Offset: 0x00090752
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06004A66 RID: 19046 RVA: 0x00092555 File Offset: 0x00090755
		// (set) Token: 0x06004A67 RID: 19047 RVA: 0x0009255D File Offset: 0x0009075D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x00092566 File Offset: 0x00090766
		// (set) Token: 0x06004A69 RID: 19049 RVA: 0x0009256E File Offset: 0x0009076E
		public EpicAccountId TargetUserId { get; set; }
	}
}

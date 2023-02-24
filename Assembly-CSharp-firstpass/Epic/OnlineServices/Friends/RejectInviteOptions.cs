using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200081E RID: 2078
	public class RejectInviteOptions
	{
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x000923A6 File Offset: 0x000905A6
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06004A4B RID: 19019 RVA: 0x000923A9 File Offset: 0x000905A9
		// (set) Token: 0x06004A4C RID: 19020 RVA: 0x000923B1 File Offset: 0x000905B1
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06004A4D RID: 19021 RVA: 0x000923BA File Offset: 0x000905BA
		// (set) Token: 0x06004A4E RID: 19022 RVA: 0x000923C2 File Offset: 0x000905C2
		public EpicAccountId TargetUserId { get; set; }
	}
}

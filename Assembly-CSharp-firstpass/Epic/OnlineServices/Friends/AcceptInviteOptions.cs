using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000800 RID: 2048
	public class AcceptInviteOptions
	{
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060049A2 RID: 18850 RVA: 0x00091A32 File Offset: 0x0008FC32
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060049A3 RID: 18851 RVA: 0x00091A35 File Offset: 0x0008FC35
		// (set) Token: 0x060049A4 RID: 18852 RVA: 0x00091A3D File Offset: 0x0008FC3D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060049A5 RID: 18853 RVA: 0x00091A46 File Offset: 0x0008FC46
		// (set) Token: 0x060049A6 RID: 18854 RVA: 0x00091A4E File Offset: 0x0008FC4E
		public EpicAccountId TargetUserId { get; set; }
	}
}

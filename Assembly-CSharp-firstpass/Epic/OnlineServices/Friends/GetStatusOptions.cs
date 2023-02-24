using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200080A RID: 2058
	public class GetStatusOptions
	{
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x00091FAF File Offset: 0x000901AF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060049E3 RID: 18915 RVA: 0x00091FB2 File Offset: 0x000901B2
		// (set) Token: 0x060049E4 RID: 18916 RVA: 0x00091FBA File Offset: 0x000901BA
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x00091FC3 File Offset: 0x000901C3
		// (set) Token: 0x060049E6 RID: 18918 RVA: 0x00091FCB File Offset: 0x000901CB
		public EpicAccountId TargetUserId { get; set; }
	}
}

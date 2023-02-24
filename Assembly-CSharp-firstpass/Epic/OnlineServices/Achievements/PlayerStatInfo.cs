using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000946 RID: 2374
	public class PlayerStatInfo
	{
		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x0600524E RID: 21070 RVA: 0x0009A40B File Offset: 0x0009860B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x0600524F RID: 21071 RVA: 0x0009A40E File Offset: 0x0009860E
		// (set) Token: 0x06005250 RID: 21072 RVA: 0x0009A416 File Offset: 0x00098616
		public string Name { get; set; }

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06005251 RID: 21073 RVA: 0x0009A41F File Offset: 0x0009861F
		// (set) Token: 0x06005252 RID: 21074 RVA: 0x0009A427 File Offset: 0x00098627
		public int CurrentValue { get; set; }

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06005253 RID: 21075 RVA: 0x0009A430 File Offset: 0x00098630
		// (set) Token: 0x06005254 RID: 21076 RVA: 0x0009A438 File Offset: 0x00098638
		public int ThresholdValue { get; set; }
	}
}

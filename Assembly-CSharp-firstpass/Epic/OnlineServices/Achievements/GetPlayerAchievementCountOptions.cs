using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200092C RID: 2348
	public class GetPlayerAchievementCountOptions
	{
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x060051B5 RID: 20917 RVA: 0x00099CDF File Offset: 0x00097EDF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x060051B6 RID: 20918 RVA: 0x00099CE2 File Offset: 0x00097EE2
		// (set) Token: 0x060051B7 RID: 20919 RVA: 0x00099CEA File Offset: 0x00097EEA
		public ProductUserId UserId { get; set; }
	}
}

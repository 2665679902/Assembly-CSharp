using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200092E RID: 2350
	public class GetUnlockedAchievementCountOptions
	{
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060051BE RID: 20926 RVA: 0x00099D63 File Offset: 0x00097F63
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060051BF RID: 20927 RVA: 0x00099D66 File Offset: 0x00097F66
		// (set) Token: 0x060051C0 RID: 20928 RVA: 0x00099D6E File Offset: 0x00097F6E
		public ProductUserId UserId { get; set; }
	}
}

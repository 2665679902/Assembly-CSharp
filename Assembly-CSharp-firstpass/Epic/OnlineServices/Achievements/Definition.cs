using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000926 RID: 2342
	public class Definition
	{
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06005152 RID: 20818 RVA: 0x00099647 File Offset: 0x00097847
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06005153 RID: 20819 RVA: 0x0009964A File Offset: 0x0009784A
		// (set) Token: 0x06005154 RID: 20820 RVA: 0x00099652 File Offset: 0x00097852
		public string AchievementId { get; set; }

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06005155 RID: 20821 RVA: 0x0009965B File Offset: 0x0009785B
		// (set) Token: 0x06005156 RID: 20822 RVA: 0x00099663 File Offset: 0x00097863
		public string DisplayName { get; set; }

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06005157 RID: 20823 RVA: 0x0009966C File Offset: 0x0009786C
		// (set) Token: 0x06005158 RID: 20824 RVA: 0x00099674 File Offset: 0x00097874
		public string Description { get; set; }

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06005159 RID: 20825 RVA: 0x0009967D File Offset: 0x0009787D
		// (set) Token: 0x0600515A RID: 20826 RVA: 0x00099685 File Offset: 0x00097885
		public string LockedDisplayName { get; set; }

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x0600515B RID: 20827 RVA: 0x0009968E File Offset: 0x0009788E
		// (set) Token: 0x0600515C RID: 20828 RVA: 0x00099696 File Offset: 0x00097896
		public string LockedDescription { get; set; }

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x0600515D RID: 20829 RVA: 0x0009969F File Offset: 0x0009789F
		// (set) Token: 0x0600515E RID: 20830 RVA: 0x000996A7 File Offset: 0x000978A7
		public string HiddenDescription { get; set; }

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x0600515F RID: 20831 RVA: 0x000996B0 File Offset: 0x000978B0
		// (set) Token: 0x06005160 RID: 20832 RVA: 0x000996B8 File Offset: 0x000978B8
		public string CompletionDescription { get; set; }

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06005161 RID: 20833 RVA: 0x000996C1 File Offset: 0x000978C1
		// (set) Token: 0x06005162 RID: 20834 RVA: 0x000996C9 File Offset: 0x000978C9
		public string UnlockedIconId { get; set; }

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06005163 RID: 20835 RVA: 0x000996D2 File Offset: 0x000978D2
		// (set) Token: 0x06005164 RID: 20836 RVA: 0x000996DA File Offset: 0x000978DA
		public string LockedIconId { get; set; }

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06005165 RID: 20837 RVA: 0x000996E3 File Offset: 0x000978E3
		// (set) Token: 0x06005166 RID: 20838 RVA: 0x000996EB File Offset: 0x000978EB
		public bool IsHidden { get; set; }

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06005167 RID: 20839 RVA: 0x000996F4 File Offset: 0x000978F4
		// (set) Token: 0x06005168 RID: 20840 RVA: 0x000996FC File Offset: 0x000978FC
		public StatThresholds[] StatThresholds { get; set; }
	}
}

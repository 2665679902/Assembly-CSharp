using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000928 RID: 2344
	public class DefinitionV2
	{
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06005183 RID: 20867 RVA: 0x00099997 File Offset: 0x00097B97
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06005184 RID: 20868 RVA: 0x0009999A File Offset: 0x00097B9A
		// (set) Token: 0x06005185 RID: 20869 RVA: 0x000999A2 File Offset: 0x00097BA2
		public string AchievementId { get; set; }

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06005186 RID: 20870 RVA: 0x000999AB File Offset: 0x00097BAB
		// (set) Token: 0x06005187 RID: 20871 RVA: 0x000999B3 File Offset: 0x00097BB3
		public string UnlockedDisplayName { get; set; }

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x000999BC File Offset: 0x00097BBC
		// (set) Token: 0x06005189 RID: 20873 RVA: 0x000999C4 File Offset: 0x00097BC4
		public string UnlockedDescription { get; set; }

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600518A RID: 20874 RVA: 0x000999CD File Offset: 0x00097BCD
		// (set) Token: 0x0600518B RID: 20875 RVA: 0x000999D5 File Offset: 0x00097BD5
		public string LockedDisplayName { get; set; }

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600518C RID: 20876 RVA: 0x000999DE File Offset: 0x00097BDE
		// (set) Token: 0x0600518D RID: 20877 RVA: 0x000999E6 File Offset: 0x00097BE6
		public string LockedDescription { get; set; }

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600518E RID: 20878 RVA: 0x000999EF File Offset: 0x00097BEF
		// (set) Token: 0x0600518F RID: 20879 RVA: 0x000999F7 File Offset: 0x00097BF7
		public string FlavorText { get; set; }

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06005190 RID: 20880 RVA: 0x00099A00 File Offset: 0x00097C00
		// (set) Token: 0x06005191 RID: 20881 RVA: 0x00099A08 File Offset: 0x00097C08
		public string UnlockedIconURL { get; set; }

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06005192 RID: 20882 RVA: 0x00099A11 File Offset: 0x00097C11
		// (set) Token: 0x06005193 RID: 20883 RVA: 0x00099A19 File Offset: 0x00097C19
		public string LockedIconURL { get; set; }

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06005194 RID: 20884 RVA: 0x00099A22 File Offset: 0x00097C22
		// (set) Token: 0x06005195 RID: 20885 RVA: 0x00099A2A File Offset: 0x00097C2A
		public bool IsHidden { get; set; }

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06005196 RID: 20886 RVA: 0x00099A33 File Offset: 0x00097C33
		// (set) Token: 0x06005197 RID: 20887 RVA: 0x00099A3B File Offset: 0x00097C3B
		public StatThresholds[] StatThresholds { get; set; }
	}
}

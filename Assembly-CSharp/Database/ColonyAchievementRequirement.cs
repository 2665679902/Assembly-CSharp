using System;

namespace Database
{
	// Token: 0x02000CC3 RID: 3267
	public abstract class ColonyAchievementRequirement
	{
		// Token: 0x0600662C RID: 26156
		public abstract bool Success();

		// Token: 0x0600662D RID: 26157 RVA: 0x00275840 File Offset: 0x00273A40
		public virtual bool Fail()
		{
			return false;
		}

		// Token: 0x0600662E RID: 26158 RVA: 0x00275843 File Offset: 0x00273A43
		public virtual string GetProgress(bool complete)
		{
			return "";
		}
	}
}

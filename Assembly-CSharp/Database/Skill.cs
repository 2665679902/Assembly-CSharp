using System;
using System.Collections.Generic;
using TUNING;

namespace Database
{
	// Token: 0x02000D01 RID: 3329
	public class Skill : Resource
	{
		// Token: 0x06006726 RID: 26406 RVA: 0x0027B9A0 File Offset: 0x00279BA0
		public Skill(string id, string name, string description, string dlcId, int tier, string hat, string badge, string skillGroup, List<SkillPerk> perks = null, List<string> priorSkills = null)
			: base(id, name)
		{
			this.description = description;
			this.dlcId = dlcId;
			this.tier = tier;
			this.hat = hat;
			this.badge = badge;
			this.skillGroup = skillGroup;
			this.perks = perks;
			if (this.perks == null)
			{
				this.perks = new List<SkillPerk>();
			}
			this.priorSkills = priorSkills;
			if (this.priorSkills == null)
			{
				this.priorSkills = new List<string>();
			}
		}

		// Token: 0x06006727 RID: 26407 RVA: 0x0027BA1A File Offset: 0x00279C1A
		public int GetMoraleExpectation()
		{
			return SKILLS.SKILL_TIER_MORALE_COST[this.tier];
		}

		// Token: 0x06006728 RID: 26408 RVA: 0x0027BA28 File Offset: 0x00279C28
		public bool GivesPerk(SkillPerk perk)
		{
			return this.perks.Contains(perk);
		}

		// Token: 0x06006729 RID: 26409 RVA: 0x0027BA38 File Offset: 0x00279C38
		public bool GivesPerk(HashedString perkId)
		{
			using (List<SkillPerk>.Enumerator enumerator = this.perks.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IdHash == perkId)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04004B96 RID: 19350
		public string description;

		// Token: 0x04004B97 RID: 19351
		public string dlcId;

		// Token: 0x04004B98 RID: 19352
		public string skillGroup;

		// Token: 0x04004B99 RID: 19353
		public string hat;

		// Token: 0x04004B9A RID: 19354
		public string badge;

		// Token: 0x04004B9B RID: 19355
		public int tier;

		// Token: 0x04004B9C RID: 19356
		public bool deprecated;

		// Token: 0x04004B9D RID: 19357
		public List<SkillPerk> perks;

		// Token: 0x04004B9E RID: 19358
		public List<string> priorSkills;
	}
}

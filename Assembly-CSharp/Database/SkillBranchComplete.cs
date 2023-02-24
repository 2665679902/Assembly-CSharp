using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CD3 RID: 3283
	public class SkillBranchComplete : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006680 RID: 26240 RVA: 0x00276478 File Offset: 0x00274678
		public SkillBranchComplete(List<Skill> skillsToMaster)
		{
			this.skillsToMaster = skillsToMaster;
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x00276488 File Offset: 0x00274688
		public override bool Success()
		{
			foreach (MinionResume minionResume in Components.MinionResumes.Items)
			{
				foreach (Skill skill in this.skillsToMaster)
				{
					if (minionResume.HasMasteredSkill(skill.Id))
					{
						if (!minionResume.HasBeenGrantedSkill(skill))
						{
							return true;
						}
						List<Skill> allPriorSkills = Db.Get().Skills.GetAllPriorSkills(skill);
						bool flag = true;
						foreach (Skill skill2 in allPriorSkills)
						{
							flag = flag && minionResume.HasMasteredSkill(skill2.Id);
						}
						if (flag)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06006682 RID: 26242 RVA: 0x002765A8 File Offset: 0x002747A8
		public void Deserialize(IReader reader)
		{
			this.skillsToMaster = new List<Skill>();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.skillsToMaster.Add(Db.Get().Skills.Get(text));
			}
		}

		// Token: 0x06006683 RID: 26243 RVA: 0x002765F5 File Offset: 0x002747F5
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.SKILL_BRANCH;
		}

		// Token: 0x04004ACE RID: 19150
		private List<Skill> skillsToMaster;
	}
}

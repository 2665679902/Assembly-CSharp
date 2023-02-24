using System;
using Database;

// Token: 0x02000BF8 RID: 3064
public class SkillListable : IListableOption
{
	// Token: 0x060060DB RID: 24795 RVA: 0x00238F74 File Offset: 0x00237174
	public SkillListable(string name)
	{
		this.skillName = name;
		Skill skill = Db.Get().Skills.TryGet(this.skillName);
		if (skill != null)
		{
			this.name = skill.Name;
			this.skillHat = skill.hat;
		}
	}

	// Token: 0x170006B5 RID: 1717
	// (get) Token: 0x060060DC RID: 24796 RVA: 0x00238FC4 File Offset: 0x002371C4
	// (set) Token: 0x060060DD RID: 24797 RVA: 0x00238FCC File Offset: 0x002371CC
	public string skillName { get; private set; }

	// Token: 0x170006B6 RID: 1718
	// (get) Token: 0x060060DE RID: 24798 RVA: 0x00238FD5 File Offset: 0x002371D5
	// (set) Token: 0x060060DF RID: 24799 RVA: 0x00238FDD File Offset: 0x002371DD
	public string skillHat { get; private set; }

	// Token: 0x060060E0 RID: 24800 RVA: 0x00238FE6 File Offset: 0x002371E6
	public string GetProperName()
	{
		return this.name;
	}

	// Token: 0x04004297 RID: 17047
	public LocString name;
}

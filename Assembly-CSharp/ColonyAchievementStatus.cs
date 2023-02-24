using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Database;

// Token: 0x02000693 RID: 1683
public class ColonyAchievementStatus
{
	// Token: 0x17000331 RID: 817
	// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000F046E File Offset: 0x000EE66E
	public List<ColonyAchievementRequirement> Requirements
	{
		get
		{
			return this.m_achievement.requirementChecklist;
		}
	}

	// Token: 0x06002DB9 RID: 11705 RVA: 0x000F047B File Offset: 0x000EE67B
	public ColonyAchievementStatus(string achievementId)
	{
		this.m_achievement = Db.Get().ColonyAchievements.TryGet(achievementId);
	}

	// Token: 0x06002DBA RID: 11706 RVA: 0x000F049C File Offset: 0x000EE69C
	public void UpdateAchievement()
	{
		if (this.Requirements.Count <= 0)
		{
			return;
		}
		if (this.m_achievement.Disabled)
		{
			return;
		}
		this.success = true;
		foreach (ColonyAchievementRequirement colonyAchievementRequirement in this.Requirements)
		{
			this.success &= colonyAchievementRequirement.Success();
			this.failed |= colonyAchievementRequirement.Fail();
		}
	}

	// Token: 0x06002DBB RID: 11707 RVA: 0x000F0534 File Offset: 0x000EE734
	public static ColonyAchievementStatus Deserialize(IReader reader, string achievementId)
	{
		bool flag = reader.ReadByte() > 0;
		bool flag2 = reader.ReadByte() > 0;
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 22))
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				Type type = Type.GetType(reader.ReadKleiString());
				if (type != null)
				{
					AchievementRequirementSerialization_Deprecated achievementRequirementSerialization_Deprecated = FormatterServices.GetUninitializedObject(type) as AchievementRequirementSerialization_Deprecated;
					Debug.Assert(achievementRequirementSerialization_Deprecated != null, string.Format("Cannot deserialize old data for type {0}", type));
					achievementRequirementSerialization_Deprecated.Deserialize(reader);
				}
			}
		}
		return new ColonyAchievementStatus(achievementId)
		{
			success = flag,
			failed = flag2
		};
	}

	// Token: 0x06002DBC RID: 11708 RVA: 0x000F05D5 File Offset: 0x000EE7D5
	public void Serialize(BinaryWriter writer)
	{
		writer.Write(this.success ? 1 : 0);
		writer.Write(this.failed ? 1 : 0);
	}

	// Token: 0x04001B0B RID: 6923
	public bool success;

	// Token: 0x04001B0C RID: 6924
	public bool failed;

	// Token: 0x04001B0D RID: 6925
	private ColonyAchievement m_achievement;
}

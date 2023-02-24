using System;
using TUNING;

// Token: 0x0200056B RID: 1387
public class BuildingInternalConstructorWorkable : Workable
{
	// Token: 0x0600218C RID: 8588 RVA: 0x000B6C40 File Offset: 0x000B4E40
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.ConstructionSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.minimumAttributeMultiplier = 0.75f;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Building.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.resetProgressOnStop = false;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x0600218D RID: 8589 RVA: 0x000B6CE3 File Offset: 0x000B4EE3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.constructorInstance = this.GetSMI<BuildingInternalConstructor.Instance>();
	}

	// Token: 0x0600218E RID: 8590 RVA: 0x000B6CF7 File Offset: 0x000B4EF7
	protected override void OnCompleteWork(Worker worker)
	{
		this.constructorInstance.ConstructionComplete(false);
	}

	// Token: 0x04001349 RID: 4937
	private BuildingInternalConstructor.Instance constructorInstance;
}

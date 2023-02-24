using System;
using TUNING;

// Token: 0x020005C6 RID: 1478
public class GeoTunerWorkable : Workable
{
	// Token: 0x060024CA RID: 9418 RVA: 0x000C7028 File Offset: 0x000C5228
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetWorkTime(30f);
		this.requiredSkillPerk = Db.Get().SkillPerks.AllowGeyserTuning.Id;
		base.SetWorkerStatusItem(Db.Get().DuplicantStatusItems.Studying);
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_geotuner_kanim") };
		this.attributeConverter = Db.Get().AttributeConverters.GeotuningSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.lightEfficiencyBonus = true;
	}
}

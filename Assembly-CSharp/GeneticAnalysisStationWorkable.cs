using System;
using TUNING;
using UnityEngine;

// Token: 0x020005C3 RID: 1475
public class GeneticAnalysisStationWorkable : Workable
{
	// Token: 0x060024AC RID: 9388 RVA: 0x000C5F90 File Offset: 0x000C4190
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.requiredSkillPerk = Db.Get().SkillPerks.CanIdentifyMutantSeeds.Id;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.AnalyzingGenes;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_genetic_analysisstation_kanim") };
		base.SetWorkTime(150f);
		this.showProgressBar = true;
		this.lightEfficiencyBonus = true;
	}

	// Token: 0x060024AD RID: 9389 RVA: 0x000C604E File Offset: 0x000C424E
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		this.IdentifyMutant();
	}

	// Token: 0x060024AE RID: 9390 RVA: 0x000C6060 File Offset: 0x000C4260
	public void IdentifyMutant()
	{
		GameObject gameObject = this.storage.FindFirst(GameTags.UnidentifiedSeed);
		DebugUtil.DevAssertArgs(gameObject != null, new object[] { "AAACCCCKKK!! GeneticAnalysisStation finished studying a seed but we don't have one in storage??" });
		if (gameObject != null)
		{
			Pickupable component = gameObject.GetComponent<Pickupable>();
			Pickupable pickupable;
			if (component.PrimaryElement.Units > 1f)
			{
				pickupable = component.Take(1f);
			}
			else
			{
				pickupable = this.storage.Drop(gameObject, true).GetComponent<Pickupable>();
			}
			pickupable.transform.SetPosition(base.transform.GetPosition() + this.finishedSeedDropOffset);
			MutantPlant component2 = pickupable.GetComponent<MutantPlant>();
			PlantSubSpeciesCatalog.Instance.IdentifySubSpecies(component2.SubSpeciesID);
			component2.Analyze();
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().LogAnalyzedSeed(component2.SpeciesID);
		}
	}

	// Token: 0x0400151D RID: 5405
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x0400151E RID: 5406
	[MyCmpReq]
	public Storage storage;

	// Token: 0x0400151F RID: 5407
	[SerializeField]
	public Vector3 finishedSeedDropOffset;

	// Token: 0x04001520 RID: 5408
	private Notification notification;

	// Token: 0x04001521 RID: 5409
	public GeneticAnalysisStation.StatesInstance statesInstance;
}

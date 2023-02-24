using System;
using TUNING;

// Token: 0x02000610 RID: 1552
public class MissionControlWorkable : Workable
{
	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06002881 RID: 10369 RVA: 0x000D6C59 File Offset: 0x000D4E59
	// (set) Token: 0x06002882 RID: 10370 RVA: 0x000D6C61 File Offset: 0x000D4E61
	public Spacecraft TargetSpacecraft
	{
		get
		{
			return this.targetSpacecraft;
		}
		set
		{
			base.WorkTimeRemaining = this.GetWorkTime();
			this.targetSpacecraft = value;
		}
	}

	// Token: 0x06002883 RID: 10371 RVA: 0x000D6C78 File Offset: 0x000D4E78
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.requiredSkillPerk = Db.Get().SkillPerks.CanMissionControl.Id;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.MissionControlling;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_mission_control_station_kanim") };
		base.SetWorkTime(90f);
		this.showProgressBar = true;
		this.lightEfficiencyBonus = true;
	}

	// Token: 0x06002884 RID: 10372 RVA: 0x000D6D36 File Offset: 0x000D4F36
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.MissionControlWorkables.Add(this);
	}

	// Token: 0x06002885 RID: 10373 RVA: 0x000D6D49 File Offset: 0x000D4F49
	protected override void OnCleanUp()
	{
		Components.MissionControlWorkables.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x000D6D5C File Offset: 0x000D4F5C
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.workStatusItem = base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.MissionControlAssistingRocket, this.TargetSpacecraft);
		this.operational.SetActive(true, false);
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x000D6DA8 File Offset: 0x000D4FA8
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.TargetSpacecraft == null)
		{
			worker.StopWork();
			return true;
		}
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x000D6DC2 File Offset: 0x000D4FC2
	protected override void OnCompleteWork(Worker worker)
	{
		Debug.Assert(this.TargetSpacecraft != null);
		base.gameObject.GetSMI<MissionControl.Instance>().ApplyEffect(this.TargetSpacecraft);
		base.OnCompleteWork(worker);
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x000D6DEF File Offset: 0x000D4FEF
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(this.workStatusItem, false);
		this.TargetSpacecraft = null;
		this.operational.SetActive(false, false);
	}

	// Token: 0x040017BF RID: 6079
	private Spacecraft targetSpacecraft;

	// Token: 0x040017C0 RID: 6080
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040017C1 RID: 6081
	private Guid workStatusItem = Guid.Empty;
}

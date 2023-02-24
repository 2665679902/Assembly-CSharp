using System;
using TUNING;

// Token: 0x0200060F RID: 1551
public class MissionControlClusterWorkable : Workable
{
	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06002876 RID: 10358 RVA: 0x000D6A45 File Offset: 0x000D4C45
	// (set) Token: 0x06002877 RID: 10359 RVA: 0x000D6A4D File Offset: 0x000D4C4D
	public Clustercraft TargetClustercraft
	{
		get
		{
			return this.targetClustercraft;
		}
		set
		{
			base.WorkTimeRemaining = this.GetWorkTime();
			this.targetClustercraft = value;
		}
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x000D6A64 File Offset: 0x000D4C64
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

	// Token: 0x06002879 RID: 10361 RVA: 0x000D6B22 File Offset: 0x000D4D22
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.MissionControlClusterWorkables.Add(this);
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x000D6B35 File Offset: 0x000D4D35
	protected override void OnCleanUp()
	{
		Components.MissionControlClusterWorkables.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x0600287B RID: 10363 RVA: 0x000D6B48 File Offset: 0x000D4D48
	public static bool IsRocketInRange(AxialI worldLocation, AxialI rocketLocation)
	{
		return AxialUtil.GetDistance(worldLocation, rocketLocation) <= 2;
	}

	// Token: 0x0600287C RID: 10364 RVA: 0x000D6B58 File Offset: 0x000D4D58
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.workStatusItem = base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.MissionControlAssistingRocket, this.TargetClustercraft);
		this.operational.SetActive(true, false);
	}

	// Token: 0x0600287D RID: 10365 RVA: 0x000D6BA4 File Offset: 0x000D4DA4
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.TargetClustercraft == null || !MissionControlClusterWorkable.IsRocketInRange(base.gameObject.GetMyWorldLocation(), this.TargetClustercraft.Location))
		{
			worker.StopWork();
			return true;
		}
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x000D6BE1 File Offset: 0x000D4DE1
	protected override void OnCompleteWork(Worker worker)
	{
		Debug.Assert(this.TargetClustercraft != null);
		base.gameObject.GetSMI<MissionControlCluster.Instance>().ApplyEffect(this.TargetClustercraft);
		base.OnCompleteWork(worker);
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x000D6C11 File Offset: 0x000D4E11
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(this.workStatusItem, false);
		this.TargetClustercraft = null;
		this.operational.SetActive(false, false);
	}

	// Token: 0x040017BC RID: 6076
	private Clustercraft targetClustercraft;

	// Token: 0x040017BD RID: 6077
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040017BE RID: 6078
	private Guid workStatusItem = Guid.Empty;
}

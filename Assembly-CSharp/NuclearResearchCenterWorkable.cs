using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x0200086E RID: 2158
public class NuclearResearchCenterWorkable : Workable
{
	// Token: 0x06003DFB RID: 15867 RVA: 0x00159D18 File Offset: 0x00157F18
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Researching;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.ALL_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.ALL_DAY_EXPERIENCE;
		this.radiationStorage = base.GetComponent<HighEnergyParticleStorage>();
		this.nrc = base.GetComponent<NuclearResearchCenter>();
		this.lightEfficiencyBonus = true;
	}

	// Token: 0x06003DFC RID: 15868 RVA: 0x00159DA4 File Offset: 0x00157FA4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(float.PositiveInfinity);
	}

	// Token: 0x06003DFD RID: 15869 RVA: 0x00159DB8 File Offset: 0x00157FB8
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		float num = dt / this.nrc.timePerPoint;
		if (Game.Instance.FastWorkersModeActive)
		{
			num *= 2f;
		}
		this.radiationStorage.ConsumeAndGet(num * this.nrc.materialPerPoint);
		this.pointsProduced += num;
		if (this.pointsProduced >= 1f)
		{
			int num2 = Mathf.FloorToInt(this.pointsProduced);
			this.pointsProduced -= (float)num2;
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Research, Research.Instance.GetResearchType("nuclear").name, base.transform, 1.5f, false);
			Research.Instance.AddResearchPoints("nuclear", (float)num2);
		}
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		return this.radiationStorage.IsEmpty() || activeResearch == null || activeResearch.PercentageCompleteResearchType("nuclear") >= 1f;
	}

	// Token: 0x06003DFE RID: 15870 RVA: 0x00159EAE File Offset: 0x001580AE
	protected override void OnAbortWork(Worker worker)
	{
		base.OnAbortWork(worker);
	}

	// Token: 0x06003DFF RID: 15871 RVA: 0x00159EB7 File Offset: 0x001580B7
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
	}

	// Token: 0x06003E00 RID: 15872 RVA: 0x00159EC0 File Offset: 0x001580C0
	public override float GetPercentComplete()
	{
		if (Research.Instance.GetActiveResearch() == null)
		{
			return 0f;
		}
		float num = Research.Instance.GetActiveResearch().progressInventory.PointsByTypeID["nuclear"];
		float num2 = 0f;
		if (!Research.Instance.GetActiveResearch().tech.costsByResearchTypeID.TryGetValue("nuclear", out num2))
		{
			return 1f;
		}
		return num / num2;
	}

	// Token: 0x06003E01 RID: 15873 RVA: 0x00159F2F File Offset: 0x0015812F
	public override bool InstantlyFinish(Worker worker)
	{
		return false;
	}

	// Token: 0x04002898 RID: 10392
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002899 RID: 10393
	[Serialize]
	private float pointsProduced;

	// Token: 0x0400289A RID: 10394
	private NuclearResearchCenter nrc;

	// Token: 0x0400289B RID: 10395
	private HighEnergyParticleStorage radiationStorage;
}

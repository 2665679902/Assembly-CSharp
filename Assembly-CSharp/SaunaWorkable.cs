using System;
using Klei;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020008FC RID: 2300
[AddComponentMenu("KMonoBehaviour/Workable/SaunaWorkable")]
public class SaunaWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x0600426F RID: 17007 RVA: 0x001768DA File Offset: 0x00174ADA
	private SaunaWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06004270 RID: 17008 RVA: 0x001768EC File Offset: 0x00174AEC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_sauna_kanim") };
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = true;
		this.workLayer = Grid.SceneLayer.BuildingUse;
		base.SetWorkTime(30f);
		this.sauna = base.GetComponent<Sauna>();
	}

	// Token: 0x06004271 RID: 17009 RVA: 0x00176951 File Offset: 0x00174B51
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.operational.SetActive(true, false);
		worker.GetComponent<Effects>().Add("SaunaRelaxing", false);
	}

	// Token: 0x06004272 RID: 17010 RVA: 0x0017697C File Offset: 0x00174B7C
	protected override void OnCompleteWork(Worker worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.sauna.specificEffect))
		{
			component.Add(this.sauna.specificEffect, true);
		}
		if (!string.IsNullOrEmpty(this.sauna.trackingEffect))
		{
			component.Add(this.sauna.trackingEffect, true);
		}
		this.operational.SetActive(false, false);
	}

	// Token: 0x06004273 RID: 17011 RVA: 0x001769E8 File Offset: 0x00174BE8
	protected override void OnStopWork(Worker worker)
	{
		this.operational.SetActive(false, false);
		worker.GetComponent<Effects>().Remove("SaunaRelaxing");
		Storage component = base.GetComponent<Storage>();
		float num;
		SimUtil.DiseaseInfo diseaseInfo;
		float num2;
		component.ConsumeAndGetDisease(SimHashes.Steam.CreateTag(), this.sauna.steamPerUseKG, out num, out diseaseInfo, out num2);
		component.AddLiquid(SimHashes.Water, this.sauna.steamPerUseKG, this.sauna.waterOutputTemp, diseaseInfo.idx, diseaseInfo.count, true, false);
	}

	// Token: 0x06004274 RID: 17012 RVA: 0x00176A68 File Offset: 0x00174C68
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.sauna.trackingEffect) && component.HasEffect(this.sauna.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.sauna.specificEffect) && component.HasEffect(this.sauna.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04002C65 RID: 11365
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002C66 RID: 11366
	public int basePriority;

	// Token: 0x04002C67 RID: 11367
	private Sauna sauna;
}

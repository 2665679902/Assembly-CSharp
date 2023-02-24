using System;
using Klei;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200092B RID: 2347
[AddComponentMenu("KMonoBehaviour/Workable/SodaFountainWorkable")]
public class SodaFountainWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x0600449A RID: 17562 RVA: 0x00182EB7 File Offset: 0x001810B7
	private SodaFountainWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x0600449B RID: 17563 RVA: 0x00182EC8 File Offset: 0x001810C8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_sodamaker_kanim") };
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = false;
		base.SetWorkTime(30f);
		this.sodaFountain = base.GetComponent<SodaFountain>();
	}

	// Token: 0x0600449C RID: 17564 RVA: 0x00182F25 File Offset: 0x00181125
	protected override void OnStartWork(Worker worker)
	{
		this.operational.SetActive(true, false);
	}

	// Token: 0x0600449D RID: 17565 RVA: 0x00182F34 File Offset: 0x00181134
	protected override void OnCompleteWork(Worker worker)
	{
		Storage component = base.GetComponent<Storage>();
		float num;
		SimUtil.DiseaseInfo diseaseInfo;
		float num2;
		component.ConsumeAndGetDisease(GameTags.Water, this.sodaFountain.waterMassPerUse, out num, out diseaseInfo, out num2);
		SimUtil.DiseaseInfo diseaseInfo2;
		component.ConsumeAndGetDisease(this.sodaFountain.ingredientTag, this.sodaFountain.ingredientMassPerUse, out num, out diseaseInfo2, out num2);
		GermExposureMonitor.Instance smi = worker.GetSMI<GermExposureMonitor.Instance>();
		if (smi != null)
		{
			smi.TryInjectDisease(diseaseInfo.idx, diseaseInfo.count, GameTags.Water, Sickness.InfectionVector.Digestion);
			smi.TryInjectDisease(diseaseInfo2.idx, diseaseInfo2.count, this.sodaFountain.ingredientTag, Sickness.InfectionVector.Digestion);
		}
		Effects component2 = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.sodaFountain.specificEffect))
		{
			component2.Add(this.sodaFountain.specificEffect, true);
		}
		if (!string.IsNullOrEmpty(this.sodaFountain.trackingEffect))
		{
			component2.Add(this.sodaFountain.trackingEffect, true);
		}
	}

	// Token: 0x0600449E RID: 17566 RVA: 0x0018301C File Offset: 0x0018121C
	protected override void OnStopWork(Worker worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x0600449F RID: 17567 RVA: 0x0018302C File Offset: 0x0018122C
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.sodaFountain.trackingEffect) && component.HasEffect(this.sodaFountain.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.sodaFountain.specificEffect) && component.HasEffect(this.sodaFountain.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04002DBE RID: 11710
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002DBF RID: 11711
	public int basePriority;

	// Token: 0x04002DC0 RID: 11712
	private SodaFountain sodaFountain;
}

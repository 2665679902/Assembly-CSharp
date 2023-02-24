using System;
using Klei;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020007EA RID: 2026
[AddComponentMenu("KMonoBehaviour/Workable/JuicerWorkable")]
public class JuicerWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06003A5F RID: 14943 RVA: 0x0014337A File Offset: 0x0014157A
	private JuicerWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06003A60 RID: 14944 RVA: 0x0014338C File Offset: 0x0014158C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_juicer_kanim") };
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = false;
		base.SetWorkTime(30f);
		this.juicer = base.GetComponent<Juicer>();
	}

	// Token: 0x06003A61 RID: 14945 RVA: 0x001433E9 File Offset: 0x001415E9
	protected override void OnStartWork(Worker worker)
	{
		this.operational.SetActive(true, false);
	}

	// Token: 0x06003A62 RID: 14946 RVA: 0x001433F8 File Offset: 0x001415F8
	protected override void OnCompleteWork(Worker worker)
	{
		Storage component = base.GetComponent<Storage>();
		float num;
		SimUtil.DiseaseInfo diseaseInfo;
		float num2;
		component.ConsumeAndGetDisease(GameTags.Water, this.juicer.waterMassPerUse, out num, out diseaseInfo, out num2);
		GermExposureMonitor.Instance smi = worker.GetSMI<GermExposureMonitor.Instance>();
		for (int i = 0; i < this.juicer.ingredientTags.Length; i++)
		{
			SimUtil.DiseaseInfo diseaseInfo2;
			component.ConsumeAndGetDisease(this.juicer.ingredientTags[i], this.juicer.ingredientMassesPerUse[i], out num, out diseaseInfo2, out num2);
			if (smi != null)
			{
				smi.TryInjectDisease(diseaseInfo2.idx, diseaseInfo2.count, this.juicer.ingredientTags[i], Sickness.InfectionVector.Digestion);
			}
		}
		if (smi != null)
		{
			smi.TryInjectDisease(diseaseInfo.idx, diseaseInfo.count, GameTags.Water, Sickness.InfectionVector.Digestion);
		}
		Effects component2 = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.juicer.specificEffect))
		{
			component2.Add(this.juicer.specificEffect, true);
		}
		if (!string.IsNullOrEmpty(this.juicer.trackingEffect))
		{
			component2.Add(this.juicer.trackingEffect, true);
		}
	}

	// Token: 0x06003A63 RID: 14947 RVA: 0x00143515 File Offset: 0x00141715
	protected override void OnStopWork(Worker worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x06003A64 RID: 14948 RVA: 0x00143524 File Offset: 0x00141724
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.juicer.trackingEffect) && component.HasEffect(this.juicer.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.juicer.specificEffect) && component.HasEffect(this.juicer.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04002651 RID: 9809
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002652 RID: 9810
	public int basePriority;

	// Token: 0x04002653 RID: 9811
	private Juicer juicer;
}

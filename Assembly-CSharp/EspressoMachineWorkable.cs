using System;
using Klei;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200075A RID: 1882
[AddComponentMenu("KMonoBehaviour/Workable/EspressoMachineWorkable")]
public class EspressoMachineWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x060033DB RID: 13275 RVA: 0x0011707C File Offset: 0x0011527C
	private EspressoMachineWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x060033DC RID: 13276 RVA: 0x00117098 File Offset: 0x00115298
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_espresso_machine_kanim") };
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = false;
		base.SetWorkTime(30f);
	}

	// Token: 0x060033DD RID: 13277 RVA: 0x001170E9 File Offset: 0x001152E9
	protected override void OnStartWork(Worker worker)
	{
		this.operational.SetActive(true, false);
	}

	// Token: 0x060033DE RID: 13278 RVA: 0x001170F8 File Offset: 0x001152F8
	protected override void OnCompleteWork(Worker worker)
	{
		Storage component = base.GetComponent<Storage>();
		float num;
		SimUtil.DiseaseInfo diseaseInfo;
		float num2;
		component.ConsumeAndGetDisease(GameTags.Water, EspressoMachine.WATER_MASS_PER_USE, out num, out diseaseInfo, out num2);
		SimUtil.DiseaseInfo diseaseInfo2;
		component.ConsumeAndGetDisease(EspressoMachine.INGREDIENT_TAG, EspressoMachine.INGREDIENT_MASS_PER_USE, out num, out diseaseInfo2, out num2);
		GermExposureMonitor.Instance smi = worker.GetSMI<GermExposureMonitor.Instance>();
		if (smi != null)
		{
			smi.TryInjectDisease(diseaseInfo.idx, diseaseInfo.count, GameTags.Water, Sickness.InfectionVector.Digestion);
			smi.TryInjectDisease(diseaseInfo2.idx, diseaseInfo2.count, EspressoMachine.INGREDIENT_TAG, Sickness.InfectionVector.Digestion);
		}
		Effects component2 = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(EspressoMachineWorkable.specificEffect))
		{
			component2.Add(EspressoMachineWorkable.specificEffect, true);
		}
		if (!string.IsNullOrEmpty(EspressoMachineWorkable.trackingEffect))
		{
			component2.Add(EspressoMachineWorkable.trackingEffect, true);
		}
	}

	// Token: 0x060033DF RID: 13279 RVA: 0x001171B0 File Offset: 0x001153B0
	protected override void OnStopWork(Worker worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x060033E0 RID: 13280 RVA: 0x001171C0 File Offset: 0x001153C0
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(EspressoMachineWorkable.trackingEffect) && component.HasEffect(EspressoMachineWorkable.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(EspressoMachineWorkable.specificEffect) && component.HasEffect(EspressoMachineWorkable.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04001FD3 RID: 8147
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001FD4 RID: 8148
	public int basePriority = RELAXATION.PRIORITY.TIER5;

	// Token: 0x04001FD5 RID: 8149
	private static string specificEffect = "Espresso";

	// Token: 0x04001FD6 RID: 8150
	private static string trackingEffect = "RecentlyRecDrink";
}

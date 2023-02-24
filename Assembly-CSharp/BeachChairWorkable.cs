using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200055C RID: 1372
[AddComponentMenu("KMonoBehaviour/Workable/BeachChairWorkable")]
public class BeachChairWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06002105 RID: 8453 RVA: 0x000B3E60 File Offset: 0x000B2060
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_beach_chair_kanim") };
		this.workAnims = null;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = false;
		this.lightEfficiencyBonus = false;
		base.SetWorkTime(150f);
		this.beachChair = base.GetComponent<BeachChair>();
	}

	// Token: 0x06002106 RID: 8454 RVA: 0x000B3EE1 File Offset: 0x000B20E1
	protected override void OnStartWork(Worker worker)
	{
		this.timeLit = 0f;
		this.beachChair.SetWorker(worker);
		this.operational.SetActive(true, false);
		worker.GetComponent<Effects>().Add("BeachChairRelaxing", false);
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x000B3F1C File Offset: 0x000B211C
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		int num = Grid.PosToCell(base.gameObject);
		bool flag = (float)Grid.LightIntensity[num] >= 9999f;
		this.beachChair.SetLit(flag);
		if (flag)
		{
			base.GetComponent<LoopingSounds>().SetParameter(this.soundPath, this.BEACH_CHAIR_LIT_PARAMETER, 1f);
			this.timeLit += dt;
		}
		else
		{
			base.GetComponent<LoopingSounds>().SetParameter(this.soundPath, this.BEACH_CHAIR_LIT_PARAMETER, 0f);
		}
		return false;
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x000B3FA4 File Offset: 0x000B21A4
	protected override void OnCompleteWork(Worker worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (this.timeLit / this.workTime >= 0.75f)
		{
			component.Add(this.beachChair.specificEffectLit, true);
			component.Remove(this.beachChair.specificEffectUnlit);
		}
		else
		{
			component.Add(this.beachChair.specificEffectUnlit, true);
			component.Remove(this.beachChair.specificEffectLit);
		}
		component.Add(this.beachChair.trackingEffect, true);
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x000B4029 File Offset: 0x000B2229
	protected override void OnStopWork(Worker worker)
	{
		this.operational.SetActive(false, false);
		worker.GetComponent<Effects>().Remove("BeachChairRelaxing");
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x000B4048 File Offset: 0x000B2248
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (component.HasEffect(this.beachChair.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (component.HasEffect(this.beachChair.specificEffectLit) || component.HasEffect(this.beachChair.specificEffectUnlit))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x040012FC RID: 4860
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040012FD RID: 4861
	private float timeLit;

	// Token: 0x040012FE RID: 4862
	public string soundPath = GlobalAssets.GetSound("BeachChair_music_lp", false);

	// Token: 0x040012FF RID: 4863
	public HashedString BEACH_CHAIR_LIT_PARAMETER = "beachChair_lit";

	// Token: 0x04001300 RID: 4864
	public int basePriority;

	// Token: 0x04001301 RID: 4865
	private BeachChair beachChair;
}

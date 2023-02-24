using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200054A RID: 1354
[AddComponentMenu("KMonoBehaviour/Workable/ArcadeMachineWorkable")]
public class ArcadeMachineWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06002048 RID: 8264 RVA: 0x000B085A File Offset: 0x000AEA5A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.synchronizeAnims = false;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		base.SetWorkTime(15f);
	}

	// Token: 0x06002049 RID: 8265 RVA: 0x000B088A File Offset: 0x000AEA8A
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		worker.GetComponent<Effects>().Add("ArcadePlaying", false);
	}

	// Token: 0x0600204A RID: 8266 RVA: 0x000B08A5 File Offset: 0x000AEAA5
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		worker.GetComponent<Effects>().Remove("ArcadePlaying");
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x000B08C0 File Offset: 0x000AEAC0
	protected override void OnCompleteWork(Worker worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(ArcadeMachineWorkable.trackingEffect))
		{
			component.Add(ArcadeMachineWorkable.trackingEffect, true);
		}
		if (!string.IsNullOrEmpty(ArcadeMachineWorkable.specificEffect))
		{
			component.Add(ArcadeMachineWorkable.specificEffect, true);
		}
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x000B0908 File Offset: 0x000AEB08
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(ArcadeMachineWorkable.trackingEffect) && component.HasEffect(ArcadeMachineWorkable.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(ArcadeMachineWorkable.specificEffect) && component.HasEffect(ArcadeMachineWorkable.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04001293 RID: 4755
	public ArcadeMachine owner;

	// Token: 0x04001294 RID: 4756
	public int basePriority = RELAXATION.PRIORITY.TIER3;

	// Token: 0x04001295 RID: 4757
	private static string specificEffect = "PlayedArcade";

	// Token: 0x04001296 RID: 4758
	private static string trackingEffect = "RecentlyPlayedArcade";
}

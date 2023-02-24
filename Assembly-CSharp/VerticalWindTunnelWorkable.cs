using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020009C8 RID: 2504
[AddComponentMenu("KMonoBehaviour/Workable/VerticalWindTunnelWorkable")]
public class VerticalWindTunnelWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06004A7E RID: 19070 RVA: 0x001A159F File Offset: 0x0019F79F
	private VerticalWindTunnelWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06004A7F RID: 19071 RVA: 0x001A15B0 File Offset: 0x0019F7B0
	public override Workable.AnimInfo GetAnim(Worker worker)
	{
		Workable.AnimInfo anim = base.GetAnim(worker);
		anim.smi = new WindTunnelWorkerStateMachine.StatesInstance(worker, this);
		return anim;
	}

	// Token: 0x06004A80 RID: 19072 RVA: 0x001A15D4 File Offset: 0x0019F7D4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = false;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		base.SetWorkTime(90f);
	}

	// Token: 0x06004A81 RID: 19073 RVA: 0x001A15FC File Offset: 0x0019F7FC
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		worker.GetComponent<Effects>().Add("VerticalWindTunnelFlying", false);
	}

	// Token: 0x06004A82 RID: 19074 RVA: 0x001A1617 File Offset: 0x0019F817
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		worker.GetComponent<Effects>().Remove("VerticalWindTunnelFlying");
	}

	// Token: 0x06004A83 RID: 19075 RVA: 0x001A1630 File Offset: 0x0019F830
	protected override void OnCompleteWork(Worker worker)
	{
		Effects component = worker.GetComponent<Effects>();
		component.Add(this.windTunnel.trackingEffect, true);
		component.Add(this.windTunnel.specificEffect, true);
	}

	// Token: 0x06004A84 RID: 19076 RVA: 0x001A1660 File Offset: 0x0019F860
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.windTunnel.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (component.HasEffect(this.windTunnel.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (component.HasEffect(this.windTunnel.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x040030E4 RID: 12516
	public VerticalWindTunnel windTunnel;

	// Token: 0x040030E5 RID: 12517
	public HashedString overrideAnim;

	// Token: 0x040030E6 RID: 12518
	public string[] preAnims;

	// Token: 0x040030E7 RID: 12519
	public string loopAnim;

	// Token: 0x040030E8 RID: 12520
	public string[] pstAnims;
}

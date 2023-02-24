using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020007AE RID: 1966
[AddComponentMenu("KMonoBehaviour/Workable/HotTubWorkable")]
public class HotTubWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x060037B4 RID: 14260 RVA: 0x0013570D File Offset: 0x0013390D
	private HotTubWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x060037B5 RID: 14261 RVA: 0x0013571D File Offset: 0x0013391D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = false;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.faceTargetWhenWorking = true;
		base.SetWorkTime(90f);
	}

	// Token: 0x060037B6 RID: 14262 RVA: 0x0013574C File Offset: 0x0013394C
	public override Workable.AnimInfo GetAnim(Worker worker)
	{
		Workable.AnimInfo anim = base.GetAnim(worker);
		anim.smi = new HotTubWorkerStateMachine.StatesInstance(worker);
		return anim;
	}

	// Token: 0x060037B7 RID: 14263 RVA: 0x0013576F File Offset: 0x0013396F
	protected override void OnStartWork(Worker worker)
	{
		this.faceLeft = UnityEngine.Random.value > 0.5f;
		worker.GetComponent<Effects>().Add("HotTubRelaxing", false);
	}

	// Token: 0x060037B8 RID: 14264 RVA: 0x00135799 File Offset: 0x00133999
	protected override void OnStopWork(Worker worker)
	{
		worker.GetComponent<Effects>().Remove("HotTubRelaxing");
	}

	// Token: 0x060037B9 RID: 14265 RVA: 0x001357AB File Offset: 0x001339AB
	public override Vector3 GetFacingTarget()
	{
		return base.transform.GetPosition() + (this.faceLeft ? Vector3.left : Vector3.right);
	}

	// Token: 0x060037BA RID: 14266 RVA: 0x001357D4 File Offset: 0x001339D4
	protected override void OnCompleteWork(Worker worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.hotTub.trackingEffect))
		{
			component.Add(this.hotTub.trackingEffect, true);
		}
		if (!string.IsNullOrEmpty(this.hotTub.specificEffect))
		{
			component.Add(this.hotTub.specificEffect, true);
		}
	}

	// Token: 0x060037BB RID: 14267 RVA: 0x00135834 File Offset: 0x00133A34
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.hotTub.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.hotTub.trackingEffect) && component.HasEffect(this.hotTub.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.hotTub.specificEffect) && component.HasEffect(this.hotTub.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04002557 RID: 9559
	public HotTub hotTub;

	// Token: 0x04002558 RID: 9560
	private bool faceLeft;
}

using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200088A RID: 2186
[AddComponentMenu("KMonoBehaviour/Workable/PhonoboxWorkable")]
public class PhonoboxWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06003EB2 RID: 16050 RVA: 0x0015E830 File Offset: 0x0015CA30
	private PhonoboxWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06003EB3 RID: 16051 RVA: 0x0015E8C9 File Offset: 0x0015CAC9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = false;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		base.SetWorkTime(15f);
	}

	// Token: 0x06003EB4 RID: 16052 RVA: 0x0015E8F4 File Offset: 0x0015CAF4
	protected override void OnCompleteWork(Worker worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.trackingEffect))
		{
			component.Add(this.trackingEffect, true);
		}
		if (!string.IsNullOrEmpty(this.specificEffect))
		{
			component.Add(this.specificEffect, true);
		}
	}

	// Token: 0x06003EB5 RID: 16053 RVA: 0x0015E940 File Offset: 0x0015CB40
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.trackingEffect) && component.HasEffect(this.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.specificEffect) && component.HasEffect(this.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x06003EB6 RID: 16054 RVA: 0x0015E99F File Offset: 0x0015CB9F
	protected override void OnStartWork(Worker worker)
	{
		this.owner.AddWorker(worker);
		worker.GetComponent<Effects>().Add("Dancing", false);
	}

	// Token: 0x06003EB7 RID: 16055 RVA: 0x0015E9BF File Offset: 0x0015CBBF
	protected override void OnStopWork(Worker worker)
	{
		this.owner.RemoveWorker(worker);
		worker.GetComponent<Effects>().Remove("Dancing");
	}

	// Token: 0x06003EB8 RID: 16056 RVA: 0x0015E9E0 File Offset: 0x0015CBE0
	public override Workable.AnimInfo GetAnim(Worker worker)
	{
		int num = UnityEngine.Random.Range(0, this.workerOverrideAnims.Length);
		this.overrideAnims = this.workerOverrideAnims[num];
		return base.GetAnim(worker);
	}

	// Token: 0x04002906 RID: 10502
	public Phonobox owner;

	// Token: 0x04002907 RID: 10503
	public int basePriority = RELAXATION.PRIORITY.TIER3;

	// Token: 0x04002908 RID: 10504
	public string specificEffect = "Danced";

	// Token: 0x04002909 RID: 10505
	public string trackingEffect = "RecentlyDanced";

	// Token: 0x0400290A RID: 10506
	public KAnimFile[][] workerOverrideAnims = new KAnimFile[][]
	{
		new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_danceone_kanim") },
		new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_dancetwo_kanim") },
		new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_dancethree_kanim") }
	};
}

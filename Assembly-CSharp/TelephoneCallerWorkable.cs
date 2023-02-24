using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020009A6 RID: 2470
[AddComponentMenu("KMonoBehaviour/Workable/TelephoneWorkable")]
public class TelephoneCallerWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x0600494E RID: 18766 RVA: 0x0019A590 File Offset: 0x00198790
	private TelephoneCallerWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.workingPstComplete = new HashedString[] { "on_pst" };
		this.workAnims = new HashedString[] { "on_pre", "on", "on_receiving", "on_pre_loop_receiving", "on_loop", "on_loop_pre" };
	}

	// Token: 0x0600494F RID: 18767 RVA: 0x0019A63C File Offset: 0x0019883C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_telephone_kanim") };
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = true;
		base.SetWorkTime(40f);
		this.telephone = base.GetComponent<Telephone>();
	}

	// Token: 0x06004950 RID: 18768 RVA: 0x0019A699 File Offset: 0x00198899
	protected override void OnStartWork(Worker worker)
	{
		this.operational.SetActive(true, false);
		this.telephone.isInUse = true;
	}

	// Token: 0x06004951 RID: 18769 RVA: 0x0019A6B4 File Offset: 0x001988B4
	protected override void OnCompleteWork(Worker worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (this.telephone.HasTag(GameTags.LongDistanceCall))
		{
			if (!string.IsNullOrEmpty(this.telephone.longDistanceEffect))
			{
				component.Add(this.telephone.longDistanceEffect, true);
			}
		}
		else if (this.telephone.wasAnswered)
		{
			if (!string.IsNullOrEmpty(this.telephone.chatEffect))
			{
				component.Add(this.telephone.chatEffect, true);
			}
		}
		else if (!string.IsNullOrEmpty(this.telephone.babbleEffect))
		{
			component.Add(this.telephone.babbleEffect, true);
		}
		if (!string.IsNullOrEmpty(this.telephone.trackingEffect))
		{
			component.Add(this.telephone.trackingEffect, true);
		}
	}

	// Token: 0x06004952 RID: 18770 RVA: 0x0019A77F File Offset: 0x0019897F
	protected override void OnStopWork(Worker worker)
	{
		this.operational.SetActive(false, false);
		this.telephone.HangUp();
	}

	// Token: 0x06004953 RID: 18771 RVA: 0x0019A79C File Offset: 0x0019899C
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.telephone.trackingEffect) && component.HasEffect(this.telephone.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.telephone.chatEffect) && component.HasEffect(this.telephone.chatEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		if (!string.IsNullOrEmpty(this.telephone.babbleEffect) && component.HasEffect(this.telephone.babbleEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04003021 RID: 12321
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04003022 RID: 12322
	public int basePriority;

	// Token: 0x04003023 RID: 12323
	private Telephone telephone;
}

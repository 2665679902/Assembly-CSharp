using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000636 RID: 1590
[AddComponentMenu("KMonoBehaviour/Workable/ResetSkillsStation")]
public class ResetSkillsStation : Workable
{
	// Token: 0x06002A0F RID: 10767 RVA: 0x000DE6E7 File Offset: 0x000DC8E7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.lightEfficiencyBonus = false;
	}

	// Token: 0x06002A10 RID: 10768 RVA: 0x000DE6F6 File Offset: 0x000DC8F6
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.OnAssign(this.assignable.assignee);
		this.assignable.OnAssign += this.OnAssign;
	}

	// Token: 0x06002A11 RID: 10769 RVA: 0x000DE726 File Offset: 0x000DC926
	private void OnAssign(IAssignableIdentity obj)
	{
		if (obj != null)
		{
			this.CreateChore();
			return;
		}
		if (this.chore != null)
		{
			this.chore.Cancel("Unassigned");
			this.chore = null;
		}
	}

	// Token: 0x06002A12 RID: 10770 RVA: 0x000DE754 File Offset: 0x000DC954
	private void CreateChore()
	{
		this.chore = new WorkChore<ResetSkillsStation>(Db.Get().ChoreTypes.UnlearnSkill, this, null, true, null, null, null, false, null, true, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
	}

	// Token: 0x06002A13 RID: 10771 RVA: 0x000DE78D File Offset: 0x000DC98D
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<Operational>().SetActive(true, false);
	}

	// Token: 0x06002A14 RID: 10772 RVA: 0x000DE7A4 File Offset: 0x000DC9A4
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		this.assignable.Unassign();
		MinionResume component = worker.GetComponent<MinionResume>();
		if (component != null)
		{
			component.ResetSkillLevels(true);
			component.SetHats(component.CurrentHat, null);
			component.ApplyTargetHat();
			this.notification = new Notification(MISC.NOTIFICATIONS.RESETSKILL.NAME, NotificationType.Good, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.RESETSKILL.TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);
			worker.GetComponent<Notifier>().Add(this.notification, "");
		}
	}

	// Token: 0x06002A15 RID: 10773 RVA: 0x000DE845 File Offset: 0x000DCA45
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		base.GetComponent<Operational>().SetActive(false, false);
		this.chore = null;
	}

	// Token: 0x040018DE RID: 6366
	[MyCmpReq]
	public Assignable assignable;

	// Token: 0x040018DF RID: 6367
	private Notification notification;

	// Token: 0x040018E0 RID: 6368
	private Chore chore;
}

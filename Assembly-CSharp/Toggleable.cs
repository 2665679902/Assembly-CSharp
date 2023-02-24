using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004DF RID: 1247
[AddComponentMenu("KMonoBehaviour/Workable/Toggleable")]
public class Toggleable : Workable
{
	// Token: 0x06001D88 RID: 7560 RVA: 0x0009D7AF File Offset: 0x0009B9AF
	private Toggleable()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x06001D89 RID: 7561 RVA: 0x0009D7C4 File Offset: 0x0009B9C4
	protected override void OnPrefabInit()
	{
		this.faceTargetWhenWorking = true;
		base.OnPrefabInit();
		this.targets = new List<KeyValuePair<IToggleHandler, Chore>>();
		base.SetWorkTime(3f);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Toggling;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_remote_kanim") };
		this.synchronizeAnims = false;
	}

	// Token: 0x06001D8A RID: 7562 RVA: 0x0009D82E File Offset: 0x0009BA2E
	public int SetTarget(IToggleHandler handler)
	{
		this.targets.Add(new KeyValuePair<IToggleHandler, Chore>(handler, null));
		return this.targets.Count - 1;
	}

	// Token: 0x06001D8B RID: 7563 RVA: 0x0009D850 File Offset: 0x0009BA50
	public IToggleHandler GetToggleHandlerForWorker(Worker worker)
	{
		int targetForWorker = this.GetTargetForWorker(worker);
		if (targetForWorker != -1)
		{
			return this.targets[targetForWorker].Key;
		}
		return null;
	}

	// Token: 0x06001D8C RID: 7564 RVA: 0x0009D880 File Offset: 0x0009BA80
	private int GetTargetForWorker(Worker worker)
	{
		for (int i = 0; i < this.targets.Count; i++)
		{
			if (this.targets[i].Value != null && this.targets[i].Value.driver != null && this.targets[i].Value.driver.gameObject == worker.gameObject)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06001D8D RID: 7565 RVA: 0x0009D908 File Offset: 0x0009BB08
	protected override void OnCompleteWork(Worker worker)
	{
		int targetForWorker = this.GetTargetForWorker(worker);
		if (targetForWorker != -1 && this.targets[targetForWorker].Key != null)
		{
			this.targets[targetForWorker] = new KeyValuePair<IToggleHandler, Chore>(this.targets[targetForWorker].Key, null);
			this.targets[targetForWorker].Key.HandleToggle();
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingSwitchToggle, false);
	}

	// Token: 0x06001D8E RID: 7566 RVA: 0x0009D994 File Offset: 0x0009BB94
	private void QueueToggle(int targetIdx)
	{
		if (this.targets[targetIdx].Value == null)
		{
			if (DebugHandler.InstantBuildMode)
			{
				this.targets[targetIdx].Key.HandleToggle();
				return;
			}
			this.targets[targetIdx] = new KeyValuePair<IToggleHandler, Chore>(this.targets[targetIdx].Key, new WorkChore<Toggleable>(Db.Get().ChoreTypes.Toggle, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true));
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.PendingSwitchToggle, null);
		}
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x0009DA44 File Offset: 0x0009BC44
	public void Toggle(int targetIdx)
	{
		if (targetIdx >= this.targets.Count)
		{
			return;
		}
		if (this.targets[targetIdx].Value == null)
		{
			this.QueueToggle(targetIdx);
			return;
		}
		this.CancelToggle(targetIdx);
	}

	// Token: 0x06001D90 RID: 7568 RVA: 0x0009DA88 File Offset: 0x0009BC88
	private void CancelToggle(int targetIdx)
	{
		if (this.targets[targetIdx].Value != null)
		{
			this.targets[targetIdx].Value.Cancel("Toggle cancelled");
			this.targets[targetIdx] = new KeyValuePair<IToggleHandler, Chore>(this.targets[targetIdx].Key, null);
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingSwitchToggle, false);
		}
	}

	// Token: 0x06001D91 RID: 7569 RVA: 0x0009DB0C File Offset: 0x0009BD0C
	public bool IsToggleQueued(int targetIdx)
	{
		return this.targets[targetIdx].Value != null;
	}

	// Token: 0x040010AC RID: 4268
	private List<KeyValuePair<IToggleHandler, Chore>> targets;
}

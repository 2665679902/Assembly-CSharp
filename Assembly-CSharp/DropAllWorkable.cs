using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000728 RID: 1832
[AddComponentMenu("KMonoBehaviour/Workable/DropAllWorkable")]
public class DropAllWorkable : Workable
{
	// Token: 0x17000399 RID: 921
	// (get) Token: 0x06003223 RID: 12835 RVA: 0x0010C395 File Offset: 0x0010A595
	// (set) Token: 0x06003224 RID: 12836 RVA: 0x0010C39D File Offset: 0x0010A59D
	private Chore Chore
	{
		get
		{
			return this._chore;
		}
		set
		{
			this._chore = value;
			this.markedForDrop = this._chore != null;
		}
	}

	// Token: 0x06003225 RID: 12837 RVA: 0x0010C3B5 File Offset: 0x0010A5B5
	protected DropAllWorkable()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x06003226 RID: 12838 RVA: 0x0010C3D4 File Offset: 0x0010A5D4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<DropAllWorkable>(493375141, DropAllWorkable.OnRefreshUserMenuDelegate);
		base.Subscribe<DropAllWorkable>(-1697596308, DropAllWorkable.OnStorageChangeDelegate);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Emptying;
		this.synchronizeAnims = false;
		base.SetWorkTime(this.dropWorkTime);
		Prioritizable.AddRef(base.gameObject);
	}

	// Token: 0x06003227 RID: 12839 RVA: 0x0010C43C File Offset: 0x0010A63C
	private Storage[] GetStorages()
	{
		if (this.storages == null)
		{
			this.storages = base.GetComponents<Storage>();
		}
		return this.storages;
	}

	// Token: 0x06003228 RID: 12840 RVA: 0x0010C458 File Offset: 0x0010A658
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.showCmd = this.GetNewShowCmd();
		if (this.markedForDrop)
		{
			this.DropAll();
		}
	}

	// Token: 0x06003229 RID: 12841 RVA: 0x0010C47C File Offset: 0x0010A67C
	public void DropAll()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.OnCompleteWork(null);
		}
		else if (this.Chore == null)
		{
			ChoreType choreType = ((!string.IsNullOrEmpty(this.choreTypeID)) ? Db.Get().ChoreTypes.Get(this.choreTypeID) : Db.Get().ChoreTypes.EmptyStorage);
			this.Chore = new WorkChore<DropAllWorkable>(choreType, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}
		else
		{
			this.Chore.Cancel("Cancelled emptying");
			this.Chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(this.workerStatusItem, false);
			base.ShowProgressBar(false);
		}
		this.RefreshStatusItem();
	}

	// Token: 0x0600322A RID: 12842 RVA: 0x0010C530 File Offset: 0x0010A730
	protected override void OnCompleteWork(Worker worker)
	{
		Storage[] array = this.GetStorages();
		for (int i = 0; i < array.Length; i++)
		{
			List<GameObject> list = new List<GameObject>(array[i].items);
			for (int j = 0; j < list.Count; j++)
			{
				GameObject gameObject = array[i].Drop(list[j], true);
				if (gameObject != null)
				{
					foreach (Tag tag in this.removeTags)
					{
						gameObject.RemoveTag(tag);
					}
					Pickupable component = gameObject.GetComponent<Pickupable>();
					if (component != null)
					{
						component.TryToOffsetIfBuried();
					}
				}
			}
		}
		this.Chore = null;
		this.RefreshStatusItem();
		base.Trigger(-1957399615, null);
	}

	// Token: 0x0600322B RID: 12843 RVA: 0x0010C610 File Offset: 0x0010A810
	private void OnRefreshUserMenu(object data)
	{
		if (this.showCmd)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = ((this.Chore == null) ? new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.EMPTYSTORAGE.NAME, new System.Action(this.DropAll), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.EMPTYSTORAGE.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.EMPTYSTORAGE.NAME_OFF, new System.Action(this.DropAll), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.EMPTYSTORAGE.TOOLTIP_OFF, true));
			Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
		}
	}

	// Token: 0x0600322C RID: 12844 RVA: 0x0010C6B4 File Offset: 0x0010A8B4
	private bool GetNewShowCmd()
	{
		bool flag = false;
		Storage[] array = this.GetStorages();
		for (int i = 0; i < array.Length; i++)
		{
			flag = flag || !array[i].IsEmpty();
		}
		return flag;
	}

	// Token: 0x0600322D RID: 12845 RVA: 0x0010C6EC File Offset: 0x0010A8EC
	private void OnStorageChange(object data)
	{
		bool newShowCmd = this.GetNewShowCmd();
		if (newShowCmd != this.showCmd)
		{
			this.showCmd = newShowCmd;
			Game.Instance.userMenu.Refresh(base.gameObject);
		}
	}

	// Token: 0x0600322E RID: 12846 RVA: 0x0010C728 File Offset: 0x0010A928
	private void RefreshStatusItem()
	{
		if (this.Chore != null && this.statusItem == Guid.Empty)
		{
			KSelectable component = base.GetComponent<KSelectable>();
			this.statusItem = component.AddStatusItem(Db.Get().BuildingStatusItems.AwaitingEmptyBuilding, null);
			return;
		}
		if (this.Chore == null && this.statusItem != Guid.Empty)
		{
			KSelectable component2 = base.GetComponent<KSelectable>();
			this.statusItem = component2.RemoveStatusItem(this.statusItem, false);
		}
	}

	// Token: 0x04001E76 RID: 7798
	[Serialize]
	private bool markedForDrop;

	// Token: 0x04001E77 RID: 7799
	private Chore _chore;

	// Token: 0x04001E78 RID: 7800
	private bool showCmd;

	// Token: 0x04001E79 RID: 7801
	private Storage[] storages;

	// Token: 0x04001E7A RID: 7802
	public float dropWorkTime = 0.1f;

	// Token: 0x04001E7B RID: 7803
	public string choreTypeID;

	// Token: 0x04001E7C RID: 7804
	[MyCmpAdd]
	private Prioritizable _prioritizable;

	// Token: 0x04001E7D RID: 7805
	public List<Tag> removeTags;

	// Token: 0x04001E7E RID: 7806
	private static readonly EventSystem.IntraObjectHandler<DropAllWorkable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<DropAllWorkable>(delegate(DropAllWorkable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04001E7F RID: 7807
	private static readonly EventSystem.IntraObjectHandler<DropAllWorkable> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<DropAllWorkable>(delegate(DropAllWorkable component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04001E80 RID: 7808
	private Guid statusItem;
}

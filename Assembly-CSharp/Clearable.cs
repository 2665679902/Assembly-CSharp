using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200045A RID: 1114
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Clearable")]
public class Clearable : Workable, ISaveLoadable, IRender1000ms
{
	// Token: 0x06001875 RID: 6261 RVA: 0x00081994 File Offset: 0x0007FB94
	protected override void OnPrefabInit()
	{
		base.Subscribe<Clearable>(2127324410, Clearable.OnCancelDelegate);
		base.Subscribe<Clearable>(856640610, Clearable.OnStoreDelegate);
		base.Subscribe<Clearable>(-2064133523, Clearable.OnAbsorbDelegate);
		base.Subscribe<Clearable>(493375141, Clearable.OnRefreshUserMenuDelegate);
		base.Subscribe<Clearable>(-1617557748, Clearable.OnEquippedDelegate);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Clearing;
		this.simRenderLoadBalance = true;
		this.autoRegisterSimRender = false;
	}

	// Token: 0x06001876 RID: 6262 RVA: 0x00081A1C File Offset: 0x0007FC1C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.isMarkedForClear)
		{
			if (this.HasTag(GameTags.Stored))
			{
				if (!base.transform.parent.GetComponent<Storage>().allowClearable)
				{
					this.isMarkedForClear = false;
				}
				else
				{
					this.MarkForClear(true, true);
				}
			}
			else
			{
				this.MarkForClear(true, false);
			}
		}
		this.RefreshClearableStatus(true);
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x00081A7D File Offset: 0x0007FC7D
	private void OnStore(object data)
	{
		this.CancelClearing();
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x00081A88 File Offset: 0x0007FC88
	private void OnCancel(object data)
	{
		for (ObjectLayerListItem objectLayerListItem = this.pickupable.objectLayerListItem; objectLayerListItem != null; objectLayerListItem = objectLayerListItem.nextItem)
		{
			if (objectLayerListItem.gameObject != null)
			{
				objectLayerListItem.gameObject.GetComponent<Clearable>().CancelClearing();
			}
		}
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x00081ACC File Offset: 0x0007FCCC
	public void CancelClearing()
	{
		if (this.isMarkedForClear)
		{
			this.isMarkedForClear = false;
			base.GetComponent<KPrefabID>().RemoveTag(GameTags.Garbage);
			Prioritizable.RemoveRef(base.gameObject);
			if (this.clearHandle.IsValid())
			{
				GlobalChoreProvider.Instance.UnregisterClearable(this.clearHandle);
				this.clearHandle.Clear();
			}
			this.RefreshClearableStatus(true);
			SimAndRenderScheduler.instance.Remove(this);
		}
	}

	// Token: 0x0600187A RID: 6266 RVA: 0x00081B40 File Offset: 0x0007FD40
	public void MarkForClear(bool restoringFromSave = false, bool allowWhenStored = false)
	{
		if (!this.isClearable)
		{
			return;
		}
		if ((!this.isMarkedForClear || restoringFromSave) && !this.pickupable.IsEntombed && !this.clearHandle.IsValid() && (!this.HasTag(GameTags.Stored) || allowWhenStored))
		{
			Prioritizable.AddRef(base.gameObject);
			base.GetComponent<KPrefabID>().AddTag(GameTags.Garbage, false);
			this.isMarkedForClear = true;
			this.clearHandle = GlobalChoreProvider.Instance.RegisterClearable(this);
			this.RefreshClearableStatus(true);
			SimAndRenderScheduler.instance.Add(this, this.simRenderLoadBalance);
		}
	}

	// Token: 0x0600187B RID: 6267 RVA: 0x00081BDB File Offset: 0x0007FDDB
	private void OnClickClear()
	{
		this.MarkForClear(false, false);
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x00081BE5 File Offset: 0x0007FDE5
	private void OnClickCancel()
	{
		this.CancelClearing();
	}

	// Token: 0x0600187D RID: 6269 RVA: 0x00081BED File Offset: 0x0007FDED
	private void OnEquipped(object data)
	{
		this.CancelClearing();
	}

	// Token: 0x0600187E RID: 6270 RVA: 0x00081BF5 File Offset: 0x0007FDF5
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.clearHandle.IsValid())
		{
			GlobalChoreProvider.Instance.UnregisterClearable(this.clearHandle);
			this.clearHandle.Clear();
		}
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x00081C28 File Offset: 0x0007FE28
	private void OnRefreshUserMenu(object data)
	{
		if (!this.isClearable || base.GetComponent<Health>() != null || this.HasTag(GameTags.Stored))
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = (this.isMarkedForClear ? new KIconButtonMenu.ButtonInfo("action_move_to_storage", UI.USERMENUACTIONS.CLEAR.NAME_OFF, new System.Action(this.OnClickCancel), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CLEAR.TOOLTIP_OFF, true) : new KIconButtonMenu.ButtonInfo("action_move_to_storage", UI.USERMENUACTIONS.CLEAR.NAME, new System.Action(this.OnClickClear), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CLEAR.TOOLTIP, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x00081CE8 File Offset: 0x0007FEE8
	private void OnAbsorb(object data)
	{
		Pickupable pickupable = (Pickupable)data;
		if (pickupable != null)
		{
			Clearable component = pickupable.GetComponent<Clearable>();
			if (component != null && component.isMarkedForClear)
			{
				this.MarkForClear(false, false);
			}
		}
	}

	// Token: 0x06001881 RID: 6273 RVA: 0x00081D25 File Offset: 0x0007FF25
	public void Render1000ms(float dt)
	{
		this.RefreshClearableStatus(false);
	}

	// Token: 0x06001882 RID: 6274 RVA: 0x00081D30 File Offset: 0x0007FF30
	public void RefreshClearableStatus(bool force_update)
	{
		if (force_update || this.isMarkedForClear)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.isMarkedForClear)
			{
				flag2 = !(flag = GlobalChoreProvider.Instance.ClearableHasDestination(this.pickupable));
			}
			this.pendingClearGuid = this.selectable.ToggleStatusItem(Db.Get().MiscStatusItems.PendingClear, this.pendingClearGuid, flag, this);
			this.pendingClearNoStorageGuid = this.selectable.ToggleStatusItem(Db.Get().MiscStatusItems.PendingClearNoStorage, this.pendingClearNoStorageGuid, flag2, this);
		}
	}

	// Token: 0x04000DA6 RID: 3494
	[MyCmpReq]
	private Pickupable pickupable;

	// Token: 0x04000DA7 RID: 3495
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04000DA8 RID: 3496
	[Serialize]
	private bool isMarkedForClear;

	// Token: 0x04000DA9 RID: 3497
	private HandleVector<int>.Handle clearHandle;

	// Token: 0x04000DAA RID: 3498
	public bool isClearable = true;

	// Token: 0x04000DAB RID: 3499
	private Guid pendingClearGuid;

	// Token: 0x04000DAC RID: 3500
	private Guid pendingClearNoStorageGuid;

	// Token: 0x04000DAD RID: 3501
	private static readonly EventSystem.IntraObjectHandler<Clearable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Clearable>(delegate(Clearable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x04000DAE RID: 3502
	private static readonly EventSystem.IntraObjectHandler<Clearable> OnStoreDelegate = new EventSystem.IntraObjectHandler<Clearable>(delegate(Clearable component, object data)
	{
		component.OnStore(data);
	});

	// Token: 0x04000DAF RID: 3503
	private static readonly EventSystem.IntraObjectHandler<Clearable> OnAbsorbDelegate = new EventSystem.IntraObjectHandler<Clearable>(delegate(Clearable component, object data)
	{
		component.OnAbsorb(data);
	});

	// Token: 0x04000DB0 RID: 3504
	private static readonly EventSystem.IntraObjectHandler<Clearable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Clearable>(delegate(Clearable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04000DB1 RID: 3505
	private static readonly EventSystem.IntraObjectHandler<Clearable> OnEquippedDelegate = new EventSystem.IntraObjectHandler<Clearable>(delegate(Clearable component, object data)
	{
		component.OnEquipped(data);
	});
}

using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200045E RID: 1118
[AddComponentMenu("KMonoBehaviour/scripts/Compostable")]
public class Compostable : KMonoBehaviour
{
	// Token: 0x060018C0 RID: 6336 RVA: 0x000835A0 File Offset: 0x000817A0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.isMarkedForCompost = base.GetComponent<KPrefabID>().HasTag(GameTags.Compostable);
		if (this.isMarkedForCompost)
		{
			this.MarkForCompost(false);
		}
		base.Subscribe<Compostable>(493375141, Compostable.OnRefreshUserMenuDelegate);
		base.Subscribe<Compostable>(856640610, Compostable.OnStoreDelegate);
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x000835FC File Offset: 0x000817FC
	private void MarkForCompost(bool force = false)
	{
		this.RefreshStatusItem();
		Storage storage = base.GetComponent<Pickupable>().storage;
		if (storage != null)
		{
			storage.Drop(base.gameObject, true);
		}
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x00083634 File Offset: 0x00081834
	private void OnToggleCompost()
	{
		if (!this.isMarkedForCompost)
		{
			Pickupable component = base.GetComponent<Pickupable>();
			if (component.storage != null)
			{
				component.storage.Drop(base.gameObject, true);
			}
			Pickupable pickupable = EntitySplitter.Split(component, component.TotalAmount, this.compostPrefab);
			if (pickupable != null)
			{
				SelectTool.Instance.SelectNextFrame(pickupable.GetComponent<KSelectable>(), true);
				return;
			}
		}
		else
		{
			Pickupable component2 = base.GetComponent<Pickupable>();
			Pickupable pickupable2 = EntitySplitter.Split(component2, component2.TotalAmount, this.originalPrefab);
			SelectTool.Instance.SelectNextFrame(pickupable2.GetComponent<KSelectable>(), true);
		}
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x000836C8 File Offset: 0x000818C8
	private void RefreshStatusItem()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		component.RemoveStatusItem(Db.Get().MiscStatusItems.MarkedForCompost, false);
		component.RemoveStatusItem(Db.Get().MiscStatusItems.MarkedForCompostInStorage, false);
		if (this.isMarkedForCompost)
		{
			if (base.GetComponent<Pickupable>() != null && base.GetComponent<Pickupable>().storage == null)
			{
				component.AddStatusItem(Db.Get().MiscStatusItems.MarkedForCompost, null);
				return;
			}
			component.AddStatusItem(Db.Get().MiscStatusItems.MarkedForCompostInStorage, null);
		}
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x00083762 File Offset: 0x00081962
	private void OnStore(object data)
	{
		this.RefreshStatusItem();
	}

	// Token: 0x060018C5 RID: 6341 RVA: 0x0008376C File Offset: 0x0008196C
	private void OnRefreshUserMenu(object data)
	{
		KIconButtonMenu.ButtonInfo buttonInfo;
		if (!this.isMarkedForCompost)
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_compost", UI.USERMENUACTIONS.COMPOST.NAME, new System.Action(this.OnToggleCompost), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.COMPOST.TOOLTIP, true);
		}
		else
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_compost", UI.USERMENUACTIONS.COMPOST.NAME_OFF, new System.Action(this.OnToggleCompost), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.COMPOST.TOOLTIP_OFF, true);
		}
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x04000DC9 RID: 3529
	[SerializeField]
	public bool isMarkedForCompost;

	// Token: 0x04000DCA RID: 3530
	public GameObject originalPrefab;

	// Token: 0x04000DCB RID: 3531
	public GameObject compostPrefab;

	// Token: 0x04000DCC RID: 3532
	private static readonly EventSystem.IntraObjectHandler<Compostable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Compostable>(delegate(Compostable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04000DCD RID: 3533
	private static readonly EventSystem.IntraObjectHandler<Compostable> OnStoreDelegate = new EventSystem.IntraObjectHandler<Compostable>(delegate(Compostable component, object data)
	{
		component.OnStore(data);
	});
}

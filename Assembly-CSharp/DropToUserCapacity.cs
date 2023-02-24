using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000729 RID: 1833
[AddComponentMenu("KMonoBehaviour/Workable/DropToUserCapacity")]
public class DropToUserCapacity : Workable
{
	// Token: 0x06003230 RID: 12848 RVA: 0x0010C7DD File Offset: 0x0010A9DD
	protected DropToUserCapacity()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x06003231 RID: 12849 RVA: 0x0010C7F0 File Offset: 0x0010A9F0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Emptying;
		base.Subscribe<DropToUserCapacity>(-945020481, DropToUserCapacity.OnStorageCapacityChangedHandler);
		base.Subscribe<DropToUserCapacity>(-1697596308, DropToUserCapacity.OnStorageChangedHandler);
		this.synchronizeAnims = false;
		base.SetWorkTime(0.1f);
	}

	// Token: 0x06003232 RID: 12850 RVA: 0x0010C84C File Offset: 0x0010AA4C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateChore();
	}

	// Token: 0x06003233 RID: 12851 RVA: 0x0010C85A File Offset: 0x0010AA5A
	private Storage[] GetStorages()
	{
		if (this.storages == null)
		{
			this.storages = base.GetComponents<Storage>();
		}
		return this.storages;
	}

	// Token: 0x06003234 RID: 12852 RVA: 0x0010C876 File Offset: 0x0010AA76
	private void OnStorageChanged(object data)
	{
		this.UpdateChore();
	}

	// Token: 0x06003235 RID: 12853 RVA: 0x0010C880 File Offset: 0x0010AA80
	public void UpdateChore()
	{
		IUserControlledCapacity component = base.GetComponent<IUserControlledCapacity>();
		if (component != null && component.AmountStored > component.UserMaxCapacity)
		{
			if (this.chore == null)
			{
				this.chore = new WorkChore<DropToUserCapacity>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
				return;
			}
		}
		else if (this.chore != null)
		{
			this.chore.Cancel("Cancelled emptying");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(this.workerStatusItem, false);
			base.ShowProgressBar(false);
		}
	}

	// Token: 0x06003236 RID: 12854 RVA: 0x0010C914 File Offset: 0x0010AB14
	protected override void OnCompleteWork(Worker worker)
	{
		Storage component = base.GetComponent<Storage>();
		IUserControlledCapacity component2 = base.GetComponent<IUserControlledCapacity>();
		float num = Mathf.Max(0f, component2.AmountStored - component2.UserMaxCapacity);
		List<GameObject> list = new List<GameObject>(component.items);
		for (int i = 0; i < list.Count; i++)
		{
			Pickupable component3 = list[i].GetComponent<Pickupable>();
			if (component3.PrimaryElement.Mass > num)
			{
				component3.Take(num).transform.SetPosition(base.transform.GetPosition());
				return;
			}
			num -= component3.PrimaryElement.Mass;
			component.Drop(component3.gameObject, true);
		}
		this.chore = null;
	}

	// Token: 0x04001E81 RID: 7809
	private Chore chore;

	// Token: 0x04001E82 RID: 7810
	private bool showCmd;

	// Token: 0x04001E83 RID: 7811
	private Storage[] storages;

	// Token: 0x04001E84 RID: 7812
	private static readonly EventSystem.IntraObjectHandler<DropToUserCapacity> OnStorageCapacityChangedHandler = new EventSystem.IntraObjectHandler<DropToUserCapacity>(delegate(DropToUserCapacity component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x04001E85 RID: 7813
	private static readonly EventSystem.IntraObjectHandler<DropToUserCapacity> OnStorageChangedHandler = new EventSystem.IntraObjectHandler<DropToUserCapacity>(delegate(DropToUserCapacity component, object data)
	{
		component.OnStorageChanged(data);
	});
}

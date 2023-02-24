using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000633 RID: 1587
[AddComponentMenu("KMonoBehaviour/scripts/Refrigerator")]
public class Refrigerator : KMonoBehaviour, IUserControlledCapacity
{
	// Token: 0x060029DC RID: 10716 RVA: 0x000DCF8D File Offset: 0x000DB18D
	protected override void OnPrefabInit()
	{
		this.filteredStorage = new FilteredStorage(this, new Tag[] { GameTags.Compostable }, this, true, Db.Get().ChoreTypes.FoodFetch);
	}

	// Token: 0x060029DD RID: 10717 RVA: 0x000DCFC0 File Offset: 0x000DB1C0
	protected override void OnSpawn()
	{
		base.GetComponent<KAnimControllerBase>().Play("off", KAnim.PlayMode.Once, 1f, 0f);
		FoodStorage component = base.GetComponent<FoodStorage>();
		component.FilteredStorage = this.filteredStorage;
		component.SpicedFoodOnly = component.SpicedFoodOnly;
		this.filteredStorage.FilterChanged();
		this.UpdateLogicCircuit();
		base.Subscribe<Refrigerator>(-905833192, Refrigerator.OnCopySettingsDelegate);
		base.Subscribe<Refrigerator>(-1697596308, Refrigerator.UpdateLogicCircuitCBDelegate);
		base.Subscribe<Refrigerator>(-592767678, Refrigerator.UpdateLogicCircuitCBDelegate);
	}

	// Token: 0x060029DE RID: 10718 RVA: 0x000DD04E File Offset: 0x000DB24E
	protected override void OnCleanUp()
	{
		this.filteredStorage.CleanUp();
	}

	// Token: 0x060029DF RID: 10719 RVA: 0x000DD05B File Offset: 0x000DB25B
	public bool IsActive()
	{
		return this.operational.IsActive;
	}

	// Token: 0x060029E0 RID: 10720 RVA: 0x000DD068 File Offset: 0x000DB268
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		Refrigerator component = gameObject.GetComponent<Refrigerator>();
		if (component == null)
		{
			return;
		}
		this.UserMaxCapacity = component.UserMaxCapacity;
	}

	// Token: 0x170002CB RID: 715
	// (get) Token: 0x060029E1 RID: 10721 RVA: 0x000DD0A3 File Offset: 0x000DB2A3
	// (set) Token: 0x060029E2 RID: 10722 RVA: 0x000DD0BB File Offset: 0x000DB2BB
	public float UserMaxCapacity
	{
		get
		{
			return Mathf.Min(this.userMaxCapacity, this.storage.capacityKg);
		}
		set
		{
			this.userMaxCapacity = value;
			this.filteredStorage.FilterChanged();
			this.UpdateLogicCircuit();
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x060029E3 RID: 10723 RVA: 0x000DD0D5 File Offset: 0x000DB2D5
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x060029E4 RID: 10724 RVA: 0x000DD0E2 File Offset: 0x000DB2E2
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x060029E5 RID: 10725 RVA: 0x000DD0E9 File Offset: 0x000DB2E9
	public float MaxCapacity
	{
		get
		{
			return this.storage.capacityKg;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x060029E6 RID: 10726 RVA: 0x000DD0F6 File Offset: 0x000DB2F6
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x060029E7 RID: 10727 RVA: 0x000DD0F9 File Offset: 0x000DB2F9
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x060029E8 RID: 10728 RVA: 0x000DD101 File Offset: 0x000DB301
	private void UpdateLogicCircuitCB(object data)
	{
		this.UpdateLogicCircuit();
	}

	// Token: 0x060029E9 RID: 10729 RVA: 0x000DD10C File Offset: 0x000DB30C
	private void UpdateLogicCircuit()
	{
		bool flag = this.filteredStorage.IsFull();
		bool isOperational = this.operational.IsOperational;
		bool flag2 = flag && isOperational;
		this.ports.SendSignal(FilteredStorage.FULL_PORT_ID, flag2 ? 1 : 0);
		this.filteredStorage.SetLogicMeter(flag2);
	}

	// Token: 0x040018CC RID: 6348
	[MyCmpGet]
	private Storage storage;

	// Token: 0x040018CD RID: 6349
	[MyCmpGet]
	private Operational operational;

	// Token: 0x040018CE RID: 6350
	[MyCmpGet]
	private LogicPorts ports;

	// Token: 0x040018CF RID: 6351
	[Serialize]
	private float userMaxCapacity = float.PositiveInfinity;

	// Token: 0x040018D0 RID: 6352
	private FilteredStorage filteredStorage;

	// Token: 0x040018D1 RID: 6353
	private static readonly EventSystem.IntraObjectHandler<Refrigerator> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Refrigerator>(delegate(Refrigerator component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x040018D2 RID: 6354
	private static readonly EventSystem.IntraObjectHandler<Refrigerator> UpdateLogicCircuitCBDelegate = new EventSystem.IntraObjectHandler<Refrigerator>(delegate(Refrigerator component, object data)
	{
		component.UpdateLogicCircuitCB(data);
	});
}

using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200062F RID: 1583
[AddComponentMenu("KMonoBehaviour/scripts/RationBox")]
public class RationBox : KMonoBehaviour, IUserControlledCapacity, IRender1000ms, IRottable
{
	// Token: 0x060029A2 RID: 10658 RVA: 0x000DBDE4 File Offset: 0x000D9FE4
	protected override void OnPrefabInit()
	{
		this.filteredStorage = new FilteredStorage(this, new Tag[] { GameTags.Compostable }, this, false, Db.Get().ChoreTypes.FoodFetch);
		base.Subscribe<RationBox>(-592767678, RationBox.OnOperationalChangedDelegate);
		base.Subscribe<RationBox>(-905833192, RationBox.OnCopySettingsDelegate);
		DiscoveredResources.Instance.Discover("FieldRation".ToTag(), GameTags.Edible);
	}

	// Token: 0x060029A3 RID: 10659 RVA: 0x000DBE5B File Offset: 0x000DA05B
	protected override void OnSpawn()
	{
		Operational component = base.GetComponent<Operational>();
		component.SetActive(component.IsOperational, false);
		this.filteredStorage.FilterChanged();
	}

	// Token: 0x060029A4 RID: 10660 RVA: 0x000DBE7A File Offset: 0x000DA07A
	protected override void OnCleanUp()
	{
		this.filteredStorage.CleanUp();
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x000DBE87 File Offset: 0x000DA087
	private void OnOperationalChanged(object data)
	{
		Operational component = base.GetComponent<Operational>();
		component.SetActive(component.IsOperational, false);
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x000DBE9C File Offset: 0x000DA09C
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		RationBox component = gameObject.GetComponent<RationBox>();
		if (component == null)
		{
			return;
		}
		this.UserMaxCapacity = component.UserMaxCapacity;
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x000DBED7 File Offset: 0x000DA0D7
	public void Render1000ms(float dt)
	{
		Rottable.SetStatusItems(this);
	}

	// Token: 0x170002BF RID: 703
	// (get) Token: 0x060029A8 RID: 10664 RVA: 0x000DBEDF File Offset: 0x000DA0DF
	// (set) Token: 0x060029A9 RID: 10665 RVA: 0x000DBEF7 File Offset: 0x000DA0F7
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
		}
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x060029AA RID: 10666 RVA: 0x000DBF0B File Offset: 0x000DA10B
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x060029AB RID: 10667 RVA: 0x000DBF18 File Offset: 0x000DA118
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x060029AC RID: 10668 RVA: 0x000DBF1F File Offset: 0x000DA11F
	public float MaxCapacity
	{
		get
		{
			return this.storage.capacityKg;
		}
	}

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x060029AD RID: 10669 RVA: 0x000DBF2C File Offset: 0x000DA12C
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x060029AE RID: 10670 RVA: 0x000DBF2F File Offset: 0x000DA12F
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x060029AF RID: 10671 RVA: 0x000DBF37 File Offset: 0x000DA137
	public float RotTemperature
	{
		get
		{
			return 277.15f;
		}
	}

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x060029B0 RID: 10672 RVA: 0x000DBF3E File Offset: 0x000DA13E
	public float PreserveTemperature
	{
		get
		{
			return 255.15f;
		}
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x000DBF8E File Offset: 0x000DA18E
	GameObject IRottable.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x040018AE RID: 6318
	[MyCmpReq]
	private Storage storage;

	// Token: 0x040018AF RID: 6319
	[Serialize]
	private float userMaxCapacity = float.PositiveInfinity;

	// Token: 0x040018B0 RID: 6320
	private FilteredStorage filteredStorage;

	// Token: 0x040018B1 RID: 6321
	private static readonly EventSystem.IntraObjectHandler<RationBox> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<RationBox>(delegate(RationBox component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x040018B2 RID: 6322
	private static readonly EventSystem.IntraObjectHandler<RationBox> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<RationBox>(delegate(RationBox component, object data)
	{
		component.OnCopySettings(data);
	});
}

using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200064D RID: 1613
[AddComponentMenu("KMonoBehaviour/scripts/StorageLocker")]
public class StorageLocker : KMonoBehaviour, IUserControlledCapacity
{
	// Token: 0x06002AEA RID: 10986 RVA: 0x000E29A3 File Offset: 0x000E0BA3
	protected override void OnPrefabInit()
	{
		this.Initialize(false);
	}

	// Token: 0x06002AEB RID: 10987 RVA: 0x000E29AC File Offset: 0x000E0BAC
	protected void Initialize(bool use_logic_meter)
	{
		base.OnPrefabInit();
		this.log = new LoggerFS("StorageLocker", 35);
		ChoreType choreType = Db.Get().ChoreTypes.Get(this.choreTypeID);
		this.filteredStorage = new FilteredStorage(this, null, this, use_logic_meter, choreType);
		base.Subscribe<StorageLocker>(-905833192, StorageLocker.OnCopySettingsDelegate);
	}

	// Token: 0x06002AEC RID: 10988 RVA: 0x000E2A08 File Offset: 0x000E0C08
	protected override void OnSpawn()
	{
		this.filteredStorage.FilterChanged();
		if (this.nameable != null && !this.lockerName.IsNullOrWhiteSpace())
		{
			this.nameable.SetName(this.lockerName);
		}
		base.Trigger(-1683615038, null);
	}

	// Token: 0x06002AED RID: 10989 RVA: 0x000E2A58 File Offset: 0x000E0C58
	protected override void OnCleanUp()
	{
		this.filteredStorage.CleanUp();
	}

	// Token: 0x06002AEE RID: 10990 RVA: 0x000E2A68 File Offset: 0x000E0C68
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		StorageLocker component = gameObject.GetComponent<StorageLocker>();
		if (component == null)
		{
			return;
		}
		this.UserMaxCapacity = component.UserMaxCapacity;
	}

	// Token: 0x06002AEF RID: 10991 RVA: 0x000E2AA3 File Offset: 0x000E0CA3
	public void UpdateForbiddenTag(Tag game_tag, bool forbidden)
	{
		if (forbidden)
		{
			this.filteredStorage.RemoveForbiddenTag(game_tag);
			return;
		}
		this.filteredStorage.AddForbiddenTag(game_tag);
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000E2AC1 File Offset: 0x000E0CC1
	// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x000E2AD9 File Offset: 0x000E0CD9
	public virtual float UserMaxCapacity
	{
		get
		{
			return Mathf.Min(this.userMaxCapacity, base.GetComponent<Storage>().capacityKg);
		}
		set
		{
			this.userMaxCapacity = value;
			this.filteredStorage.FilterChanged();
		}
	}

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000E2AED File Offset: 0x000E0CED
	public float AmountStored
	{
		get
		{
			return base.GetComponent<Storage>().MassStored();
		}
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000E2AFA File Offset: 0x000E0CFA
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000E2B01 File Offset: 0x000E0D01
	public float MaxCapacity
	{
		get
		{
			return base.GetComponent<Storage>().capacityKg;
		}
	}

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000E2B0E File Offset: 0x000E0D0E
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000E2B11 File Offset: 0x000E0D11
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x04001981 RID: 6529
	private LoggerFS log;

	// Token: 0x04001982 RID: 6530
	[Serialize]
	private float userMaxCapacity = float.PositiveInfinity;

	// Token: 0x04001983 RID: 6531
	[Serialize]
	public string lockerName = "";

	// Token: 0x04001984 RID: 6532
	protected FilteredStorage filteredStorage;

	// Token: 0x04001985 RID: 6533
	[MyCmpGet]
	private UserNameable nameable;

	// Token: 0x04001986 RID: 6534
	public string choreTypeID = Db.Get().ChoreTypes.StorageFetch.Id;

	// Token: 0x04001987 RID: 6535
	private static readonly EventSystem.IntraObjectHandler<StorageLocker> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<StorageLocker>(delegate(StorageLocker component, object data)
	{
		component.OnCopySettings(data);
	});
}

using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200093A RID: 2362
public class CargoBayCluster : KMonoBehaviour, IUserControlledCapacity
{
	// Token: 0x170004F0 RID: 1264
	// (get) Token: 0x0600452F RID: 17711 RVA: 0x00186371 File Offset: 0x00184571
	// (set) Token: 0x06004530 RID: 17712 RVA: 0x00186379 File Offset: 0x00184579
	public float UserMaxCapacity
	{
		get
		{
			return this.userMaxCapacity;
		}
		set
		{
			this.userMaxCapacity = value;
			base.Trigger(-945020481, this);
		}
	}

	// Token: 0x170004F1 RID: 1265
	// (get) Token: 0x06004531 RID: 17713 RVA: 0x0018638E File Offset: 0x0018458E
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170004F2 RID: 1266
	// (get) Token: 0x06004532 RID: 17714 RVA: 0x00186395 File Offset: 0x00184595
	public float MaxCapacity
	{
		get
		{
			return this.storage.capacityKg;
		}
	}

	// Token: 0x170004F3 RID: 1267
	// (get) Token: 0x06004533 RID: 17715 RVA: 0x001863A2 File Offset: 0x001845A2
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x170004F4 RID: 1268
	// (get) Token: 0x06004534 RID: 17716 RVA: 0x001863AF File Offset: 0x001845AF
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170004F5 RID: 1269
	// (get) Token: 0x06004535 RID: 17717 RVA: 0x001863B2 File Offset: 0x001845B2
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x170004F6 RID: 1270
	// (get) Token: 0x06004536 RID: 17718 RVA: 0x001863BA File Offset: 0x001845BA
	public float RemainingCapacity
	{
		get
		{
			return this.userMaxCapacity - this.storage.MassStored();
		}
	}

	// Token: 0x06004537 RID: 17719 RVA: 0x001863CE File Offset: 0x001845CE
	protected override void OnPrefabInit()
	{
		this.userMaxCapacity = this.storage.capacityKg;
	}

	// Token: 0x06004538 RID: 17720 RVA: 0x001863E4 File Offset: 0x001845E4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KBatchedAnimController>().Play("grounded", KAnim.PlayMode.Loop, 1f, 0f);
		base.Subscribe<CargoBayCluster>(493375141, CargoBayCluster.OnRefreshUserMenuDelegate);
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		KBatchedAnimTracker component = this.meter.gameObject.GetComponent<KBatchedAnimTracker>();
		component.matchParentOffset = true;
		component.forceAlwaysAlive = true;
		this.OnStorageChange(null);
		base.Subscribe<CargoBayCluster>(-1697596308, CargoBayCluster.OnStorageChangeDelegate);
	}

	// Token: 0x06004539 RID: 17721 RVA: 0x001864A4 File Offset: 0x001846A4
	private void OnRefreshUserMenu(object data)
	{
		KIconButtonMenu.ButtonInfo buttonInfo = new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.EMPTYSTORAGE.NAME, delegate
		{
			this.storage.DropAll(false, false, default(Vector3), true, null);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.EMPTYSTORAGE.TOOLTIP, true);
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x0600453A RID: 17722 RVA: 0x00186500 File Offset: 0x00184700
	private void OnStorageChange(object data)
	{
		this.meter.SetPositionPercent(this.storage.MassStored() / this.storage.Capacity());
		this.UpdateCargoStatusItem();
	}

	// Token: 0x0600453B RID: 17723 RVA: 0x0018652C File Offset: 0x0018472C
	private void UpdateCargoStatusItem()
	{
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component == null)
		{
			return;
		}
		CraftModuleInterface craftInterface = component.CraftInterface;
		if (craftInterface == null)
		{
			return;
		}
		Clustercraft component2 = craftInterface.GetComponent<Clustercraft>();
		if (component2 == null)
		{
			return;
		}
		component2.UpdateStatusItem();
	}

	// Token: 0x04002E1F RID: 11807
	private MeterController meter;

	// Token: 0x04002E20 RID: 11808
	[SerializeField]
	public Storage storage;

	// Token: 0x04002E21 RID: 11809
	[SerializeField]
	public CargoBay.CargoType storageType;

	// Token: 0x04002E22 RID: 11810
	[Serialize]
	private float userMaxCapacity;

	// Token: 0x04002E23 RID: 11811
	private static readonly EventSystem.IntraObjectHandler<CargoBayCluster> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<CargoBayCluster>(delegate(CargoBayCluster component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04002E24 RID: 11812
	private static readonly EventSystem.IntraObjectHandler<CargoBayCluster> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<CargoBayCluster>(delegate(CargoBayCluster component, object data)
	{
		component.OnStorageChange(data);
	});
}

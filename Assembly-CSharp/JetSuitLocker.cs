using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020005D5 RID: 1493
public class JetSuitLocker : StateMachineComponent<JetSuitLocker.StatesInstance>, ISecondaryInput
{
	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06002540 RID: 9536 RVA: 0x000C9594 File Offset: 0x000C7794
	public float FuelAvailable
	{
		get
		{
			GameObject fuel = this.GetFuel();
			float num = 0f;
			if (fuel != null)
			{
				num = fuel.GetComponent<PrimaryElement>().Mass / 100f;
				num = Math.Min(num, 1f);
			}
			return num;
		}
	}

	// Token: 0x06002541 RID: 9537 RVA: 0x000C95D8 File Offset: 0x000C77D8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.fuel_tag = SimHashes.Petroleum.CreateTag();
		this.fuel_consumer = base.gameObject.AddComponent<ConduitConsumer>();
		this.fuel_consumer.conduitType = this.portInfo.conduitType;
		this.fuel_consumer.consumptionRate = 10f;
		this.fuel_consumer.capacityTag = this.fuel_tag;
		this.fuel_consumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		this.fuel_consumer.forceAlwaysSatisfied = true;
		this.fuel_consumer.capacityKG = 100f;
		this.fuel_consumer.useSecondaryInput = true;
		RequireInputs requireInputs = base.gameObject.AddComponent<RequireInputs>();
		requireInputs.conduitConsumer = this.fuel_consumer;
		requireInputs.SetRequirements(false, true);
		int num = Grid.PosToCell(base.transform.GetPosition());
		CellOffset rotatedOffset = this.building.GetRotatedOffset(this.portInfo.offset);
		this.secondaryInputCell = Grid.OffsetCell(num, rotatedOffset);
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.portInfo.conduitType);
		this.flowNetworkItem = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Sink, this.secondaryInputCell, base.gameObject);
		networkManager.AddToNetworks(this.secondaryInputCell, this.flowNetworkItem, true);
		this.fuel_meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target_1", "meter_petrol", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, new string[] { "meter_target_1" });
		this.o2_meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target_2", "meter_oxygen", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Vector3.zero, new string[] { "meter_target_2" });
		base.smi.StartSM();
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x000C977C File Offset: 0x000C797C
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.portInfo.conduitType).RemoveFromNetworks(this.secondaryInputCell, this.flowNetworkItem, true);
		base.OnCleanUp();
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x000C97A6 File Offset: 0x000C79A6
	private GameObject GetFuel()
	{
		return this.storage.FindFirst(this.fuel_tag);
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x000C97B9 File Offset: 0x000C79B9
	public bool IsSuitFullyCharged()
	{
		return this.suit_locker.IsSuitFullyCharged();
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x000C97C6 File Offset: 0x000C79C6
	public KPrefabID GetStoredOutfit()
	{
		return this.suit_locker.GetStoredOutfit();
	}

	// Token: 0x06002546 RID: 9542 RVA: 0x000C97D4 File Offset: 0x000C79D4
	private void FuelSuit(float dt)
	{
		KPrefabID storedOutfit = this.suit_locker.GetStoredOutfit();
		if (storedOutfit == null)
		{
			return;
		}
		GameObject fuel = this.GetFuel();
		if (fuel == null)
		{
			return;
		}
		PrimaryElement component = fuel.GetComponent<PrimaryElement>();
		if (component == null)
		{
			return;
		}
		JetSuitTank component2 = storedOutfit.GetComponent<JetSuitTank>();
		float num = 375f * dt / 600f;
		num = Mathf.Min(num, 25f - component2.amount);
		num = Mathf.Min(component.Mass, num);
		component.Mass -= num;
		component2.amount += num;
	}

	// Token: 0x06002547 RID: 9543 RVA: 0x000C9871 File Offset: 0x000C7A71
	bool ISecondaryInput.HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x000C9881 File Offset: 0x000C7A81
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (this.portInfo.conduitType == type)
		{
			return this.portInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x000C98A4 File Offset: 0x000C7AA4
	public bool HasFuel()
	{
		GameObject fuel = this.GetFuel();
		return fuel != null && fuel.GetComponent<PrimaryElement>().Mass > 0f;
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x000C98D8 File Offset: 0x000C7AD8
	private void RefreshMeter()
	{
		this.o2_meter.SetPositionPercent(this.suit_locker.OxygenAvailable);
		this.fuel_meter.SetPositionPercent(this.FuelAvailable);
		this.anim_controller.SetSymbolVisiblity("oxygen_yes_bloom", this.IsOxygenTankAboveMinimumLevel());
		this.anim_controller.SetSymbolVisiblity("petrol_yes_bloom", this.IsFuelTankAboveMinimumLevel());
	}

	// Token: 0x0600254B RID: 9547 RVA: 0x000C9944 File Offset: 0x000C7B44
	public bool IsOxygenTankAboveMinimumLevel()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit != null)
		{
			SuitTank component = storedOutfit.GetComponent<SuitTank>();
			return component == null || component.PercentFull() >= TUNING.EQUIPMENT.SUITS.MINIMUM_USABLE_SUIT_CHARGE;
		}
		return false;
	}

	// Token: 0x0600254C RID: 9548 RVA: 0x000C9988 File Offset: 0x000C7B88
	public bool IsFuelTankAboveMinimumLevel()
	{
		KPrefabID storedOutfit = this.GetStoredOutfit();
		if (storedOutfit != null)
		{
			JetSuitTank component = storedOutfit.GetComponent<JetSuitTank>();
			return component == null || component.PercentFull() >= TUNING.EQUIPMENT.SUITS.MINIMUM_USABLE_SUIT_CHARGE;
		}
		return false;
	}

	// Token: 0x040015A0 RID: 5536
	[MyCmpReq]
	private Building building;

	// Token: 0x040015A1 RID: 5537
	[MyCmpReq]
	private Storage storage;

	// Token: 0x040015A2 RID: 5538
	[MyCmpReq]
	private SuitLocker suit_locker;

	// Token: 0x040015A3 RID: 5539
	[MyCmpReq]
	private KBatchedAnimController anim_controller;

	// Token: 0x040015A4 RID: 5540
	public const float FUEL_CAPACITY = 100f;

	// Token: 0x040015A5 RID: 5541
	[SerializeField]
	public ConduitPortInfo portInfo;

	// Token: 0x040015A6 RID: 5542
	private int secondaryInputCell = -1;

	// Token: 0x040015A7 RID: 5543
	private FlowUtilityNetwork.NetworkItem flowNetworkItem;

	// Token: 0x040015A8 RID: 5544
	private ConduitConsumer fuel_consumer;

	// Token: 0x040015A9 RID: 5545
	private Tag fuel_tag;

	// Token: 0x040015AA RID: 5546
	private MeterController o2_meter;

	// Token: 0x040015AB RID: 5547
	private MeterController fuel_meter;

	// Token: 0x0200122D RID: 4653
	public class States : GameStateMachine<JetSuitLocker.States, JetSuitLocker.StatesInstance, JetSuitLocker>
	{
		// Token: 0x06007950 RID: 31056 RVA: 0x002C2FAC File Offset: 0x002C11AC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.Update("RefreshMeter", delegate(JetSuitLocker.StatesInstance smi, float dt)
			{
				smi.master.RefreshMeter();
			}, UpdateRate.RENDER_200ms, false);
			this.empty.EventTransition(GameHashes.OnStorageChange, this.charging, (JetSuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() != null);
			this.charging.DefaultState(this.charging.notoperational).EventTransition(GameHashes.OnStorageChange, this.empty, (JetSuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() == null).Transition(this.charged, (JetSuitLocker.StatesInstance smi) => smi.master.IsSuitFullyCharged(), UpdateRate.SIM_200ms);
			this.charging.notoperational.TagTransition(GameTags.Operational, this.charging.operational, false);
			this.charging.operational.TagTransition(GameTags.Operational, this.charging.notoperational, true).Transition(this.charging.nofuel, (JetSuitLocker.StatesInstance smi) => !smi.master.HasFuel(), UpdateRate.SIM_200ms).Update("FuelSuit", delegate(JetSuitLocker.StatesInstance smi, float dt)
			{
				smi.master.FuelSuit(dt);
			}, UpdateRate.SIM_1000ms, false);
			this.charging.nofuel.TagTransition(GameTags.Operational, this.charging.notoperational, true).Transition(this.charging.operational, (JetSuitLocker.StatesInstance smi) => smi.master.HasFuel(), UpdateRate.SIM_200ms).ToggleStatusItem(BUILDING.STATUSITEMS.SUIT_LOCKER.NO_FUEL.NAME, BUILDING.STATUSITEMS.SUIT_LOCKER.NO_FUEL.TOOLTIP, "status_item_no_liquid_to_pump", StatusItem.IconType.Custom, NotificationType.BadMinor, false, default(HashedString), 129022, null, null, null);
			this.charged.EventTransition(GameHashes.OnStorageChange, this.empty, (JetSuitLocker.StatesInstance smi) => smi.master.GetStoredOutfit() == null);
		}

		// Token: 0x04005D36 RID: 23862
		public GameStateMachine<JetSuitLocker.States, JetSuitLocker.StatesInstance, JetSuitLocker, object>.State empty;

		// Token: 0x04005D37 RID: 23863
		public JetSuitLocker.States.ChargingStates charging;

		// Token: 0x04005D38 RID: 23864
		public GameStateMachine<JetSuitLocker.States, JetSuitLocker.StatesInstance, JetSuitLocker, object>.State charged;

		// Token: 0x02001FBC RID: 8124
		public class ChargingStates : GameStateMachine<JetSuitLocker.States, JetSuitLocker.StatesInstance, JetSuitLocker, object>.State
		{
			// Token: 0x04008D5F RID: 36191
			public GameStateMachine<JetSuitLocker.States, JetSuitLocker.StatesInstance, JetSuitLocker, object>.State notoperational;

			// Token: 0x04008D60 RID: 36192
			public GameStateMachine<JetSuitLocker.States, JetSuitLocker.StatesInstance, JetSuitLocker, object>.State operational;

			// Token: 0x04008D61 RID: 36193
			public GameStateMachine<JetSuitLocker.States, JetSuitLocker.StatesInstance, JetSuitLocker, object>.State nofuel;
		}
	}

	// Token: 0x0200122E RID: 4654
	public class StatesInstance : GameStateMachine<JetSuitLocker.States, JetSuitLocker.StatesInstance, JetSuitLocker, object>.GameInstance
	{
		// Token: 0x06007952 RID: 31058 RVA: 0x002C3202 File Offset: 0x002C1402
		public StatesInstance(JetSuitLocker jet_suit_locker)
			: base(jet_suit_locker)
		{
		}
	}
}

using System;
using UnityEngine;

// Token: 0x0200062D RID: 1581
public class RailGunPayloadOpener : StateMachineComponent<RailGunPayloadOpener.StatesInstance>, ISecondaryOutput
{
	// Token: 0x06002996 RID: 10646 RVA: 0x000DB9C0 File Offset: 0x000D9BC0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.gasOutputCell = Grid.OffsetCell(Grid.PosToCell(this), this.gasPortInfo.offset);
		this.gasDispenser = this.CreateConduitDispenser(ConduitType.Gas, this.gasOutputCell, out this.gasNetworkItem);
		this.liquidOutputCell = Grid.OffsetCell(Grid.PosToCell(this), this.liquidPortInfo.offset);
		this.liquidDispenser = this.CreateConduitDispenser(ConduitType.Liquid, this.liquidOutputCell, out this.liquidNetworkItem);
		this.solidOutputCell = Grid.OffsetCell(Grid.PosToCell(this), this.solidPortInfo.offset);
		this.solidDispenser = this.CreateSolidConduitDispenser(this.solidOutputCell, out this.solidNetworkItem);
		this.deliveryComponents = base.GetComponents<ManualDeliveryKG>();
		this.payloadStorage.gunTargetOffset = new Vector2(-1f, 1.5f);
		this.payloadMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_storage_target", "meter_storage", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		base.smi.StartSM();
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x000DBAC8 File Offset: 0x000D9CC8
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.liquidPortInfo.conduitType).RemoveFromNetworks(this.liquidOutputCell, this.liquidNetworkItem, true);
		Conduit.GetNetworkManager(this.gasPortInfo.conduitType).RemoveFromNetworks(this.gasOutputCell, this.gasNetworkItem, true);
		Game.Instance.solidConduitSystem.RemoveFromNetworks(this.solidOutputCell, this.solidDispenser, true);
		base.OnCleanUp();
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x000DBB3C File Offset: 0x000D9D3C
	private ConduitDispenser CreateConduitDispenser(ConduitType outputType, int outputCell, out FlowUtilityNetwork.NetworkItem flowNetworkItem)
	{
		ConduitDispenser conduitDispenser = base.gameObject.AddComponent<ConduitDispenser>();
		conduitDispenser.conduitType = outputType;
		conduitDispenser.useSecondaryOutput = true;
		conduitDispenser.alwaysDispense = true;
		conduitDispenser.storage = this.resourceStorage;
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(outputType);
		flowNetworkItem = new FlowUtilityNetwork.NetworkItem(outputType, Endpoint.Source, outputCell, base.gameObject);
		networkManager.AddToNetworks(outputCell, flowNetworkItem, true);
		return conduitDispenser;
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x000DBB94 File Offset: 0x000D9D94
	private SolidConduitDispenser CreateSolidConduitDispenser(int outputCell, out FlowUtilityNetwork.NetworkItem flowNetworkItem)
	{
		SolidConduitDispenser solidConduitDispenser = base.gameObject.AddComponent<SolidConduitDispenser>();
		solidConduitDispenser.storage = this.resourceStorage;
		solidConduitDispenser.alwaysDispense = true;
		solidConduitDispenser.useSecondaryOutput = true;
		solidConduitDispenser.solidOnly = true;
		flowNetworkItem = new FlowUtilityNetwork.NetworkItem(ConduitType.Solid, Endpoint.Source, outputCell, base.gameObject);
		Game.Instance.solidConduitSystem.AddToNetworks(outputCell, flowNetworkItem, true);
		return solidConduitDispenser;
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x000DBBF0 File Offset: 0x000D9DF0
	public void EmptyPayload()
	{
		Storage component = base.GetComponent<Storage>();
		if (component != null && component.items.Count > 0)
		{
			GameObject gameObject = this.payloadStorage.items[0];
			gameObject.GetComponent<Storage>().Transfer(this.resourceStorage, false, false);
			Util.KDestroyGameObject(gameObject);
			component.ConsumeIgnoringDisease(this.payloadStorage.items[0]);
		}
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x000DBC5C File Offset: 0x000D9E5C
	public bool PowerOperationalChanged()
	{
		EnergyConsumer component = base.GetComponent<EnergyConsumer>();
		return component != null && component.IsPowered;
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x000DBC81 File Offset: 0x000D9E81
	bool ISecondaryOutput.HasSecondaryConduitType(ConduitType type)
	{
		return type == this.gasPortInfo.conduitType || type == this.liquidPortInfo.conduitType || type == this.solidPortInfo.conduitType;
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x000DBCB0 File Offset: 0x000D9EB0
	CellOffset ISecondaryOutput.GetSecondaryConduitOffset(ConduitType type)
	{
		if (type == this.gasPortInfo.conduitType)
		{
			return this.gasPortInfo.offset;
		}
		if (type == this.liquidPortInfo.conduitType)
		{
			return this.liquidPortInfo.offset;
		}
		if (type != this.solidPortInfo.conduitType)
		{
			return CellOffset.none;
		}
		return this.solidPortInfo.offset;
	}

	// Token: 0x0400189A RID: 6298
	public static float delivery_time = 10f;

	// Token: 0x0400189B RID: 6299
	[SerializeField]
	public ConduitPortInfo liquidPortInfo;

	// Token: 0x0400189C RID: 6300
	private int liquidOutputCell = -1;

	// Token: 0x0400189D RID: 6301
	private FlowUtilityNetwork.NetworkItem liquidNetworkItem;

	// Token: 0x0400189E RID: 6302
	private ConduitDispenser liquidDispenser;

	// Token: 0x0400189F RID: 6303
	[SerializeField]
	public ConduitPortInfo gasPortInfo;

	// Token: 0x040018A0 RID: 6304
	private int gasOutputCell = -1;

	// Token: 0x040018A1 RID: 6305
	private FlowUtilityNetwork.NetworkItem gasNetworkItem;

	// Token: 0x040018A2 RID: 6306
	private ConduitDispenser gasDispenser;

	// Token: 0x040018A3 RID: 6307
	[SerializeField]
	public ConduitPortInfo solidPortInfo;

	// Token: 0x040018A4 RID: 6308
	private int solidOutputCell = -1;

	// Token: 0x040018A5 RID: 6309
	private FlowUtilityNetwork.NetworkItem solidNetworkItem;

	// Token: 0x040018A6 RID: 6310
	private SolidConduitDispenser solidDispenser;

	// Token: 0x040018A7 RID: 6311
	public Storage payloadStorage;

	// Token: 0x040018A8 RID: 6312
	public Storage resourceStorage;

	// Token: 0x040018A9 RID: 6313
	private ManualDeliveryKG[] deliveryComponents;

	// Token: 0x040018AA RID: 6314
	private MeterController payloadMeter;

	// Token: 0x020012B4 RID: 4788
	public class StatesInstance : GameStateMachine<RailGunPayloadOpener.States, RailGunPayloadOpener.StatesInstance, RailGunPayloadOpener, object>.GameInstance
	{
		// Token: 0x06007B2F RID: 31535 RVA: 0x002CAA2F File Offset: 0x002C8C2F
		public StatesInstance(RailGunPayloadOpener master)
			: base(master)
		{
		}

		// Token: 0x06007B30 RID: 31536 RVA: 0x002CAA38 File Offset: 0x002C8C38
		public bool HasPayload()
		{
			return base.smi.master.payloadStorage.items.Count > 0;
		}

		// Token: 0x06007B31 RID: 31537 RVA: 0x002CAA57 File Offset: 0x002C8C57
		public bool HasResources()
		{
			return base.smi.master.resourceStorage.MassStored() > 0f;
		}
	}

	// Token: 0x020012B5 RID: 4789
	public class States : GameStateMachine<RailGunPayloadOpener.States, RailGunPayloadOpener.StatesInstance, RailGunPayloadOpener>
	{
		// Token: 0x06007B32 RID: 31538 RVA: 0x002CAA78 File Offset: 0x002C8C78
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.unoperational.PlayAnim("off").EventTransition(GameHashes.OperationalFlagChanged, this.operational, (RailGunPayloadOpener.StatesInstance smi) => smi.master.PowerOperationalChanged()).Enter(delegate(RailGunPayloadOpener.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, true);
				smi.GetComponent<ManualDeliveryKG>().Pause(true, "no_power");
			});
			this.operational.Enter(delegate(RailGunPayloadOpener.StatesInstance smi)
			{
				smi.GetComponent<ManualDeliveryKG>().Pause(false, "power");
			}).EventTransition(GameHashes.OperationalFlagChanged, this.unoperational, (RailGunPayloadOpener.StatesInstance smi) => !smi.master.PowerOperationalChanged()).DefaultState(this.operational.idle)
				.EventHandler(GameHashes.OnStorageChange, delegate(RailGunPayloadOpener.StatesInstance smi)
				{
					smi.master.payloadMeter.SetPositionPercent(Mathf.Clamp01((float)smi.master.payloadStorage.items.Count / smi.master.payloadStorage.capacityKg));
				});
			this.operational.idle.PlayAnim("on").EventTransition(GameHashes.OnStorageChange, this.operational.pre, (RailGunPayloadOpener.StatesInstance smi) => smi.HasPayload());
			this.operational.pre.Enter(delegate(RailGunPayloadOpener.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, true);
			}).PlayAnim("working_pre").OnAnimQueueComplete(this.operational.loop);
			this.operational.loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).ScheduleGoTo(10f, this.operational.pst);
			this.operational.pst.PlayAnim("working_pst").Exit(delegate(RailGunPayloadOpener.StatesInstance smi)
			{
				smi.master.EmptyPayload();
				smi.GetComponent<Operational>().SetActive(false, true);
			}).OnAnimQueueComplete(this.operational.idle);
		}

		// Token: 0x04005E71 RID: 24177
		public GameStateMachine<RailGunPayloadOpener.States, RailGunPayloadOpener.StatesInstance, RailGunPayloadOpener, object>.State unoperational;

		// Token: 0x04005E72 RID: 24178
		public RailGunPayloadOpener.States.OperationalStates operational;

		// Token: 0x02001FE5 RID: 8165
		public class OperationalStates : GameStateMachine<RailGunPayloadOpener.States, RailGunPayloadOpener.StatesInstance, RailGunPayloadOpener, object>.State
		{
			// Token: 0x04008E31 RID: 36401
			public GameStateMachine<RailGunPayloadOpener.States, RailGunPayloadOpener.StatesInstance, RailGunPayloadOpener, object>.State idle;

			// Token: 0x04008E32 RID: 36402
			public GameStateMachine<RailGunPayloadOpener.States, RailGunPayloadOpener.StatesInstance, RailGunPayloadOpener, object>.State pre;

			// Token: 0x04008E33 RID: 36403
			public GameStateMachine<RailGunPayloadOpener.States, RailGunPayloadOpener.StatesInstance, RailGunPayloadOpener, object>.State loop;

			// Token: 0x04008E34 RID: 36404
			public GameStateMachine<RailGunPayloadOpener.States, RailGunPayloadOpener.StatesInstance, RailGunPayloadOpener, object>.State pst;
		}
	}
}

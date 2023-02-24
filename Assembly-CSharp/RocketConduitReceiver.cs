using System;
using UnityEngine;

// Token: 0x02000637 RID: 1591
public class RocketConduitReceiver : StateMachineComponent<RocketConduitReceiver.StatesInstance>, ISecondaryOutput
{
	// Token: 0x06002A17 RID: 10775 RVA: 0x000DE86C File Offset: 0x000DCA6C
	public void AddConduitPortToNetwork()
	{
		if (this.conduitPort.conduitDispenser == null)
		{
			return;
		}
		int num = Grid.OffsetCell(Grid.PosToCell(base.gameObject), this.conduitPortInfo.offset);
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.conduitPortInfo.conduitType);
		this.conduitPort.outputCell = num;
		this.conduitPort.networkItem = new FlowUtilityNetwork.NetworkItem(this.conduitPortInfo.conduitType, Endpoint.Source, num, base.gameObject);
		networkManager.AddToNetworks(num, this.conduitPort.networkItem, true);
	}

	// Token: 0x06002A18 RID: 10776 RVA: 0x000DE8FC File Offset: 0x000DCAFC
	public void RemoveConduitPortFromNetwork()
	{
		if (this.conduitPort.conduitDispenser == null)
		{
			return;
		}
		Conduit.GetNetworkManager(this.conduitPortInfo.conduitType).RemoveFromNetworks(this.conduitPort.outputCell, this.conduitPort.networkItem, true);
	}

	// Token: 0x06002A19 RID: 10777 RVA: 0x000DE94C File Offset: 0x000DCB4C
	private bool CanTransferFromSender()
	{
		bool flag = false;
		if ((base.smi.master.senderConduitStorage.MassStored() > 0f || base.smi.master.senderConduitStorage.items.Count > 0) && base.smi.master.conduitPort.conduitDispenser.GetConduitManager().GetPermittedFlow(base.smi.master.conduitPort.outputCell) != ConduitFlow.FlowDirections.None)
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x06002A1A RID: 10778 RVA: 0x000DE9D0 File Offset: 0x000DCBD0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.FindPartner();
		base.Subscribe<RocketConduitReceiver>(-1118736034, RocketConduitReceiver.TryFindPartner);
		base.Subscribe<RocketConduitReceiver>(546421097, RocketConduitReceiver.OnLaunchedDelegate);
		base.Subscribe<RocketConduitReceiver>(-735346771, RocketConduitReceiver.OnLandedDelegate);
		base.smi.StartSM();
		Components.RocketConduitReceivers.Add(this);
	}

	// Token: 0x06002A1B RID: 10779 RVA: 0x000DEA32 File Offset: 0x000DCC32
	protected override void OnCleanUp()
	{
		this.RemoveConduitPortFromNetwork();
		base.OnCleanUp();
		Components.RocketConduitReceivers.Remove(this);
	}

	// Token: 0x06002A1C RID: 10780 RVA: 0x000DEA4C File Offset: 0x000DCC4C
	private void FindPartner()
	{
		if (this.senderConduitStorage != null)
		{
			return;
		}
		RocketConduitSender rocketConduitSender = null;
		WorldContainer world = ClusterManager.Instance.GetWorld(base.gameObject.GetMyWorldId());
		if (world != null && world.IsModuleInterior)
		{
			foreach (RocketConduitSender rocketConduitSender2 in world.GetComponent<Clustercraft>().ModuleInterface.GetPassengerModule().GetComponents<RocketConduitSender>())
			{
				if (rocketConduitSender2.conduitPortInfo.conduitType == this.conduitPortInfo.conduitType)
				{
					rocketConduitSender = rocketConduitSender2;
					break;
				}
			}
		}
		else
		{
			ClustercraftExteriorDoor component = base.gameObject.GetComponent<ClustercraftExteriorDoor>();
			if (component.HasTargetWorld())
			{
				WorldContainer targetWorld = component.GetTargetWorld();
				foreach (RocketConduitSender rocketConduitSender3 in Components.RocketConduitSenders.GetWorldItems(targetWorld.id, false))
				{
					if (rocketConduitSender3.conduitPortInfo.conduitType == this.conduitPortInfo.conduitType)
					{
						rocketConduitSender = rocketConduitSender3;
						break;
					}
				}
			}
		}
		if (rocketConduitSender == null)
		{
			global::Debug.LogWarning("No warp conduit sender found?");
			return;
		}
		this.SetStorage(rocketConduitSender.conduitStorage);
	}

	// Token: 0x06002A1D RID: 10781 RVA: 0x000DEB88 File Offset: 0x000DCD88
	public void SetStorage(Storage conduitStorage)
	{
		this.senderConduitStorage = conduitStorage;
		this.conduitPort.SetPortInfo(base.gameObject, this.conduitPortInfo, conduitStorage);
		if (base.gameObject.GetMyWorld() != null)
		{
			this.AddConduitPortToNetwork();
		}
	}

	// Token: 0x06002A1E RID: 10782 RVA: 0x000DEBC2 File Offset: 0x000DCDC2
	bool ISecondaryOutput.HasSecondaryConduitType(ConduitType type)
	{
		return type == this.conduitPortInfo.conduitType;
	}

	// Token: 0x06002A1F RID: 10783 RVA: 0x000DEBD2 File Offset: 0x000DCDD2
	CellOffset ISecondaryOutput.GetSecondaryConduitOffset(ConduitType type)
	{
		if (type == this.conduitPortInfo.conduitType)
		{
			return this.conduitPortInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x040018E1 RID: 6369
	[SerializeField]
	public ConduitPortInfo conduitPortInfo;

	// Token: 0x040018E2 RID: 6370
	public RocketConduitReceiver.ConduitPort conduitPort;

	// Token: 0x040018E3 RID: 6371
	public Storage senderConduitStorage;

	// Token: 0x040018E4 RID: 6372
	private static readonly EventSystem.IntraObjectHandler<RocketConduitReceiver> TryFindPartner = new EventSystem.IntraObjectHandler<RocketConduitReceiver>(delegate(RocketConduitReceiver component, object data)
	{
		component.FindPartner();
	});

	// Token: 0x040018E5 RID: 6373
	private static readonly EventSystem.IntraObjectHandler<RocketConduitReceiver> OnLandedDelegate = new EventSystem.IntraObjectHandler<RocketConduitReceiver>(delegate(RocketConduitReceiver component, object data)
	{
		component.AddConduitPortToNetwork();
	});

	// Token: 0x040018E6 RID: 6374
	private static readonly EventSystem.IntraObjectHandler<RocketConduitReceiver> OnLaunchedDelegate = new EventSystem.IntraObjectHandler<RocketConduitReceiver>(delegate(RocketConduitReceiver component, object data)
	{
		component.RemoveConduitPortFromNetwork();
	});

	// Token: 0x020012C8 RID: 4808
	public struct ConduitPort
	{
		// Token: 0x06007B89 RID: 31625 RVA: 0x002CBF70 File Offset: 0x002CA170
		public void SetPortInfo(GameObject parent, ConduitPortInfo info, Storage senderStorage)
		{
			this.portInfo = info;
			ConduitDispenser conduitDispenser = parent.AddComponent<ConduitDispenser>();
			conduitDispenser.conduitType = this.portInfo.conduitType;
			conduitDispenser.useSecondaryOutput = true;
			conduitDispenser.alwaysDispense = true;
			conduitDispenser.storage = senderStorage;
			this.conduitDispenser = conduitDispenser;
		}

		// Token: 0x04005EBA RID: 24250
		public ConduitPortInfo portInfo;

		// Token: 0x04005EBB RID: 24251
		public int outputCell;

		// Token: 0x04005EBC RID: 24252
		public FlowUtilityNetwork.NetworkItem networkItem;

		// Token: 0x04005EBD RID: 24253
		public ConduitDispenser conduitDispenser;
	}

	// Token: 0x020012C9 RID: 4809
	public class StatesInstance : GameStateMachine<RocketConduitReceiver.States, RocketConduitReceiver.StatesInstance, RocketConduitReceiver, object>.GameInstance
	{
		// Token: 0x06007B8A RID: 31626 RVA: 0x002CBFB8 File Offset: 0x002CA1B8
		public StatesInstance(RocketConduitReceiver master)
			: base(master)
		{
		}
	}

	// Token: 0x020012CA RID: 4810
	public class States : GameStateMachine<RocketConduitReceiver.States, RocketConduitReceiver.StatesInstance, RocketConduitReceiver>
	{
		// Token: 0x06007B8B RID: 31627 RVA: 0x002CBFC4 File Offset: 0x002CA1C4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.off.EventTransition(GameHashes.OperationalFlagChanged, this.on, (RocketConduitReceiver.StatesInstance smi) => smi.GetComponent<Operational>().GetFlag(WarpConduitStatus.warpConnectedFlag));
			this.on.DefaultState(this.on.empty);
			this.on.empty.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Normal, null).Update(delegate(RocketConduitReceiver.StatesInstance smi, float dt)
			{
				if (smi.master.CanTransferFromSender())
				{
					smi.GoTo(this.on.hasResources);
				}
			}, UpdateRate.SIM_200ms, false);
			this.on.hasResources.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Working, null).Update(delegate(RocketConduitReceiver.StatesInstance smi, float dt)
			{
				if (!smi.master.CanTransferFromSender())
				{
					smi.GoTo(this.on.empty);
				}
			}, UpdateRate.SIM_200ms, false);
		}

		// Token: 0x04005EBE RID: 24254
		public GameStateMachine<RocketConduitReceiver.States, RocketConduitReceiver.StatesInstance, RocketConduitReceiver, object>.State off;

		// Token: 0x04005EBF RID: 24255
		public RocketConduitReceiver.States.onStates on;

		// Token: 0x02001FEB RID: 8171
		public class onStates : GameStateMachine<RocketConduitReceiver.States, RocketConduitReceiver.StatesInstance, RocketConduitReceiver, object>.State
		{
			// Token: 0x04008E55 RID: 36437
			public GameStateMachine<RocketConduitReceiver.States, RocketConduitReceiver.StatesInstance, RocketConduitReceiver, object>.State hasResources;

			// Token: 0x04008E56 RID: 36438
			public GameStateMachine<RocketConduitReceiver.States, RocketConduitReceiver.StatesInstance, RocketConduitReceiver, object>.State empty;
		}
	}
}

using System;
using UnityEngine;

// Token: 0x02000638 RID: 1592
public class RocketConduitSender : StateMachineComponent<RocketConduitSender.StatesInstance>, ISecondaryInput
{
	// Token: 0x06002A22 RID: 10786 RVA: 0x000DEC58 File Offset: 0x000DCE58
	public void AddConduitPortToNetwork()
	{
		if (this.conduitPort == null)
		{
			return;
		}
		int num = Grid.OffsetCell(Grid.PosToCell(base.gameObject), this.conduitPortInfo.offset);
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.conduitPortInfo.conduitType);
		this.conduitPort.inputCell = num;
		this.conduitPort.networkItem = new FlowUtilityNetwork.NetworkItem(this.conduitPortInfo.conduitType, Endpoint.Sink, num, base.gameObject);
		networkManager.AddToNetworks(num, this.conduitPort.networkItem, true);
	}

	// Token: 0x06002A23 RID: 10787 RVA: 0x000DECDB File Offset: 0x000DCEDB
	public void RemoveConduitPortFromNetwork()
	{
		if (this.conduitPort == null)
		{
			return;
		}
		Conduit.GetNetworkManager(this.conduitPortInfo.conduitType).RemoveFromNetworks(this.conduitPort.inputCell, this.conduitPort.networkItem, true);
	}

	// Token: 0x06002A24 RID: 10788 RVA: 0x000DED14 File Offset: 0x000DCF14
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.FindPartner();
		base.Subscribe<RocketConduitSender>(-1118736034, RocketConduitSender.TryFindPartnerDelegate);
		base.Subscribe<RocketConduitSender>(546421097, RocketConduitSender.OnLaunchedDelegate);
		base.Subscribe<RocketConduitSender>(-735346771, RocketConduitSender.OnLandedDelegate);
		base.smi.StartSM();
		Components.RocketConduitSenders.Add(this);
	}

	// Token: 0x06002A25 RID: 10789 RVA: 0x000DED76 File Offset: 0x000DCF76
	protected override void OnCleanUp()
	{
		this.RemoveConduitPortFromNetwork();
		base.OnCleanUp();
		Components.RocketConduitSenders.Remove(this);
	}

	// Token: 0x06002A26 RID: 10790 RVA: 0x000DED90 File Offset: 0x000DCF90
	private void FindPartner()
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(base.gameObject.GetMyWorldId());
		if (world != null && world.IsModuleInterior)
		{
			foreach (RocketConduitReceiver rocketConduitReceiver in world.GetComponent<Clustercraft>().ModuleInterface.GetPassengerModule().GetComponents<RocketConduitReceiver>())
			{
				if (rocketConduitReceiver.conduitPortInfo.conduitType == this.conduitPortInfo.conduitType)
				{
					this.partnerReceiver = rocketConduitReceiver;
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
				foreach (RocketConduitReceiver rocketConduitReceiver2 in Components.RocketConduitReceivers.GetWorldItems(targetWorld.id, false))
				{
					if (rocketConduitReceiver2.conduitPortInfo.conduitType == this.conduitPortInfo.conduitType)
					{
						this.partnerReceiver = rocketConduitReceiver2;
						break;
					}
				}
			}
		}
		if (this.partnerReceiver == null)
		{
			global::Debug.LogWarning("No rocket conduit receiver found?");
			return;
		}
		this.conduitPort = new RocketConduitSender.ConduitPort(base.gameObject, this.conduitPortInfo, this.conduitStorage);
		if (world != null)
		{
			this.AddConduitPortToNetwork();
		}
		this.partnerReceiver.SetStorage(this.conduitStorage);
	}

	// Token: 0x06002A27 RID: 10791 RVA: 0x000DEEF8 File Offset: 0x000DD0F8
	bool ISecondaryInput.HasSecondaryConduitType(ConduitType type)
	{
		return this.conduitPortInfo.conduitType == type;
	}

	// Token: 0x06002A28 RID: 10792 RVA: 0x000DEF08 File Offset: 0x000DD108
	CellOffset ISecondaryInput.GetSecondaryConduitOffset(ConduitType type)
	{
		if (this.conduitPortInfo.conduitType == type)
		{
			return this.conduitPortInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x040018E7 RID: 6375
	public Storage conduitStorage;

	// Token: 0x040018E8 RID: 6376
	[SerializeField]
	public ConduitPortInfo conduitPortInfo;

	// Token: 0x040018E9 RID: 6377
	private RocketConduitSender.ConduitPort conduitPort;

	// Token: 0x040018EA RID: 6378
	private RocketConduitReceiver partnerReceiver;

	// Token: 0x040018EB RID: 6379
	private static readonly EventSystem.IntraObjectHandler<RocketConduitSender> TryFindPartnerDelegate = new EventSystem.IntraObjectHandler<RocketConduitSender>(delegate(RocketConduitSender component, object data)
	{
		component.FindPartner();
	});

	// Token: 0x040018EC RID: 6380
	private static readonly EventSystem.IntraObjectHandler<RocketConduitSender> OnLandedDelegate = new EventSystem.IntraObjectHandler<RocketConduitSender>(delegate(RocketConduitSender component, object data)
	{
		component.AddConduitPortToNetwork();
	});

	// Token: 0x040018ED RID: 6381
	private static readonly EventSystem.IntraObjectHandler<RocketConduitSender> OnLaunchedDelegate = new EventSystem.IntraObjectHandler<RocketConduitSender>(delegate(RocketConduitSender component, object data)
	{
		component.RemoveConduitPortFromNetwork();
	});

	// Token: 0x020012CC RID: 4812
	private class ConduitPort
	{
		// Token: 0x06007B94 RID: 31636 RVA: 0x002CC10C File Offset: 0x002CA30C
		public ConduitPort(GameObject parent, ConduitPortInfo info, Storage targetStorage)
		{
			this.conduitPortInfo = info;
			ConduitConsumer conduitConsumer = parent.AddComponent<ConduitConsumer>();
			conduitConsumer.conduitType = this.conduitPortInfo.conduitType;
			conduitConsumer.useSecondaryInput = true;
			conduitConsumer.storage = targetStorage;
			conduitConsumer.capacityKG = targetStorage.capacityKg;
			conduitConsumer.alwaysConsume = true;
			conduitConsumer.forceAlwaysSatisfied = true;
			this.conduitConsumer = conduitConsumer;
			this.conduitConsumer.keepZeroMassObject = false;
		}

		// Token: 0x04005EC1 RID: 24257
		public ConduitPortInfo conduitPortInfo;

		// Token: 0x04005EC2 RID: 24258
		public int inputCell;

		// Token: 0x04005EC3 RID: 24259
		public FlowUtilityNetwork.NetworkItem networkItem;

		// Token: 0x04005EC4 RID: 24260
		private ConduitConsumer conduitConsumer;
	}

	// Token: 0x020012CD RID: 4813
	public class StatesInstance : GameStateMachine<RocketConduitSender.States, RocketConduitSender.StatesInstance, RocketConduitSender, object>.GameInstance
	{
		// Token: 0x06007B95 RID: 31637 RVA: 0x002CC179 File Offset: 0x002CA379
		public StatesInstance(RocketConduitSender smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020012CE RID: 4814
	public class States : GameStateMachine<RocketConduitSender.States, RocketConduitSender.StatesInstance, RocketConduitSender>
	{
		// Token: 0x06007B96 RID: 31638 RVA: 0x002CC184 File Offset: 0x002CA384
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.on;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.on.DefaultState(this.on.waiting);
			this.on.waiting.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Normal, null).EventTransition(GameHashes.OnStorageChange, this.on.working, (RocketConduitSender.StatesInstance smi) => smi.GetComponent<Storage>().MassStored() > 0f);
			this.on.working.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Working, null).DefaultState(this.on.working.ground);
			this.on.working.notOnGround.Enter(delegate(RocketConduitSender.StatesInstance smi)
			{
				smi.gameObject.GetSMI<AutoStorageDropper.Instance>().SetInvertElementFilter(true);
			}).UpdateTransition(this.on.working.ground, delegate(RocketConduitSender.StatesInstance smi, float f)
			{
				WorldContainer myWorld = smi.master.GetMyWorld();
				return myWorld && myWorld.IsModuleInterior && !myWorld.GetComponent<Clustercraft>().ModuleInterface.GetPassengerModule().HasTag(GameTags.RocketNotOnGround);
			}, UpdateRate.SIM_200ms, false).Exit(delegate(RocketConduitSender.StatesInstance smi)
			{
				if (smi.gameObject != null)
				{
					AutoStorageDropper.Instance smi2 = smi.gameObject.GetSMI<AutoStorageDropper.Instance>();
					if (smi2 != null)
					{
						smi2.SetInvertElementFilter(false);
					}
				}
			});
			this.on.working.ground.Enter(delegate(RocketConduitSender.StatesInstance smi)
			{
				smi.master.partnerReceiver.conduitPort.conduitDispenser.alwaysDispense = true;
			}).UpdateTransition(this.on.working.notOnGround, delegate(RocketConduitSender.StatesInstance smi, float f)
			{
				WorldContainer myWorld2 = smi.master.GetMyWorld();
				return myWorld2 && myWorld2.IsModuleInterior && myWorld2.GetComponent<Clustercraft>().ModuleInterface.GetPassengerModule().HasTag(GameTags.RocketNotOnGround);
			}, UpdateRate.SIM_200ms, false).Exit(delegate(RocketConduitSender.StatesInstance smi)
			{
				smi.master.partnerReceiver.conduitPort.conduitDispenser.alwaysDispense = false;
			});
		}

		// Token: 0x04005EC5 RID: 24261
		public RocketConduitSender.States.onStates on;

		// Token: 0x02001FED RID: 8173
		public class onStates : GameStateMachine<RocketConduitSender.States, RocketConduitSender.StatesInstance, RocketConduitSender, object>.State
		{
			// Token: 0x04008E59 RID: 36441
			public RocketConduitSender.States.workingStates working;

			// Token: 0x04008E5A RID: 36442
			public GameStateMachine<RocketConduitSender.States, RocketConduitSender.StatesInstance, RocketConduitSender, object>.State waiting;
		}

		// Token: 0x02001FEE RID: 8174
		public class workingStates : GameStateMachine<RocketConduitSender.States, RocketConduitSender.StatesInstance, RocketConduitSender, object>.State
		{
			// Token: 0x04008E5B RID: 36443
			public GameStateMachine<RocketConduitSender.States, RocketConduitSender.StatesInstance, RocketConduitSender, object>.State notOnGround;

			// Token: 0x04008E5C RID: 36444
			public GameStateMachine<RocketConduitSender.States, RocketConduitSender.StatesInstance, RocketConduitSender, object>.State ground;
		}
	}
}

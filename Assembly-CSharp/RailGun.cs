using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200062B RID: 1579
public class RailGun : StateMachineComponent<RailGun.StatesInstance>, ISim200ms, ISecondaryInput
{
	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x0600297B RID: 10619 RVA: 0x000DADC3 File Offset: 0x000D8FC3
	public float MaxLaunchMass
	{
		get
		{
			return 200f;
		}
	}

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x0600297C RID: 10620 RVA: 0x000DADCA File Offset: 0x000D8FCA
	public float EnergyCost
	{
		get
		{
			return base.smi.EnergyCost();
		}
	}

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x0600297D RID: 10621 RVA: 0x000DADD7 File Offset: 0x000D8FD7
	public float CurrentEnergy
	{
		get
		{
			return this.hepStorage.Particles;
		}
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x0600297E RID: 10622 RVA: 0x000DADE4 File Offset: 0x000D8FE4
	public bool AllowLaunchingFromLogic
	{
		get
		{
			return !this.hasLogicWire || (this.hasLogicWire && this.isLogicActive);
		}
	}

	// Token: 0x170002BD RID: 701
	// (get) Token: 0x0600297F RID: 10623 RVA: 0x000DAE00 File Offset: 0x000D9000
	public bool HasLogicWire
	{
		get
		{
			return this.hasLogicWire;
		}
	}

	// Token: 0x170002BE RID: 702
	// (get) Token: 0x06002980 RID: 10624 RVA: 0x000DAE08 File Offset: 0x000D9008
	public bool IsLogicActive
	{
		get
		{
			return this.isLogicActive;
		}
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x000DAE10 File Offset: 0x000D9010
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.destinationSelector = base.GetComponent<ClusterDestinationSelector>();
		this.resourceStorage = base.GetComponent<Storage>();
		this.particleStorage = base.GetComponent<HighEnergyParticleStorage>();
		if (RailGun.noSurfaceSightStatusItem == null)
		{
			RailGun.noSurfaceSightStatusItem = new StatusItem("RAILGUN_PATH_NOT_CLEAR", "BUILDING", "status_item_no_sky", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		}
		if (RailGun.noDestinationStatusItem == null)
		{
			RailGun.noDestinationStatusItem = new StatusItem("RAILGUN_NO_DESTINATION", "BUILDING", "status_item_no_sky", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		}
		this.gasInputCell = Grid.OffsetCell(Grid.PosToCell(this), this.gasPortInfo.offset);
		this.gasConsumer = this.CreateConduitConsumer(ConduitType.Gas, this.gasInputCell, out this.gasNetworkItem);
		this.liquidInputCell = Grid.OffsetCell(Grid.PosToCell(this), this.liquidPortInfo.offset);
		this.liquidConsumer = this.CreateConduitConsumer(ConduitType.Liquid, this.liquidInputCell, out this.liquidNetworkItem);
		this.solidInputCell = Grid.OffsetCell(Grid.PosToCell(this), this.solidPortInfo.offset);
		this.solidConsumer = this.CreateSolidConduitConsumer(this.solidInputCell, out this.solidNetworkItem);
		this.CreateMeters();
		base.smi.StartSM();
		if (RailGun.infoStatusItemLogic == null)
		{
			RailGun.infoStatusItemLogic = new StatusItem("LogicOperationalInfo", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			RailGun.infoStatusItemLogic.resolveStringCallback = new Func<string, object, string>(RailGun.ResolveInfoStatusItemString);
		}
		this.CheckLogicWireState();
		base.Subscribe<RailGun>(-801688580, RailGun.OnLogicValueChangedDelegate);
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x000DAFB0 File Offset: 0x000D91B0
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.liquidPortInfo.conduitType).RemoveFromNetworks(this.liquidInputCell, this.liquidNetworkItem, true);
		Conduit.GetNetworkManager(this.gasPortInfo.conduitType).RemoveFromNetworks(this.gasInputCell, this.gasNetworkItem, true);
		Game.Instance.solidConduitSystem.RemoveFromNetworks(this.solidInputCell, this.solidConsumer, true);
		base.OnCleanUp();
	}

	// Token: 0x06002983 RID: 10627 RVA: 0x000DB024 File Offset: 0x000D9224
	private void CreateMeters()
	{
		this.resourceMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_storage_target", "meter_storage", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		this.particleMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_orb_target", "meter_orb", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
	}

	// Token: 0x06002984 RID: 10628 RVA: 0x000DB077 File Offset: 0x000D9277
	bool ISecondaryInput.HasSecondaryConduitType(ConduitType type)
	{
		return this.liquidPortInfo.conduitType == type || this.gasPortInfo.conduitType == type || this.solidPortInfo.conduitType == type;
	}

	// Token: 0x06002985 RID: 10629 RVA: 0x000DB0A8 File Offset: 0x000D92A8
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (this.liquidPortInfo.conduitType == type)
		{
			return this.liquidPortInfo.offset;
		}
		if (this.gasPortInfo.conduitType == type)
		{
			return this.gasPortInfo.offset;
		}
		if (this.solidPortInfo.conduitType == type)
		{
			return this.solidPortInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x06002986 RID: 10630 RVA: 0x000DB108 File Offset: 0x000D9308
	private LogicCircuitNetwork GetNetwork()
	{
		int portCell = base.GetComponent<LogicPorts>().GetPortCell(RailGun.PORT_ID);
		return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
	}

	// Token: 0x06002987 RID: 10631 RVA: 0x000DB138 File Offset: 0x000D9338
	private void CheckLogicWireState()
	{
		LogicCircuitNetwork network = this.GetNetwork();
		this.hasLogicWire = network != null;
		int num = ((network != null) ? network.OutputValue : 1);
		bool flag = LogicCircuitNetwork.IsBitActive(0, num);
		this.isLogicActive = flag;
		base.smi.sm.allowedFromLogic.Set(this.AllowLaunchingFromLogic, base.smi, false);
		base.GetComponent<KSelectable>().ToggleStatusItem(RailGun.infoStatusItemLogic, network != null, this);
	}

	// Token: 0x06002988 RID: 10632 RVA: 0x000DB1AB File Offset: 0x000D93AB
	private void OnLogicValueChanged(object data)
	{
		if (((LogicValueChanged)data).portID == RailGun.PORT_ID)
		{
			this.CheckLogicWireState();
		}
	}

	// Token: 0x06002989 RID: 10633 RVA: 0x000DB1CA File Offset: 0x000D93CA
	private static string ResolveInfoStatusItemString(string format_str, object data)
	{
		RailGun railGun = (RailGun)data;
		Operational operational = railGun.operational;
		return railGun.AllowLaunchingFromLogic ? BUILDING.STATUSITEMS.LOGIC.LOGIC_CONTROLLED_ENABLED : BUILDING.STATUSITEMS.LOGIC.LOGIC_CONTROLLED_DISABLED;
	}

	// Token: 0x0600298A RID: 10634 RVA: 0x000DB1F4 File Offset: 0x000D93F4
	public void Sim200ms(float dt)
	{
		WorldContainer myWorld = this.GetMyWorld();
		Extents extents = base.GetComponent<Building>().GetExtents();
		int x = extents.x;
		int num = extents.x + extents.width - 2;
		int num2 = extents.y + extents.height;
		int num3 = Grid.XYToCell(x, num2);
		int num4 = Grid.XYToCell(num, num2);
		bool flag = true;
		int num5 = (int)myWorld.maximumBounds.y;
		for (int i = num3; i <= num4; i++)
		{
			int num6 = i;
			while (Grid.CellRow(num6) <= num5)
			{
				if (!Grid.IsValidCell(num6) || Grid.Solid[num6])
				{
					flag = false;
				}
				num6 = Grid.CellAbove(num6);
			}
		}
		this.operational.SetFlag(RailGun.noSurfaceSight, flag);
		this.operational.SetFlag(RailGun.noDestination, this.destinationSelector.GetDestinationWorld() >= 0);
		KSelectable component = base.GetComponent<KSelectable>();
		component.ToggleStatusItem(RailGun.noSurfaceSightStatusItem, !flag, null);
		component.ToggleStatusItem(RailGun.noDestinationStatusItem, this.destinationSelector.GetDestinationWorld() < 0, null);
		this.UpdateMeters();
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x000DB308 File Offset: 0x000D9508
	private void UpdateMeters()
	{
		this.resourceMeter.SetPositionPercent(Mathf.Clamp01(this.resourceStorage.MassStored() / this.resourceStorage.capacityKg));
		this.particleMeter.SetPositionPercent(Mathf.Clamp01(this.particleStorage.Particles / this.particleStorage.capacity));
	}

	// Token: 0x0600298C RID: 10636 RVA: 0x000DB364 File Offset: 0x000D9564
	private void LaunchProjectile()
	{
		Extents extents = base.GetComponent<Building>().GetExtents();
		Vector2I vector2I = Grid.PosToXY(base.transform.position);
		vector2I.y += extents.height + 1;
		int num = Grid.XYToCell(vector2I.x, vector2I.y);
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("RailGunPayload"), Grid.CellToPosCBC(num, Grid.SceneLayer.Front));
		float num2 = 0f;
		while (num2 < this.launchMass && this.resourceStorage.MassStored() > 0f)
		{
			num2 += this.resourceStorage.Transfer(gameObject.GetComponent<Storage>(), GameTags.Stored, this.launchMass - num2, false, true);
		}
		this.particleStorage.ConsumeAndGet(base.smi.EnergyCost());
		gameObject.SetActive(true);
		if (this.destinationSelector.GetDestinationWorld() >= 0)
		{
			RailGunPayload.StatesInstance smi = gameObject.GetSMI<RailGunPayload.StatesInstance>();
			smi.takeoffVelocity = 35f;
			smi.StartSM();
			smi.Launch(base.gameObject.GetMyWorldLocation(), this.destinationSelector.GetDestination());
		}
	}

	// Token: 0x0600298D RID: 10637 RVA: 0x000DB479 File Offset: 0x000D9679
	private ConduitConsumer CreateConduitConsumer(ConduitType inputType, int inputCell, out FlowUtilityNetwork.NetworkItem flowNetworkItem)
	{
		ConduitConsumer conduitConsumer = base.gameObject.AddComponent<ConduitConsumer>();
		conduitConsumer.conduitType = inputType;
		conduitConsumer.useSecondaryInput = true;
		IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(inputType);
		flowNetworkItem = new FlowUtilityNetwork.NetworkItem(inputType, Endpoint.Sink, inputCell, base.gameObject);
		networkManager.AddToNetworks(inputCell, flowNetworkItem, true);
		return conduitConsumer;
	}

	// Token: 0x0600298E RID: 10638 RVA: 0x000DB4B3 File Offset: 0x000D96B3
	private SolidConduitConsumer CreateSolidConduitConsumer(int inputCell, out FlowUtilityNetwork.NetworkItem flowNetworkItem)
	{
		SolidConduitConsumer solidConduitConsumer = base.gameObject.AddComponent<SolidConduitConsumer>();
		solidConduitConsumer.useSecondaryInput = true;
		flowNetworkItem = new FlowUtilityNetwork.NetworkItem(ConduitType.Solid, Endpoint.Sink, inputCell, base.gameObject);
		Game.Instance.solidConduitSystem.AddToNetworks(inputCell, flowNetworkItem, true);
		return solidConduitConsumer;
	}

	// Token: 0x04001871 RID: 6257
	[Serialize]
	public float launchMass = 200f;

	// Token: 0x04001872 RID: 6258
	public float MinLaunchMass = 2f;

	// Token: 0x04001873 RID: 6259
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001874 RID: 6260
	[MyCmpGet]
	private KAnimControllerBase kac;

	// Token: 0x04001875 RID: 6261
	[MyCmpGet]
	public HighEnergyParticleStorage hepStorage;

	// Token: 0x04001876 RID: 6262
	public Storage resourceStorage;

	// Token: 0x04001877 RID: 6263
	private MeterController resourceMeter;

	// Token: 0x04001878 RID: 6264
	private HighEnergyParticleStorage particleStorage;

	// Token: 0x04001879 RID: 6265
	private MeterController particleMeter;

	// Token: 0x0400187A RID: 6266
	private ClusterDestinationSelector destinationSelector;

	// Token: 0x0400187B RID: 6267
	public static readonly Operational.Flag noSurfaceSight = new Operational.Flag("noSurfaceSight", Operational.Flag.Type.Requirement);

	// Token: 0x0400187C RID: 6268
	private static StatusItem noSurfaceSightStatusItem;

	// Token: 0x0400187D RID: 6269
	public static readonly Operational.Flag noDestination = new Operational.Flag("noDestination", Operational.Flag.Type.Requirement);

	// Token: 0x0400187E RID: 6270
	private static StatusItem noDestinationStatusItem;

	// Token: 0x0400187F RID: 6271
	[SerializeField]
	public ConduitPortInfo liquidPortInfo;

	// Token: 0x04001880 RID: 6272
	private int liquidInputCell = -1;

	// Token: 0x04001881 RID: 6273
	private FlowUtilityNetwork.NetworkItem liquidNetworkItem;

	// Token: 0x04001882 RID: 6274
	private ConduitConsumer liquidConsumer;

	// Token: 0x04001883 RID: 6275
	[SerializeField]
	public ConduitPortInfo gasPortInfo;

	// Token: 0x04001884 RID: 6276
	private int gasInputCell = -1;

	// Token: 0x04001885 RID: 6277
	private FlowUtilityNetwork.NetworkItem gasNetworkItem;

	// Token: 0x04001886 RID: 6278
	private ConduitConsumer gasConsumer;

	// Token: 0x04001887 RID: 6279
	[SerializeField]
	public ConduitPortInfo solidPortInfo;

	// Token: 0x04001888 RID: 6280
	private int solidInputCell = -1;

	// Token: 0x04001889 RID: 6281
	private FlowUtilityNetwork.NetworkItem solidNetworkItem;

	// Token: 0x0400188A RID: 6282
	private SolidConduitConsumer solidConsumer;

	// Token: 0x0400188B RID: 6283
	public static readonly HashedString PORT_ID = "LogicLaunching";

	// Token: 0x0400188C RID: 6284
	private bool hasLogicWire;

	// Token: 0x0400188D RID: 6285
	private bool isLogicActive;

	// Token: 0x0400188E RID: 6286
	private static StatusItem infoStatusItemLogic;

	// Token: 0x0400188F RID: 6287
	public bool FreeStartHex;

	// Token: 0x04001890 RID: 6288
	public bool FreeDestinationHex;

	// Token: 0x04001891 RID: 6289
	private static readonly EventSystem.IntraObjectHandler<RailGun> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<RailGun>(delegate(RailGun component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x020012AA RID: 4778
	public class StatesInstance : GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.GameInstance
	{
		// Token: 0x06007B03 RID: 31491 RVA: 0x002C9C46 File Offset: 0x002C7E46
		public StatesInstance(RailGun smi)
			: base(smi)
		{
		}

		// Token: 0x06007B04 RID: 31492 RVA: 0x002C9C4F File Offset: 0x002C7E4F
		public bool HasResources()
		{
			return base.smi.master.resourceStorage.MassStored() >= base.smi.master.launchMass;
		}

		// Token: 0x06007B05 RID: 31493 RVA: 0x002C9C7B File Offset: 0x002C7E7B
		public bool HasEnergy()
		{
			return base.smi.master.particleStorage.Particles > this.EnergyCost();
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x002C9C9A File Offset: 0x002C7E9A
		public bool HasDestination()
		{
			return base.smi.master.destinationSelector.GetDestinationWorld() != base.smi.master.GetMyWorldId();
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x002C9CC6 File Offset: 0x002C7EC6
		public bool IsDestinationReachable(bool forceRefresh = false)
		{
			if (forceRefresh)
			{
				this.UpdatePath();
			}
			return base.smi.master.destinationSelector.GetDestinationWorld() != base.smi.master.GetMyWorldId() && this.PathLength() != -1;
		}

		// Token: 0x06007B08 RID: 31496 RVA: 0x002C9D08 File Offset: 0x002C7F08
		public int PathLength()
		{
			if (base.smi.m_cachedPath == null)
			{
				this.UpdatePath();
			}
			if (base.smi.m_cachedPath == null)
			{
				return -1;
			}
			int num = base.smi.m_cachedPath.Count;
			if (base.master.FreeStartHex)
			{
				num--;
			}
			if (base.master.FreeDestinationHex)
			{
				num--;
			}
			return num;
		}

		// Token: 0x06007B09 RID: 31497 RVA: 0x002C9D6C File Offset: 0x002C7F6C
		public void UpdatePath()
		{
			this.m_cachedPath = ClusterGrid.Instance.GetPath(base.gameObject.GetMyWorldLocation(), base.smi.master.destinationSelector.GetDestination(), base.smi.master.destinationSelector);
		}

		// Token: 0x06007B0A RID: 31498 RVA: 0x002C9DB9 File Offset: 0x002C7FB9
		public float EnergyCost()
		{
			return Mathf.Max(0f, 0f + (float)this.PathLength() * 10f);
		}

		// Token: 0x06007B0B RID: 31499 RVA: 0x002C9DD8 File Offset: 0x002C7FD8
		public bool MayTurnOn()
		{
			return this.HasEnergy() && this.IsDestinationReachable(false) && base.master.operational.IsOperational && base.sm.allowedFromLogic.Get(this);
		}

		// Token: 0x04005E4C RID: 24140
		public const int INVALID_PATH_LENGTH = -1;

		// Token: 0x04005E4D RID: 24141
		private List<AxialI> m_cachedPath;
	}

	// Token: 0x020012AB RID: 4779
	public class States : GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun>
	{
		// Token: 0x06007B0C RID: 31500 RVA: 0x002C9E10 File Offset: 0x002C8010
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			this.root.EventHandler(GameHashes.ClusterDestinationChanged, delegate(RailGun.StatesInstance smi)
			{
				smi.UpdatePath();
			});
			this.off.PlayAnim("off").EventTransition(GameHashes.OnParticleStorageChanged, this.on, (RailGun.StatesInstance smi) => smi.MayTurnOn()).EventTransition(GameHashes.ClusterDestinationChanged, this.on, (RailGun.StatesInstance smi) => smi.MayTurnOn())
				.EventTransition(GameHashes.OperationalChanged, this.on, (RailGun.StatesInstance smi) => smi.MayTurnOn())
				.ParamTransition<bool>(this.allowedFromLogic, this.on, (RailGun.StatesInstance smi, bool p) => smi.MayTurnOn());
			this.on.DefaultState(this.on.power_on).EventTransition(GameHashes.OperationalChanged, this.on.power_off, (RailGun.StatesInstance smi) => !smi.master.operational.IsOperational).EventTransition(GameHashes.ClusterDestinationChanged, this.on.power_off, (RailGun.StatesInstance smi) => !smi.IsDestinationReachable(false))
				.EventTransition(GameHashes.ClusterFogOfWarRevealed, (RailGun.StatesInstance smi) => Game.Instance, this.on.power_off, (RailGun.StatesInstance smi) => !smi.IsDestinationReachable(true))
				.EventTransition(GameHashes.OnParticleStorageChanged, this.on.power_off, (RailGun.StatesInstance smi) => !smi.MayTurnOn())
				.ParamTransition<bool>(this.allowedFromLogic, this.on.power_off, (RailGun.StatesInstance smi, bool p) => !p)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Normal, null);
			this.on.power_on.PlayAnim("power_on").OnAnimQueueComplete(this.on.wait_for_storage);
			this.on.power_off.PlayAnim("power_off").OnAnimQueueComplete(this.off);
			this.on.wait_for_storage.PlayAnim("on", KAnim.PlayMode.Loop).EventTransition(GameHashes.ClusterDestinationChanged, this.on.power_off, (RailGun.StatesInstance smi) => !smi.HasEnergy()).EventTransition(GameHashes.OnStorageChange, this.on.working, (RailGun.StatesInstance smi) => smi.HasResources() && smi.sm.cooldownTimer.Get(smi) <= 0f)
				.EventTransition(GameHashes.OperationalChanged, this.on.working, (RailGun.StatesInstance smi) => smi.HasResources() && smi.sm.cooldownTimer.Get(smi) <= 0f)
				.ParamTransition<float>(this.cooldownTimer, this.on.cooldown, (RailGun.StatesInstance smi, float p) => p > 0f);
			this.on.working.DefaultState(this.on.working.pre).Enter(delegate(RailGun.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit(delegate(RailGun.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			});
			this.on.working.pre.PlayAnim("working_pre").OnAnimQueueComplete(this.on.working.loop);
			this.on.working.loop.PlayAnim("working_loop").OnAnimQueueComplete(this.on.working.fire);
			this.on.working.fire.Enter(delegate(RailGun.StatesInstance smi)
			{
				if (smi.IsDestinationReachable(false))
				{
					smi.master.LaunchProjectile();
					smi.sm.payloadsFiredSinceCooldown.Delta(1, smi);
					if (smi.sm.payloadsFiredSinceCooldown.Get(smi) >= 6)
					{
						smi.sm.cooldownTimer.Set(30f, smi, false);
					}
				}
			}).GoTo(this.on.working.bounce);
			this.on.working.bounce.ParamTransition<float>(this.cooldownTimer, this.on.working.pst, (RailGun.StatesInstance smi, float p) => p > 0f || !smi.HasResources()).ParamTransition<int>(this.payloadsFiredSinceCooldown, this.on.working.loop, (RailGun.StatesInstance smi, int p) => p < 6 && smi.HasResources());
			this.on.working.pst.PlayAnim("working_pst").OnAnimQueueComplete(this.on.wait_for_storage);
			this.on.cooldown.DefaultState(this.on.cooldown.pre).ToggleMainStatusItem(Db.Get().BuildingStatusItems.RailGunCooldown, null);
			this.on.cooldown.pre.PlayAnim("cooldown_pre").OnAnimQueueComplete(this.on.cooldown.loop);
			this.on.cooldown.loop.PlayAnim("cooldown_loop", KAnim.PlayMode.Loop).ParamTransition<float>(this.cooldownTimer, this.on.cooldown.pst, (RailGun.StatesInstance smi, float p) => p <= 0f).Update(delegate(RailGun.StatesInstance smi, float dt)
			{
				this.cooldownTimer.Delta(-dt, smi);
			}, UpdateRate.SIM_1000ms, false);
			this.on.cooldown.pst.PlayAnim("cooldown_pst").OnAnimQueueComplete(this.on.wait_for_storage).Exit(delegate(RailGun.StatesInstance smi)
			{
				smi.sm.payloadsFiredSinceCooldown.Set(0, smi, false);
			});
		}

		// Token: 0x04005E4E RID: 24142
		public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State off;

		// Token: 0x04005E4F RID: 24143
		public RailGun.States.OnStates on;

		// Token: 0x04005E50 RID: 24144
		public StateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.FloatParameter cooldownTimer;

		// Token: 0x04005E51 RID: 24145
		public StateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.IntParameter payloadsFiredSinceCooldown;

		// Token: 0x04005E52 RID: 24146
		public StateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.BoolParameter allowedFromLogic;

		// Token: 0x04005E53 RID: 24147
		public StateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.BoolParameter updatePath;

		// Token: 0x02001FE1 RID: 8161
		public class WorkingStates : GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State
		{
			// Token: 0x04008E0D RID: 36365
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State pre;

			// Token: 0x04008E0E RID: 36366
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State loop;

			// Token: 0x04008E0F RID: 36367
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State fire;

			// Token: 0x04008E10 RID: 36368
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State bounce;

			// Token: 0x04008E11 RID: 36369
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State pst;
		}

		// Token: 0x02001FE2 RID: 8162
		public class CooldownStates : GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State
		{
			// Token: 0x04008E12 RID: 36370
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State pre;

			// Token: 0x04008E13 RID: 36371
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State loop;

			// Token: 0x04008E14 RID: 36372
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State pst;
		}

		// Token: 0x02001FE3 RID: 8163
		public class OnStates : GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State
		{
			// Token: 0x04008E15 RID: 36373
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State power_on;

			// Token: 0x04008E16 RID: 36374
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State wait_for_storage;

			// Token: 0x04008E17 RID: 36375
			public GameStateMachine<RailGun.States, RailGun.StatesInstance, RailGun, object>.State power_off;

			// Token: 0x04008E18 RID: 36376
			public RailGun.States.WorkingStates working;

			// Token: 0x04008E19 RID: 36377
			public RailGun.States.CooldownStates cooldown;
		}
	}
}

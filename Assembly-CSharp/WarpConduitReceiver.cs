using System;
using UnityEngine;

// Token: 0x0200066A RID: 1642
public class WarpConduitReceiver : StateMachineComponent<WarpConduitReceiver.StatesInstance>, ISecondaryOutput
{
	// Token: 0x06002C43 RID: 11331 RVA: 0x000E8634 File Offset: 0x000E6834
	private bool CanTransferFromSender()
	{
		bool flag = false;
		if ((base.smi.master.senderGasStorage.MassStored() > 0f || base.smi.master.senderGasStorage.items.Count > 0) && base.smi.master.gasPort.dispenser.GetConduitManager().GetPermittedFlow(base.smi.master.gasPort.outputCell) != ConduitFlow.FlowDirections.None)
		{
			flag = true;
		}
		if ((base.smi.master.senderLiquidStorage.MassStored() > 0f || base.smi.master.senderLiquidStorage.items.Count > 0) && base.smi.master.liquidPort.dispenser.GetConduitManager().GetPermittedFlow(base.smi.master.liquidPort.outputCell) != ConduitFlow.FlowDirections.None)
		{
			flag = true;
		}
		if ((base.smi.master.senderSolidStorage.MassStored() > 0f || base.smi.master.senderSolidStorage.items.Count > 0) && base.smi.master.solidPort.dispenser != null && base.smi.master.solidPort.dispenser.GetConduitManager().GetPermittedFlow(base.smi.master.solidPort.outputCell) != ConduitFlow.FlowDirections.None)
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x06002C44 RID: 11332 RVA: 0x000E87B4 File Offset: 0x000E69B4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.FindPartner();
		base.smi.StartSM();
	}

	// Token: 0x06002C45 RID: 11333 RVA: 0x000E87D0 File Offset: 0x000E69D0
	private void FindPartner()
	{
		if (this.senderGasStorage != null)
		{
			return;
		}
		WarpConduitSender warpConduitSender = null;
		SaveGame.Instance.GetComponent<WorldGenSpawner>().SpawnTag("WarpConduitSender");
		foreach (WarpConduitSender warpConduitSender2 in UnityEngine.Object.FindObjectsOfType<WarpConduitSender>())
		{
			if (warpConduitSender2.GetMyWorldId() != this.GetMyWorldId())
			{
				warpConduitSender = warpConduitSender2;
				break;
			}
		}
		if (warpConduitSender == null)
		{
			global::Debug.LogWarning("No warp conduit sender found - maybe POI stomping or failure to spawn?");
			return;
		}
		this.SetStorage(warpConduitSender.gasStorage, warpConduitSender.liquidStorage, warpConduitSender.solidStorage);
		WarpConduitStatus.UpdateWarpConduitsOperational(warpConduitSender.gameObject, base.gameObject);
	}

	// Token: 0x06002C46 RID: 11334 RVA: 0x000E886C File Offset: 0x000E6A6C
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.liquidPortInfo.conduitType).RemoveFromNetworks(this.liquidPort.outputCell, this.liquidPort.networkItem, true);
		if (this.gasPort.portInfo != null)
		{
			Conduit.GetNetworkManager(this.gasPort.portInfo.conduitType).RemoveFromNetworks(this.gasPort.outputCell, this.gasPort.networkItem, true);
		}
		else
		{
			global::Debug.LogWarning("Conduit Receiver gasPort portInfo is null in OnCleanUp");
		}
		Game.Instance.solidConduitSystem.RemoveFromNetworks(this.solidPort.outputCell, this.solidPort.networkItem, true);
		base.OnCleanUp();
	}

	// Token: 0x06002C47 RID: 11335 RVA: 0x000E891B File Offset: 0x000E6B1B
	public void OnActivatedChanged(object data)
	{
		if (this.senderGasStorage == null)
		{
			this.FindPartner();
		}
		WarpConduitStatus.UpdateWarpConduitsOperational((this.senderGasStorage != null) ? this.senderGasStorage.gameObject : null, base.gameObject);
	}

	// Token: 0x06002C48 RID: 11336 RVA: 0x000E8958 File Offset: 0x000E6B58
	public void SetStorage(Storage gasStorage, Storage liquidStorage, Storage solidStorage)
	{
		this.senderGasStorage = gasStorage;
		this.senderLiquidStorage = liquidStorage;
		this.senderSolidStorage = solidStorage;
		this.gasPort.SetPortInfo(base.gameObject, this.gasPortInfo, gasStorage, 1);
		this.liquidPort.SetPortInfo(base.gameObject, this.liquidPortInfo, liquidStorage, 2);
		this.solidPort.SetPortInfo(base.gameObject, this.solidPortInfo, solidStorage, 3);
		Vector3 position = this.liquidPort.airlock.gameObject.transform.position;
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().transform.position = position + new Vector3(0f, 0f, -0.1f);
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().enabled = false;
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().enabled = true;
	}

	// Token: 0x06002C49 RID: 11337 RVA: 0x000E8A4F File Offset: 0x000E6C4F
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return type == this.gasPortInfo.conduitType || type == this.liquidPortInfo.conduitType || type == this.solidPortInfo.conduitType;
	}

	// Token: 0x06002C4A RID: 11338 RVA: 0x000E8A80 File Offset: 0x000E6C80
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (type == this.gasPortInfo.conduitType)
		{
			return this.gasPortInfo.offset;
		}
		if (type == this.liquidPortInfo.conduitType)
		{
			return this.liquidPortInfo.offset;
		}
		if (type == this.solidPortInfo.conduitType)
		{
			return this.solidPortInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x04001A50 RID: 6736
	[SerializeField]
	public ConduitPortInfo liquidPortInfo;

	// Token: 0x04001A51 RID: 6737
	private WarpConduitReceiver.ConduitPort liquidPort;

	// Token: 0x04001A52 RID: 6738
	[SerializeField]
	public ConduitPortInfo solidPortInfo;

	// Token: 0x04001A53 RID: 6739
	private WarpConduitReceiver.ConduitPort solidPort;

	// Token: 0x04001A54 RID: 6740
	[SerializeField]
	public ConduitPortInfo gasPortInfo;

	// Token: 0x04001A55 RID: 6741
	private WarpConduitReceiver.ConduitPort gasPort;

	// Token: 0x04001A56 RID: 6742
	public Storage senderGasStorage;

	// Token: 0x04001A57 RID: 6743
	public Storage senderLiquidStorage;

	// Token: 0x04001A58 RID: 6744
	public Storage senderSolidStorage;

	// Token: 0x0200132E RID: 4910
	public struct ConduitPort
	{
		// Token: 0x06007CEB RID: 31979 RVA: 0x002D1ED0 File Offset: 0x002D00D0
		public void SetPortInfo(GameObject parent, ConduitPortInfo info, Storage senderStorage, int number)
		{
			this.portInfo = info;
			this.outputCell = Grid.OffsetCell(Grid.PosToCell(parent), this.portInfo.offset);
			if (this.portInfo.conduitType != ConduitType.Solid)
			{
				ConduitDispenser conduitDispenser = parent.AddComponent<ConduitDispenser>();
				conduitDispenser.conduitType = this.portInfo.conduitType;
				conduitDispenser.useSecondaryOutput = true;
				conduitDispenser.alwaysDispense = true;
				conduitDispenser.storage = senderStorage;
				this.dispenser = conduitDispenser;
				IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.portInfo.conduitType);
				this.networkItem = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Source, this.outputCell, parent);
				networkManager.AddToNetworks(this.outputCell, this.networkItem, true);
			}
			else
			{
				SolidConduitDispenser solidConduitDispenser = parent.AddComponent<SolidConduitDispenser>();
				solidConduitDispenser.storage = senderStorage;
				solidConduitDispenser.alwaysDispense = true;
				solidConduitDispenser.useSecondaryOutput = true;
				solidConduitDispenser.solidOnly = true;
				this.networkItem = new FlowUtilityNetwork.NetworkItem(ConduitType.Solid, Endpoint.Source, this.outputCell, parent);
				Game.Instance.solidConduitSystem.AddToNetworks(this.outputCell, this.networkItem, true);
			}
			string text = "airlock_" + number.ToString();
			string text2 = "airlock_target_" + number.ToString();
			this.pre = "airlock_" + number.ToString() + "_pre";
			this.loop = "airlock_" + number.ToString() + "_loop";
			this.pst = "airlock_" + number.ToString() + "_pst";
			this.airlock = new MeterController(parent.GetComponent<KBatchedAnimController>(), text2, text, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { text2 });
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x002D2070 File Offset: 0x002D0270
		public void UpdatePortAnim()
		{
			bool flag;
			if (this.portInfo != null && this.portInfo.conduitType == ConduitType.Solid)
			{
				flag = this.networkItem.GameObject.GetComponent<SolidConduitDispenser>().IsDispensing;
			}
			else
			{
				flag = this.dispenser != null && !this.dispenser.blocked && !this.dispenser.empty;
				flag = flag && this.dispenser.GetConduitManager().GetPermittedFlow(this.outputCell) > ConduitFlow.FlowDirections.None;
			}
			if (flag != this.open)
			{
				this.open = flag;
				if (this.open)
				{
					this.airlock.meterController.Play(this.pre, KAnim.PlayMode.Once, 1f, 0f);
					this.airlock.meterController.Queue(this.loop, KAnim.PlayMode.Loop, 1f, 0f);
					return;
				}
				this.airlock.meterController.Play(this.pst, KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x04005FC2 RID: 24514
		public ConduitPortInfo portInfo;

		// Token: 0x04005FC3 RID: 24515
		public int outputCell;

		// Token: 0x04005FC4 RID: 24516
		public FlowUtilityNetwork.NetworkItem networkItem;

		// Token: 0x04005FC5 RID: 24517
		public ConduitDispenser dispenser;

		// Token: 0x04005FC6 RID: 24518
		public MeterController airlock;

		// Token: 0x04005FC7 RID: 24519
		private bool open;

		// Token: 0x04005FC8 RID: 24520
		private string pre;

		// Token: 0x04005FC9 RID: 24521
		private string loop;

		// Token: 0x04005FCA RID: 24522
		private string pst;
	}

	// Token: 0x0200132F RID: 4911
	public class StatesInstance : GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.GameInstance
	{
		// Token: 0x06007CED RID: 31981 RVA: 0x002D218B File Offset: 0x002D038B
		public StatesInstance(WarpConduitReceiver master)
			: base(master)
		{
		}
	}

	// Token: 0x02001330 RID: 4912
	public class States : GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver>
	{
		// Token: 0x06007CEE RID: 31982 RVA: 0x002D2194 File Offset: 0x002D0394
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.EventHandler(GameHashes.BuildingActivated, delegate(WarpConduitReceiver.StatesInstance smi, object data)
			{
				smi.master.OnActivatedChanged(data);
			});
			this.off.PlayAnim("off").Enter(delegate(WarpConduitReceiver.StatesInstance smi)
			{
				smi.master.gasPort.UpdatePortAnim();
				smi.master.liquidPort.UpdatePortAnim();
				smi.master.solidPort.UpdatePortAnim();
			}).EventTransition(GameHashes.OperationalFlagChanged, this.on, (WarpConduitReceiver.StatesInstance smi) => smi.GetComponent<Operational>().GetFlag(WarpConduitStatus.warpConnectedFlag));
			this.on.DefaultState(this.on.empty).Update(delegate(WarpConduitReceiver.StatesInstance smi, float dt)
			{
				smi.master.gasPort.UpdatePortAnim();
				smi.master.liquidPort.UpdatePortAnim();
				smi.master.solidPort.UpdatePortAnim();
			}, UpdateRate.SIM_200ms, false);
			this.on.empty.QueueAnim("idle", false, null).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Normal, null).Update(delegate(WarpConduitReceiver.StatesInstance smi, float dt)
			{
				if (smi.master.CanTransferFromSender())
				{
					smi.GoTo(this.on.hasResources);
				}
			}, UpdateRate.SIM_200ms, false);
			this.on.hasResources.PlayAnim("working_pre").QueueAnim("working_loop", true, null).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Working, null)
				.Update(delegate(WarpConduitReceiver.StatesInstance smi, float dt)
				{
					if (!smi.master.CanTransferFromSender())
					{
						smi.GoTo(this.on.empty);
					}
				}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(WarpConduitReceiver.StatesInstance smi)
				{
					smi.Play("working_pst", KAnim.PlayMode.Once);
				});
		}

		// Token: 0x04005FCB RID: 24523
		public GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.State off;

		// Token: 0x04005FCC RID: 24524
		public WarpConduitReceiver.States.onStates on;

		// Token: 0x02002018 RID: 8216
		public class onStates : GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.State
		{
			// Token: 0x04008F33 RID: 36659
			public GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.State hasResources;

			// Token: 0x04008F34 RID: 36660
			public GameStateMachine<WarpConduitReceiver.States, WarpConduitReceiver.StatesInstance, WarpConduitReceiver, object>.State empty;
		}
	}
}

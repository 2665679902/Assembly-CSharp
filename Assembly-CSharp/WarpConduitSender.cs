using System;
using UnityEngine;

// Token: 0x0200066C RID: 1644
public class WarpConduitSender : StateMachineComponent<WarpConduitSender.StatesInstance>, ISecondaryInput
{
	// Token: 0x06002C4E RID: 11342 RVA: 0x000E8C3C File Offset: 0x000E6E3C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Storage[] components = base.GetComponents<Storage>();
		this.gasStorage = components[0];
		this.liquidStorage = components[1];
		this.solidStorage = components[2];
		this.gasPort = new WarpConduitSender.ConduitPort(base.gameObject, this.gasPortInfo, 1, this.gasStorage);
		this.liquidPort = new WarpConduitSender.ConduitPort(base.gameObject, this.liquidPortInfo, 2, this.liquidStorage);
		this.solidPort = new WarpConduitSender.ConduitPort(base.gameObject, this.solidPortInfo, 3, this.solidStorage);
		Vector3 position = this.liquidPort.airlock.gameObject.transform.position;
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().transform.position = position + new Vector3(0f, 0f, -0.1f);
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().enabled = false;
		this.liquidPort.airlock.gameObject.GetComponent<KBatchedAnimController>().enabled = true;
		this.FindPartner();
		WarpConduitStatus.UpdateWarpConduitsOperational(base.gameObject, (this.receiver != null) ? this.receiver.gameObject : null);
		base.smi.StartSM();
	}

	// Token: 0x06002C4F RID: 11343 RVA: 0x000E8D8D File Offset: 0x000E6F8D
	public void OnActivatedChanged(object data)
	{
		WarpConduitStatus.UpdateWarpConduitsOperational(base.gameObject, (this.receiver != null) ? this.receiver.gameObject : null);
	}

	// Token: 0x06002C50 RID: 11344 RVA: 0x000E8DB8 File Offset: 0x000E6FB8
	private void FindPartner()
	{
		SaveGame.Instance.GetComponent<WorldGenSpawner>().SpawnTag("WarpConduitReceiver");
		foreach (WarpConduitReceiver warpConduitReceiver in UnityEngine.Object.FindObjectsOfType<WarpConduitReceiver>())
		{
			if (warpConduitReceiver.GetMyWorldId() != this.GetMyWorldId())
			{
				this.receiver = warpConduitReceiver;
				break;
			}
		}
		if (this.receiver == null)
		{
			global::Debug.LogWarning("No warp conduit receiver found - maybe POI stomping or failure to spawn?");
			return;
		}
		this.receiver.SetStorage(this.gasStorage, this.liquidStorage, this.solidStorage);
	}

	// Token: 0x06002C51 RID: 11345 RVA: 0x000E8E40 File Offset: 0x000E7040
	protected override void OnCleanUp()
	{
		Conduit.GetNetworkManager(this.liquidPortInfo.conduitType).RemoveFromNetworks(this.liquidPort.inputCell, this.liquidPort.networkItem, true);
		Conduit.GetNetworkManager(this.gasPortInfo.conduitType).RemoveFromNetworks(this.gasPort.inputCell, this.gasPort.networkItem, true);
		Game.Instance.solidConduitSystem.RemoveFromNetworks(this.solidPort.inputCell, this.solidPort.solidConsumer, true);
		base.OnCleanUp();
	}

	// Token: 0x06002C52 RID: 11346 RVA: 0x000E8ED1 File Offset: 0x000E70D1
	bool ISecondaryInput.HasSecondaryConduitType(ConduitType type)
	{
		return this.liquidPortInfo.conduitType == type || this.gasPortInfo.conduitType == type || this.solidPortInfo.conduitType == type;
	}

	// Token: 0x06002C53 RID: 11347 RVA: 0x000E8F00 File Offset: 0x000E7100
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

	// Token: 0x04001A5A RID: 6746
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001A5B RID: 6747
	public Storage gasStorage;

	// Token: 0x04001A5C RID: 6748
	public Storage liquidStorage;

	// Token: 0x04001A5D RID: 6749
	public Storage solidStorage;

	// Token: 0x04001A5E RID: 6750
	public WarpConduitReceiver receiver;

	// Token: 0x04001A5F RID: 6751
	[SerializeField]
	public ConduitPortInfo liquidPortInfo;

	// Token: 0x04001A60 RID: 6752
	private WarpConduitSender.ConduitPort liquidPort;

	// Token: 0x04001A61 RID: 6753
	[SerializeField]
	public ConduitPortInfo gasPortInfo;

	// Token: 0x04001A62 RID: 6754
	private WarpConduitSender.ConduitPort gasPort;

	// Token: 0x04001A63 RID: 6755
	[SerializeField]
	public ConduitPortInfo solidPortInfo;

	// Token: 0x04001A64 RID: 6756
	private WarpConduitSender.ConduitPort solidPort;

	// Token: 0x02001331 RID: 4913
	private class ConduitPort
	{
		// Token: 0x06007CF2 RID: 31986 RVA: 0x002D2378 File Offset: 0x002D0578
		public ConduitPort(GameObject parent, ConduitPortInfo info, int number, Storage targetStorage)
		{
			this.portInfo = info;
			this.inputCell = Grid.OffsetCell(Grid.PosToCell(parent), this.portInfo.offset);
			if (this.portInfo.conduitType != ConduitType.Solid)
			{
				ConduitConsumer conduitConsumer = parent.AddComponent<ConduitConsumer>();
				conduitConsumer.conduitType = this.portInfo.conduitType;
				conduitConsumer.useSecondaryInput = true;
				conduitConsumer.storage = targetStorage;
				conduitConsumer.capacityKG = targetStorage.capacityKg;
				conduitConsumer.alwaysConsume = false;
				this.conduitConsumer = conduitConsumer;
				this.conduitConsumer.keepZeroMassObject = false;
				IUtilityNetworkMgr networkManager = Conduit.GetNetworkManager(this.portInfo.conduitType);
				this.networkItem = new FlowUtilityNetwork.NetworkItem(this.portInfo.conduitType, Endpoint.Sink, this.inputCell, parent);
				networkManager.AddToNetworks(this.inputCell, this.networkItem, true);
			}
			else
			{
				this.solidConsumer = parent.AddComponent<SolidConduitConsumer>();
				this.solidConsumer.useSecondaryInput = true;
				this.solidConsumer.storage = targetStorage;
				this.networkItem = new FlowUtilityNetwork.NetworkItem(ConduitType.Solid, Endpoint.Sink, this.inputCell, parent);
				Game.Instance.solidConduitSystem.AddToNetworks(this.inputCell, this.networkItem, true);
			}
			string text = "airlock_" + number.ToString();
			string text2 = "airlock_target_" + number.ToString();
			this.pre = "airlock_" + number.ToString() + "_pre";
			this.loop = "airlock_" + number.ToString() + "_loop";
			this.pst = "airlock_" + number.ToString() + "_pst";
			this.airlock = new MeterController(parent.GetComponent<KBatchedAnimController>(), text2, text, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { text2 });
		}

		// Token: 0x06007CF3 RID: 31987 RVA: 0x002D253C File Offset: 0x002D073C
		public void Update()
		{
			bool flag = false;
			if (this.conduitConsumer != null)
			{
				flag = this.conduitConsumer.IsConnected && this.conduitConsumer.IsSatisfied && this.conduitConsumer.consumedLastTick;
			}
			else if (this.solidConsumer != null)
			{
				flag = this.solidConsumer.IsConnected && this.solidConsumer.IsConsuming;
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

		// Token: 0x04005FCD RID: 24525
		public ConduitPortInfo portInfo;

		// Token: 0x04005FCE RID: 24526
		public int inputCell;

		// Token: 0x04005FCF RID: 24527
		public FlowUtilityNetwork.NetworkItem networkItem;

		// Token: 0x04005FD0 RID: 24528
		private ConduitConsumer conduitConsumer;

		// Token: 0x04005FD1 RID: 24529
		public SolidConduitConsumer solidConsumer;

		// Token: 0x04005FD2 RID: 24530
		public MeterController airlock;

		// Token: 0x04005FD3 RID: 24531
		private bool open;

		// Token: 0x04005FD4 RID: 24532
		private string pre;

		// Token: 0x04005FD5 RID: 24533
		private string loop;

		// Token: 0x04005FD6 RID: 24534
		private string pst;
	}

	// Token: 0x02001332 RID: 4914
	public class StatesInstance : GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.GameInstance
	{
		// Token: 0x06007CF4 RID: 31988 RVA: 0x002D263C File Offset: 0x002D083C
		public StatesInstance(WarpConduitSender smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001333 RID: 4915
	public class States : GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender>
	{
		// Token: 0x06007CF5 RID: 31989 RVA: 0x002D2648 File Offset: 0x002D0848
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.EventHandler(GameHashes.BuildingActivated, delegate(WarpConduitSender.StatesInstance smi, object data)
			{
				smi.master.OnActivatedChanged(data);
			});
			this.off.PlayAnim("off").Enter(delegate(WarpConduitSender.StatesInstance smi)
			{
				smi.master.gasPort.Update();
				smi.master.liquidPort.Update();
				smi.master.solidPort.Update();
			}).EventTransition(GameHashes.OperationalChanged, this.on, (WarpConduitSender.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational);
			this.on.DefaultState(this.on.waiting).Update(delegate(WarpConduitSender.StatesInstance smi, float dt)
			{
				smi.master.gasPort.Update();
				smi.master.liquidPort.Update();
				smi.master.solidPort.Update();
			}, UpdateRate.SIM_200ms, false);
			this.on.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Working, null)
				.Exit(delegate(WarpConduitSender.StatesInstance smi)
				{
					smi.Play("working_pst", KAnim.PlayMode.Once);
				});
			this.on.waiting.QueueAnim("idle", false, null).ToggleMainStatusItem(Db.Get().BuildingStatusItems.Normal, null).EventTransition(GameHashes.OnStorageChange, this.on.working, (WarpConduitSender.StatesInstance smi) => smi.GetComponent<Storage>().MassStored() > 0f);
		}

		// Token: 0x04005FD7 RID: 24535
		public GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.State off;

		// Token: 0x04005FD8 RID: 24536
		public WarpConduitSender.States.onStates on;

		// Token: 0x0200201A RID: 8218
		public class onStates : GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.State
		{
			// Token: 0x04008F3B RID: 36667
			public GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.State working;

			// Token: 0x04008F3C RID: 36668
			public GameStateMachine<WarpConduitSender.States, WarpConduitSender.StatesInstance, WarpConduitSender, object>.State waiting;
		}
	}
}

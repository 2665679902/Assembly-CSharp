using System;

// Token: 0x02000611 RID: 1553
public class ModularConduitPortController : GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>
{
	// Token: 0x0600288B RID: 10379 RVA: 0x000D6E38 File Offset: 0x000D5038
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		ModularConduitPortController.InitializeStatusItems();
		this.off.PlayAnim("off", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.on, (ModularConduitPortController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on.DefaultState(this.on.idle).EventTransition(GameHashes.OperationalChanged, this.off, (ModularConduitPortController.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		this.on.idle.PlayAnim("idle").ParamTransition<bool>(this.hasRocket, this.on.finished, GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.IsTrue).ToggleStatusItem(ModularConduitPortController.idleStatusItem, null);
		this.on.finished.PlayAnim("finished", KAnim.PlayMode.Loop).ParamTransition<bool>(this.hasRocket, this.on.idle, GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.IsFalse).ParamTransition<bool>(this.isUnloading, this.on.unloading, GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.IsTrue)
			.ParamTransition<bool>(this.isLoading, this.on.loading, GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.IsTrue)
			.ToggleStatusItem(ModularConduitPortController.loadedStatusItem, null);
		this.on.unloading.Enter("SetActive(true)", delegate(ModularConduitPortController.Instance smi)
		{
			smi.operational.SetActive(true, false);
		}).Exit("SetActive(false)", delegate(ModularConduitPortController.Instance smi)
		{
			smi.operational.SetActive(false, false);
		}).PlayAnim("unloading_pre")
			.QueueAnim("unloading_loop", true, null)
			.ParamTransition<bool>(this.isUnloading, this.on.unloading_pst, GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.IsFalse)
			.ParamTransition<bool>(this.hasRocket, this.on.unloading_pst, GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.IsFalse)
			.ToggleStatusItem(ModularConduitPortController.unloadingStatusItem, null);
		this.on.unloading_pst.PlayAnim("unloading_pst").OnAnimQueueComplete(this.on.finished);
		this.on.loading.Enter("SetActive(true)", delegate(ModularConduitPortController.Instance smi)
		{
			smi.operational.SetActive(true, false);
		}).Exit("SetActive(false)", delegate(ModularConduitPortController.Instance smi)
		{
			smi.operational.SetActive(false, false);
		}).PlayAnim("loading_pre")
			.QueueAnim("loading_loop", true, null)
			.ParamTransition<bool>(this.isLoading, this.on.loading_pst, GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.IsFalse)
			.ParamTransition<bool>(this.hasRocket, this.on.loading_pst, GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.IsFalse)
			.ToggleStatusItem(ModularConduitPortController.loadingStatusItem, null);
		this.on.loading_pst.PlayAnim("loading_pst").OnAnimQueueComplete(this.on.finished);
	}

	// Token: 0x0600288C RID: 10380 RVA: 0x000D7140 File Offset: 0x000D5340
	private static void InitializeStatusItems()
	{
		if (ModularConduitPortController.idleStatusItem == null)
		{
			ModularConduitPortController.idleStatusItem = new StatusItem("ROCKET_PORT_IDLE", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			ModularConduitPortController.unloadingStatusItem = new StatusItem("ROCKET_PORT_UNLOADING", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			ModularConduitPortController.loadingStatusItem = new StatusItem("ROCKET_PORT_LOADING", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			ModularConduitPortController.loadedStatusItem = new StatusItem("ROCKET_PORT_LOADED", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		}
	}

	// Token: 0x040017C2 RID: 6082
	private GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.State off;

	// Token: 0x040017C3 RID: 6083
	private ModularConduitPortController.OnStates on;

	// Token: 0x040017C4 RID: 6084
	public StateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.BoolParameter isUnloading;

	// Token: 0x040017C5 RID: 6085
	public StateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.BoolParameter isLoading;

	// Token: 0x040017C6 RID: 6086
	public StateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.BoolParameter hasRocket;

	// Token: 0x040017C7 RID: 6087
	private static StatusItem idleStatusItem;

	// Token: 0x040017C8 RID: 6088
	private static StatusItem unloadingStatusItem;

	// Token: 0x040017C9 RID: 6089
	private static StatusItem loadingStatusItem;

	// Token: 0x040017CA RID: 6090
	private static StatusItem loadedStatusItem;

	// Token: 0x02001280 RID: 4736
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005DFD RID: 24061
		public ModularConduitPortController.Mode mode;
	}

	// Token: 0x02001281 RID: 4737
	public enum Mode
	{
		// Token: 0x04005DFF RID: 24063
		Unload,
		// Token: 0x04005E00 RID: 24064
		Both,
		// Token: 0x04005E01 RID: 24065
		Load
	}

	// Token: 0x02001282 RID: 4738
	private class OnStates : GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.State
	{
		// Token: 0x04005E02 RID: 24066
		public GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.State idle;

		// Token: 0x04005E03 RID: 24067
		public GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.State unloading;

		// Token: 0x04005E04 RID: 24068
		public GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.State unloading_pst;

		// Token: 0x04005E05 RID: 24069
		public GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.State loading;

		// Token: 0x04005E06 RID: 24070
		public GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.State loading_pst;

		// Token: 0x04005E07 RID: 24071
		public GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.State finished;
	}

	// Token: 0x02001283 RID: 4739
	public new class Instance : GameStateMachine<ModularConduitPortController, ModularConduitPortController.Instance, IStateMachineTarget, ModularConduitPortController.Def>.GameInstance
	{
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06007A8D RID: 31373 RVA: 0x002C8583 File Offset: 0x002C6783
		public ModularConduitPortController.Mode SelectedMode
		{
			get
			{
				return base.def.mode;
			}
		}

		// Token: 0x06007A8E RID: 31374 RVA: 0x002C8590 File Offset: 0x002C6790
		public Instance(IStateMachineTarget master, ModularConduitPortController.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x002C859A File Offset: 0x002C679A
		public ConduitType GetConduitType()
		{
			return base.GetComponent<IConduitConsumer>().ConduitType;
		}

		// Token: 0x06007A90 RID: 31376 RVA: 0x002C85A7 File Offset: 0x002C67A7
		public void SetUnloading(bool isUnloading)
		{
			base.sm.isUnloading.Set(isUnloading, this, false);
		}

		// Token: 0x06007A91 RID: 31377 RVA: 0x002C85BD File Offset: 0x002C67BD
		public void SetLoading(bool isLoading)
		{
			base.sm.isLoading.Set(isLoading, this, false);
		}

		// Token: 0x06007A92 RID: 31378 RVA: 0x002C85D3 File Offset: 0x002C67D3
		public void SetRocket(bool hasRocket)
		{
			base.sm.hasRocket.Set(hasRocket, this, false);
		}

		// Token: 0x06007A93 RID: 31379 RVA: 0x002C85E9 File Offset: 0x002C67E9
		public bool IsLoading()
		{
			return base.sm.isLoading.Get(this);
		}

		// Token: 0x04005E08 RID: 24072
		[MyCmpGet]
		public Operational operational;
	}
}

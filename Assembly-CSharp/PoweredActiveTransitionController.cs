using System;

// Token: 0x0200004F RID: 79
public class PoweredActiveTransitionController : GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>
{
	// Token: 0x06000165 RID: 357 RVA: 0x0000A058 File Offset: 0x00008258
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on_pre, (PoweredActiveTransitionController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.on);
		this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.on_pst, (PoweredActiveTransitionController.Instance smi) => !smi.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.working, (PoweredActiveTransitionController.Instance smi) => smi.GetComponent<Operational>().IsActive);
		this.on_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.off);
		this.working.PlayAnim("working_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.on_pst, (PoweredActiveTransitionController.Instance smi) => !smi.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.on, (PoweredActiveTransitionController.Instance smi) => !smi.GetComponent<Operational>().IsActive)
			.Enter(delegate(PoweredActiveTransitionController.Instance smi)
			{
				if (smi.def.showWorkingStatus)
				{
					smi.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.Working, null);
				}
			})
			.Exit(delegate(PoweredActiveTransitionController.Instance smi)
			{
				if (smi.def.showWorkingStatus)
				{
					smi.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.Working, false);
				}
			});
	}

	// Token: 0x040000C8 RID: 200
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State off;

	// Token: 0x040000C9 RID: 201
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State on;

	// Token: 0x040000CA RID: 202
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State on_pre;

	// Token: 0x040000CB RID: 203
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State on_pst;

	// Token: 0x040000CC RID: 204
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State working;

	// Token: 0x02000DD4 RID: 3540
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005057 RID: 20567
		public bool showWorkingStatus;
	}

	// Token: 0x02000DD5 RID: 3541
	public new class Instance : GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.GameInstance
	{
		// Token: 0x06006AEB RID: 27371 RVA: 0x00295BC9 File Offset: 0x00293DC9
		public Instance(IStateMachineTarget master, PoweredActiveTransitionController.Def def)
			: base(master, def)
		{
		}
	}
}

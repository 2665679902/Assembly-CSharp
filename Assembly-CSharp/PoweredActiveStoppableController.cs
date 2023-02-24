using System;

// Token: 0x0200004E RID: 78
public class PoweredActiveStoppableController : GameStateMachine<PoweredActiveStoppableController, PoweredActiveStoppableController.Instance>
{
	// Token: 0x06000163 RID: 355 RVA: 0x00009F34 File Offset: 0x00008134
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.ActiveChanged, this.working_pre, (PoweredActiveStoppableController.Instance smi) => smi.GetComponent<Operational>().IsActive);
		this.working_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working_loop);
		this.working_loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.stop, (PoweredActiveStoppableController.Instance smi) => !smi.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.working_pst, (PoweredActiveStoppableController.Instance smi) => !smi.GetComponent<Operational>().IsActive);
		this.working_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.off);
		this.stop.PlayAnim("stop").OnAnimQueueComplete(this.off);
	}

	// Token: 0x040000C3 RID: 195
	public GameStateMachine<PoweredActiveStoppableController, PoweredActiveStoppableController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040000C4 RID: 196
	public GameStateMachine<PoweredActiveStoppableController, PoweredActiveStoppableController.Instance, IStateMachineTarget, object>.State working_pre;

	// Token: 0x040000C5 RID: 197
	public GameStateMachine<PoweredActiveStoppableController, PoweredActiveStoppableController.Instance, IStateMachineTarget, object>.State working_loop;

	// Token: 0x040000C6 RID: 198
	public GameStateMachine<PoweredActiveStoppableController, PoweredActiveStoppableController.Instance, IStateMachineTarget, object>.State working_pst;

	// Token: 0x040000C7 RID: 199
	public GameStateMachine<PoweredActiveStoppableController, PoweredActiveStoppableController.Instance, IStateMachineTarget, object>.State stop;

	// Token: 0x02000DD1 RID: 3537
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000DD2 RID: 3538
	public new class Instance : GameStateMachine<PoweredActiveStoppableController, PoweredActiveStoppableController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06006AE4 RID: 27364 RVA: 0x00295B76 File Offset: 0x00293D76
		public Instance(IStateMachineTarget master, PoweredActiveStoppableController.Def def)
			: base(master, def)
		{
		}
	}
}

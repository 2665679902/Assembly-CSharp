using System;

// Token: 0x02000049 RID: 73
public class ActiveController : GameStateMachine<ActiveController, ActiveController.Instance>
{
	// Token: 0x06000157 RID: 343 RVA: 0x000098F4 File Offset: 0x00007AF4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.ActiveChanged, this.working_pre, (ActiveController.Instance smi) => smi.GetComponent<Operational>().IsActive);
		this.working_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working_loop);
		this.working_loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.ActiveChanged, this.working_pst, (ActiveController.Instance smi) => !smi.GetComponent<Operational>().IsActive);
		this.working_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.off);
	}

	// Token: 0x040000B3 RID: 179
	public GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040000B4 RID: 180
	public GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.State working_pre;

	// Token: 0x040000B5 RID: 181
	public GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.State working_loop;

	// Token: 0x040000B6 RID: 182
	public GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.State working_pst;

	// Token: 0x02000DC0 RID: 3520
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000DC1 RID: 3521
	public new class Instance : GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06006AB9 RID: 27321 RVA: 0x002958C4 File Offset: 0x00293AC4
		public Instance(IStateMachineTarget master, ActiveController.Def def)
			: base(master, def)
		{
		}
	}
}

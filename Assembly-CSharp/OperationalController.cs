using System;

// Token: 0x0200004C RID: 76
public class OperationalController : GameStateMachine<OperationalController, OperationalController.Instance>
{
	// Token: 0x0600015F RID: 351 RVA: 0x00009C40 File Offset: 0x00007E40
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.root.EventTransition(GameHashes.OperationalChanged, this.off, (OperationalController.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.working_pre, (OperationalController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.working_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working_loop);
		this.working_loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.working_pst, (OperationalController.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		this.working_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.off);
	}

	// Token: 0x040000BC RID: 188
	public GameStateMachine<OperationalController, OperationalController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040000BD RID: 189
	public GameStateMachine<OperationalController, OperationalController.Instance, IStateMachineTarget, object>.State working_pre;

	// Token: 0x040000BE RID: 190
	public GameStateMachine<OperationalController, OperationalController.Instance, IStateMachineTarget, object>.State working_loop;

	// Token: 0x040000BF RID: 191
	public GameStateMachine<OperationalController, OperationalController.Instance, IStateMachineTarget, object>.State working_pst;

	// Token: 0x02000DCA RID: 3530
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000DCB RID: 3531
	public new class Instance : GameStateMachine<OperationalController, OperationalController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06006AD1 RID: 27345 RVA: 0x00295A55 File Offset: 0x00293C55
		public Instance(IStateMachineTarget master, OperationalController.Def def)
			: base(master, def)
		{
		}
	}
}

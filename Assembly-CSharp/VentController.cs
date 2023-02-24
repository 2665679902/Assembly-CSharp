using System;

// Token: 0x02000052 RID: 82
public class VentController : GameStateMachine<VentController, VentController.Instance>
{
	// Token: 0x0600016B RID: 363 RVA: 0x0000A388 File Offset: 0x00008588
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.root.EventTransition(GameHashes.VentClosed, this.closed, (VentController.Instance smi) => smi.GetComponent<Vent>().Closed()).EventTransition(GameHashes.VentOpen, this.off, (VentController.Instance smi) => !smi.GetComponent<Vent>().Closed());
		this.off.PlayAnim("off").EventTransition(GameHashes.VentAnimatingChanged, this.working_pre, (VentController.Instance smi) => smi.GetComponent<Exhaust>().IsAnimating());
		this.working_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working_loop);
		this.working_loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.VentAnimatingChanged, this.working_pst, (VentController.Instance smi) => !smi.GetComponent<Exhaust>().IsAnimating());
		this.working_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.off);
		this.closed.PlayAnim("closed").EventTransition(GameHashes.VentAnimatingChanged, this.working_pre, (VentController.Instance smi) => smi.GetComponent<Exhaust>().IsAnimating());
	}

	// Token: 0x040000D2 RID: 210
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040000D3 RID: 211
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State working_pre;

	// Token: 0x040000D4 RID: 212
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State working_loop;

	// Token: 0x040000D5 RID: 213
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State working_pst;

	// Token: 0x040000D6 RID: 214
	public GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.State closed;

	// Token: 0x040000D7 RID: 215
	public StateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.BoolParameter isAnimating;

	// Token: 0x02000DDD RID: 3549
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000DDE RID: 3550
	public new class Instance : GameStateMachine<VentController, VentController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06006B02 RID: 27394 RVA: 0x00295D14 File Offset: 0x00293F14
		public Instance(IStateMachineTarget master, VentController.Def def)
			: base(master, def)
		{
		}
	}
}

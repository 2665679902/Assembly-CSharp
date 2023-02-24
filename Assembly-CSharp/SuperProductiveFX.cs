using System;
using UnityEngine;

// Token: 0x0200053D RID: 1341
public class SuperProductiveFX : GameStateMachine<SuperProductiveFX, SuperProductiveFX.Instance>
{
	// Token: 0x06002011 RID: 8209 RVA: 0x000AF57C File Offset: 0x000AD77C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.pre;
		base.Target(this.fx);
		this.root.OnSignal(this.wasProductive, this.productive, (SuperProductiveFX.Instance smi) => smi.GetCurrentState() != smi.sm.pst).OnSignal(this.destroyFX, this.pst);
		this.pre.PlayAnim("productive_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.idle);
		this.idle.PlayAnim("productive_loop", KAnim.PlayMode.Loop);
		this.productive.QueueAnim("productive_achievement", false, null).OnAnimQueueComplete(this.idle);
		this.pst.PlayAnim("productive_pst").EventHandler(GameHashes.AnimQueueComplete, delegate(SuperProductiveFX.Instance smi)
		{
			smi.DestroyFX();
		});
	}

	// Token: 0x04001259 RID: 4697
	public StateMachine<SuperProductiveFX, SuperProductiveFX.Instance, IStateMachineTarget, object>.Signal wasProductive;

	// Token: 0x0400125A RID: 4698
	public StateMachine<SuperProductiveFX, SuperProductiveFX.Instance, IStateMachineTarget, object>.Signal destroyFX;

	// Token: 0x0400125B RID: 4699
	public StateMachine<SuperProductiveFX, SuperProductiveFX.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x0400125C RID: 4700
	public GameStateMachine<SuperProductiveFX, SuperProductiveFX.Instance, IStateMachineTarget, object>.State pre;

	// Token: 0x0400125D RID: 4701
	public GameStateMachine<SuperProductiveFX, SuperProductiveFX.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x0400125E RID: 4702
	public GameStateMachine<SuperProductiveFX, SuperProductiveFX.Instance, IStateMachineTarget, object>.State productive;

	// Token: 0x0400125F RID: 4703
	public GameStateMachine<SuperProductiveFX, SuperProductiveFX.Instance, IStateMachineTarget, object>.State pst;

	// Token: 0x0200116F RID: 4463
	public new class Instance : GameStateMachine<SuperProductiveFX, SuperProductiveFX.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007691 RID: 30353 RVA: 0x002B8348 File Offset: 0x002B6548
		public Instance(IStateMachineTarget master, Vector3 offset)
			: base(master)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("productive_fx_kanim", master.gameObject.transform.GetPosition() + offset, master.gameObject.transform, true, Grid.SceneLayer.Front, false);
			base.sm.fx.Set(kbatchedAnimController.gameObject, base.smi, false);
		}

		// Token: 0x06007692 RID: 30354 RVA: 0x002B83AA File Offset: 0x002B65AA
		public void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
			base.smi.StopSM("destroyed");
		}
	}
}

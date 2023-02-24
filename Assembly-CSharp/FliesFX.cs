using System;
using UnityEngine;

// Token: 0x02000539 RID: 1337
public class FliesFX : GameStateMachine<FliesFX, FliesFX.Instance>
{
	// Token: 0x06002005 RID: 8197 RVA: 0x000AEDAC File Offset: 0x000ACFAC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.Target(this.fx);
		this.root.PlayAnim("swarm_pre").QueueAnim("swarm_loop", true, null).Exit("DestroyFX", delegate(FliesFX.Instance smi)
		{
			smi.DestroyFX();
		});
	}

	// Token: 0x0400124D RID: 4685
	public StateMachine<FliesFX, FliesFX.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x02001166 RID: 4454
	public new class Instance : GameStateMachine<FliesFX, FliesFX.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007672 RID: 30322 RVA: 0x002B7D74 File Offset: 0x002B5F74
		public Instance(IStateMachineTarget master, Vector3 offset)
			: base(master)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("fly_swarm_kanim", base.smi.master.transform.GetPosition() + offset, base.smi.master.transform, false, Grid.SceneLayer.Front, false);
			base.sm.fx.Set(kbatchedAnimController.gameObject, base.smi, false);
		}

		// Token: 0x06007673 RID: 30323 RVA: 0x002B7DE0 File Offset: 0x002B5FE0
		public void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
		}
	}
}

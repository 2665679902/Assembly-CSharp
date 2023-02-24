using System;
using UnityEngine;

// Token: 0x0200053C RID: 1340
public class SicknessCuredFX : GameStateMachine<SicknessCuredFX, SicknessCuredFX.Instance>
{
	// Token: 0x0600200F RID: 8207 RVA: 0x000AF510 File Offset: 0x000AD710
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.Target(this.fx);
		this.root.PlayAnim("upgrade").OnAnimQueueComplete(null).Exit("DestroyFX", delegate(SicknessCuredFX.Instance smi)
		{
			smi.DestroyFX();
		});
	}

	// Token: 0x04001258 RID: 4696
	public StateMachine<SicknessCuredFX, SicknessCuredFX.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x0200116D RID: 4461
	public new class Instance : GameStateMachine<SicknessCuredFX, SicknessCuredFX.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600768C RID: 30348 RVA: 0x002B82AC File Offset: 0x002B64AC
		public Instance(IStateMachineTarget master, Vector3 offset)
			: base(master)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("recentlyhealed_fx_kanim", master.gameObject.transform.GetPosition() + offset, master.gameObject.transform, true, Grid.SceneLayer.Front, false);
			base.sm.fx.Set(kbatchedAnimController.gameObject, base.smi, false);
		}

		// Token: 0x0600768D RID: 30349 RVA: 0x002B830E File Offset: 0x002B650E
		public void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
		}
	}
}

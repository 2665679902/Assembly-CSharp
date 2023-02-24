using System;
using UnityEngine;

// Token: 0x0200053A RID: 1338
public class GameplayEventFX : GameStateMachine<GameplayEventFX, GameplayEventFX.Instance>
{
	// Token: 0x06002007 RID: 8199 RVA: 0x000AEE1C File Offset: 0x000AD01C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.Target(this.fx);
		this.root.PlayAnim("event_pre").OnAnimQueueComplete(this.single).Exit("DestroyFX", delegate(GameplayEventFX.Instance smi)
		{
			smi.DestroyFX();
		});
		this.single.PlayAnim("event_loop", KAnim.PlayMode.Loop).ParamTransition<int>(this.notificationCount, this.multiple, (GameplayEventFX.Instance smi, int p) => p > 1);
		this.multiple.PlayAnim("event_loop_multiple", KAnim.PlayMode.Loop).ParamTransition<int>(this.notificationCount, this.single, (GameplayEventFX.Instance smi, int p) => p == 1);
	}

	// Token: 0x0400124E RID: 4686
	public StateMachine<GameplayEventFX, GameplayEventFX.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x0400124F RID: 4687
	public StateMachine<GameplayEventFX, GameplayEventFX.Instance, IStateMachineTarget, object>.IntParameter notificationCount;

	// Token: 0x04001250 RID: 4688
	public GameStateMachine<GameplayEventFX, GameplayEventFX.Instance, IStateMachineTarget, object>.State single;

	// Token: 0x04001251 RID: 4689
	public GameStateMachine<GameplayEventFX, GameplayEventFX.Instance, IStateMachineTarget, object>.State multiple;

	// Token: 0x02001168 RID: 4456
	public new class Instance : GameStateMachine<GameplayEventFX, GameplayEventFX.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007677 RID: 30327 RVA: 0x002B7E1C File Offset: 0x002B601C
		public Instance(IStateMachineTarget master, Vector3 offset)
			: base(master)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("event_alert_fx_kanim", base.smi.master.transform.GetPosition() + offset, base.smi.master.transform, false, Grid.SceneLayer.Front, false);
			base.sm.fx.Set(kbatchedAnimController.gameObject, base.smi, false);
		}

		// Token: 0x06007678 RID: 30328 RVA: 0x002B7E88 File Offset: 0x002B6088
		public void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
		}

		// Token: 0x04005ABD RID: 23229
		public int previousCount;
	}
}

using System;
using UnityEngine;

// Token: 0x02000537 RID: 1335
public class FXAnim : GameStateMachine<FXAnim, FXAnim.Instance>
{
	// Token: 0x06002000 RID: 8192 RVA: 0x000AEA44 File Offset: 0x000ACC44
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		base.Target(this.fx);
		this.loop.Enter(delegate(FXAnim.Instance smi)
		{
			smi.Enter();
		}).EventTransition(GameHashes.AnimQueueComplete, this.restart, null).Exit("Post", delegate(FXAnim.Instance smi)
		{
			smi.Exit();
		});
		this.restart.GoTo(this.loop);
	}

	// Token: 0x0400124A RID: 4682
	public StateMachine<FXAnim, FXAnim.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x0400124B RID: 4683
	public GameStateMachine<FXAnim, FXAnim.Instance, IStateMachineTarget, object>.State loop;

	// Token: 0x0400124C RID: 4684
	public GameStateMachine<FXAnim, FXAnim.Instance, IStateMachineTarget, object>.State restart;

	// Token: 0x02001163 RID: 4451
	public new class Instance : GameStateMachine<FXAnim, FXAnim.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007661 RID: 30305 RVA: 0x002B7BB0 File Offset: 0x002B5DB0
		public Instance(IStateMachineTarget master, string kanim_file, string anim, KAnim.PlayMode mode, Vector3 offset, Color32 tint_colour)
			: base(master)
		{
			this.animController = FXHelpers.CreateEffect(kanim_file, base.smi.master.transform.GetPosition() + offset, base.smi.master.transform, false, Grid.SceneLayer.Front, false);
			this.animController.gameObject.Subscribe(-1061186183, new Action<object>(this.OnAnimQueueComplete));
			this.animController.TintColour = tint_colour;
			base.sm.fx.Set(this.animController.gameObject, base.smi, false);
			this.anim = anim;
			this.mode = mode;
		}

		// Token: 0x06007662 RID: 30306 RVA: 0x002B7C61 File Offset: 0x002B5E61
		public void Enter()
		{
			this.animController.Play(this.anim, this.mode, 1f, 0f);
		}

		// Token: 0x06007663 RID: 30307 RVA: 0x002B7C89 File Offset: 0x002B5E89
		public void Exit()
		{
			this.DestroyFX();
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x002B7C91 File Offset: 0x002B5E91
		private void OnAnimQueueComplete(object data)
		{
			this.DestroyFX();
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x002B7C99 File Offset: 0x002B5E99
		private void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
		}

		// Token: 0x04005AAE RID: 23214
		private string anim;

		// Token: 0x04005AAF RID: 23215
		private KAnim.PlayMode mode;

		// Token: 0x04005AB0 RID: 23216
		private KBatchedAnimController animController;
	}
}

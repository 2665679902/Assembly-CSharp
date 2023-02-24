using System;

// Token: 0x020000A4 RID: 164
public class AnimInterruptStates : GameStateMachine<AnimInterruptStates, AnimInterruptStates.Instance, IStateMachineTarget, AnimInterruptStates.Def>
{
	// Token: 0x060002CF RID: 719 RVA: 0x00016B60 File Offset: 0x00014D60
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.play_anim;
		this.play_anim.Enter(new StateMachine<AnimInterruptStates, AnimInterruptStates.Instance, IStateMachineTarget, AnimInterruptStates.Def>.State.Callback(this.PlayAnim)).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Behaviours.PlayInterruptAnim, false);
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00016BA0 File Offset: 0x00014DA0
	private void PlayAnim(AnimInterruptStates.Instance smi)
	{
		KBatchedAnimController kbatchedAnimController = smi.Get<KBatchedAnimController>();
		HashedString[] anims = smi.GetSMI<AnimInterruptMonitor.Instance>().anims;
		kbatchedAnimController.Play(anims[0], KAnim.PlayMode.Once, 1f, 0f);
		for (int i = 1; i < anims.Length; i++)
		{
			kbatchedAnimController.Queue(anims[i], KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x040001DC RID: 476
	public GameStateMachine<AnimInterruptStates, AnimInterruptStates.Instance, IStateMachineTarget, AnimInterruptStates.Def>.State play_anim;

	// Token: 0x040001DD RID: 477
	public GameStateMachine<AnimInterruptStates, AnimInterruptStates.Instance, IStateMachineTarget, AnimInterruptStates.Def>.State behaviourcomplete;

	// Token: 0x02000DFC RID: 3580
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000DFD RID: 3581
	public new class Instance : GameStateMachine<AnimInterruptStates, AnimInterruptStates.Instance, IStateMachineTarget, AnimInterruptStates.Def>.GameInstance
	{
		// Token: 0x06006B54 RID: 27476 RVA: 0x0029643F File Offset: 0x0029463F
		public Instance(Chore<AnimInterruptStates.Instance> chore, AnimInterruptStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Behaviours.PlayInterruptAnim);
		}
	}
}

using System;

// Token: 0x020006BD RID: 1725
public class AnimInterruptMonitor : GameStateMachine<AnimInterruptMonitor, AnimInterruptMonitor.Instance, IStateMachineTarget, AnimInterruptMonitor.Def>
{
	// Token: 0x06002EF9 RID: 12025 RVA: 0x000F89DA File Offset: 0x000F6BDA
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Behaviours.PlayInterruptAnim, new StateMachine<AnimInterruptMonitor, AnimInterruptMonitor.Instance, IStateMachineTarget, AnimInterruptMonitor.Def>.Transition.ConditionCallback(AnimInterruptMonitor.ShoulPlayAnim), new Action<AnimInterruptMonitor.Instance>(AnimInterruptMonitor.ClearAnim));
	}

	// Token: 0x06002EFA RID: 12026 RVA: 0x000F8A0D File Offset: 0x000F6C0D
	private static bool ShoulPlayAnim(AnimInterruptMonitor.Instance smi)
	{
		return smi.anims != null;
	}

	// Token: 0x06002EFB RID: 12027 RVA: 0x000F8A18 File Offset: 0x000F6C18
	private static void ClearAnim(AnimInterruptMonitor.Instance smi)
	{
		smi.anims = null;
	}

	// Token: 0x02001396 RID: 5014
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001397 RID: 5015
	public new class Instance : GameStateMachine<AnimInterruptMonitor, AnimInterruptMonitor.Instance, IStateMachineTarget, AnimInterruptMonitor.Def>.GameInstance
	{
		// Token: 0x06007E64 RID: 32356 RVA: 0x002D9123 File Offset: 0x002D7323
		public Instance(IStateMachineTarget master, AnimInterruptMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007E65 RID: 32357 RVA: 0x002D912D File Offset: 0x002D732D
		public void PlayAnim(HashedString anim)
		{
			this.PlayAnimSequence(new HashedString[] { anim });
		}

		// Token: 0x06007E66 RID: 32358 RVA: 0x002D9143 File Offset: 0x002D7343
		public void PlayAnimSequence(HashedString[] anims)
		{
			this.anims = anims;
			base.GetComponent<CreatureBrain>().UpdateBrain();
		}

		// Token: 0x04006125 RID: 24869
		public HashedString[] anims;
	}
}

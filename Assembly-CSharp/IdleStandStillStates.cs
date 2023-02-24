using System;
using STRINGS;

// Token: 0x020000CB RID: 203
public class IdleStandStillStates : GameStateMachine<IdleStandStillStates, IdleStandStillStates.Instance, IStateMachineTarget, IdleStandStillStates.Def>
{
	// Token: 0x06000389 RID: 905 RVA: 0x0001B89C File Offset: 0x00019A9C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.IDLE.NAME, CREATURES.STATUSITEMS.IDLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).ToggleTag(GameTags.Idle);
		this.loop.Enter(new StateMachine<IdleStandStillStates, IdleStandStillStates.Instance, IStateMachineTarget, IdleStandStillStates.Def>.State.Callback(this.PlayIdle));
	}

	// Token: 0x0600038A RID: 906 RVA: 0x0001B91C File Offset: 0x00019B1C
	public void PlayIdle(IdleStandStillStates.Instance smi)
	{
		KAnimControllerBase component = smi.GetComponent<KAnimControllerBase>();
		if (smi.def.customIdleAnim != null)
		{
			HashedString invalid = HashedString.Invalid;
			HashedString hashedString = smi.def.customIdleAnim(smi, ref invalid);
			if (hashedString != HashedString.Invalid)
			{
				if (invalid != HashedString.Invalid)
				{
					component.Play(invalid, KAnim.PlayMode.Once, 1f, 0f);
				}
				component.Queue(hashedString, KAnim.PlayMode.Loop, 1f, 0f);
				return;
			}
		}
		component.Play("idle", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x04000249 RID: 585
	private GameStateMachine<IdleStandStillStates, IdleStandStillStates.Instance, IStateMachineTarget, IdleStandStillStates.Def>.State loop;

	// Token: 0x02000E7C RID: 3708
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005195 RID: 20885
		public IdleStandStillStates.Def.IdleAnimCallback customIdleAnim;

		// Token: 0x02001E8F RID: 7823
		// (Invoke) Token: 0x06009BF6 RID: 39926
		public delegate HashedString IdleAnimCallback(IdleStandStillStates.Instance smi, ref HashedString pre_anim);
	}

	// Token: 0x02000E7D RID: 3709
	public new class Instance : GameStateMachine<IdleStandStillStates, IdleStandStillStates.Instance, IStateMachineTarget, IdleStandStillStates.Def>.GameInstance
	{
		// Token: 0x06006C60 RID: 27744 RVA: 0x00297DD6 File Offset: 0x00295FD6
		public Instance(Chore<IdleStandStillStates.Instance> chore, IdleStandStillStates.Def def)
			: base(chore, def)
		{
		}
	}
}

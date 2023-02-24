using System;
using STRINGS;

// Token: 0x020000B2 RID: 178
public class CreatureSleepStates : GameStateMachine<CreatureSleepStates, CreatureSleepStates.Instance, IStateMachineTarget, CreatureSleepStates.Def>
{
	// Token: 0x06000321 RID: 801 RVA: 0x00018EFC File Offset: 0x000170FC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.pre;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.SLEEPING.NAME, CREATURES.STATUSITEMS.SLEEPING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.pre.QueueAnim("sleep_pre", false, null).OnAnimQueueComplete(this.loop);
		this.loop.QueueAnim("sleep_loop", true, null).Transition(this.pst, new StateMachine<CreatureSleepStates, CreatureSleepStates.Instance, IStateMachineTarget, CreatureSleepStates.Def>.Transition.ConditionCallback(CreatureSleepStates.ShouldWakeUp), UpdateRate.SIM_1000ms);
		this.pst.QueueAnim("sleep_pst", false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Behaviours.SleepBehaviour, false);
	}

	// Token: 0x06000322 RID: 802 RVA: 0x00018FD1 File Offset: 0x000171D1
	public static bool ShouldWakeUp(CreatureSleepStates.Instance smi)
	{
		return !GameClock.Instance.IsNighttime();
	}

	// Token: 0x04000207 RID: 519
	public GameStateMachine<CreatureSleepStates, CreatureSleepStates.Instance, IStateMachineTarget, CreatureSleepStates.Def>.State pre;

	// Token: 0x04000208 RID: 520
	public GameStateMachine<CreatureSleepStates, CreatureSleepStates.Instance, IStateMachineTarget, CreatureSleepStates.Def>.State loop;

	// Token: 0x04000209 RID: 521
	public GameStateMachine<CreatureSleepStates, CreatureSleepStates.Instance, IStateMachineTarget, CreatureSleepStates.Def>.State pst;

	// Token: 0x0400020A RID: 522
	public GameStateMachine<CreatureSleepStates, CreatureSleepStates.Instance, IStateMachineTarget, CreatureSleepStates.Def>.State behaviourcomplete;

	// Token: 0x02000E31 RID: 3633
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E32 RID: 3634
	public new class Instance : GameStateMachine<CreatureSleepStates, CreatureSleepStates.Instance, IStateMachineTarget, CreatureSleepStates.Def>.GameInstance
	{
		// Token: 0x06006BC9 RID: 27593 RVA: 0x00296F2E File Offset: 0x0029512E
		public Instance(Chore<CreatureSleepStates.Instance> chore, CreatureSleepStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Behaviours.SleepBehaviour);
		}
	}
}

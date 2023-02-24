using System;
using STRINGS;

// Token: 0x020000AE RID: 174
public class CallAdultStates : GameStateMachine<CallAdultStates, CallAdultStates.Instance, IStateMachineTarget, CallAdultStates.Def>
{
	// Token: 0x0600030A RID: 778 RVA: 0x0001831C File Offset: 0x0001651C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.pre;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.SLEEPING.NAME, CREATURES.STATUSITEMS.SLEEPING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.pre.QueueAnim("call_pre", false, null).OnAnimQueueComplete(this.loop);
		this.loop.QueueAnim("call_loop", false, null).OnAnimQueueComplete(this.pst);
		this.pst.QueueAnim("call_pst", false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Behaviours.CallAdultBehaviour, false);
	}

	// Token: 0x040001FB RID: 507
	public GameStateMachine<CallAdultStates, CallAdultStates.Instance, IStateMachineTarget, CallAdultStates.Def>.State pre;

	// Token: 0x040001FC RID: 508
	public GameStateMachine<CallAdultStates, CallAdultStates.Instance, IStateMachineTarget, CallAdultStates.Def>.State loop;

	// Token: 0x040001FD RID: 509
	public GameStateMachine<CallAdultStates, CallAdultStates.Instance, IStateMachineTarget, CallAdultStates.Def>.State pst;

	// Token: 0x040001FE RID: 510
	public GameStateMachine<CallAdultStates, CallAdultStates.Instance, IStateMachineTarget, CallAdultStates.Def>.State behaviourcomplete;

	// Token: 0x02000E22 RID: 3618
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E23 RID: 3619
	public new class Instance : GameStateMachine<CallAdultStates, CallAdultStates.Instance, IStateMachineTarget, CallAdultStates.Def>.GameInstance
	{
		// Token: 0x06006B9E RID: 27550 RVA: 0x00296B90 File Offset: 0x00294D90
		public Instance(Chore<CallAdultStates.Instance> chore, CallAdultStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Behaviours.CallAdultBehaviour);
		}
	}
}

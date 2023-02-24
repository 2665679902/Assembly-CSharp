using System;
using System.Collections.Generic;

// Token: 0x02000621 RID: 1569
public class PartyCake : GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>
{
	// Token: 0x06002918 RID: 10520 RVA: 0x000D91D0 File Offset: 0x000D73D0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.creating.ready;
		this.creating.ready.PlayAnim("base").GoTo(this.creating.tier1);
		this.creating.tier1.InitializeStates(this.masterTarget, "tier_1", this.creating.tier2);
		this.creating.tier2.InitializeStates(this.masterTarget, "tier_2", this.creating.tier3);
		this.creating.tier3.InitializeStates(this.masterTarget, "tier_3", this.ready_to_party);
		this.ready_to_party.PlayAnim("unlit");
		this.party.PlayAnim("lit");
		this.complete.PlayAnim("finished");
	}

	// Token: 0x06002919 RID: 10521 RVA: 0x000D92B4 File Offset: 0x000D74B4
	private static Chore CreateFetchChore(PartyCake.StatesInstance smi)
	{
		return new FetchChore(Db.Get().ChoreTypes.FarmFetch, smi.GetComponent<Storage>(), 10f, new HashSet<Tag> { "MushBar".ToTag() }, FetchChore.MatchCriteria.MatchID, Tag.Invalid, null, null, true, null, null, null, Operational.State.Functional, 0);
	}

	// Token: 0x0600291A RID: 10522 RVA: 0x000D9304 File Offset: 0x000D7504
	private static Chore CreateWorkChore(PartyCake.StatesInstance smi)
	{
		Workable component = smi.master.GetComponent<PartyCakeWorkable>();
		return new WorkChore<PartyCakeWorkable>(Db.Get().ChoreTypes.Cook, component, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Work, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
	}

	// Token: 0x0400182A RID: 6186
	public PartyCake.CreatingStates creating;

	// Token: 0x0400182B RID: 6187
	public GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State ready_to_party;

	// Token: 0x0400182C RID: 6188
	public GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State party;

	// Token: 0x0400182D RID: 6189
	public GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State complete;

	// Token: 0x0200129D RID: 4765
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200129E RID: 4766
	public class CreatingStates : GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State
	{
		// Token: 0x04005E3B RID: 24123
		public GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State ready;

		// Token: 0x04005E3C RID: 24124
		public PartyCake.CreatingStates.Tier tier1;

		// Token: 0x04005E3D RID: 24125
		public PartyCake.CreatingStates.Tier tier2;

		// Token: 0x04005E3E RID: 24126
		public PartyCake.CreatingStates.Tier tier3;

		// Token: 0x04005E3F RID: 24127
		public GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State finish;

		// Token: 0x02001FDE RID: 8158
		public class Tier : GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State
		{
			// Token: 0x0600A13D RID: 41277 RVA: 0x00342A88 File Offset: 0x00340C88
			public GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State InitializeStates(StateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.TargetParameter targ, string animPrefix, GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State success)
			{
				base.root.Target(targ).DefaultState(this.fetch);
				this.fetch.PlayAnim(animPrefix + "_ready").ToggleChore(new Func<PartyCake.StatesInstance, Chore>(PartyCake.CreateFetchChore), this.work);
				this.work.Enter(delegate(PartyCake.StatesInstance smi)
				{
					KBatchedAnimController component = smi.GetComponent<KBatchedAnimController>();
					component.Play(animPrefix + "_working", KAnim.PlayMode.Once, 1f, 0f);
					component.SetPositionPercent(0f);
				}).ToggleChore(new Func<PartyCake.StatesInstance, Chore>(PartyCake.CreateWorkChore), success, this.work);
				return this;
			}

			// Token: 0x04008E00 RID: 36352
			public GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State fetch;

			// Token: 0x04008E01 RID: 36353
			public GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.State work;
		}
	}

	// Token: 0x0200129F RID: 4767
	public class StatesInstance : GameStateMachine<PartyCake, PartyCake.StatesInstance, IStateMachineTarget, PartyCake.Def>.GameInstance
	{
		// Token: 0x06007AE9 RID: 31465 RVA: 0x002C9943 File Offset: 0x002C7B43
		public StatesInstance(IStateMachineTarget smi, PartyCake.Def def)
			: base(smi, def)
		{
		}
	}
}

using System;
using Klei.AI;

// Token: 0x0200081A RID: 2074
public class BladderMonitor : GameStateMachine<BladderMonitor, BladderMonitor.Instance>
{
	// Token: 0x06003C3A RID: 15418 RVA: 0x0014F784 File Offset: 0x0014D984
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.Transition(this.urgentwant, (BladderMonitor.Instance smi) => smi.NeedsToPee(), UpdateRate.SIM_200ms).Transition(this.breakwant, (BladderMonitor.Instance smi) => smi.WantsToPee(), UpdateRate.SIM_200ms);
		this.urgentwant.InitializeStates(this.satisfied).ToggleThought(Db.Get().Thoughts.FullBladder, null).ToggleExpression(Db.Get().Expressions.FullBladder, null)
			.ToggleStateMachine((BladderMonitor.Instance smi) => new PeeChoreMonitor.Instance(smi.master))
			.ToggleEffect("FullBladder");
		this.breakwant.InitializeStates(this.satisfied);
		this.breakwant.wanting.Transition(this.urgentwant, (BladderMonitor.Instance smi) => smi.NeedsToPee(), UpdateRate.SIM_200ms).EventTransition(GameHashes.ScheduleBlocksChanged, this.satisfied, (BladderMonitor.Instance smi) => !smi.WantsToPee());
		this.breakwant.peeing.ToggleThought(Db.Get().Thoughts.BreakBladder, null);
	}

	// Token: 0x04002733 RID: 10035
	public GameStateMachine<BladderMonitor, BladderMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x04002734 RID: 10036
	public BladderMonitor.WantsToPeeStates urgentwant;

	// Token: 0x04002735 RID: 10037
	public BladderMonitor.WantsToPeeStates breakwant;

	// Token: 0x02001578 RID: 5496
	public class WantsToPeeStates : GameStateMachine<BladderMonitor, BladderMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x06008401 RID: 33793 RVA: 0x002E910C File Offset: 0x002E730C
		public GameStateMachine<BladderMonitor, BladderMonitor.Instance, IStateMachineTarget, object>.State InitializeStates(GameStateMachine<BladderMonitor, BladderMonitor.Instance, IStateMachineTarget, object>.State donePeeingState)
		{
			base.DefaultState(this.wanting).ToggleUrge(Db.Get().Urges.Pee).ToggleStateMachine((BladderMonitor.Instance smi) => new ToiletMonitor.Instance(smi.master));
			this.wanting.EventTransition(GameHashes.BeginChore, this.peeing, (BladderMonitor.Instance smi) => smi.IsPeeing());
			this.peeing.EventTransition(GameHashes.EndChore, donePeeingState, (BladderMonitor.Instance smi) => !smi.IsPeeing());
			return this;
		}

		// Token: 0x040066DA RID: 26330
		public GameStateMachine<BladderMonitor, BladderMonitor.Instance, IStateMachineTarget, object>.State wanting;

		// Token: 0x040066DB RID: 26331
		public GameStateMachine<BladderMonitor, BladderMonitor.Instance, IStateMachineTarget, object>.State peeing;
	}

	// Token: 0x02001579 RID: 5497
	public new class Instance : GameStateMachine<BladderMonitor, BladderMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008403 RID: 33795 RVA: 0x002E91CE File Offset: 0x002E73CE
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.bladder = Db.Get().Amounts.Bladder.Lookup(master.gameObject);
			this.choreDriver = base.GetComponent<ChoreDriver>();
		}

		// Token: 0x06008404 RID: 33796 RVA: 0x002E9204 File Offset: 0x002E7404
		public bool NeedsToPee()
		{
			DebugUtil.DevAssert(base.master != null, "master ref null", null);
			DebugUtil.DevAssert(!base.master.isNull, "master isNull", null);
			KPrefabID component = base.master.GetComponent<KPrefabID>();
			DebugUtil.DevAssert(component, "kpid was null", null);
			return !component.HasTag(GameTags.Asleep) && this.bladder.value >= 100f;
		}

		// Token: 0x06008405 RID: 33797 RVA: 0x002E927D File Offset: 0x002E747D
		public bool WantsToPee()
		{
			return this.NeedsToPee() || (this.IsPeeTime() && this.bladder.value >= 40f);
		}

		// Token: 0x06008406 RID: 33798 RVA: 0x002E92A8 File Offset: 0x002E74A8
		public bool IsPeeing()
		{
			return this.choreDriver.HasChore() && this.choreDriver.GetCurrentChore().SatisfiesUrge(Db.Get().Urges.Pee);
		}

		// Token: 0x06008407 RID: 33799 RVA: 0x002E92D8 File Offset: 0x002E74D8
		public bool IsPeeTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Hygiene);
		}

		// Token: 0x040066DC RID: 26332
		private AmountInstance bladder;

		// Token: 0x040066DD RID: 26333
		private ChoreDriver choreDriver;
	}
}

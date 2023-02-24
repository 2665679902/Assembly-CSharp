using System;

// Token: 0x0200083E RID: 2110
public class RationMonitor : GameStateMachine<RationMonitor, RationMonitor.Instance>
{
	// Token: 0x06003CE4 RID: 15588 RVA: 0x00153FF0 File Offset: 0x001521F0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.rationsavailable;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.root.EventHandler(GameHashes.EatCompleteEater, delegate(RationMonitor.Instance smi, object d)
		{
			smi.OnEatComplete(d);
		}).EventHandler(GameHashes.NewDay, (RationMonitor.Instance smi) => GameClock.Instance, delegate(RationMonitor.Instance smi)
		{
			smi.OnNewDay();
		}).ParamTransition<float>(this.rationsAteToday, this.rationsavailable, (RationMonitor.Instance smi, float p) => smi.HasRationsAvailable())
			.ParamTransition<float>(this.rationsAteToday, this.outofrations, (RationMonitor.Instance smi, float p) => !smi.HasRationsAvailable());
		this.rationsavailable.DefaultState(this.rationsavailable.noediblesavailable);
		this.rationsavailable.noediblesavailable.InitializeStates(this.masterTarget, Db.Get().DuplicantStatusItems.NoRationsAvailable).EventTransition(GameHashes.ColonyHasRationsChanged, new Func<RationMonitor.Instance, KMonoBehaviour>(RationMonitor.GetSaveGame), this.rationsavailable.ediblesunreachable, new StateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(RationMonitor.AreThereAnyEdibles));
		this.rationsavailable.ediblereachablebutnotpermitted.InitializeStates(this.masterTarget, Db.Get().DuplicantStatusItems.RationsNotPermitted).EventTransition(GameHashes.ColonyHasRationsChanged, new Func<RationMonitor.Instance, KMonoBehaviour>(RationMonitor.GetSaveGame), this.rationsavailable.noediblesavailable, new StateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(RationMonitor.AreThereNoEdibles)).EventTransition(GameHashes.ClosestEdibleChanged, this.rationsavailable.ediblesunreachable, new StateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(RationMonitor.NotIsEdibleInReachButNotPermitted));
		this.rationsavailable.ediblesunreachable.InitializeStates(this.masterTarget, Db.Get().DuplicantStatusItems.RationsUnreachable).EventTransition(GameHashes.ColonyHasRationsChanged, new Func<RationMonitor.Instance, KMonoBehaviour>(RationMonitor.GetSaveGame), this.rationsavailable.noediblesavailable, new StateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(RationMonitor.AreThereNoEdibles)).EventTransition(GameHashes.ClosestEdibleChanged, this.rationsavailable.edibleavailable, new StateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(RationMonitor.IsEdibleAvailable))
			.EventTransition(GameHashes.ClosestEdibleChanged, this.rationsavailable.ediblereachablebutnotpermitted, new StateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(RationMonitor.IsEdibleInReachButNotPermitted));
		this.rationsavailable.edibleavailable.ToggleChore((RationMonitor.Instance smi) => new EatChore(smi.master), this.rationsavailable.noediblesavailable).DefaultState(this.rationsavailable.edibleavailable.readytoeat);
		this.rationsavailable.edibleavailable.readytoeat.EventTransition(GameHashes.ClosestEdibleChanged, this.rationsavailable.noediblesavailable, null).EventTransition(GameHashes.BeginChore, this.rationsavailable.edibleavailable.eating, (RationMonitor.Instance smi) => smi.IsEating());
		this.rationsavailable.edibleavailable.eating.DoNothing();
		this.outofrations.InitializeStates(this.masterTarget, Db.Get().DuplicantStatusItems.DailyRationLimitReached);
	}

	// Token: 0x06003CE5 RID: 15589 RVA: 0x0015433E File Offset: 0x0015253E
	private static bool AreThereNoEdibles(RationMonitor.Instance smi)
	{
		return !RationMonitor.AreThereAnyEdibles(smi);
	}

	// Token: 0x06003CE6 RID: 15590 RVA: 0x0015434C File Offset: 0x0015254C
	private static bool AreThereAnyEdibles(RationMonitor.Instance smi)
	{
		if (SaveGame.Instance != null)
		{
			ColonyRationMonitor.Instance smi2 = SaveGame.Instance.GetSMI<ColonyRationMonitor.Instance>();
			if (smi2 != null)
			{
				return !smi2.IsOutOfRations();
			}
		}
		return false;
	}

	// Token: 0x06003CE7 RID: 15591 RVA: 0x0015437F File Offset: 0x0015257F
	private static KMonoBehaviour GetSaveGame(RationMonitor.Instance smi)
	{
		return SaveGame.Instance;
	}

	// Token: 0x06003CE8 RID: 15592 RVA: 0x00154386 File Offset: 0x00152586
	private static bool IsEdibleAvailable(RationMonitor.Instance smi)
	{
		return smi.GetEdible() != null;
	}

	// Token: 0x06003CE9 RID: 15593 RVA: 0x00154394 File Offset: 0x00152594
	private static bool NotIsEdibleInReachButNotPermitted(RationMonitor.Instance smi)
	{
		return !RationMonitor.IsEdibleInReachButNotPermitted(smi);
	}

	// Token: 0x06003CEA RID: 15594 RVA: 0x0015439F File Offset: 0x0015259F
	private static bool IsEdibleInReachButNotPermitted(RationMonitor.Instance smi)
	{
		return smi.GetComponent<Sensors>().GetSensor<ClosestEdibleSensor>().edibleInReachButNotPermitted;
	}

	// Token: 0x040027CE RID: 10190
	public StateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.FloatParameter rationsAteToday;

	// Token: 0x040027CF RID: 10191
	public RationMonitor.RationsAvailableState rationsavailable;

	// Token: 0x040027D0 RID: 10192
	public GameStateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.HungrySubState outofrations;

	// Token: 0x020015D2 RID: 5586
	public class EdibleAvailablestate : GameStateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040067FA RID: 26618
		public GameStateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.State readytoeat;

		// Token: 0x040067FB RID: 26619
		public GameStateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.State eating;
	}

	// Token: 0x020015D3 RID: 5587
	public class RationsAvailableState : GameStateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040067FC RID: 26620
		public GameStateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.HungrySubState noediblesavailable;

		// Token: 0x040067FD RID: 26621
		public GameStateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.HungrySubState ediblereachablebutnotpermitted;

		// Token: 0x040067FE RID: 26622
		public GameStateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.HungrySubState ediblesunreachable;

		// Token: 0x040067FF RID: 26623
		public RationMonitor.EdibleAvailablestate edibleavailable;
	}

	// Token: 0x020015D4 RID: 5588
	public new class Instance : GameStateMachine<RationMonitor, RationMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008576 RID: 34166 RVA: 0x002EC846 File Offset: 0x002EAA46
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.choreDriver = master.GetComponent<ChoreDriver>();
		}

		// Token: 0x06008577 RID: 34167 RVA: 0x002EC85B File Offset: 0x002EAA5B
		public Edible GetEdible()
		{
			return base.GetComponent<Sensors>().GetSensor<ClosestEdibleSensor>().GetEdible();
		}

		// Token: 0x06008578 RID: 34168 RVA: 0x002EC86D File Offset: 0x002EAA6D
		public bool HasRationsAvailable()
		{
			return true;
		}

		// Token: 0x06008579 RID: 34169 RVA: 0x002EC870 File Offset: 0x002EAA70
		public float GetRationsAteToday()
		{
			return base.sm.rationsAteToday.Get(base.smi);
		}

		// Token: 0x0600857A RID: 34170 RVA: 0x002EC888 File Offset: 0x002EAA88
		public float GetRationsRemaining()
		{
			return 1f;
		}

		// Token: 0x0600857B RID: 34171 RVA: 0x002EC88F File Offset: 0x002EAA8F
		public bool IsEating()
		{
			return this.choreDriver.HasChore() && this.choreDriver.GetCurrentChore().choreType.urge == Db.Get().Urges.Eat;
		}

		// Token: 0x0600857C RID: 34172 RVA: 0x002EC8C6 File Offset: 0x002EAAC6
		public void OnNewDay()
		{
			base.smi.sm.rationsAteToday.Set(0f, base.smi, false);
		}

		// Token: 0x0600857D RID: 34173 RVA: 0x002EC8EC File Offset: 0x002EAAEC
		public void OnEatComplete(object data)
		{
			Edible edible = (Edible)data;
			base.sm.rationsAteToday.Delta(edible.caloriesConsumed, base.smi);
			RationTracker.Get().RegisterRationsConsumed(edible);
		}

		// Token: 0x04006800 RID: 26624
		private ChoreDriver choreDriver;
	}
}

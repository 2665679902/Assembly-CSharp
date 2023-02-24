using System;
using Klei.AI;

// Token: 0x0200081D RID: 2077
public class CalorieMonitor : GameStateMachine<CalorieMonitor, CalorieMonitor.Instance>
{
	// Token: 0x06003C53 RID: 15443 RVA: 0x0014FF64 File Offset: 0x0014E164
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.satisfied.Transition(this.hungry, (CalorieMonitor.Instance smi) => smi.IsHungry(), UpdateRate.SIM_200ms);
		this.hungry.DefaultState(this.hungry.normal).Transition(this.satisfied, (CalorieMonitor.Instance smi) => smi.IsSatisfied(), UpdateRate.SIM_200ms).EventTransition(GameHashes.BeginChore, this.eating, (CalorieMonitor.Instance smi) => smi.IsEating());
		this.hungry.working.EventTransition(GameHashes.ScheduleBlocksChanged, this.hungry.normal, (CalorieMonitor.Instance smi) => smi.IsEatTime()).Transition(this.hungry.starving, (CalorieMonitor.Instance smi) => smi.IsStarving(), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.Hungry, null);
		this.hungry.normal.EventTransition(GameHashes.ScheduleBlocksChanged, this.hungry.working, (CalorieMonitor.Instance smi) => !smi.IsEatTime()).Transition(this.hungry.starving, (CalorieMonitor.Instance smi) => smi.IsStarving(), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.Hungry, null)
			.ToggleUrge(Db.Get().Urges.Eat)
			.ToggleExpression(Db.Get().Expressions.Hungry, null)
			.ToggleThought(Db.Get().Thoughts.Starving, null);
		this.hungry.starving.Transition(this.hungry.normal, (CalorieMonitor.Instance smi) => !smi.IsStarving(), UpdateRate.SIM_200ms).Transition(this.depleted, (CalorieMonitor.Instance smi) => smi.IsDepleted(), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.Starving, null)
			.ToggleUrge(Db.Get().Urges.Eat)
			.ToggleExpression(Db.Get().Expressions.Hungry, null)
			.ToggleThought(Db.Get().Thoughts.Starving, null);
		this.eating.EventTransition(GameHashes.EndChore, this.satisfied, (CalorieMonitor.Instance smi) => !smi.IsEating());
		this.depleted.ToggleTag(GameTags.CaloriesDepleted).Enter(delegate(CalorieMonitor.Instance smi)
		{
			smi.Kill();
		});
	}

	// Token: 0x0400273D RID: 10045
	public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x0400273E RID: 10046
	public CalorieMonitor.HungryState hungry;

	// Token: 0x0400273F RID: 10047
	public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State eating;

	// Token: 0x04002740 RID: 10048
	public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State incapacitated;

	// Token: 0x04002741 RID: 10049
	public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State depleted;

	// Token: 0x02001581 RID: 5505
	public class HungryState : GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040066EF RID: 26351
		public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State working;

		// Token: 0x040066F0 RID: 26352
		public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State normal;

		// Token: 0x040066F1 RID: 26353
		public GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.State starving;
	}

	// Token: 0x02001582 RID: 5506
	public new class Instance : GameStateMachine<CalorieMonitor, CalorieMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600841A RID: 33818 RVA: 0x002E943A File Offset: 0x002E763A
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.calories = Db.Get().Amounts.Calories.Lookup(base.gameObject);
		}

		// Token: 0x0600841B RID: 33819 RVA: 0x002E9463 File Offset: 0x002E7663
		private float GetCalories0to1()
		{
			return this.calories.value / this.calories.GetMax();
		}

		// Token: 0x0600841C RID: 33820 RVA: 0x002E947C File Offset: 0x002E767C
		public bool IsEatTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Eat);
		}

		// Token: 0x0600841D RID: 33821 RVA: 0x002E949D File Offset: 0x002E769D
		public bool IsHungry()
		{
			return this.GetCalories0to1() < 0.825f;
		}

		// Token: 0x0600841E RID: 33822 RVA: 0x002E94AC File Offset: 0x002E76AC
		public bool IsStarving()
		{
			return this.GetCalories0to1() < 0.25f;
		}

		// Token: 0x0600841F RID: 33823 RVA: 0x002E94BB File Offset: 0x002E76BB
		public bool IsSatisfied()
		{
			return this.GetCalories0to1() > 0.95f;
		}

		// Token: 0x06008420 RID: 33824 RVA: 0x002E94CC File Offset: 0x002E76CC
		public bool IsEating()
		{
			ChoreDriver component = base.master.GetComponent<ChoreDriver>();
			return component.HasChore() && component.GetCurrentChore().choreType.urge == Db.Get().Urges.Eat;
		}

		// Token: 0x06008421 RID: 33825 RVA: 0x002E9510 File Offset: 0x002E7710
		public bool IsDepleted()
		{
			return this.calories.value <= 0f;
		}

		// Token: 0x06008422 RID: 33826 RVA: 0x002E9527 File Offset: 0x002E7727
		public bool ShouldExitInfirmary()
		{
			return !this.IsStarving();
		}

		// Token: 0x06008423 RID: 33827 RVA: 0x002E9532 File Offset: 0x002E7732
		public void Kill()
		{
			if (base.gameObject.GetSMI<DeathMonitor.Instance>() != null)
			{
				base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Starvation);
			}
		}

		// Token: 0x040066F2 RID: 26354
		public AmountInstance calories;
	}
}

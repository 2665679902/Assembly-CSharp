using System;
using Klei.AI;

// Token: 0x02000847 RID: 2119
public class StaminaMonitor : GameStateMachine<StaminaMonitor, StaminaMonitor.Instance>
{
	// Token: 0x06003D12 RID: 15634 RVA: 0x001552D0 File Offset: 0x001534D0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.root.ToggleStateMachine((StaminaMonitor.Instance smi) => new UrgeMonitor.Instance(smi.master, Db.Get().Urges.Sleep, Db.Get().Amounts.Stamina, Db.Get().ScheduleBlockTypes.Sleep, 100f, 0f, false)).ToggleStateMachine((StaminaMonitor.Instance smi) => new SleepChoreMonitor.Instance(smi.master));
		this.satisfied.Transition(this.sleepy, (StaminaMonitor.Instance smi) => smi.NeedsToSleep() || smi.WantsToSleep(), UpdateRate.SIM_200ms);
		this.sleepy.Update("Check Sleep State", delegate(StaminaMonitor.Instance smi, float dt)
		{
			smi.TryExitSleepState();
		}, UpdateRate.SIM_1000ms, false).DefaultState(this.sleepy.needssleep);
		this.sleepy.needssleep.Transition(this.sleepy.sleeping, (StaminaMonitor.Instance smi) => smi.IsSleeping(), UpdateRate.SIM_200ms).ToggleExpression(Db.Get().Expressions.Tired, null).ToggleStatusItem(Db.Get().DuplicantStatusItems.Tired, null)
			.ToggleThought(Db.Get().Thoughts.Sleepy, null);
		this.sleepy.sleeping.Enter(delegate(StaminaMonitor.Instance smi)
		{
			smi.CheckDebugFastWorkMode();
		}).Transition(this.satisfied, (StaminaMonitor.Instance smi) => !smi.IsSleeping(), UpdateRate.SIM_200ms);
	}

	// Token: 0x040027F8 RID: 10232
	public GameStateMachine<StaminaMonitor, StaminaMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040027F9 RID: 10233
	public StaminaMonitor.SleepyState sleepy;

	// Token: 0x040027FA RID: 10234
	private const float OUTSIDE_SCHEDULE_STAMINA_THRESHOLD = 0f;

	// Token: 0x020015E6 RID: 5606
	public class SleepyState : GameStateMachine<StaminaMonitor, StaminaMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006839 RID: 26681
		public GameStateMachine<StaminaMonitor, StaminaMonitor.Instance, IStateMachineTarget, object>.State needssleep;

		// Token: 0x0400683A RID: 26682
		public GameStateMachine<StaminaMonitor, StaminaMonitor.Instance, IStateMachineTarget, object>.State sleeping;
	}

	// Token: 0x020015E7 RID: 5607
	public new class Instance : GameStateMachine<StaminaMonitor, StaminaMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060085D1 RID: 34257 RVA: 0x002ED438 File Offset: 0x002EB638
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.stamina = Db.Get().Amounts.Stamina.Lookup(base.gameObject);
			this.choreDriver = base.GetComponent<ChoreDriver>();
			this.schedulable = base.GetComponent<Schedulable>();
		}

		// Token: 0x060085D2 RID: 34258 RVA: 0x002ED484 File Offset: 0x002EB684
		public bool NeedsToSleep()
		{
			return this.stamina.value <= 0f;
		}

		// Token: 0x060085D3 RID: 34259 RVA: 0x002ED49B File Offset: 0x002EB69B
		public bool WantsToSleep()
		{
			return this.choreDriver.HasChore() && this.choreDriver.GetCurrentChore().SatisfiesUrge(Db.Get().Urges.Sleep);
		}

		// Token: 0x060085D4 RID: 34260 RVA: 0x002ED4CB File Offset: 0x002EB6CB
		public void TryExitSleepState()
		{
			if (!this.NeedsToSleep() && !this.WantsToSleep())
			{
				base.smi.GoTo(base.smi.sm.satisfied);
			}
		}

		// Token: 0x060085D5 RID: 34261 RVA: 0x002ED4F8 File Offset: 0x002EB6F8
		public bool IsSleeping()
		{
			bool flag = false;
			if (this.WantsToSleep() && this.choreDriver.GetComponent<Worker>().workable != null)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060085D6 RID: 34262 RVA: 0x002ED52A File Offset: 0x002EB72A
		public void CheckDebugFastWorkMode()
		{
			if (Game.Instance.FastWorkersModeActive)
			{
				this.stamina.value = this.stamina.GetMax();
			}
		}

		// Token: 0x060085D7 RID: 34263 RVA: 0x002ED550 File Offset: 0x002EB750
		public bool ShouldExitSleep()
		{
			if (this.schedulable.IsAllowed(Db.Get().ScheduleBlockTypes.Sleep))
			{
				return false;
			}
			Narcolepsy component = base.GetComponent<Narcolepsy>();
			return (!(component != null) || !component.IsNarcolepsing()) && this.stamina.value >= this.stamina.GetMax();
		}

		// Token: 0x0400683B RID: 26683
		private ChoreDriver choreDriver;

		// Token: 0x0400683C RID: 26684
		private Schedulable schedulable;

		// Token: 0x0400683D RID: 26685
		public AmountInstance stamina;
	}
}

using System;
using Klei.AI;

// Token: 0x0200081C RID: 2076
public class BreathMonitor : GameStateMachine<BreathMonitor, BreathMonitor.Instance>
{
	// Token: 0x06003C46 RID: 15430 RVA: 0x0014FBFC File Offset: 0x0014DDFC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.DefaultState(this.satisfied.full).Transition(this.lowbreath, new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(BreathMonitor.IsLowBreath), UpdateRate.SIM_200ms);
		this.satisfied.full.Transition(this.satisfied.notfull, new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(BreathMonitor.IsNotFullBreath), UpdateRate.SIM_200ms).Enter(new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State.Callback(BreathMonitor.HideBreathBar));
		this.satisfied.notfull.Transition(this.satisfied.full, new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(BreathMonitor.IsFullBreath), UpdateRate.SIM_200ms).Enter(new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State.Callback(BreathMonitor.ShowBreathBar));
		this.lowbreath.DefaultState(this.lowbreath.nowheretorecover).Transition(this.satisfied, new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(BreathMonitor.IsFullBreath), UpdateRate.SIM_200ms).ToggleExpression(Db.Get().Expressions.RecoverBreath, new Func<BreathMonitor.Instance, bool>(BreathMonitor.IsNotInBreathableArea))
			.ToggleUrge(Db.Get().Urges.RecoverBreath)
			.ToggleThought(Db.Get().Thoughts.Suffocating, null)
			.ToggleTag(GameTags.HoldingBreath)
			.Enter(new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State.Callback(BreathMonitor.ShowBreathBar))
			.Enter(new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State.Callback(BreathMonitor.UpdateRecoverBreathCell))
			.Update(new Action<BreathMonitor.Instance, float>(BreathMonitor.UpdateRecoverBreathCell), UpdateRate.RENDER_1000ms, true);
		this.lowbreath.nowheretorecover.ParamTransition<int>(this.recoverBreathCell, this.lowbreath.recoveryavailable, new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.Parameter<int>.Callback(BreathMonitor.IsValidRecoverCell));
		this.lowbreath.recoveryavailable.ParamTransition<int>(this.recoverBreathCell, this.lowbreath.nowheretorecover, new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.Parameter<int>.Callback(BreathMonitor.IsNotValidRecoverCell)).Enter(new StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State.Callback(BreathMonitor.UpdateRecoverBreathCell)).ToggleChore(new Func<BreathMonitor.Instance, Chore>(BreathMonitor.CreateRecoverBreathChore), this.lowbreath.nowheretorecover);
	}

	// Token: 0x06003C47 RID: 15431 RVA: 0x0014FDF4 File Offset: 0x0014DFF4
	private static bool IsLowBreath(BreathMonitor.Instance smi)
	{
		if (smi.master.gameObject.GetMyWorld().AlertManager.IsRedAlert())
		{
			return smi.breath.value < 45.454548f;
		}
		return smi.breath.value < 72.72727f;
	}

	// Token: 0x06003C48 RID: 15432 RVA: 0x0014FE42 File Offset: 0x0014E042
	private static Chore CreateRecoverBreathChore(BreathMonitor.Instance smi)
	{
		return new RecoverBreathChore(smi.master);
	}

	// Token: 0x06003C49 RID: 15433 RVA: 0x0014FE4F File Offset: 0x0014E04F
	private static bool IsNotFullBreath(BreathMonitor.Instance smi)
	{
		return !BreathMonitor.IsFullBreath(smi);
	}

	// Token: 0x06003C4A RID: 15434 RVA: 0x0014FE5A File Offset: 0x0014E05A
	private static bool IsFullBreath(BreathMonitor.Instance smi)
	{
		return smi.breath.value >= smi.breath.GetMax();
	}

	// Token: 0x06003C4B RID: 15435 RVA: 0x0014FE77 File Offset: 0x0014E077
	private static bool IsNotInBreathableArea(BreathMonitor.Instance smi)
	{
		return !smi.breather.IsBreathableElementAtCell(Grid.PosToCell(smi), null);
	}

	// Token: 0x06003C4C RID: 15436 RVA: 0x0014FE8E File Offset: 0x0014E08E
	private static void ShowBreathBar(BreathMonitor.Instance smi)
	{
		if (NameDisplayScreen.Instance != null)
		{
			NameDisplayScreen.Instance.SetBreathDisplay(smi.gameObject, new Func<float>(smi.GetBreath), true);
		}
	}

	// Token: 0x06003C4D RID: 15437 RVA: 0x0014FEBA File Offset: 0x0014E0BA
	private static void HideBreathBar(BreathMonitor.Instance smi)
	{
		if (NameDisplayScreen.Instance != null)
		{
			NameDisplayScreen.Instance.SetBreathDisplay(smi.gameObject, null, false);
		}
	}

	// Token: 0x06003C4E RID: 15438 RVA: 0x0014FEDB File Offset: 0x0014E0DB
	private static bool IsValidRecoverCell(BreathMonitor.Instance smi, int cell)
	{
		return cell != Grid.InvalidCell;
	}

	// Token: 0x06003C4F RID: 15439 RVA: 0x0014FEE8 File Offset: 0x0014E0E8
	private static bool IsNotValidRecoverCell(BreathMonitor.Instance smi, int cell)
	{
		return !BreathMonitor.IsValidRecoverCell(smi, cell);
	}

	// Token: 0x06003C50 RID: 15440 RVA: 0x0014FEF4 File Offset: 0x0014E0F4
	private static void UpdateRecoverBreathCell(BreathMonitor.Instance smi, float dt)
	{
		BreathMonitor.UpdateRecoverBreathCell(smi);
	}

	// Token: 0x06003C51 RID: 15441 RVA: 0x0014FEFC File Offset: 0x0014E0FC
	private static void UpdateRecoverBreathCell(BreathMonitor.Instance smi)
	{
		smi.query.Reset();
		smi.navigator.RunQuery(smi.query);
		int num = smi.query.GetResultCell();
		if (!smi.breather.IsBreathableElementAtCell(num, null))
		{
			num = PathFinder.InvalidCell;
		}
		smi.sm.recoverBreathCell.Set(num, smi, false);
	}

	// Token: 0x0400273A RID: 10042
	public BreathMonitor.SatisfiedState satisfied;

	// Token: 0x0400273B RID: 10043
	public BreathMonitor.LowBreathState lowbreath;

	// Token: 0x0400273C RID: 10044
	public StateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.IntParameter recoverBreathCell;

	// Token: 0x0200157E RID: 5502
	public class LowBreathState : GameStateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040066E7 RID: 26343
		public GameStateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State nowheretorecover;

		// Token: 0x040066E8 RID: 26344
		public GameStateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State recoveryavailable;
	}

	// Token: 0x0200157F RID: 5503
	public class SatisfiedState : GameStateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040066E9 RID: 26345
		public GameStateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State full;

		// Token: 0x040066EA RID: 26346
		public GameStateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.State notfull;
	}

	// Token: 0x02001580 RID: 5504
	public new class Instance : GameStateMachine<BreathMonitor, BreathMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008416 RID: 33814 RVA: 0x002E9390 File Offset: 0x002E7590
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.breath = Db.Get().Amounts.Breath.Lookup(master.gameObject);
			this.query = new SafetyQuery(Game.Instance.safetyConditions.RecoverBreathChecker, base.GetComponent<KMonoBehaviour>(), int.MaxValue);
			this.navigator = base.GetComponent<Navigator>();
			this.breather = base.GetComponent<OxygenBreather>();
		}

		// Token: 0x06008417 RID: 33815 RVA: 0x002E9401 File Offset: 0x002E7601
		public int GetRecoverCell()
		{
			return base.sm.recoverBreathCell.Get(base.smi);
		}

		// Token: 0x06008418 RID: 33816 RVA: 0x002E9419 File Offset: 0x002E7619
		public float GetBreath()
		{
			return this.breath.value / this.breath.GetMax();
		}

		// Token: 0x040066EB RID: 26347
		public AmountInstance breath;

		// Token: 0x040066EC RID: 26348
		public SafetyQuery query;

		// Token: 0x040066ED RID: 26349
		public Navigator navigator;

		// Token: 0x040066EE RID: 26350
		public OxygenBreather breather;
	}
}

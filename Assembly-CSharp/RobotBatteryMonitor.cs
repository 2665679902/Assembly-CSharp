using System;
using Klei.AI;

// Token: 0x020008E9 RID: 2281
public class RobotBatteryMonitor : GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>
{
	// Token: 0x060041B9 RID: 16825 RVA: 0x0017189C File Offset: 0x0016FA9C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.drainingStates;
		this.drainingStates.DefaultState(this.drainingStates.highBattery).Transition(this.deadBattery, new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.BatteryDead), UpdateRate.SIM_200ms).Transition(this.needsRechargeStates, new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.NeedsRecharge), UpdateRate.SIM_200ms);
		this.drainingStates.highBattery.Transition(this.drainingStates.lowBattery, GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Not(new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.ChargeDecent)), UpdateRate.SIM_200ms);
		this.drainingStates.lowBattery.Transition(this.drainingStates.highBattery, new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.ChargeDecent), UpdateRate.SIM_200ms).ToggleStatusItem(delegate(RobotBatteryMonitor.Instance smi)
		{
			if (!smi.def.canCharge)
			{
				return Db.Get().RobotStatusItems.LowBatteryNoCharge;
			}
			return Db.Get().RobotStatusItems.LowBattery;
		}, (RobotBatteryMonitor.Instance smi) => smi.gameObject);
		this.needsRechargeStates.DefaultState(this.needsRechargeStates.lowBattery).Transition(this.deadBattery, new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.BatteryDead), UpdateRate.SIM_200ms).Transition(this.drainingStates, new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.ChargeComplete), UpdateRate.SIM_200ms)
			.ToggleBehaviour(GameTags.Robots.Behaviours.RechargeBehaviour, (RobotBatteryMonitor.Instance smi) => smi.def.canCharge, null);
		this.needsRechargeStates.lowBattery.ToggleStatusItem(delegate(RobotBatteryMonitor.Instance smi)
		{
			if (!smi.def.canCharge)
			{
				return Db.Get().RobotStatusItems.LowBatteryNoCharge;
			}
			return Db.Get().RobotStatusItems.LowBattery;
		}, (RobotBatteryMonitor.Instance smi) => smi.gameObject).Transition(this.needsRechargeStates.mediumBattery, new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.ChargeDecent), UpdateRate.SIM_200ms);
		this.needsRechargeStates.mediumBattery.Transition(this.needsRechargeStates.lowBattery, GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Not(new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.ChargeDecent)), UpdateRate.SIM_200ms).Transition(this.needsRechargeStates.trickleCharge, new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.ChargeFull), UpdateRate.SIM_200ms);
		this.needsRechargeStates.trickleCharge.Transition(this.needsRechargeStates.mediumBattery, GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Not(new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.Transition.ConditionCallback(RobotBatteryMonitor.ChargeFull)), UpdateRate.SIM_200ms);
		this.deadBattery.ToggleStatusItem(Db.Get().RobotStatusItems.DeadBattery, (RobotBatteryMonitor.Instance smi) => smi.gameObject).Enter(delegate(RobotBatteryMonitor.Instance smi)
		{
			if (smi.GetSMI<DeathMonitor.Instance>() != null)
			{
				smi.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.DeadBattery);
			}
		});
	}

	// Token: 0x060041BA RID: 16826 RVA: 0x00171B46 File Offset: 0x0016FD46
	public static bool NeedsRecharge(RobotBatteryMonitor.Instance smi)
	{
		return smi.amountInstance.value <= 0f || GameClock.Instance.IsNighttime();
	}

	// Token: 0x060041BB RID: 16827 RVA: 0x00171B66 File Offset: 0x0016FD66
	public static bool ChargeDecent(RobotBatteryMonitor.Instance smi)
	{
		return smi.amountInstance.value >= smi.amountInstance.GetMax() * smi.def.lowBatteryWarningPercent;
	}

	// Token: 0x060041BC RID: 16828 RVA: 0x00171B8F File Offset: 0x0016FD8F
	public static bool ChargeFull(RobotBatteryMonitor.Instance smi)
	{
		return smi.amountInstance.value >= smi.amountInstance.GetMax();
	}

	// Token: 0x060041BD RID: 16829 RVA: 0x00171BAC File Offset: 0x0016FDAC
	public static bool ChargeComplete(RobotBatteryMonitor.Instance smi)
	{
		return smi.amountInstance.value >= smi.amountInstance.GetMax() && !GameClock.Instance.IsNighttime();
	}

	// Token: 0x060041BE RID: 16830 RVA: 0x00171BD5 File Offset: 0x0016FDD5
	public static bool BatteryDead(RobotBatteryMonitor.Instance smi)
	{
		return !smi.def.canCharge && smi.amountInstance.value == 0f;
	}

	// Token: 0x04002BCA RID: 11210
	public StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.ObjectParameter<Storage> internalStorage = new StateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.ObjectParameter<Storage>();

	// Token: 0x04002BCB RID: 11211
	public RobotBatteryMonitor.NeedsRechargeStates needsRechargeStates;

	// Token: 0x04002BCC RID: 11212
	public RobotBatteryMonitor.DrainingStates drainingStates;

	// Token: 0x04002BCD RID: 11213
	public GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.State deadBattery;

	// Token: 0x020016A7 RID: 5799
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006A8A RID: 27274
		public string batteryAmountId;

		// Token: 0x04006A8B RID: 27275
		public float lowBatteryWarningPercent;

		// Token: 0x04006A8C RID: 27276
		public bool canCharge;
	}

	// Token: 0x020016A8 RID: 5800
	public class DrainingStates : GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.State
	{
		// Token: 0x04006A8D RID: 27277
		public GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.State highBattery;

		// Token: 0x04006A8E RID: 27278
		public GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.State lowBattery;
	}

	// Token: 0x020016A9 RID: 5801
	public class NeedsRechargeStates : GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.State
	{
		// Token: 0x04006A8F RID: 27279
		public GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.State lowBattery;

		// Token: 0x04006A90 RID: 27280
		public GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.State mediumBattery;

		// Token: 0x04006A91 RID: 27281
		public GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.State trickleCharge;
	}

	// Token: 0x020016AA RID: 5802
	public new class Instance : GameStateMachine<RobotBatteryMonitor, RobotBatteryMonitor.Instance, IStateMachineTarget, RobotBatteryMonitor.Def>.GameInstance
	{
		// Token: 0x06008829 RID: 34857 RVA: 0x002F4D60 File Offset: 0x002F2F60
		public Instance(IStateMachineTarget master, RobotBatteryMonitor.Def def)
			: base(master, def)
		{
			this.amountInstance = Db.Get().Amounts.Get(def.batteryAmountId).Lookup(base.gameObject);
			this.amountInstance.SetValue(this.amountInstance.GetMax());
		}

		// Token: 0x04006A92 RID: 27282
		public AmountInstance amountInstance;
	}
}

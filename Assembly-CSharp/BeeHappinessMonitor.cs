using System;
using Klei.AI;
using STRINGS;

// Token: 0x020006BF RID: 1727
public class BeeHappinessMonitor : GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>
{
	// Token: 0x06002F01 RID: 12033 RVA: 0x000F8BC4 File Offset: 0x000F6DC4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.Transition(this.happy, new StateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Transition.ConditionCallback(BeeHappinessMonitor.IsHappy), UpdateRate.SIM_1000ms).Transition(this.unhappy, GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Not(new StateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Transition.ConditionCallback(BeeHappinessMonitor.IsHappy)), UpdateRate.SIM_1000ms);
		this.happy.ToggleEffect((BeeHappinessMonitor.Instance smi) => this.happyEffect).TriggerOnEnter(GameHashes.Happy, null).Transition(this.satisfied, GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Not(new StateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Transition.ConditionCallback(BeeHappinessMonitor.IsHappy)), UpdateRate.SIM_1000ms);
		this.unhappy.TriggerOnEnter(GameHashes.Unhappy, null).Transition(this.satisfied, new StateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Transition.ConditionCallback(BeeHappinessMonitor.IsHappy), UpdateRate.SIM_1000ms).ToggleEffect((BeeHappinessMonitor.Instance smi) => this.unhappyEffect);
		this.happyEffect = new Effect("Happy", CREATURES.MODIFIERS.HAPPY.NAME, CREATURES.MODIFIERS.HAPPY.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.unhappyEffect = new Effect("Unhappy", CREATURES.MODIFIERS.UNHAPPY.NAME, CREATURES.MODIFIERS.UNHAPPY.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
	}

	// Token: 0x06002F02 RID: 12034 RVA: 0x000F8D0A File Offset: 0x000F6F0A
	private static bool IsHappy(BeeHappinessMonitor.Instance smi)
	{
		return smi.happiness.GetTotalValue() >= smi.def.threshold;
	}

	// Token: 0x04001C47 RID: 7239
	private GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.State satisfied;

	// Token: 0x04001C48 RID: 7240
	private GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.State happy;

	// Token: 0x04001C49 RID: 7241
	private GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.State unhappy;

	// Token: 0x04001C4A RID: 7242
	private Effect happyEffect;

	// Token: 0x04001C4B RID: 7243
	private Effect unhappyEffect;

	// Token: 0x0200139B RID: 5019
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400612C RID: 24876
		public float threshold;
	}

	// Token: 0x0200139C RID: 5020
	public new class Instance : GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.GameInstance
	{
		// Token: 0x06007E6E RID: 32366 RVA: 0x002D9343 File Offset: 0x002D7543
		public Instance(IStateMachineTarget master, BeeHappinessMonitor.Def def)
			: base(master, def)
		{
			this.happiness = base.gameObject.GetAttributes().Add(Db.Get().CritterAttributes.Happiness);
		}

		// Token: 0x0400612D RID: 24877
		public AttributeInstance happiness;
	}
}

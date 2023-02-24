using System;
using Klei.AI;
using STRINGS;

// Token: 0x020006DA RID: 1754
public class HappinessMonitor : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>
{
	// Token: 0x06002FBB RID: 12219 RVA: 0x000FC040 File Offset: 0x000FA240
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.Transition(this.happy, new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsHappy), UpdateRate.SIM_1000ms).Transition(this.unhappy, GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Not(new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsHappy)), UpdateRate.SIM_1000ms);
		this.happy.DefaultState(this.happy.wild).Transition(this.satisfied, GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Not(new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsHappy)), UpdateRate.SIM_1000ms);
		this.happy.wild.ToggleEffect((HappinessMonitor.Instance smi) => this.happyWildEffect).TagTransition(GameTags.Creatures.Wild, this.happy.tame, true);
		this.happy.tame.ToggleEffect((HappinessMonitor.Instance smi) => this.happyTameEffect).TagTransition(GameTags.Creatures.Wild, this.happy.wild, false);
		this.unhappy.DefaultState(this.unhappy.wild).Transition(this.satisfied, new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsHappy), UpdateRate.SIM_1000ms).ToggleTag(GameTags.Creatures.Unhappy);
		this.unhappy.wild.ToggleEffect((HappinessMonitor.Instance smi) => this.unhappyWildEffect).TagTransition(GameTags.Creatures.Wild, this.unhappy.tame, true);
		this.unhappy.tame.ToggleEffect((HappinessMonitor.Instance smi) => this.unhappyTameEffect).TagTransition(GameTags.Creatures.Wild, this.unhappy.wild, false);
		this.happyWildEffect = new Effect("Happy", CREATURES.MODIFIERS.HAPPY.NAME, CREATURES.MODIFIERS.HAPPY.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.happyTameEffect = new Effect("Happy", CREATURES.MODIFIERS.HAPPY.NAME, CREATURES.MODIFIERS.HAPPY.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.unhappyWildEffect = new Effect("Unhappy", CREATURES.MODIFIERS.UNHAPPY.NAME, CREATURES.MODIFIERS.UNHAPPY.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		this.unhappyTameEffect = new Effect("Unhappy", CREATURES.MODIFIERS.UNHAPPY.NAME, CREATURES.MODIFIERS.UNHAPPY.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		this.happyTameEffect.Add(new AttributeModifier(Db.Get().Amounts.Fertility.deltaAttribute.Id, 9f, CREATURES.MODIFIERS.HAPPY.NAME, true, false, true));
		this.unhappyWildEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, -15f, CREATURES.MODIFIERS.UNHAPPY.NAME, false, false, true));
		this.unhappyTameEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, -80f, CREATURES.MODIFIERS.UNHAPPY.NAME, false, false, true));
	}

	// Token: 0x06002FBC RID: 12220 RVA: 0x000FC365 File Offset: 0x000FA565
	private static bool IsHappy(HappinessMonitor.Instance smi)
	{
		return smi.happiness.GetTotalValue() >= smi.def.threshold;
	}

	// Token: 0x04001CBE RID: 7358
	private GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State satisfied;

	// Token: 0x04001CBF RID: 7359
	private HappinessMonitor.HappyState happy;

	// Token: 0x04001CC0 RID: 7360
	private HappinessMonitor.UnhappyState unhappy;

	// Token: 0x04001CC1 RID: 7361
	private Effect happyWildEffect;

	// Token: 0x04001CC2 RID: 7362
	private Effect happyTameEffect;

	// Token: 0x04001CC3 RID: 7363
	private Effect unhappyWildEffect;

	// Token: 0x04001CC4 RID: 7364
	private Effect unhappyTameEffect;

	// Token: 0x020013DA RID: 5082
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040061D2 RID: 25042
		public float threshold;
	}

	// Token: 0x020013DB RID: 5083
	public class UnhappyState : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State
	{
		// Token: 0x040061D3 RID: 25043
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State wild;

		// Token: 0x040061D4 RID: 25044
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State tame;
	}

	// Token: 0x020013DC RID: 5084
	public class HappyState : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State
	{
		// Token: 0x040061D5 RID: 25045
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State wild;

		// Token: 0x040061D6 RID: 25046
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State tame;
	}

	// Token: 0x020013DD RID: 5085
	public new class Instance : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.GameInstance
	{
		// Token: 0x06007F55 RID: 32597 RVA: 0x002DC1DB File Offset: 0x002DA3DB
		public Instance(IStateMachineTarget master, HappinessMonitor.Def def)
			: base(master, def)
		{
			this.happiness = base.gameObject.GetAttributes().Add(Db.Get().CritterAttributes.Happiness);
		}

		// Token: 0x040061D7 RID: 25047
		public AttributeInstance happiness;
	}
}

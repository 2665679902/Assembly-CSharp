using System;

// Token: 0x020000C5 RID: 197
public class HiveGrowthMonitor : GameStateMachine<HiveGrowthMonitor, HiveGrowthMonitor.Instance, IStateMachineTarget, HiveGrowthMonitor.Def>
{
	// Token: 0x0600036E RID: 878 RVA: 0x0001AF25 File Offset: 0x00019125
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Behaviours.GrowUpBehaviour, new StateMachine<HiveGrowthMonitor, HiveGrowthMonitor.Instance, IStateMachineTarget, HiveGrowthMonitor.Def>.Transition.ConditionCallback(HiveGrowthMonitor.IsGrowing), null);
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0001AF4D File Offset: 0x0001914D
	public static bool IsGrowing(HiveGrowthMonitor.Instance smi)
	{
		return !smi.GetSMI<BeeHive.StatesInstance>().IsFullyGrown();
	}

	// Token: 0x02000E6C RID: 3692
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E6D RID: 3693
	public new class Instance : GameStateMachine<HiveGrowthMonitor, HiveGrowthMonitor.Instance, IStateMachineTarget, HiveGrowthMonitor.Def>.GameInstance
	{
		// Token: 0x06006C38 RID: 27704 RVA: 0x00297A08 File Offset: 0x00295C08
		public Instance(IStateMachineTarget master, HiveGrowthMonitor.Def def)
			: base(master, def)
		{
		}
	}
}

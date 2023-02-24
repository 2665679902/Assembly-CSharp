using System;

// Token: 0x020006C7 RID: 1735
public class CreatureSleepMonitor : GameStateMachine<CreatureSleepMonitor, CreatureSleepMonitor.Instance, IStateMachineTarget, CreatureSleepMonitor.Def>
{
	// Token: 0x06002F37 RID: 12087 RVA: 0x000F9A09 File Offset: 0x000F7C09
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Behaviours.SleepBehaviour, new StateMachine<CreatureSleepMonitor, CreatureSleepMonitor.Instance, IStateMachineTarget, CreatureSleepMonitor.Def>.Transition.ConditionCallback(CreatureSleepMonitor.ShouldSleep), null);
	}

	// Token: 0x06002F38 RID: 12088 RVA: 0x000F9A31 File Offset: 0x000F7C31
	public static bool ShouldSleep(CreatureSleepMonitor.Instance smi)
	{
		return GameClock.Instance.IsNighttime();
	}

	// Token: 0x020013AB RID: 5035
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020013AC RID: 5036
	public new class Instance : GameStateMachine<CreatureSleepMonitor, CreatureSleepMonitor.Instance, IStateMachineTarget, CreatureSleepMonitor.Def>.GameInstance
	{
		// Token: 0x06007E9B RID: 32411 RVA: 0x002D97D6 File Offset: 0x002D79D6
		public Instance(IStateMachineTarget master, CreatureSleepMonitor.Def def)
			: base(master, def)
		{
		}
	}
}

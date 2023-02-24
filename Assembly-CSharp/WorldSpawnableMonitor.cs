using System;

// Token: 0x02000470 RID: 1136
public class WorldSpawnableMonitor : GameStateMachine<WorldSpawnableMonitor, WorldSpawnableMonitor.Instance, IStateMachineTarget, WorldSpawnableMonitor.Def>
{
	// Token: 0x06001921 RID: 6433 RVA: 0x000867D9 File Offset: 0x000849D9
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
	}

	// Token: 0x020010B7 RID: 4279
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005881 RID: 22657
		public Func<int, int> adjustSpawnLocationCb;
	}

	// Token: 0x020010B8 RID: 4280
	public new class Instance : GameStateMachine<WorldSpawnableMonitor, WorldSpawnableMonitor.Instance, IStateMachineTarget, WorldSpawnableMonitor.Def>.GameInstance
	{
		// Token: 0x06007417 RID: 29719 RVA: 0x002B2519 File Offset: 0x002B0719
		public Instance(IStateMachineTarget master, WorldSpawnableMonitor.Def def)
			: base(master, def)
		{
		}
	}
}

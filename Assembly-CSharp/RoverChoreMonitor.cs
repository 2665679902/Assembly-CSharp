using System;
using KSerialization;

// Token: 0x020006EA RID: 1770
public class RoverChoreMonitor : GameStateMachine<RoverChoreMonitor, RoverChoreMonitor.Instance, IStateMachineTarget, RoverChoreMonitor.Def>
{
	// Token: 0x06003021 RID: 12321 RVA: 0x000FE518 File Offset: 0x000FC718
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		this.loop.ToggleBehaviour(GameTags.Creatures.Tunnel, (RoverChoreMonitor.Instance smi) => true, null).ToggleBehaviour(GameTags.Creatures.Builder, (RoverChoreMonitor.Instance smi) => true, null);
	}

	// Token: 0x04001D07 RID: 7431
	public GameStateMachine<RoverChoreMonitor, RoverChoreMonitor.Instance, IStateMachineTarget, RoverChoreMonitor.Def>.State loop;

	// Token: 0x020013FF RID: 5119
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001400 RID: 5120
	public new class Instance : GameStateMachine<RoverChoreMonitor, RoverChoreMonitor.Instance, IStateMachineTarget, RoverChoreMonitor.Def>.GameInstance
	{
		// Token: 0x06007FC5 RID: 32709 RVA: 0x002DDB1B File Offset: 0x002DBD1B
		public Instance(IStateMachineTarget master, RoverChoreMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007FC6 RID: 32710 RVA: 0x002DDB2C File Offset: 0x002DBD2C
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
		}

		// Token: 0x04006231 RID: 25137
		[Serialize]
		public int lastDigCell = -1;

		// Token: 0x04006232 RID: 25138
		private Action<object> OnDestinationReachedDelegate;
	}
}

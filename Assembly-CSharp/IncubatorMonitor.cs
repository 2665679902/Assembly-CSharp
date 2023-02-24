using System;

// Token: 0x0200046C RID: 1132
public class IncubatorMonitor : GameStateMachine<IncubatorMonitor, IncubatorMonitor.Instance, IStateMachineTarget, IncubatorMonitor.Def>
{
	// Token: 0x06001913 RID: 6419 RVA: 0x00086048 File Offset: 0x00084248
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.not;
		this.not.EventTransition(GameHashes.OnStore, this.in_incubator, new StateMachine<IncubatorMonitor, IncubatorMonitor.Instance, IStateMachineTarget, IncubatorMonitor.Def>.Transition.ConditionCallback(IncubatorMonitor.InIncubator));
		this.in_incubator.ToggleTag(GameTags.Creatures.InIncubator).EventTransition(GameHashes.OnStore, this.not, GameStateMachine<IncubatorMonitor, IncubatorMonitor.Instance, IStateMachineTarget, IncubatorMonitor.Def>.Not(new StateMachine<IncubatorMonitor, IncubatorMonitor.Instance, IStateMachineTarget, IncubatorMonitor.Def>.Transition.ConditionCallback(IncubatorMonitor.InIncubator)));
	}

	// Token: 0x06001914 RID: 6420 RVA: 0x000860B2 File Offset: 0x000842B2
	public static bool InIncubator(IncubatorMonitor.Instance smi)
	{
		return smi.gameObject.transform.parent && smi.gameObject.transform.parent.GetComponent<EggIncubator>() != null;
	}

	// Token: 0x04000E08 RID: 3592
	public GameStateMachine<IncubatorMonitor, IncubatorMonitor.Instance, IStateMachineTarget, IncubatorMonitor.Def>.State not;

	// Token: 0x04000E09 RID: 3593
	public GameStateMachine<IncubatorMonitor, IncubatorMonitor.Instance, IStateMachineTarget, IncubatorMonitor.Def>.State in_incubator;

	// Token: 0x020010AC RID: 4268
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020010AD RID: 4269
	public new class Instance : GameStateMachine<IncubatorMonitor, IncubatorMonitor.Instance, IStateMachineTarget, IncubatorMonitor.Def>.GameInstance
	{
		// Token: 0x060073F6 RID: 29686 RVA: 0x002B210C File Offset: 0x002B030C
		public Instance(IStateMachineTarget master, IncubatorMonitor.Def def)
			: base(master, def)
		{
		}
	}
}

using System;

// Token: 0x02000824 RID: 2084
public class DebugGoToMonitor : GameStateMachine<DebugGoToMonitor, DebugGoToMonitor.Instance, IStateMachineTarget, DebugGoToMonitor.Def>
{
	// Token: 0x06003C69 RID: 15465 RVA: 0x00150A14 File Offset: 0x0014EC14
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.DoNothing();
		this.hastarget.ToggleChore((DebugGoToMonitor.Instance smi) => new MoveChore(smi.master, Db.Get().ChoreTypes.DebugGoTo, (MoveChore.StatesInstance smii) => DebugHandler.GetMouseCell(), false), this.satisfied);
	}

	// Token: 0x04002758 RID: 10072
	public GameStateMachine<DebugGoToMonitor, DebugGoToMonitor.Instance, IStateMachineTarget, DebugGoToMonitor.Def>.State satisfied;

	// Token: 0x04002759 RID: 10073
	public GameStateMachine<DebugGoToMonitor, DebugGoToMonitor.Instance, IStateMachineTarget, DebugGoToMonitor.Def>.State hastarget;

	// Token: 0x02001594 RID: 5524
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001595 RID: 5525
	public new class Instance : GameStateMachine<DebugGoToMonitor, DebugGoToMonitor.Instance, IStateMachineTarget, DebugGoToMonitor.Def>.GameInstance
	{
		// Token: 0x06008468 RID: 33896 RVA: 0x002E9D1C File Offset: 0x002E7F1C
		public Instance(IStateMachineTarget target, DebugGoToMonitor.Def def)
			: base(target, def)
		{
		}

		// Token: 0x06008469 RID: 33897 RVA: 0x002E9D26 File Offset: 0x002E7F26
		public void GoToCursor()
		{
			base.smi.GoTo(base.smi.sm.satisfied);
			base.smi.GoTo(base.smi.sm.hastarget);
		}
	}
}

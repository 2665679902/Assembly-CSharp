using System;

// Token: 0x02000821 RID: 2081
public class CreatureDebugGoToMonitor : GameStateMachine<CreatureDebugGoToMonitor, CreatureDebugGoToMonitor.Instance, IStateMachineTarget, CreatureDebugGoToMonitor.Def>
{
	// Token: 0x06003C5C RID: 15452 RVA: 0x00150576 File Offset: 0x0014E776
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.HasDebugDestination, new StateMachine<CreatureDebugGoToMonitor, CreatureDebugGoToMonitor.Instance, IStateMachineTarget, CreatureDebugGoToMonitor.Def>.Transition.ConditionCallback(CreatureDebugGoToMonitor.HasTargetCell), new Action<CreatureDebugGoToMonitor.Instance>(CreatureDebugGoToMonitor.ClearTargetCell));
	}

	// Token: 0x06003C5D RID: 15453 RVA: 0x001505A9 File Offset: 0x0014E7A9
	private static bool HasTargetCell(CreatureDebugGoToMonitor.Instance smi)
	{
		return smi.targetCell != Grid.InvalidCell;
	}

	// Token: 0x06003C5E RID: 15454 RVA: 0x001505BB File Offset: 0x0014E7BB
	private static void ClearTargetCell(CreatureDebugGoToMonitor.Instance smi)
	{
		smi.targetCell = Grid.InvalidCell;
	}

	// Token: 0x0200158C RID: 5516
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200158D RID: 5517
	public new class Instance : GameStateMachine<CreatureDebugGoToMonitor, CreatureDebugGoToMonitor.Instance, IStateMachineTarget, CreatureDebugGoToMonitor.Def>.GameInstance
	{
		// Token: 0x0600844C RID: 33868 RVA: 0x002E99FA File Offset: 0x002E7BFA
		public Instance(IStateMachineTarget target, CreatureDebugGoToMonitor.Def def)
			: base(target, def)
		{
		}

		// Token: 0x0600844D RID: 33869 RVA: 0x002E9A0F File Offset: 0x002E7C0F
		public void GoToCursor()
		{
			this.targetCell = DebugHandler.GetMouseCell();
		}

		// Token: 0x0400670F RID: 26383
		public int targetCell = Grid.InvalidCell;
	}
}

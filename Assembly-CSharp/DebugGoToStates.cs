using System;
using STRINGS;

// Token: 0x020000B5 RID: 181
public class DebugGoToStates : GameStateMachine<DebugGoToStates, DebugGoToStates.Instance, IStateMachineTarget, DebugGoToStates.Def>
{
	// Token: 0x06000336 RID: 822 RVA: 0x000198B4 File Offset: 0x00017AB4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.moving;
		this.moving.MoveTo(new Func<DebugGoToStates.Instance, int>(DebugGoToStates.GetTargetCell), this.behaviourcomplete, this.behaviourcomplete, true).ToggleStatusItem(CREATURES.STATUSITEMS.DEBUGGOTO.NAME, CREATURES.STATUSITEMS.DEBUGGOTO.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.behaviourcomplete.BehaviourComplete(GameTags.HasDebugDestination, false);
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00019940 File Offset: 0x00017B40
	private static int GetTargetCell(DebugGoToStates.Instance smi)
	{
		return smi.GetSMI<CreatureDebugGoToMonitor.Instance>().targetCell;
	}

	// Token: 0x04000215 RID: 533
	public GameStateMachine<DebugGoToStates, DebugGoToStates.Instance, IStateMachineTarget, DebugGoToStates.Def>.State moving;

	// Token: 0x04000216 RID: 534
	public GameStateMachine<DebugGoToStates, DebugGoToStates.Instance, IStateMachineTarget, DebugGoToStates.Def>.State behaviourcomplete;

	// Token: 0x02000E3C RID: 3644
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E3D RID: 3645
	public new class Instance : GameStateMachine<DebugGoToStates, DebugGoToStates.Instance, IStateMachineTarget, DebugGoToStates.Def>.GameInstance
	{
		// Token: 0x06006BE1 RID: 27617 RVA: 0x002971BE File Offset: 0x002953BE
		public Instance(Chore<DebugGoToStates.Instance> chore, DebugGoToStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.HasDebugDestination);
		}
	}
}

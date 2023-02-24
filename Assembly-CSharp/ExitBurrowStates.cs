using System;
using STRINGS;

// Token: 0x020000BC RID: 188
public class ExitBurrowStates : GameStateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>
{
	// Token: 0x0600034E RID: 846 RVA: 0x0001A144 File Offset: 0x00018344
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.exiting;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.EMERGING.NAME, CREATURES.STATUSITEMS.EMERGING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.exiting.PlayAnim("emerge").Enter(new StateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>.State.Callback(ExitBurrowStates.MoveToCellAbove)).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToExitBurrow, false);
	}

	// Token: 0x0600034F RID: 847 RVA: 0x0001A1DF File Offset: 0x000183DF
	private static void MoveToCellAbove(ExitBurrowStates.Instance smi)
	{
		smi.transform.SetPosition(Grid.CellToPosCBC(Grid.CellAbove(Grid.PosToCell(smi.transform.GetPosition())), Grid.SceneLayer.Creatures));
	}

	// Token: 0x04000228 RID: 552
	private GameStateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>.State exiting;

	// Token: 0x04000229 RID: 553
	private GameStateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>.State behaviourcomplete;

	// Token: 0x02000E52 RID: 3666
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E53 RID: 3667
	public new class Instance : GameStateMachine<ExitBurrowStates, ExitBurrowStates.Instance, IStateMachineTarget, ExitBurrowStates.Def>.GameInstance
	{
		// Token: 0x06006C03 RID: 27651 RVA: 0x00297414 File Offset: 0x00295614
		public Instance(Chore<ExitBurrowStates.Instance> chore, ExitBurrowStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToExitBurrow);
		}
	}
}

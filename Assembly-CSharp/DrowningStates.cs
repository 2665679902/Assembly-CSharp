using System;
using STRINGS;

// Token: 0x020000BA RID: 186
public class DrowningStates : GameStateMachine<DrowningStates, DrowningStates.Instance, IStateMachineTarget, DrowningStates.Def>
{
	// Token: 0x06000344 RID: 836 RVA: 0x00019D30 File Offset: 0x00017F30
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.drown;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.DROWNING.NAME, CREATURES.STATUSITEMS.DROWNING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).TagTransition(GameTags.Creatures.Drowning, null, true);
		this.drown.PlayAnim("drown_pre").QueueAnim("drown_loop", true, null).Transition(this.drown_pst, new StateMachine<DrowningStates, DrowningStates.Instance, IStateMachineTarget, DrowningStates.Def>.Transition.ConditionCallback(this.UpdateSafeCell), UpdateRate.SIM_1000ms);
		this.drown_pst.PlayAnim("drown_pst").OnAnimQueueComplete(this.move_to_safe);
		this.move_to_safe.MoveTo((DrowningStates.Instance smi) => smi.safeCell, null, null, false);
	}

	// Token: 0x06000345 RID: 837 RVA: 0x00019E18 File Offset: 0x00018018
	public bool UpdateSafeCell(DrowningStates.Instance smi)
	{
		Navigator component = smi.GetComponent<Navigator>();
		DrowningStates.EscapeCellQuery escapeCellQuery = new DrowningStates.EscapeCellQuery(smi.GetComponent<DrowningMonitor>());
		component.RunQuery(escapeCellQuery);
		smi.safeCell = escapeCellQuery.GetResultCell();
		return smi.safeCell != Grid.InvalidCell;
	}

	// Token: 0x04000221 RID: 545
	public GameStateMachine<DrowningStates, DrowningStates.Instance, IStateMachineTarget, DrowningStates.Def>.State drown;

	// Token: 0x04000222 RID: 546
	public GameStateMachine<DrowningStates, DrowningStates.Instance, IStateMachineTarget, DrowningStates.Def>.State drown_pst;

	// Token: 0x04000223 RID: 547
	public GameStateMachine<DrowningStates, DrowningStates.Instance, IStateMachineTarget, DrowningStates.Def>.State move_to_safe;

	// Token: 0x02000E4B RID: 3659
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E4C RID: 3660
	public new class Instance : GameStateMachine<DrowningStates, DrowningStates.Instance, IStateMachineTarget, DrowningStates.Def>.GameInstance
	{
		// Token: 0x06006BF8 RID: 27640 RVA: 0x00297368 File Offset: 0x00295568
		public Instance(Chore<DrowningStates.Instance> chore, DrowningStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.HasTag, GameTags.Creatures.Drowning);
		}

		// Token: 0x04005143 RID: 20803
		public int safeCell = Grid.InvalidCell;
	}

	// Token: 0x02000E4D RID: 3661
	public class EscapeCellQuery : PathFinderQuery
	{
		// Token: 0x06006BF9 RID: 27641 RVA: 0x00297397 File Offset: 0x00295597
		public EscapeCellQuery(DrowningMonitor monitor)
		{
			this.monitor = monitor;
		}

		// Token: 0x06006BFA RID: 27642 RVA: 0x002973A6 File Offset: 0x002955A6
		public override bool IsMatch(int cell, int parent_cell, int cost)
		{
			return this.monitor.IsCellSafe(cell);
		}

		// Token: 0x04005144 RID: 20804
		private DrowningMonitor monitor;
	}
}

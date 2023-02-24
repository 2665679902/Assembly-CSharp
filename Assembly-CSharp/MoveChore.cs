using System;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class MoveChore : Chore<MoveChore.StatesInstance>
{
	// Token: 0x0600127A RID: 4730 RVA: 0x00062D84 File Offset: 0x00060F84
	public MoveChore(IStateMachineTarget target, ChoreType chore_type, Func<MoveChore.StatesInstance, int> get_cell_callback, bool update_cell = false)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new MoveChore.StatesInstance(this, target.gameObject, get_cell_callback, update_cell);
	}

	// Token: 0x02000F78 RID: 3960
	public class StatesInstance : GameStateMachine<MoveChore.States, MoveChore.StatesInstance, MoveChore, object>.GameInstance
	{
		// Token: 0x06006F9D RID: 28573 RVA: 0x002A2FB0 File Offset: 0x002A11B0
		public StatesInstance(MoveChore master, GameObject mover, Func<MoveChore.StatesInstance, int> get_cell_callback, bool update_cell = false)
			: base(master)
		{
			this.getCellCallback = get_cell_callback;
			base.sm.mover.Set(mover, base.smi, false);
		}

		// Token: 0x0400549E RID: 21662
		public Func<MoveChore.StatesInstance, int> getCellCallback;
	}

	// Token: 0x02000F79 RID: 3961
	public class States : GameStateMachine<MoveChore.States, MoveChore.StatesInstance, MoveChore>
	{
		// Token: 0x06006F9E RID: 28574 RVA: 0x002A2FDC File Offset: 0x002A11DC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.approach;
			base.Target(this.mover);
			this.root.MoveTo((MoveChore.StatesInstance smi) => smi.getCellCallback(smi), null, null, false);
		}

		// Token: 0x0400549F RID: 21663
		public GameStateMachine<MoveChore.States, MoveChore.StatesInstance, MoveChore, object>.ApproachSubState<IApproachable> approach;

		// Token: 0x040054A0 RID: 21664
		public StateMachine<MoveChore.States, MoveChore.StatesInstance, MoveChore, object>.TargetParameter mover;

		// Token: 0x040054A1 RID: 21665
		public StateMachine<MoveChore.States, MoveChore.StatesInstance, MoveChore, object>.TargetParameter locator;
	}
}

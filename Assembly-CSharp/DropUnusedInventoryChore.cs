using System;
using UnityEngine;

// Token: 0x02000409 RID: 1033
public class DropUnusedInventoryChore : Chore<DropUnusedInventoryChore.StatesInstance>
{
	// Token: 0x06001573 RID: 5491 RVA: 0x0006F2D4 File Offset: 0x0006D4D4
	public DropUnusedInventoryChore(ChoreType chore_type, IStateMachineTarget target)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), true, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new DropUnusedInventoryChore.StatesInstance(this);
	}

	// Token: 0x02001036 RID: 4150
	public class StatesInstance : GameStateMachine<DropUnusedInventoryChore.States, DropUnusedInventoryChore.StatesInstance, DropUnusedInventoryChore, object>.GameInstance
	{
		// Token: 0x06007288 RID: 29320 RVA: 0x002ADECB File Offset: 0x002AC0CB
		public StatesInstance(DropUnusedInventoryChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02001037 RID: 4151
	public class States : GameStateMachine<DropUnusedInventoryChore.States, DropUnusedInventoryChore.StatesInstance, DropUnusedInventoryChore>
	{
		// Token: 0x06007289 RID: 29321 RVA: 0x002ADED4 File Offset: 0x002AC0D4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.dropping;
			this.dropping.Enter(delegate(DropUnusedInventoryChore.StatesInstance smi)
			{
				smi.GetComponent<Storage>().DropAll(false, false, default(Vector3), true, null);
			}).GoTo(this.success);
			this.success.ReturnSuccess();
		}

		// Token: 0x040056AC RID: 22188
		public GameStateMachine<DropUnusedInventoryChore.States, DropUnusedInventoryChore.StatesInstance, DropUnusedInventoryChore, object>.State dropping;

		// Token: 0x040056AD RID: 22189
		public GameStateMachine<DropUnusedInventoryChore.States, DropUnusedInventoryChore.StatesInstance, DropUnusedInventoryChore, object>.State success;
	}
}

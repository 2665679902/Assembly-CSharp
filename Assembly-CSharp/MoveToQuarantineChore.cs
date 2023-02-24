using System;
using UnityEngine;

// Token: 0x0200038B RID: 907
public class MoveToQuarantineChore : Chore<MoveToQuarantineChore.StatesInstance>
{
	// Token: 0x0600127B RID: 4731 RVA: 0x00062DC0 File Offset: 0x00060FC0
	public MoveToQuarantineChore(IStateMachineTarget target, KMonoBehaviour quarantine_area)
		: base(Db.Get().ChoreTypes.MoveToQuarantine, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new MoveToQuarantineChore.StatesInstance(this, target.gameObject);
		base.smi.sm.locator.Set(quarantine_area.gameObject, base.smi, false);
	}

	// Token: 0x02000F7A RID: 3962
	public class StatesInstance : GameStateMachine<MoveToQuarantineChore.States, MoveToQuarantineChore.StatesInstance, MoveToQuarantineChore, object>.GameInstance
	{
		// Token: 0x06006FA0 RID: 28576 RVA: 0x002A3033 File Offset: 0x002A1233
		public StatesInstance(MoveToQuarantineChore master, GameObject quarantined)
			: base(master)
		{
			base.sm.quarantined.Set(quarantined, base.smi, false);
		}
	}

	// Token: 0x02000F7B RID: 3963
	public class States : GameStateMachine<MoveToQuarantineChore.States, MoveToQuarantineChore.StatesInstance, MoveToQuarantineChore>
	{
		// Token: 0x06006FA1 RID: 28577 RVA: 0x002A3055 File Offset: 0x002A1255
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.approach;
			this.approach.InitializeStates(this.quarantined, this.locator, this.success, null, null, null);
			this.success.ReturnSuccess();
		}

		// Token: 0x040054A2 RID: 21666
		public StateMachine<MoveToQuarantineChore.States, MoveToQuarantineChore.StatesInstance, MoveToQuarantineChore, object>.TargetParameter locator;

		// Token: 0x040054A3 RID: 21667
		public StateMachine<MoveToQuarantineChore.States, MoveToQuarantineChore.StatesInstance, MoveToQuarantineChore, object>.TargetParameter quarantined;

		// Token: 0x040054A4 RID: 21668
		public GameStateMachine<MoveToQuarantineChore.States, MoveToQuarantineChore.StatesInstance, MoveToQuarantineChore, object>.ApproachSubState<IApproachable> approach;

		// Token: 0x040054A5 RID: 21669
		public GameStateMachine<MoveToQuarantineChore.States, MoveToQuarantineChore.StatesInstance, MoveToQuarantineChore, object>.State success;
	}
}

using System;
using UnityEngine;

// Token: 0x0200039B RID: 923
public class TakeOffHatChore : Chore<TakeOffHatChore.StatesInstance>
{
	// Token: 0x060012A1 RID: 4769 RVA: 0x00063C80 File Offset: 0x00061E80
	public TakeOffHatChore(IStateMachineTarget target, ChoreType chore_type)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new TakeOffHatChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000F9F RID: 3999
	public class StatesInstance : GameStateMachine<TakeOffHatChore.States, TakeOffHatChore.StatesInstance, TakeOffHatChore, object>.GameInstance
	{
		// Token: 0x06007012 RID: 28690 RVA: 0x002A573C File Offset: 0x002A393C
		public StatesInstance(TakeOffHatChore master, GameObject duplicant)
			: base(master)
		{
			base.sm.duplicant.Set(duplicant, base.smi, false);
		}
	}

	// Token: 0x02000FA0 RID: 4000
	public class States : GameStateMachine<TakeOffHatChore.States, TakeOffHatChore.StatesInstance, TakeOffHatChore>
	{
		// Token: 0x06007013 RID: 28691 RVA: 0x002A5760 File Offset: 0x002A3960
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.remove_hat_pre;
			base.Target(this.duplicant);
			this.remove_hat_pre.Enter(delegate(TakeOffHatChore.StatesInstance smi)
			{
				if (this.duplicant.Get(smi).GetComponent<MinionResume>().CurrentHat != null)
				{
					smi.GoTo(this.remove_hat);
					return;
				}
				smi.GoTo(this.complete);
			});
			this.remove_hat.ToggleAnims("anim_hat_kanim", 0f, "").PlayAnim("hat_off").OnAnimQueueComplete(this.complete);
			this.complete.Enter(delegate(TakeOffHatChore.StatesInstance smi)
			{
				smi.master.GetComponent<MinionResume>().RemoveHat();
			}).ReturnSuccess();
		}

		// Token: 0x04005506 RID: 21766
		public StateMachine<TakeOffHatChore.States, TakeOffHatChore.StatesInstance, TakeOffHatChore, object>.TargetParameter duplicant;

		// Token: 0x04005507 RID: 21767
		public GameStateMachine<TakeOffHatChore.States, TakeOffHatChore.StatesInstance, TakeOffHatChore, object>.State remove_hat_pre;

		// Token: 0x04005508 RID: 21768
		public GameStateMachine<TakeOffHatChore.States, TakeOffHatChore.StatesInstance, TakeOffHatChore, object>.State remove_hat;

		// Token: 0x04005509 RID: 21769
		public GameStateMachine<TakeOffHatChore.States, TakeOffHatChore.StatesInstance, TakeOffHatChore, object>.State complete;
	}
}

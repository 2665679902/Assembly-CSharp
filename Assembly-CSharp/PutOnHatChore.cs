using System;
using UnityEngine;

// Token: 0x0200038F RID: 911
public class PutOnHatChore : Chore<PutOnHatChore.StatesInstance>
{
	// Token: 0x06001282 RID: 4738 RVA: 0x0006301C File Offset: 0x0006121C
	public PutOnHatChore(IStateMachineTarget target, ChoreType chore_type)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new PutOnHatChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000F82 RID: 3970
	public class StatesInstance : GameStateMachine<PutOnHatChore.States, PutOnHatChore.StatesInstance, PutOnHatChore, object>.GameInstance
	{
		// Token: 0x06006FB1 RID: 28593 RVA: 0x002A359E File Offset: 0x002A179E
		public StatesInstance(PutOnHatChore master, GameObject duplicant)
			: base(master)
		{
			base.sm.duplicant.Set(duplicant, base.smi, false);
		}
	}

	// Token: 0x02000F83 RID: 3971
	public class States : GameStateMachine<PutOnHatChore.States, PutOnHatChore.StatesInstance, PutOnHatChore>
	{
		// Token: 0x06006FB2 RID: 28594 RVA: 0x002A35C0 File Offset: 0x002A17C0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.applyHat_pre;
			base.Target(this.duplicant);
			this.applyHat_pre.ToggleAnims("anim_hat_kanim", 0f, "").Enter(delegate(PutOnHatChore.StatesInstance smi)
			{
				this.duplicant.Get(smi).GetComponent<MinionResume>().ApplyTargetHat();
			}).PlayAnim("hat_first")
				.OnAnimQueueComplete(this.applyHat);
			this.applyHat.ToggleAnims("anim_hat_kanim", 0f, "").PlayAnim("working_pst").OnAnimQueueComplete(this.complete);
			this.complete.ReturnSuccess();
		}

		// Token: 0x040054B5 RID: 21685
		public StateMachine<PutOnHatChore.States, PutOnHatChore.StatesInstance, PutOnHatChore, object>.TargetParameter duplicant;

		// Token: 0x040054B6 RID: 21686
		public GameStateMachine<PutOnHatChore.States, PutOnHatChore.StatesInstance, PutOnHatChore, object>.State applyHat_pre;

		// Token: 0x040054B7 RID: 21687
		public GameStateMachine<PutOnHatChore.States, PutOnHatChore.StatesInstance, PutOnHatChore, object>.State applyHat;

		// Token: 0x040054B8 RID: 21688
		public GameStateMachine<PutOnHatChore.States, PutOnHatChore.StatesInstance, PutOnHatChore, object>.State complete;
	}
}

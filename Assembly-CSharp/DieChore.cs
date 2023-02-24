using System;

// Token: 0x0200037D RID: 893
public class DieChore : Chore<DieChore.StatesInstance>
{
	// Token: 0x06001235 RID: 4661 RVA: 0x000613DC File Offset: 0x0005F5DC
	public DieChore(IStateMachineTarget master, Death death)
		: base(Db.Get().ChoreTypes.Die, master, master.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		this.showAvailabilityInHoverText = false;
		base.smi = new DieChore.StatesInstance(this, death);
	}

	// Token: 0x02000F58 RID: 3928
	public class StatesInstance : GameStateMachine<DieChore.States, DieChore.StatesInstance, DieChore, object>.GameInstance
	{
		// Token: 0x06006F37 RID: 28471 RVA: 0x0029FDA8 File Offset: 0x0029DFA8
		public StatesInstance(DieChore master, Death death)
			: base(master)
		{
			base.sm.death.Set(death, base.smi, false);
		}

		// Token: 0x06006F38 RID: 28472 RVA: 0x0029FDCC File Offset: 0x0029DFCC
		public void PlayPreAnim()
		{
			string preAnim = base.sm.death.Get(base.smi).preAnim;
			base.GetComponent<KAnimControllerBase>().Play(preAnim, KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x02000F59 RID: 3929
	public class States : GameStateMachine<DieChore.States, DieChore.StatesInstance, DieChore>
	{
		// Token: 0x06006F39 RID: 28473 RVA: 0x0029FE14 File Offset: 0x0029E014
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.dying;
			this.dying.OnAnimQueueComplete(this.dead).Enter("PlayAnim", delegate(DieChore.StatesInstance smi)
			{
				smi.PlayPreAnim();
			});
			this.dead.ReturnSuccess();
		}

		// Token: 0x04005428 RID: 21544
		public GameStateMachine<DieChore.States, DieChore.StatesInstance, DieChore, object>.State dying;

		// Token: 0x04005429 RID: 21545
		public GameStateMachine<DieChore.States, DieChore.StatesInstance, DieChore, object>.State dead;

		// Token: 0x0400542A RID: 21546
		public StateMachine<DieChore.States, DieChore.StatesInstance, DieChore, object>.ResourceParameter<Death> death;
	}
}

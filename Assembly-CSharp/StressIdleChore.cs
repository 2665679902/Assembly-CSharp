using System;
using UnityEngine;

// Token: 0x02000398 RID: 920
public class StressIdleChore : Chore<StressIdleChore.StatesInstance>
{
	// Token: 0x0600129C RID: 4764 RVA: 0x00063A40 File Offset: 0x00061C40
	public StressIdleChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.StressIdle, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new StressIdleChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000F98 RID: 3992
	public class StatesInstance : GameStateMachine<StressIdleChore.States, StressIdleChore.StatesInstance, StressIdleChore, object>.GameInstance
	{
		// Token: 0x06007001 RID: 28673 RVA: 0x002A53AE File Offset: 0x002A35AE
		public StatesInstance(StressIdleChore master, GameObject idler)
			: base(master)
		{
			base.sm.idler.Set(idler, base.smi, false);
		}
	}

	// Token: 0x02000F99 RID: 3993
	public class States : GameStateMachine<StressIdleChore.States, StressIdleChore.StatesInstance, StressIdleChore>
	{
		// Token: 0x06007002 RID: 28674 RVA: 0x002A53D0 File Offset: 0x002A35D0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			base.Target(this.idler);
			this.idle.PlayAnim("idle_default", KAnim.PlayMode.Loop);
		}

		// Token: 0x040054F4 RID: 21748
		public StateMachine<StressIdleChore.States, StressIdleChore.StatesInstance, StressIdleChore, object>.TargetParameter idler;

		// Token: 0x040054F5 RID: 21749
		public GameStateMachine<StressIdleChore.States, StressIdleChore.StatesInstance, StressIdleChore, object>.State idle;
	}
}

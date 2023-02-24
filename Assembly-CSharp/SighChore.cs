using System;
using UnityEngine;

// Token: 0x02000395 RID: 917
public class SighChore : Chore<SighChore.StatesInstance>
{
	// Token: 0x06001294 RID: 4756 RVA: 0x000637AC File Offset: 0x000619AC
	public SighChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.Sigh, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new SighChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000F91 RID: 3985
	public class StatesInstance : GameStateMachine<SighChore.States, SighChore.StatesInstance, SighChore, object>.GameInstance
	{
		// Token: 0x06006FE6 RID: 28646 RVA: 0x002A480E File Offset: 0x002A2A0E
		public StatesInstance(SighChore master, GameObject sigher)
			: base(master)
		{
			base.sm.sigher.Set(sigher, base.smi, false);
		}
	}

	// Token: 0x02000F92 RID: 3986
	public class States : GameStateMachine<SighChore.States, SighChore.StatesInstance, SighChore>
	{
		// Token: 0x06006FE7 RID: 28647 RVA: 0x002A4830 File Offset: 0x002A2A30
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.Target(this.sigher);
			this.root.PlayAnim("emote_depressed").OnAnimQueueComplete(null);
		}

		// Token: 0x040054DB RID: 21723
		public StateMachine<SighChore.States, SighChore.StatesInstance, SighChore, object>.TargetParameter sigher;
	}
}

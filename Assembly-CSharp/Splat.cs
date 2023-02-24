using System;

// Token: 0x0200098E RID: 2446
public class Splat : GameStateMachine<Splat, Splat.StatesInstance>
{
	// Token: 0x06004873 RID: 18547 RVA: 0x0019656C File Offset: 0x0019476C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleChore((Splat.StatesInstance smi) => new WorkChore<SplatWorkable>(Db.Get().ChoreTypes.Mop, smi.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true), this.complete);
		this.complete.Enter(delegate(Splat.StatesInstance smi)
		{
			Util.KDestroyGameObject(smi.master.gameObject);
		});
	}

	// Token: 0x04002FA0 RID: 12192
	public GameStateMachine<Splat, Splat.StatesInstance, IStateMachineTarget, object>.State complete;

	// Token: 0x02001786 RID: 6022
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001787 RID: 6023
	public class StatesInstance : GameStateMachine<Splat, Splat.StatesInstance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008B34 RID: 35636 RVA: 0x002FEF50 File Offset: 0x002FD150
		public StatesInstance(IStateMachineTarget master, Splat.Def def)
			: base(master, def)
		{
		}
	}
}

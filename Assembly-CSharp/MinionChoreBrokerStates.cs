using System;

// Token: 0x020000D1 RID: 209
public class MinionChoreBrokerStates : GameStateMachine<MinionChoreBrokerStates, MinionChoreBrokerStates.Instance, IStateMachineTarget, MinionChoreBrokerStates.Def>
{
	// Token: 0x060003A3 RID: 931 RVA: 0x0001C38C File Offset: 0x0001A58C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.hasChore;
		this.root.DoNothing();
		this.hasChore.Enter(delegate(MinionChoreBrokerStates.Instance smi)
		{
		});
	}

	// Token: 0x04000259 RID: 601
	private GameStateMachine<MinionChoreBrokerStates, MinionChoreBrokerStates.Instance, IStateMachineTarget, MinionChoreBrokerStates.Def>.State hasChore;

	// Token: 0x02000E8D RID: 3725
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E8E RID: 3726
	public new class Instance : GameStateMachine<MinionChoreBrokerStates, MinionChoreBrokerStates.Instance, IStateMachineTarget, MinionChoreBrokerStates.Def>.GameInstance
	{
		// Token: 0x06006C87 RID: 27783 RVA: 0x0029819C File Offset: 0x0029639C
		public Instance(Chore<MinionChoreBrokerStates.Instance> chore, MinionChoreBrokerStates.Def def)
			: base(chore, def)
		{
		}
	}
}

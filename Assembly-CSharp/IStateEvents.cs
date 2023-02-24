using System;

// Token: 0x02000607 RID: 1543
public interface IStateEvents<StateMachine, StateMachineInstance>
{
	// Token: 0x0600283E RID: 10302
	void Initialize(StateMachine sm);

	// Token: 0x0600283F RID: 10303
	void OnEnter(StateMachineInstance smi);

	// Token: 0x06002840 RID: 10304
	void OnUpdate(StateMachineInstance smi, float dt);

	// Token: 0x06002841 RID: 10305
	void OnExit(StateMachineInstance smi);

	// Token: 0x06002842 RID: 10306
	void CleanUp(StateMachineInstance smi);

	// Token: 0x06002843 RID: 10307
	void OnAnimComplete(MegaBrainTank.StatesInstance smi, HashedString completedAnim);
}

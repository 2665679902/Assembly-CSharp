using System;

// Token: 0x02000608 RID: 1544
public class BrainTankState : GameStateMachine<MegaBrainTank.States, MegaBrainTank.StatesInstance, MegaBrainTank, object>.State, IStateEvents<MegaBrainTank.States, MegaBrainTank.StatesInstance>
{
	// Token: 0x06002844 RID: 10308 RVA: 0x000D5D7F File Offset: 0x000D3F7F
	public virtual void Initialize(MegaBrainTank.States sm)
	{
	}

	// Token: 0x06002845 RID: 10309 RVA: 0x000D5D81 File Offset: 0x000D3F81
	public virtual void OnEnter(MegaBrainTank.StatesInstance smi)
	{
	}

	// Token: 0x06002846 RID: 10310 RVA: 0x000D5D83 File Offset: 0x000D3F83
	public virtual void OnUpdate(MegaBrainTank.StatesInstance smi, float dt)
	{
	}

	// Token: 0x06002847 RID: 10311 RVA: 0x000D5D85 File Offset: 0x000D3F85
	public virtual void OnExit(MegaBrainTank.StatesInstance smi)
	{
	}

	// Token: 0x06002848 RID: 10312 RVA: 0x000D5D87 File Offset: 0x000D3F87
	public virtual void CleanUp(MegaBrainTank.StatesInstance smi)
	{
	}

	// Token: 0x06002849 RID: 10313 RVA: 0x000D5D89 File Offset: 0x000D3F89
	public virtual void OnAnimComplete(MegaBrainTank.StatesInstance smi, HashedString completedAnim)
	{
	}
}

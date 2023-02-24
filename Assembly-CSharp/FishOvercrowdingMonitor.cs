using System;

// Token: 0x020006D7 RID: 1751
public class FishOvercrowdingMonitor : GameStateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>
{
	// Token: 0x06002F9B RID: 12187 RVA: 0x000FBA04 File Offset: 0x000F9C04
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.Enter(new StateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.State.Callback(FishOvercrowdingMonitor.Register)).Exit(new StateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.State.Callback(FishOvercrowdingMonitor.Unregister));
		this.satisfied.DoNothing();
		this.overcrowded.DoNothing();
	}

	// Token: 0x06002F9C RID: 12188 RVA: 0x000FBA5A File Offset: 0x000F9C5A
	private static void Register(FishOvercrowdingMonitor.Instance smi)
	{
		FishOvercrowingManager.Instance.Add(smi);
	}

	// Token: 0x06002F9D RID: 12189 RVA: 0x000FBA68 File Offset: 0x000F9C68
	private static void Unregister(FishOvercrowdingMonitor.Instance smi)
	{
		FishOvercrowingManager instance = FishOvercrowingManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.Remove(smi);
	}

	// Token: 0x04001CAC RID: 7340
	public GameStateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.State satisfied;

	// Token: 0x04001CAD RID: 7341
	public GameStateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.State overcrowded;

	// Token: 0x020013D5 RID: 5077
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020013D6 RID: 5078
	public new class Instance : GameStateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.GameInstance
	{
		// Token: 0x06007F3F RID: 32575 RVA: 0x002DBC61 File Offset: 0x002D9E61
		public Instance(IStateMachineTarget master, FishOvercrowdingMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x002DBC6B File Offset: 0x002D9E6B
		public void SetOvercrowdingInfo(int cell_count, int fish_count)
		{
			this.cellCount = cell_count;
			this.fishCount = fish_count;
		}

		// Token: 0x040061C9 RID: 25033
		public int cellCount;

		// Token: 0x040061CA RID: 25034
		public int fishCount;
	}
}

using System;

// Token: 0x020000AA RID: 170
public class BeeHiveMonitor : GameStateMachine<BeeHiveMonitor, BeeHiveMonitor.Instance, IStateMachineTarget, BeeHiveMonitor.Def>
{
	// Token: 0x060002F6 RID: 758 RVA: 0x00017BC8 File Offset: 0x00015DC8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.EventTransition(GameHashes.Nighttime, (BeeHiveMonitor.Instance smi) => GameClock.Instance, this.night, (BeeHiveMonitor.Instance smi) => GameClock.Instance.IsNighttime());
		this.night.EventTransition(GameHashes.NewDay, (BeeHiveMonitor.Instance smi) => GameClock.Instance, this.idle, (BeeHiveMonitor.Instance smi) => !GameClock.Instance.IsNighttime()).ToggleBehaviour(GameTags.Creatures.WantsToMakeHome, new StateMachine<BeeHiveMonitor, BeeHiveMonitor.Instance, IStateMachineTarget, BeeHiveMonitor.Def>.Transition.ConditionCallback(this.ShouldMakeHome), null);
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00017C9E File Offset: 0x00015E9E
	public bool ShouldMakeHome(BeeHiveMonitor.Instance smi)
	{
		return !this.CanGoHome(smi);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00017CAA File Offset: 0x00015EAA
	public bool CanGoHome(BeeHiveMonitor.Instance smi)
	{
		return smi.gameObject.GetComponent<Bee>().FindHiveInRoom() != null;
	}

	// Token: 0x040001F1 RID: 497
	public GameStateMachine<BeeHiveMonitor, BeeHiveMonitor.Instance, IStateMachineTarget, BeeHiveMonitor.Def>.State idle;

	// Token: 0x040001F2 RID: 498
	public GameStateMachine<BeeHiveMonitor, BeeHiveMonitor.Instance, IStateMachineTarget, BeeHiveMonitor.Def>.State night;

	// Token: 0x02000E13 RID: 3603
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E14 RID: 3604
	public new class Instance : GameStateMachine<BeeHiveMonitor, BeeHiveMonitor.Instance, IStateMachineTarget, BeeHiveMonitor.Def>.GameInstance
	{
		// Token: 0x06006B7C RID: 27516 RVA: 0x00296933 File Offset: 0x00294B33
		public Instance(IStateMachineTarget master, BeeHiveMonitor.Def def)
			: base(master, def)
		{
		}
	}
}

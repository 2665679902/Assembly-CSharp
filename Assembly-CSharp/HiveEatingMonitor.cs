using System;

// Token: 0x020000C3 RID: 195
public class HiveEatingMonitor : GameStateMachine<HiveEatingMonitor, HiveEatingMonitor.Instance, IStateMachineTarget, HiveEatingMonitor.Def>
{
	// Token: 0x06000368 RID: 872 RVA: 0x0001AD97 File Offset: 0x00018F97
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToEat, new StateMachine<HiveEatingMonitor, HiveEatingMonitor.Instance, IStateMachineTarget, HiveEatingMonitor.Def>.Transition.ConditionCallback(HiveEatingMonitor.ShouldEat), null);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0001ADBF File Offset: 0x00018FBF
	public static bool ShouldEat(HiveEatingMonitor.Instance smi)
	{
		return smi.storage.FindFirst(smi.def.consumedOre) != null;
	}

	// Token: 0x02000E66 RID: 3686
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005169 RID: 20841
		public Tag consumedOre;
	}

	// Token: 0x02000E67 RID: 3687
	public new class Instance : GameStateMachine<HiveEatingMonitor, HiveEatingMonitor.Instance, IStateMachineTarget, HiveEatingMonitor.Def>.GameInstance
	{
		// Token: 0x06006C2E RID: 27694 RVA: 0x00297977 File Offset: 0x00295B77
		public Instance(IStateMachineTarget master, HiveEatingMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0400516A RID: 20842
		[MyCmpReq]
		public Storage storage;
	}
}

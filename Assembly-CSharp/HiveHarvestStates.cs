using System;

// Token: 0x020000C6 RID: 198
public class HiveHarvestStates : GameStateMachine<HiveHarvestStates, HiveHarvestStates.Instance, IStateMachineTarget, HiveHarvestStates.Def>
{
	// Token: 0x06000371 RID: 881 RVA: 0x0001AF65 File Offset: 0x00019165
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.DoNothing();
	}

	// Token: 0x02000E6E RID: 3694
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E6F RID: 3695
	public new class Instance : GameStateMachine<HiveHarvestStates, HiveHarvestStates.Instance, IStateMachineTarget, HiveHarvestStates.Def>.GameInstance
	{
		// Token: 0x06006C3A RID: 27706 RVA: 0x00297A1A File Offset: 0x00295C1A
		public Instance(Chore<HiveHarvestStates.Instance> chore, HiveHarvestStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Behaviours.HarvestHiveBehaviour);
		}
	}
}

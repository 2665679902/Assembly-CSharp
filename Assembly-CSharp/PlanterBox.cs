using System;

// Token: 0x02000626 RID: 1574
[SkipSaveFileSerialization]
public class PlanterBox : StateMachineComponent<PlanterBox.SMInstance>
{
	// Token: 0x06002949 RID: 10569 RVA: 0x000DA479 File Offset: 0x000D8679
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x0400184E RID: 6222
	[MyCmpReq]
	private PlantablePlot plantablePlot;

	// Token: 0x020012A4 RID: 4772
	public class SMInstance : GameStateMachine<PlanterBox.States, PlanterBox.SMInstance, PlanterBox, object>.GameInstance
	{
		// Token: 0x06007AF7 RID: 31479 RVA: 0x002C99EA File Offset: 0x002C7BEA
		public SMInstance(PlanterBox master)
			: base(master)
		{
		}
	}

	// Token: 0x020012A5 RID: 4773
	public class States : GameStateMachine<PlanterBox.States, PlanterBox.SMInstance, PlanterBox>
	{
		// Token: 0x06007AF8 RID: 31480 RVA: 0x002C99F4 File Offset: 0x002C7BF4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			this.empty.EventTransition(GameHashes.OccupantChanged, this.full, (PlanterBox.SMInstance smi) => smi.master.plantablePlot.Occupant != null).PlayAnim("off");
			this.full.EventTransition(GameHashes.OccupantChanged, this.empty, (PlanterBox.SMInstance smi) => smi.master.plantablePlot.Occupant == null).PlayAnim("on");
		}

		// Token: 0x04005E45 RID: 24133
		public GameStateMachine<PlanterBox.States, PlanterBox.SMInstance, PlanterBox, object>.State empty;

		// Token: 0x04005E46 RID: 24134
		public GameStateMachine<PlanterBox.States, PlanterBox.SMInstance, PlanterBox, object>.State full;
	}
}

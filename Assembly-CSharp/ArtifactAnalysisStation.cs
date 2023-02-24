using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000575 RID: 1397
public class ArtifactAnalysisStation : GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>
{
	// Token: 0x060021BD RID: 8637 RVA: 0x000B7BB8 File Offset: 0x000B5DB8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.ready, new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.IsOperational));
		this.operational.EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Not(new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.IsOperational))).EventTransition(GameHashes.OnStorageChange, this.ready, new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.HasArtifactToStudy));
		this.ready.EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Not(new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.IsOperational))).EventTransition(GameHashes.OnStorageChange, this.operational, GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Not(new StateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.Transition.ConditionCallback(this.HasArtifactToStudy))).ToggleChore(new Func<ArtifactAnalysisStation.StatesInstance, Chore>(this.CreateChore), this.operational);
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x000B7C94 File Offset: 0x000B5E94
	private bool HasArtifactToStudy(ArtifactAnalysisStation.StatesInstance smi)
	{
		return smi.storage.GetMassAvailable(GameTags.CharmedArtifact) >= 1f;
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x000B7CB0 File Offset: 0x000B5EB0
	private bool IsOperational(ArtifactAnalysisStation.StatesInstance smi)
	{
		return smi.GetComponent<Operational>().IsOperational;
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x000B7CC0 File Offset: 0x000B5EC0
	private Chore CreateChore(ArtifactAnalysisStation.StatesInstance smi)
	{
		return new WorkChore<ArtifactAnalysisStationWorkable>(Db.Get().ChoreTypes.AnalyzeArtifact, smi.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x04001373 RID: 4979
	public GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.State inoperational;

	// Token: 0x04001374 RID: 4980
	public GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.State operational;

	// Token: 0x04001375 RID: 4981
	public GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.State ready;

	// Token: 0x0200119C RID: 4508
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200119D RID: 4509
	public class StatesInstance : GameStateMachine<ArtifactAnalysisStation, ArtifactAnalysisStation.StatesInstance, IStateMachineTarget, ArtifactAnalysisStation.Def>.GameInstance
	{
		// Token: 0x06007741 RID: 30529 RVA: 0x002BAF9D File Offset: 0x002B919D
		public StatesInstance(IStateMachineTarget master, ArtifactAnalysisStation.Def def)
			: base(master, def)
		{
			this.workable.statesInstance = this;
		}

		// Token: 0x06007742 RID: 30530 RVA: 0x002BAFB3 File Offset: 0x002B91B3
		public override void StartSM()
		{
			base.StartSM();
		}

		// Token: 0x04005B65 RID: 23397
		[MyCmpReq]
		public Storage storage;

		// Token: 0x04005B66 RID: 23398
		[MyCmpReq]
		public ManualDeliveryKG manualDelivery;

		// Token: 0x04005B67 RID: 23399
		[MyCmpReq]
		public ArtifactAnalysisStationWorkable workable;

		// Token: 0x04005B68 RID: 23400
		[Serialize]
		private HashSet<Tag> forbiddenSeeds;
	}
}

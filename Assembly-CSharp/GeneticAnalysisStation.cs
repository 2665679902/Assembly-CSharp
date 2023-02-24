using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x020005C2 RID: 1474
public class GeneticAnalysisStation : GameStateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>
{
	// Token: 0x060024A7 RID: 9383 RVA: 0x000C5E48 File Offset: 0x000C4048
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.ready, new StateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.Transition.ConditionCallback(this.IsOperational));
		this.operational.EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.Not(new StateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.Transition.ConditionCallback(this.IsOperational))).EventTransition(GameHashes.OnStorageChange, this.ready, new StateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.Transition.ConditionCallback(this.HasSeedToStudy));
		this.ready.EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.Not(new StateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.Transition.ConditionCallback(this.IsOperational))).EventTransition(GameHashes.OnStorageChange, this.operational, GameStateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.Not(new StateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.Transition.ConditionCallback(this.HasSeedToStudy))).ToggleChore(new Func<GeneticAnalysisStation.StatesInstance, Chore>(this.CreateChore), this.operational);
	}

	// Token: 0x060024A8 RID: 9384 RVA: 0x000C5F24 File Offset: 0x000C4124
	private bool HasSeedToStudy(GeneticAnalysisStation.StatesInstance smi)
	{
		return smi.storage.GetMassAvailable(GameTags.UnidentifiedSeed) >= 1f;
	}

	// Token: 0x060024A9 RID: 9385 RVA: 0x000C5F40 File Offset: 0x000C4140
	private bool IsOperational(GeneticAnalysisStation.StatesInstance smi)
	{
		return smi.GetComponent<Operational>().IsOperational;
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x000C5F50 File Offset: 0x000C4150
	private Chore CreateChore(GeneticAnalysisStation.StatesInstance smi)
	{
		return new WorkChore<GeneticAnalysisStationWorkable>(Db.Get().ChoreTypes.AnalyzeSeed, smi.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x0400151A RID: 5402
	public GameStateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.State inoperational;

	// Token: 0x0400151B RID: 5403
	public GameStateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.State operational;

	// Token: 0x0400151C RID: 5404
	public GameStateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.State ready;

	// Token: 0x02001202 RID: 4610
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001203 RID: 4611
	public class StatesInstance : GameStateMachine<GeneticAnalysisStation, GeneticAnalysisStation.StatesInstance, IStateMachineTarget, GeneticAnalysisStation.Def>.GameInstance
	{
		// Token: 0x060078B1 RID: 30897 RVA: 0x002C0542 File Offset: 0x002BE742
		public StatesInstance(IStateMachineTarget master, GeneticAnalysisStation.Def def)
			: base(master, def)
		{
			this.workable.statesInstance = this;
		}

		// Token: 0x060078B2 RID: 30898 RVA: 0x002C0558 File Offset: 0x002BE758
		public override void StartSM()
		{
			base.StartSM();
			this.RefreshFetchTags();
		}

		// Token: 0x060078B3 RID: 30899 RVA: 0x002C0568 File Offset: 0x002BE768
		public void SetSeedForbidden(Tag seedID, bool forbidden)
		{
			if (this.forbiddenSeeds == null)
			{
				this.forbiddenSeeds = new HashSet<Tag>();
			}
			bool flag;
			if (forbidden)
			{
				flag = this.forbiddenSeeds.Add(seedID);
			}
			else
			{
				flag = this.forbiddenSeeds.Remove(seedID);
			}
			if (flag)
			{
				this.RefreshFetchTags();
			}
		}

		// Token: 0x060078B4 RID: 30900 RVA: 0x002C05B0 File Offset: 0x002BE7B0
		public bool GetSeedForbidden(Tag seedID)
		{
			if (this.forbiddenSeeds == null)
			{
				this.forbiddenSeeds = new HashSet<Tag>();
			}
			return this.forbiddenSeeds.Contains(seedID);
		}

		// Token: 0x060078B5 RID: 30901 RVA: 0x002C05D4 File Offset: 0x002BE7D4
		private void RefreshFetchTags()
		{
			if (this.forbiddenSeeds == null)
			{
				this.manualDelivery.ForbiddenTags = null;
				return;
			}
			Tag[] array = new Tag[this.forbiddenSeeds.Count];
			int num = 0;
			foreach (Tag tag in this.forbiddenSeeds)
			{
				array[num++] = tag;
				this.storage.Drop(tag);
			}
			this.manualDelivery.ForbiddenTags = array;
		}

		// Token: 0x04005CB3 RID: 23731
		[MyCmpReq]
		public Storage storage;

		// Token: 0x04005CB4 RID: 23732
		[MyCmpReq]
		public ManualDeliveryKG manualDelivery;

		// Token: 0x04005CB5 RID: 23733
		[MyCmpReq]
		public GeneticAnalysisStationWorkable workable;

		// Token: 0x04005CB6 RID: 23734
		[Serialize]
		private HashSet<Tag> forbiddenSeeds;
	}
}

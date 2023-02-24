using System;
using STRINGS;

// Token: 0x020006C4 RID: 1732
public class CreatureDiseaseCleaner : GameStateMachine<CreatureDiseaseCleaner, CreatureDiseaseCleaner.Instance, IStateMachineTarget, CreatureDiseaseCleaner.Def>
{
	// Token: 0x06002F2B RID: 12075 RVA: 0x000F9490 File Offset: 0x000F7690
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.cleaning;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.CLEANING.NAME, CREATURES.STATUSITEMS.CLEANING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.cleaning.DefaultState(this.cleaning.clean_pre).ScheduleGoTo((CreatureDiseaseCleaner.Instance smi) => smi.def.cleanDuration, this.cleaning.clean_pst);
		this.cleaning.clean_pre.PlayAnim("clean_water_pre").OnAnimQueueComplete(this.cleaning.clean);
		this.cleaning.clean.Enter(delegate(CreatureDiseaseCleaner.Instance smi)
		{
			smi.EnableDiseaseEmitter(true);
		}).QueueAnim("clean_water_loop", true, null).Transition(this.cleaning.clean_pst, (CreatureDiseaseCleaner.Instance smi) => !smi.GetSMI<CleaningMonitor.Instance>().CanCleanElementState(), UpdateRate.SIM_1000ms)
			.Exit(delegate(CreatureDiseaseCleaner.Instance smi)
			{
				smi.EnableDiseaseEmitter(false);
			});
		this.cleaning.clean_pst.PlayAnim("clean_water_pst").OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Cleaning, false);
	}

	// Token: 0x04001C5B RID: 7259
	public GameStateMachine<CreatureDiseaseCleaner, CreatureDiseaseCleaner.Instance, IStateMachineTarget, CreatureDiseaseCleaner.Def>.State behaviourcomplete;

	// Token: 0x04001C5C RID: 7260
	public CreatureDiseaseCleaner.CleaningStates cleaning;

	// Token: 0x04001C5D RID: 7261
	public StateMachine<CreatureDiseaseCleaner, CreatureDiseaseCleaner.Instance, IStateMachineTarget, CreatureDiseaseCleaner.Def>.Signal cellChangedSignal;

	// Token: 0x020013A4 RID: 5028
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06007E80 RID: 32384 RVA: 0x002D9531 File Offset: 0x002D7731
		public Def(float duration)
		{
			this.cleanDuration = duration;
		}

		// Token: 0x04006138 RID: 24888
		public float cleanDuration;
	}

	// Token: 0x020013A5 RID: 5029
	public class CleaningStates : GameStateMachine<CreatureDiseaseCleaner, CreatureDiseaseCleaner.Instance, IStateMachineTarget, CreatureDiseaseCleaner.Def>.State
	{
		// Token: 0x04006139 RID: 24889
		public GameStateMachine<CreatureDiseaseCleaner, CreatureDiseaseCleaner.Instance, IStateMachineTarget, CreatureDiseaseCleaner.Def>.State clean_pre;

		// Token: 0x0400613A RID: 24890
		public GameStateMachine<CreatureDiseaseCleaner, CreatureDiseaseCleaner.Instance, IStateMachineTarget, CreatureDiseaseCleaner.Def>.State clean;

		// Token: 0x0400613B RID: 24891
		public GameStateMachine<CreatureDiseaseCleaner, CreatureDiseaseCleaner.Instance, IStateMachineTarget, CreatureDiseaseCleaner.Def>.State clean_pst;
	}

	// Token: 0x020013A6 RID: 5030
	public new class Instance : GameStateMachine<CreatureDiseaseCleaner, CreatureDiseaseCleaner.Instance, IStateMachineTarget, CreatureDiseaseCleaner.Def>.GameInstance
	{
		// Token: 0x06007E82 RID: 32386 RVA: 0x002D9548 File Offset: 0x002D7748
		public Instance(Chore<CreatureDiseaseCleaner.Instance> chore, CreatureDiseaseCleaner.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Cleaning);
		}

		// Token: 0x06007E83 RID: 32387 RVA: 0x002D956C File Offset: 0x002D776C
		public void EnableDiseaseEmitter(bool enable = true)
		{
			DiseaseEmitter component = base.GetComponent<DiseaseEmitter>();
			if (component != null)
			{
				component.SetEnable(enable);
			}
		}
	}
}

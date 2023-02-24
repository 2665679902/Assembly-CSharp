using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000388 RID: 904
public class MingleChore : Chore<MingleChore.StatesInstance>, IWorkerPrioritizable
{
	// Token: 0x06001272 RID: 4722 RVA: 0x00062A00 File Offset: 0x00060C00
	public MingleChore(IStateMachineTarget target)
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "HasMingleCell";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_MINGLE_CELL;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((MingleChore)data).smi.HasMingleCell();
		};
		this.HasMingleCell = precondition;
		base..ctor(Db.Get().ChoreTypes.Relax, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.high, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime);
		this.showAvailabilityInHoverText = false;
		base.smi = new MingleChore.StatesInstance(this, target.gameObject);
		base.AddPrecondition(this.HasMingleCell, this);
		base.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		base.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Recreation);
		base.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, this);
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x00062AF9 File Offset: 0x00060CF9
	protected override StatusItem GetStatusItem()
	{
		return Db.Get().DuplicantStatusItems.Mingling;
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x00062B0A File Offset: 0x00060D0A
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		return true;
	}

	// Token: 0x040009FA RID: 2554
	private int basePriority = RELAXATION.PRIORITY.TIER1;

	// Token: 0x040009FB RID: 2555
	private Chore.Precondition HasMingleCell;

	// Token: 0x02000F72 RID: 3954
	public class States : GameStateMachine<MingleChore.States, MingleChore.StatesInstance, MingleChore>
	{
		// Token: 0x06006F89 RID: 28553 RVA: 0x002A29E0 File Offset: 0x002A0BE0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.mingle;
			base.Target(this.mingler);
			this.root.EventTransition(GameHashes.ScheduleBlocksChanged, null, (MingleChore.StatesInstance smi) => !smi.IsRecTime());
			this.mingle.Transition(this.walk, (MingleChore.StatesInstance smi) => smi.IsSameRoom(), UpdateRate.SIM_200ms).Transition(this.move, (MingleChore.StatesInstance smi) => !smi.IsSameRoom(), UpdateRate.SIM_200ms);
			this.move.Transition(null, (MingleChore.StatesInstance smi) => !smi.HasMingleCell(), UpdateRate.SIM_200ms).MoveTo((MingleChore.StatesInstance smi) => smi.GetMingleCell(), this.onfloor, null, false);
			this.walk.Transition(null, (MingleChore.StatesInstance smi) => !smi.HasMingleCell(), UpdateRate.SIM_200ms).TriggerOnEnter(GameHashes.BeginWalk, null).TriggerOnExit(GameHashes.EndWalk, null)
				.ToggleAnims("anim_loco_walk_kanim", 0f, "")
				.MoveTo((MingleChore.StatesInstance smi) => smi.GetMingleCell(), this.onfloor, null, false);
			this.onfloor.ToggleAnims("anim_generic_convo_kanim", 0f, "").PlayAnim("idle", KAnim.PlayMode.Loop).ScheduleGoTo((MingleChore.StatesInstance smi) => (float)UnityEngine.Random.Range(5, 10), this.success)
				.ToggleTag(GameTags.AlwaysConverse);
			this.success.ReturnSuccess();
		}

		// Token: 0x0400548B RID: 21643
		public StateMachine<MingleChore.States, MingleChore.StatesInstance, MingleChore, object>.TargetParameter mingler;

		// Token: 0x0400548C RID: 21644
		public GameStateMachine<MingleChore.States, MingleChore.StatesInstance, MingleChore, object>.State mingle;

		// Token: 0x0400548D RID: 21645
		public GameStateMachine<MingleChore.States, MingleChore.StatesInstance, MingleChore, object>.State move;

		// Token: 0x0400548E RID: 21646
		public GameStateMachine<MingleChore.States, MingleChore.StatesInstance, MingleChore, object>.State walk;

		// Token: 0x0400548F RID: 21647
		public GameStateMachine<MingleChore.States, MingleChore.StatesInstance, MingleChore, object>.State onfloor;

		// Token: 0x04005490 RID: 21648
		public GameStateMachine<MingleChore.States, MingleChore.StatesInstance, MingleChore, object>.State success;
	}

	// Token: 0x02000F73 RID: 3955
	public class StatesInstance : GameStateMachine<MingleChore.States, MingleChore.StatesInstance, MingleChore, object>.GameInstance
	{
		// Token: 0x06006F8B RID: 28555 RVA: 0x002A2BD9 File Offset: 0x002A0DD9
		public StatesInstance(MingleChore master, GameObject mingler)
			: base(master)
		{
			this.mingler = mingler;
			base.sm.mingler.Set(mingler, base.smi, false);
			this.mingleCellSensor = base.GetComponent<Sensors>().GetSensor<MingleCellSensor>();
		}

		// Token: 0x06006F8C RID: 28556 RVA: 0x002A2C13 File Offset: 0x002A0E13
		public bool IsRecTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Recreation);
		}

		// Token: 0x06006F8D RID: 28557 RVA: 0x002A2C34 File Offset: 0x002A0E34
		public int GetMingleCell()
		{
			return this.mingleCellSensor.GetCell();
		}

		// Token: 0x06006F8E RID: 28558 RVA: 0x002A2C41 File Offset: 0x002A0E41
		public bool HasMingleCell()
		{
			return this.mingleCellSensor.GetCell() != Grid.InvalidCell;
		}

		// Token: 0x06006F8F RID: 28559 RVA: 0x002A2C58 File Offset: 0x002A0E58
		public bool IsSameRoom()
		{
			int num = Grid.PosToCell(this.mingler);
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
			CavityInfo cavityForCell2 = Game.Instance.roomProber.GetCavityForCell(this.GetMingleCell());
			return cavityForCell != null && cavityForCell2 != null && cavityForCell.handle == cavityForCell2.handle;
		}

		// Token: 0x04005491 RID: 21649
		private MingleCellSensor mingleCellSensor;

		// Token: 0x04005492 RID: 21650
		private GameObject mingler;
	}
}

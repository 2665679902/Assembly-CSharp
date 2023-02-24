using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000928 RID: 2344
[SerializationConfig(MemberSerialization.OptIn)]
public class SocialGatheringPoint : StateMachineComponent<SocialGatheringPoint.StatesInstance>
{
	// Token: 0x06004485 RID: 17541 RVA: 0x001827EC File Offset: 0x001809EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.workables = new SocialGatheringPointWorkable[this.choreOffsets.Length];
		for (int i = 0; i < this.workables.Length; i++)
		{
			Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(this), this.choreOffsets[i]), Grid.SceneLayer.Move);
			SocialGatheringPointWorkable socialGatheringPointWorkable = ChoreHelpers.CreateLocator("SocialGatheringPointWorkable", vector).AddOrGet<SocialGatheringPointWorkable>();
			socialGatheringPointWorkable.basePriority = this.basePriority;
			socialGatheringPointWorkable.specificEffect = this.socialEffect;
			socialGatheringPointWorkable.OnWorkableEventCB = new Action<Workable, Workable.WorkableEvent>(this.OnWorkableEvent);
			socialGatheringPointWorkable.SetWorkTime(this.workTime);
			this.workables[i] = socialGatheringPointWorkable;
		}
		this.tracker = new SocialChoreTracker(base.gameObject, this.choreOffsets);
		this.tracker.choreCount = this.choreCount;
		this.tracker.CreateChoreCB = new Func<int, Chore>(this.CreateChore);
		base.smi.StartSM();
	}

	// Token: 0x06004486 RID: 17542 RVA: 0x001828E0 File Offset: 0x00180AE0
	protected override void OnCleanUp()
	{
		if (this.tracker != null)
		{
			this.tracker.Clear();
			this.tracker = null;
		}
		if (this.workables != null)
		{
			for (int i = 0; i < this.workables.Length; i++)
			{
				if (this.workables[i])
				{
					Util.KDestroyGameObject(this.workables[i]);
					this.workables[i] = null;
				}
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x06004487 RID: 17543 RVA: 0x00182950 File Offset: 0x00180B50
	private Chore CreateChore(int i)
	{
		Workable workable = this.workables[i];
		ChoreType relax = Db.Get().ChoreTypes.Relax;
		IStateMachineTarget stateMachineTarget = workable;
		ChoreProvider choreProvider = null;
		bool flag = true;
		Action<Chore> action = null;
		Action<Chore> action2 = null;
		ScheduleBlockType recreation = Db.Get().ScheduleBlockTypes.Recreation;
		WorkChore<SocialGatheringPointWorkable> workChore = new WorkChore<SocialGatheringPointWorkable>(relax, stateMachineTarget, choreProvider, flag, action, action2, new Action<Chore>(this.OnSocialChoreEnd), false, recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, false);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, workable);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, workable);
		return workChore;
	}

	// Token: 0x06004488 RID: 17544 RVA: 0x001829DA File Offset: 0x00180BDA
	private void OnSocialChoreEnd(Chore chore)
	{
		if (base.smi.IsInsideState(base.smi.sm.on))
		{
			this.tracker.Update(true);
		}
	}

	// Token: 0x06004489 RID: 17545 RVA: 0x00182A05 File Offset: 0x00180C05
	private void OnWorkableEvent(Workable workable, Workable.WorkableEvent workable_event)
	{
		if (workable_event == Workable.WorkableEvent.WorkStarted)
		{
			if (this.OnSocializeBeginCB != null)
			{
				this.OnSocializeBeginCB();
				return;
			}
		}
		else if (workable_event == Workable.WorkableEvent.WorkStopped && this.OnSocializeEndCB != null)
		{
			this.OnSocializeEndCB();
		}
	}

	// Token: 0x04002DAC RID: 11692
	public CellOffset[] choreOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(1, 0)
	};

	// Token: 0x04002DAD RID: 11693
	public int choreCount = 2;

	// Token: 0x04002DAE RID: 11694
	public int basePriority;

	// Token: 0x04002DAF RID: 11695
	public string socialEffect;

	// Token: 0x04002DB0 RID: 11696
	public float workTime = 15f;

	// Token: 0x04002DB1 RID: 11697
	public System.Action OnSocializeBeginCB;

	// Token: 0x04002DB2 RID: 11698
	public System.Action OnSocializeEndCB;

	// Token: 0x04002DB3 RID: 11699
	private SocialChoreTracker tracker;

	// Token: 0x04002DB4 RID: 11700
	private SocialGatheringPointWorkable[] workables;

	// Token: 0x02001707 RID: 5895
	public class States : GameStateMachine<SocialGatheringPoint.States, SocialGatheringPoint.StatesInstance, SocialGatheringPoint>
	{
		// Token: 0x0600894E RID: 35150 RVA: 0x002F868C File Offset: 0x002F688C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.DoNothing();
			this.off.TagTransition(GameTags.Operational, this.on, false);
			this.on.TagTransition(GameTags.Operational, this.off, true).Enter("CreateChore", delegate(SocialGatheringPoint.StatesInstance smi)
			{
				smi.master.tracker.Update(true);
			}).Exit("CancelChore", delegate(SocialGatheringPoint.StatesInstance smi)
			{
				smi.master.tracker.Update(false);
			});
		}

		// Token: 0x04006BCA RID: 27594
		public GameStateMachine<SocialGatheringPoint.States, SocialGatheringPoint.StatesInstance, SocialGatheringPoint, object>.State off;

		// Token: 0x04006BCB RID: 27595
		public GameStateMachine<SocialGatheringPoint.States, SocialGatheringPoint.StatesInstance, SocialGatheringPoint, object>.State on;
	}

	// Token: 0x02001708 RID: 5896
	public class StatesInstance : GameStateMachine<SocialGatheringPoint.States, SocialGatheringPoint.StatesInstance, SocialGatheringPoint, object>.GameInstance
	{
		// Token: 0x06008950 RID: 35152 RVA: 0x002F8737 File Offset: 0x002F6937
		public StatesInstance(SocialGatheringPoint smi)
			: base(smi)
		{
		}
	}
}
